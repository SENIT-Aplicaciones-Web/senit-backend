using Senit.Platform.API.FrontDesk.Interfaces.Acl;
using Senit.Platform.API.Iam.Application.CommandServices;
using Senit.Platform.API.Iam.Domain.Model.Aggregates;
using Senit.Platform.API.Iam.Domain.Model.Commands;
using Senit.Platform.API.Iam.Domain.Model.Errors;
using Senit.Platform.API.Iam.Domain.Repositories;
using Senit.Platform.API.Shared.Application.Model;
using Senit.Platform.API.Shared.Domain.Repositories;

namespace Senit.Platform.API.Iam.Application.Internal.CommandServices;

/// <summary>
///     Command service implementation for user use cases.
/// </summary>
public class UserCommandService(
    IUserRepository repository,
    IFrontDeskContextFacade frontDeskContextFacade,
    IHotelStaffMemberRepository hotelStaffMemberRepository,
    IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task<ApplicationResult<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        if (!await frontDeskContextFacade.HotelExists(command.HotelId, cancellationToken))
            return ApplicationResult<User>.Failure(nameof(IamErrors.HotelNotFound), StatusCodes.Status404NotFound);

        var existingUser = await repository.FindByEmailAsync(command.Email, cancellationToken);
        if (existingUser != null)
        {
            var hasActiveAssignment = await hotelStaffMemberRepository.HasActiveAssignmentAsync(existingUser.Id, cancellationToken);
            if (existingUser.Status == "active" && hasActiveAssignment)
                return ApplicationResult<User>.Failure(nameof(IamErrors.UserAlreadyActiveInHotel), StatusCodes.Status409Conflict);

            existingUser.Update(
                command.HotelId,
                command.FullName,
                command.Username,
                command.Email,
                command.Password,
                command.Role,
                command.Status);

            var existingAssignment = await hotelStaffMemberRepository.FindByHotelIdAndUserIdAsync(
                command.HotelId,
                existingUser.Id,
                cancellationToken);

            if (existingAssignment == null)
            {
                existingAssignment = new HotelStaffMember(
                    Guid.NewGuid().ToString(),
                    command.HotelId,
                    existingUser.Id,
                    command.Role,
                    command.Status);

                await hotelStaffMemberRepository.AddAsync(existingAssignment, cancellationToken);
            }
            else
            {
                existingAssignment.Update(command.Role, command.Status);
                hotelStaffMemberRepository.Update(existingAssignment);
            }

            repository.Update(existingUser);
            await unitOfWork.CompleteAsync(cancellationToken);

            return ApplicationResult<User>.Created(existingUser);
        }

        var entity = new User(
            Guid.NewGuid().ToString(),
            command.HotelId,
            command.FullName,
            command.Username,
            command.Email,
            command.Password,
            command.Role,
            command.Status);

        await repository.AddAsync(entity, cancellationToken);

        var staffAssignment = new HotelStaffMember(
            Guid.NewGuid().ToString(),
            command.HotelId,
            entity.Id,
            command.Role,
            command.Status);

        await hotelStaffMemberRepository.AddAsync(staffAssignment, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<User>.Created(entity);
    }

    public async Task<ApplicationResult<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<User>.Failure(nameof(IamErrors.UserNotFound), StatusCodes.Status404NotFound);

        if (!await frontDeskContextFacade.HotelExists(command.HotelId, cancellationToken))
            return ApplicationResult<User>.Failure(nameof(IamErrors.HotelNotFound), StatusCodes.Status404NotFound);

        if (await repository.ExistsByEmailAsync(command.Email, command.Id, cancellationToken))
            return ApplicationResult<User>.Failure(nameof(IamErrors.DuplicateEmail), StatusCodes.Status409Conflict);

        entity.Update(
            command.HotelId,
            command.FullName,
            command.Username,
            command.Email,
            command.Password,
            command.Role,
            command.Status);

        var staffAssignment = await hotelStaffMemberRepository.FindByHotelIdAndUserIdAsync(
            command.HotelId,
            entity.Id,
            cancellationToken);

        if (staffAssignment == null)
        {
            staffAssignment = new HotelStaffMember(
                Guid.NewGuid().ToString(),
                command.HotelId,
                entity.Id,
                command.Role,
                command.Status);

            await hotelStaffMemberRepository.AddAsync(staffAssignment, cancellationToken);
        }
        else
        {
            staffAssignment.Update(command.Role, command.Status);
            hotelStaffMemberRepository.Update(staffAssignment);
        }

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<User>.Success(entity);
    }

    public async Task<ApplicationResult<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(IamErrors.UserNotFound), StatusCodes.Status404NotFound);

        entity.Deactivate();
        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }

    public async Task<ApplicationResult<bool>> Handle(RemoveUserFromHotelCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await repository.FindByIdAsync(command.UserId, cancellationToken);
        if (entity == null)
            return ApplicationResult<bool>.Failure(nameof(IamErrors.UserNotFound), StatusCodes.Status404NotFound);

        var staffAssignment = await hotelStaffMemberRepository.FindByHotelIdAndUserIdAsync(
            command.HotelId,
            command.UserId,
            cancellationToken);

        if (staffAssignment == null || staffAssignment.Status == "inactive")
            return ApplicationResult<bool>.Failure(nameof(IamErrors.UserNotAssignedToHotel), StatusCodes.Status409Conflict);

        staffAssignment.Deactivate();
        hotelStaffMemberRepository.Update(staffAssignment);

        var nextActiveAssignment = await hotelStaffMemberRepository.FindFirstActiveAssignmentByUserIdExceptHotelAsync(
            command.UserId,
            command.HotelId,
            cancellationToken);

        if (nextActiveAssignment == null)
        {
            entity.Deactivate();
        }
        else
        {
            entity.ChangeDefaultHotel(nextActiveAssignment.HotelId);
            entity.ChangeRole(nextActiveAssignment.Role);
            entity.Activate();
        }

        repository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        return ApplicationResult<bool>.Success(true);
    }
}

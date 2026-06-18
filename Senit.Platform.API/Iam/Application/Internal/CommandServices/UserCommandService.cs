using Senit.Platform.API.FrontDesk.Domain.Repositories;
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
    IHotelRepository hotelRepository,
    IHotelStaffMemberRepository hotelStaffMemberRepository,
    IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task<ApplicationResult<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var hotel = await hotelRepository.FindByIdAsync(command.HotelId, cancellationToken);
        if (hotel == null)
            return ApplicationResult<User>.Failure(nameof(IamErrors.HotelNotFound), StatusCodes.Status404NotFound);

        if (await repository.ExistsByEmailAsync(command.Email, cancellationToken: cancellationToken))
            return ApplicationResult<User>.Failure(nameof(IamErrors.DuplicateEmail), StatusCodes.Status409Conflict);

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

        var hotel = await hotelRepository.FindByIdAsync(command.HotelId, cancellationToken);
        if (hotel == null)
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

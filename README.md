# Senit Platform API

Deploy: https://senit-backend.onrender.com/swagger/index.html

Base URL: https://senit-backend.onrender.com/api/v1

## Endpoints

- `/hotels`
- `/users`
- `/authentication`
- `/rooms`
- `/reservations`
- `/guests`
- `/guest-stays`
- `/consumptions`
- `/payments`
- `/invoices`
- `/cleaning-tasks`
- `/notifications`
- `/subscriptions`
- `/subscription-payments`

## Test credentials

- `admin@admin.com` / `123456`
- `recepcion@recepcion.com` / `12345`

## Main Endpoints Description

### Authentication

Endpoint group used to manage user access to the Senit platform. It includes the sign-in and sign-up operations, allowing registered users to authenticate with their credentials and new users to create an account associated with the system.

Base route: `/authentication`

Main operations:
- `POST /authentication/sign-in`: Allows a registered user to log in using email and password.
- `POST /authentication/sign-up`: Allows the creation of a new user account and its initial hotel association.

### Rooms

Endpoint group used to manage hotel rooms within the platform. It allows the system to register, list, update and delete rooms, including operational information such as room number, floor, type, capacity, price per hour and current status.

Base route: `/rooms`

Main operations:
- `GET /rooms`: Retrieves all registered rooms.
- `POST /rooms`: Creates a new room.
- `PUT /rooms/{roomId}`: Updates the information or status of an existing room.
- `DELETE /rooms/{roomId}`: Deletes a registered room.

### Reservations

Endpoint group used to manage hotel reservations. It supports the registration and update of reservations, helping the platform organize room availability and reduce conflicts related to overlapping bookings.

Base route: `/reservations`

Main operations:
- `GET /reservations`: Retrieves all registered reservations.
- `POST /reservations`: Creates a new reservation.
- `PUT /reservations/{reservationId}`: Updates an existing reservation.

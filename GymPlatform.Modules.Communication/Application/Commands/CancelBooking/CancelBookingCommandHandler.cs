using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.CancelBooking;

public class CancelBookingCommandHandler : ICommandHandler<CancelBookingCommand, BookingResponse>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly ICommunicationUnitOfWork _unitOfWork;

    public CancelBookingCommandHandler(IBookingRepository bookingRepository, ISessionRepository sessionRepository, ICommunicationUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BookingResponse>> HandleAsync(CancelBookingCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var booking = await _bookingRepository.GetByIdAsync(command.BookingId, cancellationToken);
            if (booking is null)
            {
                return Result<BookingResponse>.Failure("Booking not found.");
            }

            var session = await _sessionRepository.GetByIdAsync(booking.SessionId, cancellationToken);
            if (session is null)
            {
                return Result<BookingResponse>.Failure("Session not found.");
            }

            session.Cancel(booking);
            await _sessionRepository.UpdateAsync(session, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<BookingResponse>.Success(new BookingResponse
            {
                Id = booking.Id,
                SessionId = booking.SessionId,
                MemberId = booking.MemberId,
                CreatedAt = booking.CreatedAt
            });
        }
        catch (CommunicationDomainException ex)
        {
            return Result<BookingResponse>.Failure(ex.Message);
        }
    }
}

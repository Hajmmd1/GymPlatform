using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.BookSession;

public class BookSessionCommandHandler : ICommandHandler<BookSessionCommand, BookingResponse>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly ICommunicationUnitOfWork _unitOfWork;

    public BookSessionCommandHandler(ISessionRepository sessionRepository, IBookingRepository bookingRepository, ICommunicationUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BookingResponse>> HandleAsync(BookSessionCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var session = await _sessionRepository.GetByIdAsync(command.SessionId, cancellationToken);
            if (session is null)
            {
                return Result<BookingResponse>.Failure("Session not found.");
            }

            if (session.IsCancelled)
            {
                return Result<BookingResponse>.Failure("Session is cancelled.");
            }

            var hasActiveBooking = await _bookingRepository.HasActiveBookingAsync(command.SessionId, command.MemberId, cancellationToken);
            if (hasActiveBooking)
            {
                return Result<BookingResponse>.Failure("Member already has a confirmed booking for this session.");
            }

            session.Book(command.MemberId);
            await _sessionRepository.UpdateAsync(session, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var booking = session.Bookings.Last(b => b.MemberId == command.MemberId);

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

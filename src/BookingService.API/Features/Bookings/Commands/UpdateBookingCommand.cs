using BookingService.API.Common.Exceptions;
using BookingService.API.Features.Bookings.Models;
using BookingService.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingService.API.Features.Bookings.Commands;

public sealed class UpdateBookingCommand : IRequest<BookingResponse>
{
    public UpdateBookingCommand(int bookingId, CreateBookingRequest booking)
    {
        BookingId = bookingId;
        Booking = booking;
    }

    public int BookingId { get; set; }
    public CreateBookingRequest Booking { get; set; }
}

public sealed class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, BookingResponse>
{
    private readonly DataContext _context;

    public UpdateBookingCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<BookingResponse> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .SingleOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken)
            ?? throw new NotFoundException("Booking not found");

        booking.ServiceId = request.Booking.ServiceId;
        await _context.SaveChangesAsync(cancellationToken);

        return booking.Map();
    }
}

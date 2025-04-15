using BookingService.API.Features.Bookings.Models;
using FluentValidation;

namespace BookingService.API.Features.Bookings.Validators;

public sealed class UpdateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public UpdateBookingRequestValidator()
    {
        RuleFor(b => b.ServiceId)
            .NotEmpty().WithMessage("ServiceId is required.")
            .GreaterThan(0).WithMessage("ServiceId must be a positive number.");
    }
}

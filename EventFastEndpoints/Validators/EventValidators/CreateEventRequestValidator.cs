using EventFastEndpoints.DTOs.Requests.EventRequests;
using FastEndpoints;
using FluentValidation;

public class CreateEventRequestValidator : Validator<CreateEventRequest>
{
    public CreateEventRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Etkinlik başlığı boş olamaz.")
            .MaximumLength(100).WithMessage("Etkinlik başlığı en fazla 100 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

        RuleFor(x => x.Date)
            .GreaterThan(DateTime.UtcNow).WithMessage("Etkinlik tarihi gelecekte olmalıdır.");

        RuleFor(x => x.LocationInfo).NotNull().WithMessage("Lokasyon bilgileri zorunludur.");

        RuleFor(x => x.LocationInfo.Location)
            .NotEmpty().WithMessage("Lokasyon adı boş olamaz.");

        RuleFor(x => x.LocationInfo.CityId)
            .GreaterThan(0).WithMessage("Geçerli bir şehir ID'si girilmelidir.");

        RuleFor(x => x.Amenities).NotNull().WithMessage("İmkanlar (Amenities) boş olamaz.");
    }
}

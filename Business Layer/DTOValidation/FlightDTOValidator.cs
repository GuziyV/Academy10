using Data_Access_Layer.Models;
using FluentValidation;
using Shared.DTOs;

namespace Business_Layer.DTOValidation
{
    public class FlightDTOValidator : AbstractValidator<FlightDTO>
    {
        public FlightDTOValidator()
        {
            RuleFor(f => f.DepartureFrom).NotEmpty();
            RuleFor(f => f.Destination).NotEmpty();
        }
    }
}
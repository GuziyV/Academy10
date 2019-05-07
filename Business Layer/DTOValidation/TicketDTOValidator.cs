using Data_Access_Layer.Models;
using FluentValidation;
using Shared.DTos;

namespace Business_Layer.DTOValidation
{
    public class TicketDTOValidator : AbstractValidator<TicketDTO>
    {
        public TicketDTOValidator()
        {
            RuleFor(t => t.FlightNumber).NotEqual(0);
        }
    }
}
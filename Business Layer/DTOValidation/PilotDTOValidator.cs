using Data_Access_Layer.Models;
using FluentValidation;
using Shared.DTos;

namespace Business_Layer.DTOValidation
{
    public class PilotDTOValidator : AbstractValidator<PilotDTO>
    {
        public PilotDTOValidator()
        {
            RuleFor(p => p.Surname).NotEmpty().NotNull();
        }
    }
}
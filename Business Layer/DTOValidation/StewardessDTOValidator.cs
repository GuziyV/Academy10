﻿using Data_Access_Layer.Models;
using FluentValidation;
using Shared.DTos;

namespace Business_Layer.DTOValidation
{
    public class StewardessDTOValidator : AbstractValidator<StewardessDTO>
    {
        public StewardessDTOValidator()
        {
            RuleFor(s => s.Surname).NotEmpty();
        }
    }
}
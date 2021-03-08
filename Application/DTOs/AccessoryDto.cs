using FluentValidation;
using System;

namespace Application.DTOs
{
    public class AccessoryDto : EntityDto
    {
        public double Cost { get; set; }

    }

    public class AccessoryDtoValidator : AbstractValidator<AccessoryDto>
    {
        public AccessoryDtoValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Accessory Name is required.");
            RuleFor(a => a.Cost).GreaterThan(0.0).WithMessage("Cost must be more than 0.");
        }
    }
}
using Domain.Utility;
using FluentValidation;
using System;

namespace Application.InputModels
{
    public class OperatingExpenseInputModel : EntityInputModel
    {
        public double TotalCost { get; set; }

        public OperatingExpenseType Type { get; set; }
    }

    public class AccessoryDtoValidator : AbstractValidator<OperatingExpenseInputModel>
    {
        public AccessoryDtoValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(a => a.TotalCost).GreaterThan(0.0).WithMessage("Total cost must be more than 0.");
            RuleFor(a => a.Type).NotEmpty().WithMessage("Type is required.");
        }
    }
}

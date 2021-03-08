using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Application.InputModels
{
    public class TypeOfClothInputModel : EntityInputModel
    {
        public double ProductionDays { get; set; }

        public IEnumerable<Guid> MeasurementHeaderId { get; set; }

        public IEnumerable<AccessoryWithQuantityDto> Accessories { get; set; }

        public IEnumerable<Guid> IncurredExpenseId { get; set; }
    }

    public class TypeOfClothInputModelValidator : AbstractValidator<TypeOfClothInputModel>
    {
        public TypeOfClothInputModelValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(a => a.ProductionDays).GreaterThan(0.0).WithMessage("ProductionDays must be more than 0.");
        }
    }
}

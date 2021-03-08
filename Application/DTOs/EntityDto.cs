using FluentValidation;
using System;

namespace Application.DTOs
{
    public class EntityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }

    public class EntityDtoValidator : AbstractValidator<EntityDto>
    {
        public EntityDtoValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("MeasurementHeader Name is required.");
        }
    }
}
using System;
using System.Collections.Generic;
using FluentValidation;

namespace Application.DTOs
{
    public class ReadyToWearDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TypeOfClothId { get; set; }

        public string TypeOfCloth { get; set; }

        public int NumberInStock { get; set; }

        public double Cost { get; set; }

        public ICollection<ReadyToWearPhotoDto> Photos { get; set; }

        public List<string> PhotoUrls { get; set; }

        public string MainPhoto { get; set; }

    }

    public class ReadyToWearDtoValidator : AbstractValidator<ReadyToWearDto>
    {
        public ReadyToWearDtoValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(a => a.TypeOfClothId).NotEmpty().WithMessage("Type of cloth is required.");
            RuleFor(a => a.NumberInStock).GreaterThan(-1).WithMessage("Number in stock is required.");
        }
    }
}
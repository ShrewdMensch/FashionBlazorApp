using FluentValidation;

namespace Application.DTOs
{
    public class AccessoryWithQuantityDto : EntityDto
    {
        public int Quantity { get; set; }

    }

}
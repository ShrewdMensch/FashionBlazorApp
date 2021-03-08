using System;

namespace Application.InputModels
{
    public class ReadyToWearInputModel : EntityInputModel
    {
        public Guid TypeOfClothId { get; set; }
        public int NumberInStock { get; set; }
    }
}

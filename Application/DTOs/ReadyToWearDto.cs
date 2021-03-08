using System;
using System.Collections.Generic;

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

        public List<string> Photos { get; set; }

        public string MainPhoto { get; set; }

    }
}
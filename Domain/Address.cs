﻿using System;

namespace Domain
{
    public class Address
    {
        public Guid Id { get; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }
    }
}

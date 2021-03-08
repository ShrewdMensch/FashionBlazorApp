using System;

namespace Domain
{
    public class Employee
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string GuarantorName { get; set; }

        public string GuarantorAddress { get; set; }

        public string GuarantorPhoneNumber { get; set; }

        public Guid DesignationId { get; set; }

        public virtual EmployeeDesignation Designation { get; set; }

        public virtual Address Address { get; set; }
    }
}

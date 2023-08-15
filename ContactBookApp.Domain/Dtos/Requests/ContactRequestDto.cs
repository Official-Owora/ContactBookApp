﻿namespace ContactBookApp.Domain.Dtos.Requests
{
    public class ContactRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ImageURL { get; set; }
    }
}

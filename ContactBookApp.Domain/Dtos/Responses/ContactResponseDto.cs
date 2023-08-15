namespace ContactBookApp.Domain.Dtos.Responses
{
    public class ContactResponseDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ImageURL { get; set; }
    }
}

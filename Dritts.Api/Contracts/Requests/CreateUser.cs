namespace Dritts.Api.Contracts.Requests
{
    public record struct CreateUser(string FirstName, string MiddleName, string LastName, string PhoneNumber)
    {
    }
}

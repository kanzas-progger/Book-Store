namespace BookStore.API.Contracts
{
    public record BookRequest(string title, string description, decimal price);
}

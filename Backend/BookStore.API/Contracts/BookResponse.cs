namespace BookStore.API.Contracts
{
    public record BookResponse(Guid id, string title, string description, decimal price);
}

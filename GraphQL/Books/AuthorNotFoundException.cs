namespace GraphQL.Books;

public class AuthorNotFoundException : Exception
{
    public AuthorNotFoundException(string message) : base(message)
    {

    }
}
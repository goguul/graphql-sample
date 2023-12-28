using GraphQL.Data;

namespace GraphQL.Books;

[ExtendObjectType(OperationTypeNames.Subscription)]
public class BookSubscription
{
    [Subscribe]
    public Book BookAdded([EventMessage] Book book) => book;
}
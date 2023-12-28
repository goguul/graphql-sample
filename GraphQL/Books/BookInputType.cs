using GraphQL.Data;

namespace GraphQL.Books;

public class BookInputType: InputObjectType<Book>
{
    protected override void Configure(IInputObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Ignore(d => d.Id);
        descriptor.Ignore(d => d.Author);
    }
}
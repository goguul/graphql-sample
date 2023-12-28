using GraphQL.Data;

namespace GraphQL.Authors;

public class AuthorInputType : InputObjectType<Author>
{
    protected override void Configure(IInputObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Ignore(d => d.Id);
        descriptor.Ignore(d => d.Books);
    }
}
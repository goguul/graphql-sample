using GraphQL.Data;

namespace GraphQL.Authors;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class AuthorMutation
{
    public async Task<Author> AddAuthor(
        Author author,
        LibraryDbContext libraryDbContext)
    {
        var newAuthor = new Author
        {
            Name = author.Name
        };

        await libraryDbContext.Authors.AddAsync(newAuthor);
        await libraryDbContext.SaveChangesAsync();

        return newAuthor;
    }
}
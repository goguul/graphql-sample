using GraphQL.Data;
using HotChocolate.Subscriptions;

namespace GraphQL.Books;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class BookMutation
{
    [Error(typeof(AuthorNotFoundException))]
    public async Task<Book> AddBook(
        Book book,
        LibraryDbContext libraryDbContext,
        [Service] ITopicEventSender topicEventSender)
    {
        _ = await libraryDbContext.Authors.FindAsync(book.AuthorId)
            ?? throw new AuthorNotFoundException("AUTHOR_NOT_FOUND");
            
        var newBook = new Book()
        {
            Title = book.Title,
            AuthorId = book.AuthorId
        };

        libraryDbContext.Books.Add(newBook);
        await libraryDbContext.SaveChangesAsync();
        await topicEventSender.SendAsync(nameof(BookSubscription.BookAdded), newBook);
        return newBook;
    }
}

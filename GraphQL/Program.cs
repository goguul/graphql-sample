using GraphQL.Authors;
using GraphQL.Books;
using GraphQL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<LibraryDbContext>(options => options.UseSqlite("Data source=library.db"));

builder.Services.AddGraphQLServer()
    .RegisterDbContext<LibraryDbContext>(DbContextKind.Pooled)
    .AddInMemorySubscriptions()
    .AddMutationConventions()
    .AddQueryType()
    .AddMutationType()
    .AddSubscriptionType()
    .AddTypeExtension<BookQuery>()
    .AddTypeExtension<BookMutation>()
    .AddTypeExtension<BookSubscription>()
    .AddTypeExtension<BookType>()
    .AddTypeExtension<BookInputType>()
    .AddDataLoader<BookByIdDataLoader>()
    .AddTypeExtension<AuthorQuery>()
    .AddTypeExtension<AuthorMutation>()
    .AddTypeExtension<AuthorType>()
    .AddTypeExtension<AuthorInputType>()
    .AddDataLoader<AuthorByIdDataLoader>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddGlobalObjectIdentification();


var app = builder.Build();

app.UseWebSockets();
app.MapGraphQL();

app.Run();

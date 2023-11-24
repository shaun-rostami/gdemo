namespace GApi;

public class QueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field("authors")
            .Type<ListType<AuthorType>>()
            .Resolve(ctx =>
                DataContainer.Authors);

        descriptor.Field("books")
            .Type<ListType<BookType>>()
            .Resolve(ctx => DataContainer.Books);


        descriptor.Field("booksWithAuthors")
            .Type<ListType<BookWithAuthorType>>()
            .Resolve(ctx => GetBooksAndAuthors());
    }
    private static List<BookWithAuthor> GetBooksAndAuthors()
    {
        return DataContainer.Books.Select(book => new BookWithAuthor
        {
            Id = book.Id,
            Title = book.Title,
            Author = DataContainer.Authors.FirstOrDefault(author => author.Id == book.AuthorId)
        }).ToList();
    }
}
public class MutationType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Field("addAuthor")
            .Argument("name", arg => arg.Type<NonNullType<StringType>>())
            .Argument("id", arg => arg.Type<NonNullType<IntType>>())
            .Type<AuthorType>()
            .Resolve(ctx =>
            {
                var name = ctx.ArgumentValue<string>("name");
                var id = ctx.ArgumentValue<int>("id");
                var newAuthor = new Author { Id = id, Name = name };

                DataContainer.Authors.Add(newAuthor);

                return newAuthor;
            });

        descriptor.Field("addBook")
            .Argument("id", arg => arg.Type<NonNullType<IntType>>())
            .Argument("title", arg => arg.Type<NonNullType<StringType>>())
            .Argument("authorId", arg => arg.Type<NonNullType<IntType>>())
            .Type<BookType>()
            .Resolve(ctx =>
            {
                var id = ctx.ArgumentValue<int>("id");
                var title = ctx.ArgumentValue<string>("title");
                var authorId = ctx.ArgumentValue<int>("authorId");

                if (DataContainer.Authors.Count(author => author.Id == authorId) > 0)
                {
                    var newBook = new Book { Id = id, Title = title, AuthorId = authorId };

                    DataContainer.Books.Add(newBook);

                    return newBook;
                }
                throw new ArgumentException("The provided author ID does not exist.");
            });
    }
}


public class AuthorType : ObjectType<Author>
{
}

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Field(t => t.AuthorId)
            .Type<NonNullType<IntType>>();
    }
}

public class BookWithAuthorType : ObjectType<BookWithAuthor>
{
}

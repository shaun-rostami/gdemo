namespace GApi;

public static class DataContainer {
    static DataContainer()
    {
        // Init
        Authors = new List<Author>
        {
            new Author{Id=1, Name="Sam"  },
            new Author{Id=2, Name="Mike"  },
            new Author{Id=3, Name="Jane"  },
            new Author{Id=4, Name="Rob"  },
        };
        Books = new List<Book>
        {
            new Book{Id=1, AuthorId = 2, Title = "Book 1"  },
            new Book{Id=2, AuthorId = 1, Title = "Book 2"  },
            new Book{Id=3, AuthorId = 4, Title = "Book 3"  },
            new Book{Id=4, AuthorId = 4, Title = "Book 4"  },
            new Book{Id=5, AuthorId = 3, Title = "Book 5"  },
        };
    }

    public static List<Author> Authors { get; set; }

    public static List<Book> Books { get; set; }
}

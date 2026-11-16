using PublisherDomain;
using PublisherData;

using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}


GetAuthors();
AddAuthor();
GetAuthors();


void AddAuthor()
{
    using var context = new PubContext();
    context.Authors.Add(new Author { FirstName = "Julie", LastName = "Newf" });
    context.SaveChanges();
}

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        System.Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}
using LR4;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text;

var builder = WebApplication.CreateBuilder();

builder.Configuration.AddJsonFile("./config/library-info.json");

builder.Services.Configure<LibraryInfo>(builder.Configuration);

builder.Services.AddRouting(
    options => options.ConstraintMap.Add("id", typeof(IdRouteConstraint))
);

var libraryInfo = builder.Configuration.Get<LibraryInfo>();

var app = builder.Build();

app.Map("/library", (IOptions<LibraryInfo> libraryInfo) => $"Welcome to the library \"{libraryInfo.Value.Name}\"!");

app.Map("/library/books", (HttpContext context, IOptions<LibraryInfo> libraryInfo) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    StringBuilder pageContent = new StringBuilder();

    pageContent.Append($"<h1>Available books in \"{libraryInfo.Value.Name}\"</h1>");
    pageContent.Append("<ol>");
    foreach (var bookName in libraryInfo.Value.AvailableBooks)
    {
        pageContent.Append($"<li>{bookName}</li>");
    }
    pageContent.Append("</ol>");

    context.Response.WriteAsync(pageContent.ToString());
});

app.Map(
    "/library/profile/{id:id(0,"+libraryInfo.MaxMembersCount+")=0}",
    (int id, HttpContext context, IOptions<LibraryInfo> libraryInfo) =>
    {
        var member = libraryInfo.Value.Members[id];

        context.Response.ContentType = "text/html; charset=utf-8";

        StringBuilder pageContent = new StringBuilder();

        pageContent.Append("<h1>Member info</h1>");
        pageContent.Append($"<p>Name:{member.Name}</p>");
        pageContent.Append($"<p>Age:{member.Age}</p>");
        pageContent.Append($"<p>Number of borrowed books:{member.BorrowedBooksCount}</p>");
        pageContent.Append($"<p>Group:{member.Group}</p>");

        context.Response.WriteAsync(pageContent.ToString());
    }
    );

app.Run();
using System.Collections.Immutable;
using Bogus;
using LectionServer.Contracts;
using LectionServer.Models;
using Microsoft.AspNetCore.Http;

namespace LectionServer.Services;

public class BookService
{
    private string name = "Book.json";
    private JSONManager<List<Book>> jsonManager;

    public BookService()
    {
        jsonManager = new(name);
    }

    private readonly List<Book> _books = new();

    public IImmutableList<Book> GetBooks(Guid userId)
    {
        _books.Clear();
        _books.AddRange(jsonManager.Get() == null ? new() : jsonManager.Get());

        return _books.Where(x => x.UserId == userId).ToImmutableList();
    }
    public Book? GetBook(Guid id, Guid userId)
    {
        _books.Clear();
        _books.AddRange(jsonManager.Get() == null ? new() : jsonManager.Get());

        return _books.SingleOrDefault(x => x.Id == id && x.UserId == userId);
    }

    public Book AddBook(BookRequest request, Guid userId)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Author = request.Author,
            Name = request.Name
        };
        _books.Add(book);
        jsonManager.Set(_books);

        return book;
    }

    public Book? UpdateBook(Guid id, BookRequest request, Guid userId)
    {
        var book = GetBook(id, userId);
        if (book is null) return null;
        book.Author = request.Author;
        book.Name = request.Name;

        _books[_books.FindIndex(_books => _books.Id == book.Id)] = book;
        jsonManager.Set(_books);

        return book;
    }

    public void DeleteBook(Guid id, Guid userId)
    {
        var book = GetBook(id, userId);
        if (book is null) return;
        _books.Remove(book);
        jsonManager.Set(_books);
    }

    public void ClearBooks(Guid userId)
    {
        _books.RemoveAll(x => x.UserId == userId);
        jsonManager.Set(_books);
    }
}
﻿using BookApp.Data.Models;
using BookApp.Services.Data.Interfaces;
using BookApp.Web.ViewModels.Book;
using BookApp.Data.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services.Data
{
    public class BookService : IBookService
    {
        private IRepository<Book, int> bookRepository;
        private IRepository<Author, int> authorRepository;

        public BookService(IRepository<Book, int> bookRepository, IRepository<Author, int> authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        public async Task CreateBookAsync(CreateBookViewModel model)
        {
            var author = await authorRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(a => a.Name == model.AuthorName);

            if (author == null)
            {
                author = new Author { Name = model.AuthorName };
                await authorRepository.AddAsync(author);
            }

            Book book = new Book
            {
                Title = model.Title,
                Genre = model.Genre,
                Pages = model.Pages,
                Price = model.Price,
                Description = model.Description,
                Publisher = model.Publisher,
                ImageUrl = model.ImageUrl,
                Author = author
            };

            await this.bookRepository.AddAsync(book);
        }

        public async Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int id)
        {
            Book? book = await this.bookRepository
                .GetAllAttached()
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            BookDetailsViewModel? viewModel = null;

            if (book != null)
            {
                viewModel = new BookDetailsViewModel()
                {
                    Id = id,
                    Title = book.Title,
                    Genre = book.Genre,
                    Pages = book.Pages,
                    Description = book.Description,
                    Publisher = book.Publisher,
                    Price = book.Price,
                    ImageUrl = book.ImageUrl,
                    Author = book.Author.Name
                };
            }

            return viewModel;
        }

        public async Task<EditBookViewModel> GetBookForEditByIdAsync(int id)
        {
            Book? book = await this.bookRepository
              .GetAllAttached()
              .FirstOrDefaultAsync(c => c.Id == id);

            var bookModel = new EditBookViewModel()
            {
                Title = book.Title,
                Genre = book.Genre,
                Pages = book.Pages,
                Description = book.Description,
                Publisher = book.Publisher,
                Price = book.Price,
                ImageUrl = book.ImageUrl,
                AuthorId = book.AuthorId,
                //AuthorName = book.Author.Name
            };

            return bookModel;
        }

        public async Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync()
        {
            IEnumerable<BookIndexViewModel> books = await this.bookRepository
                .GetAllAttached()
                .OrderBy(b => b.Title)
                .Select(b => new BookIndexViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    AuthorName = b.Author.Name,
                    AuthorId = b.Author.Id
                })
                .ToArrayAsync();

            return books;
        }

        public async Task<bool> EditBookAsync(EditBookViewModel model)
        {
            var book = new Book
            {
                Id= model.Id,
                Title = model.Title,
                Genre = model.Genre,
                Pages = model.Pages,
                Description = model.Description,
                Publisher = model.Publisher,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                AuthorId = model.AuthorId,

                /*Author = new Author()
                {
                    Id = model.AuthorId,
                    Name = model.AuthorName
				}*/
            };

            bool result = await this.bookRepository.UpdateAsync(book);
            return result;
        }
    }
}

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
        public async Task<int> GetTotalBooksCountAsync()
        {
            return await this.bookRepository
                .GetAllAttached()
                .Where(b => b.IsDeleted == false)
                .CountAsync();
        }

        public async Task<IEnumerable<BookIndexViewModel>> GetBooksByPageAsync(int page, int pageSize)
        {
            var books = await this.bookRepository
                .GetAllAttached()
                .Where(b => b.IsDeleted == false)
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize) 
                .Take(pageSize) 
                .Select(b => new BookIndexViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    AuthorName = b.Author.Name,
                    AuthorId = b.Author.Id
                })
                .ToListAsync();

            return books;
        }

        public async Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync()
        {
            IEnumerable<BookIndexViewModel> books = await this.bookRepository
                .GetAllAttached()
                .Where(b => b.IsDeleted == false)
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
                .Where(b => b.IsDeleted == false)
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
              .Include(b => b.Author)
              .Where(b => b.IsDeleted == false)
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
                AuthorName = book.Author.Name
            };

            return bookModel;
        }

        public async Task<bool> EditBookAsync(EditBookViewModel model)
        {
            var author = await authorRepository.FirstOrDefaultAsync(a => a.Id == model.AuthorId);

            if (author == null && !string.IsNullOrWhiteSpace(model.AuthorName))
            {
                author = await authorRepository.FirstOrDefaultAsync(a => a.Name == model.AuthorName);

                if (author == null)
                {
                    author = new Author
                    {
                        Id = model.AuthorId,
                        Name = model.AuthorName
                    };

                    await authorRepository.AddAsync(author);
                }
            }

            var book = await bookRepository.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (book == null)
            {
                return false;
            }

            book.Title = model.Title;
            book.Genre = model.Genre;
            book.Pages = model.Pages;
            book.Description = model.Description;
            book.Publisher = model.Publisher;
            book.Price = model.Price;
            book.ImageUrl = model.ImageUrl;
            book.Author = author;

            bool result = await this.bookRepository.UpdateAsync(book);
            return result;
        }

        public async Task<DeleteBookViewModel?> GetBookForDeleteByIdAsync(int id)
        {
            DeleteBookViewModel? bookToDelete = await this.bookRepository
                .GetAllAttached()
                .Where(c => c.IsDeleted == false)
                .Select(b => new DeleteBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                })
                .FirstOrDefaultAsync(b => b.Id.ToString().ToLower() == id.ToString().ToLower());

            return bookToDelete;
        }

        public async Task<bool> SoftDeleteBookAsync(int id)
        {
            Book? bookToDelete = await this.bookRepository
                .FirstOrDefaultAsync(c => c.Id.ToString().ToLower() == id.ToString().ToLower());

            if (bookToDelete == null)
            {
                return false;
            }

            bookToDelete.IsDeleted = true;
            return await this.bookRepository.UpdateAsync(bookToDelete);
        }

        public async Task<IEnumerable<BookIndexViewModel>> SearchBooksAsync(string searchString, int page, int pageSize)
        {
            var query = this.bookRepository
                .GetAllAttached()
                .Include(b => b.Author)
                .Where(b => b.IsDeleted == false);

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(searchString) ||
                    b.Genre.ToLower().Contains(searchString) ||
                    b.Author.Name.ToLower().Contains(searchString));
            }

            var books = await query
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookIndexViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    AuthorName = b.Author.Name,
                    AuthorId = b.Author.Id
                })
                .ToListAsync();

            return books;
        }

        public async Task<int> GetSearchBooksCountAsync(string searchString)
        {
            var query = this.bookRepository
                .GetAllAttached()
                .Where(b => b.IsDeleted == false);

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(searchString) ||
                    b.Genre.ToLower().Contains(searchString) ||
                    b.Author.Name.ToLower().Contains(searchString));
            }

            return await query.CountAsync();
        }
    }
}

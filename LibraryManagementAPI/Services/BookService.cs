using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.IRepositories;
using LibraryManagementAPI.IServices;
using LibraryManagementAPI.Model;

namespace LibraryManagementAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(string? searchTerm = null)
        {
            var books = await _bookRepository.GetAllAsync(searchTerm);
            return books.Select(MapToDto);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : MapToDto(book);
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                ISBN = createBookDto.ISBN,
                PublishedDate = createBookDto.PublishedDate
            };

            var createdBook = await _bookRepository.CreateAsync(book);
            return MapToDto(createdBook);
        }

        public async Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = new Book
            {
                Title = updateBookDto.Title,
                Author = updateBookDto.Author,
                ISBN = updateBookDto.ISBN,
                PublishedDate = updateBookDto.PublishedDate
            };

            var updatedBook = await _bookRepository.UpdateAsync(id, book);
            return updatedBook == null ? null : MapToDto(updatedBook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }

        private static BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate
            };
        }
    }
}

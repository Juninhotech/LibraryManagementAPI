using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.IServices
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync(string? searchTerm = null);
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);
        Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}

using LibraryManagementAPI.Model;

namespace LibraryManagementAPI.IRepositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync(string? searchTerm = null);
        Task<Book?> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task<Book?> UpdateAsync(int id, Book book);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}

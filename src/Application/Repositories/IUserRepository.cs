using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
using backend.Presentation.DTOs.User;

namespace backend.Application.Repositories;

public interface IUserRepository
{
    public Task<McResult<User>> GetUserByIdAsync(Guid id);
    public Task<McResult<IEnumerable<User>>> GetAllUserAsync();
    public Task<McResult<User>> GetUserByEmailAsync(string email);
    public Task<McResult<User>> GetUserByUserNameAsync(string userName);
    public Task<McResult<User>> CreateUserAsync(UserInputDto user);
    public Task<McResult<User>> UpdateUserAsync(UserUpdateDto user);
    public Task<McResult<IEnumerable<Deck>>> GetDecksByUserAsync(Guid id);
    public Task<McResult<IEnumerable<Tournament>>> GetTournamentsByUserAsync(Guid id);    
}
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
using backend.Presentation.DTOs.User;

namespace backend.Application.Repositories;

public interface IUserRepository
{
    public Task<McResult<User>> GetUserByIdAsync(Guid id);
    public Task<McResult<User>> GetUserByEmailAsync(string email);
    public Task<McResult<User>> GetUserByUserNameAsync(string userName);
    public Task<McResult<User>> CreateUserAsync(UserInputDto user);
    public Task<McResult<User>> UpdateUserAsync(UserUpdateDto user);
}
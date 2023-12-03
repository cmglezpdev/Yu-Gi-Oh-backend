using AutoMapper;
using backend.Application.Repositories;
using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Deck;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;
public class UserService
{
    private readonly AppDbContext _context;
    private readonly IUserRepository userRepository;

    public UserService(AppDbContext context,IMapper _mapper,IUserRepository userRepository)
    {
        _context = context;
        this.userRepository = userRepository;
    }
    public async Task<McResult<IEnumerable<Deck>>> GetDecksByUserAsync(Guid id)
    {
        return await userRepository.GetDecksByUserAsync(id);
    }

    public async Task<McResult<IEnumerable<Tournament>>> GetTournamentsByUserAsync(Guid id)
    {
        return await userRepository.GetTournamentsByUserAsync(id);
    }

    public async Task<McResult<IEnumerable<User>>> GetAllUserAsync()
    {
        return await userRepository.GetAllUserAsync();
    }

    public async Task<McResult<User>> GetUserByIdAsync(Guid id)
    {
        return await userRepository.GetUserByIdAsync(id);
    }

    public async Task<McResult<int>> GetWinsByUserAsync(Guid id)
    {
        return await userRepository.GetWinsByUserAsync(id);
    }

    public async Task<McResult<int>> GetLosesByUserAsync(Guid id)
    {
        return await userRepository.GetLosesByUserAsync(id);
    }
}
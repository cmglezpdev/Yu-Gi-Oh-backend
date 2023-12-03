using backend.Infrastructure.Entities;

namespace backend.Application.Providers;

public interface IJwtProvider
{
    public Task<string> Generate(User user);
}
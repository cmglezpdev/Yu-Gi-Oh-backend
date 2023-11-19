using backend.Infrastructure.Entities;

namespace backend.Application.Providers;

public interface IJwtProvider
{
    public string Generate(User user);
}
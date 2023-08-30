using TaskThinkland.Api.DTOs.UserDTOs;
using TaskThinkland.Api.Filters;

namespace TaskThinkland.Api.Services.UserService;

public interface IUserService
{
    ValueTask<UserDto> InsertAsync(CreateUserDto dto);
    ValueTask<UserTokenDto> LoginAsync(LoginUserDto dto);
    ValueTask<IEnumerable<UserDto>> GetAllAsync(UserFilter filter);
    ValueTask<UserDto> GetByIdAsync(Guid userId);
    ValueTask<UserDto> UpdateAsync(Guid userId, UpdateUserDto dto);
    ValueTask DeleteAsync(Guid userId);
}
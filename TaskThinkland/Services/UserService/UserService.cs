using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskThinkland.Api.Context.Repositories.GenericRepositories;
using TaskThinkland.Api.DTOs.UserDTOs;
using TaskThinkland.Api.Entities;
using TaskThinkland.Api.Exceptions;
using TaskThinkland.Api.Extensions;
using TaskThinkland.Api.Filters;
using TaskThinkland.Api.PaginationModels;
using TaskThinkland.Api.Services.Options;

namespace TaskThinkland.Api.Services.UserService;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly HttpContextHelper _httpContext;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;

    public UserService(IGenericRepository<User> userRepository, HttpContextHelper httpContext,
        IMapper mapper, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _httpContext = httpContext;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public async ValueTask<UserDto> InsertAsync(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, dto.Password);

        var newUser = await _userRepository.InsertAsync(user);

        return _mapper.Map<UserDto>(newUser);
    }

    public async ValueTask<UserTokenDto> LoginAsync(LoginUserDto dto)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Username == dto.Username);

        if (user is null)
            throw new NotFoundException($"{typeof(User)} not found!");

        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if(result != PasswordVerificationResult.Success)
            throw new AnotherExceptions("Password in valid!");

        var (token, expiresInMinutes) = _jwtService.GenerateToken(user);

        return new UserTokenDto()
        {
            Username = user.Username,
            ExpiresInMinutes = expiresInMinutes,
            Token = token,
        };
    }

    public async ValueTask<IEnumerable<UserDto>> GetAllAsync(UserFilter filter)
    {
        var query = _userRepository.SelectAll().AsQueryable();

        if (filter.Username is not null)
            query = query.Where(u => u.Username.ToLower().Equals(filter.Username.ToLower()));

        if (filter.FromDateTime is not null)
            query = query.Where(u => u.CreateAt >= filter.FromDateTime);

        if (filter.ToDateTime is not null)
            query = query.Where(u => u.CreateAt <= filter.ToDateTime);

        var users = await query.AsNoTracking().ToPagedListAsync(_httpContext, filter);

        return users.Select(u => _mapper.Map<UserDto>(u));
    }

    public async ValueTask<UserDto> GetByIdAsync(Guid userId)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Id == userId);

        if (user is null)
            throw new NotFoundException($"{typeof(User)} not found!");

        return _mapper.Map<UserDto>(user);
    }

    public async ValueTask<UserDto> UpdateAsync(Guid userId, UpdateUserDto dto)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Id == userId);

        if (user is null)
            throw new NotFoundException($"{typeof(User)} not found!");

        user.Username = dto.Username ?? user.Username;

        if (dto.Password is not null)
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, dto.Password);

        var updatedUser = await _userRepository.UpdateAsync(user);

        return _mapper.Map<UserDto>(updatedUser);
    }

    public async ValueTask DeleteAsync(Guid userId)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Id == userId);

        if (user is null)
            throw new NotFoundException($"{typeof(User)} not found!");

        await _userRepository.DeleteAsync(user);
    }
}
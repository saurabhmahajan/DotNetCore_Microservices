using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dtos;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services;

public class AuthService(
    AppDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IJwtTokenGenerator tokenGenerator)
    : IAuthService
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        var applicationUser = new ApplicationUser
        {
            UserName = registrationRequestDto.Email,
            Name = registrationRequestDto.Name,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            PhoneNumber = registrationRequestDto.PhoneNumber
        };

        try
        {
            var result = await _userManager.CreateAsync(applicationUser, registrationRequestDto.Password);
            return result.Succeeded ? "" : string.Concat(result.Errors.SelectMany(x => x.Description));
        }
        catch (Exception ex)
        {
            return "Registration failed.";
        }
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == loginRequestDto.UserName.ToLower());
        if (user == null) return new LoginResponseDto();
        var passwordCheck = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
        if (!passwordCheck) return new LoginResponseDto();

        return new LoginResponseDto
        {
            User = new UserDto
            { Email = user.Email, Id = user.Id, Name = user.Name, PhoneNumber = user.PhoneNumber },
            Token = tokenGenerator.GenerateToken(user)
        };
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        if (user is null) return false;
        var doesRoleExist = await _roleManager.RoleExistsAsync(roleName);
        if (doesRoleExist is false)
        {
            var role = new IdentityRole(roleName.ToUpper());
            await _roleManager.CreateAsync(role);
        }

        await _userManager.AddToRoleAsync(user, roleName);
        return true;
    }
}
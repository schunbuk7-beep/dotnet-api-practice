using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotnetApiPractice.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        _logger.LogInformation("Login attempt for {Email}", loginDto.Email);

        if (loginDto.Email == "admin@school.com" && loginDto.Password == "Admin123!")
        {
            var token = GenerateJwtToken(loginDto.Email);
            _logger.LogInformation("Login successful for {Email}", loginDto.Email);
            return Ok(new { Token = token });
        }

        _logger.LogWarning("Invalid login attempt for {Email}", loginDto.Email);
        return Unauthorized(new { Message = "Invalid email or password" });
    }

    private string GenerateJwtToken(string email)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(jwtSettings["ExpiryInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
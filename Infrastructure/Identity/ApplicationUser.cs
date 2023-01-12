using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Vendor { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? TokenExpiryDate { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
}

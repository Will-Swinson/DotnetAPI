namespace DotnetAPI.Models
{
  public partial class UserForRegistration
  {
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string PasswordHash { get; set; } = "";


  }
}

namespace DotnetAPI.Dtos
{
  public partial class UserForConfirmationDto
  {
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }


    UserForConfirmationDto()
    {
      if (PasswordHash == null)
      {
        PasswordHash = new byte[0];
      }

      if (PasswordSalt == null)
      {
        PasswordSalt = new byte[0];
      }
    }

  }
}
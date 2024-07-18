using DotnetAPI.Data;
using DotnetAPI.Models;
using DotnetAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]

  public class UserController : ControllerBase
  {
    private DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
      _dapper = new DataContextDapper(config);
      Console.WriteLine(config.GetConnectionString("DefaultConnection"));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
      string sql = "SELECT * FROM TutorialAppSchema.Users;";
      IEnumerable<User> users = _dapper.LoadData<User>(sql);
      return users;
    }

    [HttpGet("GetUserById/{userId}")]
    public User GetUserById(int userId)
    {
      string sql = $"SELECT * FROM TutorialAppSchema.Users WHERE UserId = {userId};";
      User user = _dapper.LoadDataSingle<User>(sql);
      return user;
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
      string sql = @$"
      INSERT INTO TutorialAppSchema.Users (
      [FirstName],
      [LastName],
      [Email],
      [Gender],
      [Active]
      ) VALUES (
      '{user.FirstName}',
      '{user.LastName}',
      '{user.Email}',
      '{user.Gender}',
      '{user.Active}'
      );";

      bool isValid = _dapper.Execute(sql);
      if (!isValid)
      {
        return BadRequest();
      }
      return Ok();
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
      string sql = @$"
      UPDATE TutorialAppSchema.Users
      SET [FirstName] = '{user.FirstName}',
      [LastName] = '{user.LastName}',
      [Email] = '{user.Email}',
      [Gender] = '{user.Gender}',
      [Active] = '{user.Active}'
      WHERE UserId = '{user.UserId}';";

      bool isValid = _dapper.Execute(sql);
      if (!isValid)
      {
        return BadRequest();
      }
      return Ok();
    }

    [HttpDelete("DeleteUser/{userId}")]

    public IActionResult DeleteUser(int userId)
    {
      string sql = $"DELETE FROM TutorialAppSchema.Users WHERE UserId = {userId};";
      bool isValid = _dapper.Execute(sql);
      if (!isValid)
      {
        return BadRequest();
      }
      return Ok();
    }
  };



}
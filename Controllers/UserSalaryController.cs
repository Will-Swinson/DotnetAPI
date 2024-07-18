using DotnetAPI.Data;
using DotnetAPI.Models;
using DotnetAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]

  public class UserSalaryController : ControllerBase
  {
    private DataContextDapper _dapper;
    public UserSalaryController(IConfiguration config)
    {
      _dapper = new DataContextDapper(config);
      Console.WriteLine(config.GetConnectionString("DefaultConnection"));
    }

    [HttpGet("GetUsersSalary")]
    public IEnumerable<UserSalary> GetUsersSalary()
    {
      string sql = "SELECT * FROM TutorialAppSchema.UserSalary;";
      IEnumerable<UserSalary> users = _dapper.LoadData<UserSalary>(sql);
      return users;
    }

    [HttpGet("GetUserSalaryById/{userId}")]
    public UserSalary GetUserSalaryById(int userId)
    {
      string sql = $"SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId = {userId};";
      UserSalary user = _dapper.LoadDataSingle<UserSalary>(sql);
      return user;
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalaryToAddDto user)
    {
      string sql = @$"
      INSERT INTO TutorialAppSchema.UserSalary (
      [Salary]
      ) VALUES (
      '{user.Salary}',
      );";

      bool isValid = _dapper.Execute(sql);
      if (!isValid)
      {
        return BadRequest();
      }
      return Ok();
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary user)
    {
      string sql = @$"
      UPDATE TutorialAppSchema.UserSalary
      SET [Salary] = '{user.Salary}',
      WHERE UserId = '{user.UserId}';";

      bool isValid = _dapper.Execute(sql);
      if (!isValid)
      {
        return BadRequest();
      }
      return Ok();
    }

    [HttpDelete("DeleteUserSalary/{userId}")]

    public IActionResult DeleteUserSalary(int userId)
    {
      string sql = $"DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = {userId};";
      bool isValid = _dapper.Execute(sql);
      if (!isValid)
      {
        return BadRequest();
      }
      return Ok();
    }
  };



}
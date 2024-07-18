using DotnetAPI.Data;
using DotnetAPI.Models;
using DotnetAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]

  public class UserJobInfoController : ControllerBase
  {
    private DataContextDapper _dapper;
    public UserJobInfoController(IConfiguration config)
    {
      _dapper = new DataContextDapper(config);
      Console.WriteLine(config.GetConnectionString("DefaultConnection"));
    }

    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo()
    {
      string sql = "SELECT * FROM TutorialAppSchema.UserJobInfo;";
      IEnumerable<UserJobInfo> users = _dapper.LoadData<UserJobInfo>(sql);
      return users;
    }

    [HttpGet("GetUserJobInfoById/{userId}")]
    public UserJobInfo GetUserJobInfoById(int userId)
    {
      string sql = $"SELECT * FROM TutorialAppSchema.UserJobInfo WHERE UserId = {userId};";
      UserJobInfo user = _dapper.LoadDataSingle<UserJobInfo>(sql);
      return user;
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfoToAddDto user)
    {
      string sql = @$"
      INSERT INTO TutorialAppSchema.UserJobInfo (
      [JobTitle],
      [Department]
      ) VALUES (
      '{user.JobTitle}',
      '{user.Department}'
      );";

      bool isValid = _dapper.Execute(sql);
      if (!isValid)
      {
        return BadRequest();
      }
      return Ok();
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo user)
    {
      string sql = @$"
      UPDATE TutorialAppSchema.UserJobInfo
      SET [JobTitle] = '{user.JobTitle}',
      [Department] = '{user.Department}'
      WHERE UserId = '{user.UserId}';";

      bool isValid = _dapper.Execute(sql);
      if (!isValid)
      {
        return BadRequest();
      }
      return Ok();
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]

    public IActionResult DeleteUserJobInfo(int userId)
    {
      string sql = $"DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId = {userId};";
      bool isValid = _dapper.Execute(sql);
      if (!isValid)
      {
        return BadRequest();
      }
      return Ok();
    }
  };



}
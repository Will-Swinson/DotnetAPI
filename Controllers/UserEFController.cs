using DotnetAPI.Data;
using DotnetAPI.Models;
using DotnetAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace DotnetAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]

  public class UserEFController : ControllerBase
  {
    IMapper _mapper;
    private IUserRepository _userRepository;
    public UserEFController(IConfiguration config, IUserRepository userRepository)

    {
      _userRepository = userRepository;
      _mapper = new Mapper(new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<UserToAddDto, User>();
        cfg.CreateMap<UserJobInfoToAddDto, UserJobInfo>();
        cfg.CreateMap<UserSalaryToAddDto, UserSalary>();
        cfg.CreateMap<UserForRegistrationDto, UserForRegistration>();
      }));

      Console.WriteLine(config.GetConnectionString("DefaultConnection"));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
      IEnumerable<User> users = _userRepository.GetUsers();
      return users;
    }

    [HttpGet("GetUserById/{userId}")]
    public User GetUserById(int userId)
    {
      User user = _userRepository.GetUserById(userId);
      return user;


    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
      User userDb = _mapper.Map<User>(user);


      if (userDb != null)
      {
        _userRepository.AddEntity<User>(userDb);
        if (_userRepository.SaveChanges())
        {
          return Ok();
        }

      }

      throw new Exception("Failed to add");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
      User? userDb = _userRepository.GetUserById(user.UserId);

      if (userDb != null)
      {
        userDb.FirstName = user.FirstName != "" ? user.FirstName : userDb.FirstName;
        userDb.LastName = user.LastName != "" ? user.LastName : userDb.LastName;
        userDb.Email = user.Email != "" ? user.Email : userDb.Email;
        userDb.Gender = user.Gender != "" ? user.Gender : userDb.Gender;
        userDb.Active = user.Active != userDb.Active ? user.Active : userDb.Active;

        if (_userRepository.SaveChanges())
        {
          return Ok();
        }

      }

      throw new Exception("Failed to edit");

    }

    [HttpDelete("DeleteUser/{userId}")]

    public IActionResult DeleteUser(int userId)
    {
      User? userDb = _userRepository.GetUserById(userId);

      if (userDb != null)
      {
        _userRepository.RemoveEntity<User>(userDb);

        if (_userRepository.SaveChanges())
        {
          return Ok();
        }

      }

      throw new Exception("Failed to delete");
    }

    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo()
    {
      IEnumerable<UserJobInfo> usersJobInfo = _userRepository.GetUsersJobInfo();
      return usersJobInfo;
    }

    [HttpGet("GetUserJobInfoById/{userId}")]
    public UserJobInfo GetUserJobInfoById(int userId)
    {
      UserJobInfo userJobInfo = _userRepository.GetUserJobInfoById(userId);
      return userJobInfo;


    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfoToAddDto userJobInfo)
    {
      UserJobInfo userDb = _mapper.Map<UserJobInfo>(userJobInfo);


      if (userDb != null)
      {
        _userRepository.AddEntity<UserJobInfo>(userDb);
        if (_userRepository.SaveChanges())
        {
          return Ok();
        }

      }

      throw new Exception("Failed to add");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
      UserJobInfo? userDb = _userRepository.GetUserJobInfoById(userJobInfo.UserId);

      if (userDb != null)
      {
        userDb.JobTitle = userJobInfo.JobTitle != "" ? userJobInfo.JobTitle : userDb.JobTitle;
        userDb.Department = userJobInfo.Department != "" ? userJobInfo.Department : userDb.Department;
        if (_userRepository.SaveChanges())
        {
          return Ok();
        }

      }

      throw new Exception("Failed to edit");

    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]

    public IActionResult DeleteUserJobInfo(int userId)
    {
      UserJobInfo? userDb = _userRepository.GetUserJobInfoById(userId);

      if (userDb != null)
      {
        _userRepository.RemoveEntity<UserJobInfo>(userDb);

        if (_userRepository.SaveChanges())
        {
          return Ok();
        }

      }

      throw new Exception("Failed to delete");
    }

    [HttpGet("GetUsersSalary")]
    public IEnumerable<UserSalary> GetUsersSalary()
    {
      IEnumerable<UserSalary> usersSalary = _userRepository.GetUsersSalary();
      return usersSalary;
    }

    [HttpGet("GetUserSalaryById/{userId}")]
    public UserSalary GetUserSalaryById(int userId)
    {
      UserSalary userSalary = _userRepository.GetUserSalaryById(userId);
      return userSalary;


    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserJobInfoToAddDto userSalary)
    {
      UserSalary userDb = _mapper.Map<UserSalary>(userSalary);


      if (userDb != null)
      {
        _userRepository.AddEntity<UserSalary>(userDb);
        if (_userRepository.SaveChanges())
        {
          return Ok();
        }

      }

      throw new Exception("Failed to add");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
      UserSalary? userDb = _userRepository.GetUserSalaryById(userSalary.UserId);

      if (userDb != null)
      {
        userDb.Salary = userSalary.Salary != 0.0m ? userSalary.Salary : userDb.Salary;
        if (_userRepository.SaveChanges())
        {
          return Ok();
        }

      }

      throw new Exception("Failed to edit");

    }

    [HttpDelete("DeleteUserSalary/{userId}")]

    public IActionResult DeleteUserSalary(int userId)
    {
      UserSalary? userDb = _userRepository.GetUserSalaryById(userId);

      if (userDb != null)
      {
        _userRepository.RemoveEntity<UserSalary>(userDb);

        if (_userRepository.SaveChanges())
        {
          return Ok();
        }

      }

      throw new Exception("Failed to delete");
    }
  };



}
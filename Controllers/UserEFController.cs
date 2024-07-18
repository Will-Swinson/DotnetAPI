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
    private DataContextEF _entityFramework;
    public UserEFController(IConfiguration config, IUserRepository userRepository)

    {
      _userRepository = userRepository;
      _entityFramework = new DataContextEF(config);
      _mapper = new Mapper(new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<UserToAddDto, User>();
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
  };



}
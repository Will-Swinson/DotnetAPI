
using DotnetAPI.Models;
namespace DotnetAPI.Data
{

  public class UserRepository : IUserRepository
  {

    private DataContextEF _entityFramework;
    public UserRepository(IConfiguration config)

    {
      _entityFramework = new DataContextEF(config);
    }

    public bool SaveChanges()
    {
      return _entityFramework.SaveChanges() > 0;
    }

    public void AddEntity<T>(T entity)
    {
      if (entity != null)
      {
        _entityFramework.Add(entity);
      }
    }

    public void RemoveEntity<T>(T entity)
    {
      if (entity != null)
      {
        _entityFramework.Remove(entity);
      }
    }

    
    public IEnumerable<User> GetUsers()
    {
      IEnumerable<User> users = _entityFramework.Users.ToList<User>();
      return users;
    }

    public User GetUserById(int userId)
    {
      User? user = _entityFramework.Users
      .Where(u => u.UserId == userId)
      .FirstOrDefault<User>();

      if (user != null)
      {
        return user;
      }

      throw new Exception("User not found");

    }

  }
}
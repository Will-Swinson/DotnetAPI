using DotnetAPI.Models;

namespace DotnetAPI.Data
{
  public interface IUserRepository
  {
    bool SaveChanges();
    void AddEntity<T>(T entity);
    void RemoveEntity<T>(T entity);

    IEnumerable<User> GetUsers();
    User GetUserById(int userId);

    IEnumerable<UserJobInfo> GetUsersJobInfo();
    UserJobInfo GetUserJobInfoById(int userId);

    IEnumerable<UserSalary> GetUsersSalary();
    UserSalary GetUserSalaryById(int userId);
  }
}
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotnetAPI.Data
{
  public class DataContextDapper
  {
    private readonly IConfiguration _config;
    private readonly string? _connectionString;

    public DataContextDapper(IConfiguration config)
    {
      _config = config;
      _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<T> LoadData<T>(string sql)
    {

      IDbConnection connection = new SqlConnection(_connectionString);

      return connection.Query<T>(sql);

    }

    public T LoadDataSingle<T>(string sql)
    {

      IDbConnection connection = new SqlConnection(_connectionString);

      return connection.QuerySingle<T>(sql);

    }

    public bool Execute(string sql)
    {

      IDbConnection connection = new SqlConnection(_connectionString);

      return connection.Execute(sql) > 0;
    }

    public int ExecuteWithRowCount(string sql)
    {

      IDbConnection connection = new SqlConnection(_connectionString);

      return connection.Execute(sql);
    }
  }
}
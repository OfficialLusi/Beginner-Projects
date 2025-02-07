using BloggingPlatform_BE.Domain.Interfaces;
using Microsoft.Data.Sqlite;

namespace BloggingPlatform_BE.Infrastructure.Repository;


/* tips:
 * For create the tables at the startup, it's better to get the connection from an InitializeTables method
 * and using the same instance of the connection (to avoid creating a connection for each table)
 
 * For the CRUD operations, it's better to create a connection for each query that need to be execute
 */ 

public class RepositoryService : IRepositoryService
{
    private string _dbName; // BloggingPlatform_SQLite_DB
    private string _connectionString;

    public RepositoryService(string connectionString, string dbName)
    {
        #region InitialChecks
        InitialCheck(connectionString, "ConnectionString cannot be null");
        InitialCheck(dbName, "Database name cannot be null");
        #endregion

        _dbName = dbName;
        _connectionString = connectionString;
    }

    #region startup methods
    public void InitializeTables()
    {
        using (SqliteConnection connection = new SqliteConnection())
        {
            connection.Open();
            CreateUserTable(connection);
            CreateBlogPostTable(connection);
        }
    }

    private void CreateUserTable(SqliteConnection connection)
    {
        string query = $"CREATE TABLE [IF NOT EXISTS] {_dbName}.Users (  " +
                        "UserId INT PRIMARY KEY,                         " +
                        "UserGuid STRING NOT NULL,                       " +
                        "UserName STRING NOT NULL                        " +
                        "UserSurname STRING NOT NULL                     " +
                        "UserEmail STRING NOT NULL                       " +
                        "UserPassword STRING NOT NULL                    " +
                        "UserCreatedOn STRING NOT NULL                   " +
                        ")                                               ";

        using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = query;
            command.ExecuteNonQuery();
        }
    }

    private void CreateBlogPostTable(SqliteConnection connection)
    {

        string query = $"CREATE TABLE [IF NOT EXISTS] {_dbName}.BlogPosts ( " +
                        "PostId INT PRIMARY KEY,                            " +
                        "UserId INT FOREIGN KEY,                            " +
                        "PostGuid STRING NOT NULL                           " +
                        "PostTitle STRING NOT NULL                          " +
                        "PostContent STRING NOT NULL                        " +
                        "PostCreatedOn STRING NOT NULL                      " +
                        "PostModifiedOn STRING NOT NULL                     " +
                        ")                                                  ";

        using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

    }
    #endregion


    #region helper
    private void InitialCheck(object variable, string message)
    {
        if (variable is null)
        {
            Exception _ = null;
            throw new ArgumentNullException(message, _);
        }
    }
    #endregion

}

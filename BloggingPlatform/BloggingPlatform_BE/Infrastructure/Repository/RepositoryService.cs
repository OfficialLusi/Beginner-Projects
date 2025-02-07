using BloggingPlatform_BE.Application.DTOs;
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

    #region User
    public void AddUser(UserDto user)
    {
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Users (UserGuid, UserName, UserSurname, UserEmail, UserPassword, UserCreatedOn) " +
                           "VALUES (:UserGuid, :UserName, :UserSurname, :UserEmail, :UserPassword, :UserCreatedOn)      ";

            SqliteParameter[] parameters = new SqliteParameter[]
            {
            new(":UserGuid", user.UserGuid.ToString()),
            new(":UserName", user.UserName),
            new(":UserSurname", user.UserSurname),
            new(":UserEmail", user.UserEmail),
            new(":UserPassword", user.UserPassword),
            new(":UserCreatedOn", user.UserCreatedOn),
            };

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateUser(UserDto user)
    {
        // todo add control on user id on the caller method
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            string query = "UPDATE Users SET                " +
                           "UserGuid = :UserGuid,           " +
                           "UserName = :UserName,           " +
                           "UserSurname = :UserSurname,     " +
                           "UserEmail = :UserEmail,         " +
                           "UserPassword = :UserPassword,   " +
                           "UserCreatedOn = :UserCreatedOn  " +
                           "WHERE UserId = :UserId          ";

            SqliteParameter[] parameters = new SqliteParameter[]
            {
                new(":UserGuid", user.UserGuid.ToString()),
                new(":UserName", user.UserName),
                new(":UserSurname", user.UserSurname),
                new(":UserEmail", user.UserEmail),
                new(":UserPassword", user.UserPassword),
                new(":UserCreatedOn", user.UserCreatedOn),
                new(":UserId", user.UserId)
            };

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteUser(UserDto user)
    {
        // todo add control on user id on the caller method
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            int userId = user.UserId;

            string query = "DELETE FROM Users       " +
                           "WHERE UserId = :UserId  ";

            SqliteParameter parameter = new SqliteParameter(":UserId", userId);

            using (SqliteCommand command = connection.CreateCommand()) 
            { 
                command.CommandText= query;
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }

    #endregion

    #region BlogPost

    public void AddBlogPost(BlogPostDto blogPost, int userId)
    {
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            string query = "INSERT INTO BlogPosts (UserId, PostGuid, PostTitle, PostContent, PostTags, PostCreatedOn, PostModifiedOn)   " +
                           "VALUES (:UserId, :PostGuid, :PostTitle, :PostContent, :PostTags, :PostCreatedOn, :PostModifiedOn)           ";

            SqliteParameter[] parameters = new SqliteParameter[]
            {
                new(":UserId", userId),
                new(":PostGuid", blogPost.PostGuid.ToString()),
                new(":PostTitle", blogPost.PostTitle),
                new(":PostContent", blogPost.PostContent),
                new(":PostTags", blogPost.PostTags),
                new(":PostCreatedOn", DateTime.UtcNow),
                new(":PostModifiedOn", DBNull.Value)
            };

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }
    }    
    
    public void UpdateBlogPost(BlogPostDto blogPost)
    {
        // todo add control on user id on the caller method
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            string query = "UPDATE BlogPosts SET                " +
                           "PostGuid = :PostGuid,               " +
                           "PostTitle = :PostTitle,             " +
                           "PostContent = :PostContent,         " +
                           "PostTags = :PostTags,               " +
                           "PostModifiedOn = :PostModifiedOn    " +
                           "WHERE PostId = :PostId              ";

            SqliteParameter[] parameters = new SqliteParameter[]
            {
                new(":PostId", blogPost.PostId),
                new(":PostGuid", blogPost.PostGuid.ToString()),
                new(":PostTitle", blogPost.PostTitle),
                new(":PostContent", blogPost.PostContent),
                new(":PostTags", blogPost.PostTags),
                new(":PostModifiedOn", DateTime.UtcNow)
            };

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }
    }    
    
    public void DeleteBlogPost(BlogPostDto blogPost)
    {
        // todo add control on user id on the caller method
        using (SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            int postId = blogPost.PostId;

            string query = "DELETE FROM BlogPosts   " +
                           "WHERE PostId = :PostId  ";

            SqliteParameter parameter = new SqliteParameter(":PostId", postId);

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
            }
        }
    }


    #endregion

    #region startup methods
    public void InitializeTables()
    {
        using (SqliteConnection connection = new SqliteConnection())
        {
            connection.Open();
            CreateUserTable(connection);
            CreateBlogPostTable(connection);
            CreateAdminUser(connection);
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
                        "PostModifiedOn STRING NULL                         " +
                        ")                                                  ";

        using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = query;
            command.ExecuteNonQuery();
        }
    }

    private void CreateAdminUser(SqliteConnection connection)
    {
        string query = "INSERT INTO Users (UserGuid, UserName, UserSurname, UserEmail, UserPassword, UserCreatedOn) " +
                       "VALUES (:UserGuid, :UserName, :UserSurname, :UserEmail, :UserPassword, :UserCreatedOn)      ";

        SqliteParameter[] parameters = new SqliteParameter[]
        {
            new(":UserGuid", Guid.NewGuid().ToString()),
            new(":UserName", "admin"),
            new(":UserSurname", "admin"),
            new(":UserEmail", "admin@admin.admin"),
            new(":UserPassword", "admin"),
            new(":UserCreatedOn", DateTime.UtcNow.ToString()),
        };

        using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = query;
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
        }
    }
    #endregion


    #region helper
    private void InitialCheck(object variable, string message)
    {
        if (variable is null)
        {
            Exception? _ = null;
            throw new ArgumentNullException(message, _);
        }
    }
    #endregion

}

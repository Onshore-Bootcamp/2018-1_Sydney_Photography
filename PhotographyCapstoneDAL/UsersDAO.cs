using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using PhotographyCapstoneDAL.Models;
using System.Data.SqlClient;

namespace PhotographyCapstoneDAL
{ 
 
    public class UsersDAO
    {
        public UsersDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;

        public void AddUser(UsersDO newUser)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ADD_USER", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();

                storedProcedure.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                storedProcedure.Parameters.AddWithValue("@LastName", newUser.LastName);
                storedProcedure.Parameters.AddWithValue("@Email", newUser.Email);
                storedProcedure.Parameters.AddWithValue("@City", newUser.City);
                storedProcedure.Parameters.AddWithValue("@UserName", newUser.UserName);
                storedProcedure.Parameters.AddWithValue("@Password", newUser.Password);
                storedProcedure.Parameters.AddWithValue("@RoleID", newUser.RoleID);

                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogFile.DataFile(ex: ex);
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }

            }
        }

        public UsersDO ReadUser(string UserName)
        {
            UsersDO user = null;
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("READ_USER_BY_USERNAME", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@userName", UserName);

                connectionToSql.Open();
                SqlDataReader reader = storedProcedure.ExecuteReader();
                reader.Read();

                user = new UsersDO(
                    (long)reader["UserID"],
                    reader["FirstName"]as string,
                    reader["LastName"]as string,
                    reader["Email"]as string,
                    reader["City"]as string,
                    reader["UserName"]as string,
                    reader["Password"]as string,
                    (byte)reader["RoleID"],
                    reader["Description"]as string    
                    );
                reader.Close();
            }
            catch (Exception ex)
            {
                LogFile.DataFile(ex: ex);
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
                ;
            }
            return user;
        }

        public void UpdateUser(UsersDO userDO)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("UPDATE_USER", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();

                storedProcedure.Parameters.AddWithValue("@UserID", userDO.UserID);
                storedProcedure.Parameters.AddWithValue("@FirstName", userDO.FirstName);
                storedProcedure.Parameters.AddWithValue("@LastName", userDO.LastName);
                storedProcedure.Parameters.AddWithValue("@Email", userDO.Email);
                storedProcedure.Parameters.AddWithValue("@City", userDO.City);
                storedProcedure.Parameters.AddWithValue("@UserName", userDO.UserName);
                storedProcedure.Parameters.AddWithValue("@RoleID", userDO.RoleID);

                storedProcedure.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                LogFile.DataFile(ex: ex);
                throw ex;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }

            }
        }

        public void DeleteUser(long identification)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("DELETE_USER", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@UserID", identification);

                connectionToSql.Open();
                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogFile.DataFile(ex: ex);
                throw ex;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }

            }
        }

        public List<UsersDO> ReadALLUsers()
        {
            List<UsersDO> usersList = new List<UsersDO>();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("READ_ALL_USERS", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();
                SqlDataReader sqlDataReader = storedProcedure.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    UsersDO user = new UsersDO();
                    usersList.Add(user);
                    user.UserID = (long)sqlDataReader["UserID"];
                    user.FirstName = sqlDataReader["FirstName"] as string;
                    user.LastName = sqlDataReader["LastName"] as string;
                    user.Email = sqlDataReader["Email"] as string;
                    user.City = sqlDataReader["City"] as string;
                    user.UserName = sqlDataReader["UserName"] as string;
                    user.Password = sqlDataReader["Password"] as string;
                    user.RoleID = (byte)sqlDataReader["RoleID"];
                    user.RoleName = sqlDataReader["Description"] as string;
                }
            }
            catch (Exception ex)
            {
                LogFile.DataFile(ex: ex);
                throw ex;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }


            }
            return usersList;
        }

        public UsersDO ReadUserByID(long userID)
        {
            UsersDO user = new UsersDO();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("READ_USER_BY_ID", connectionToSql);
                storedProcedure.Parameters.AddWithValue("@UserID", userID);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                connectionToSql.Open();
                SqlDataReader sqlDataReader = storedProcedure.ExecuteReader();

                sqlDataReader.Read();
                user.UserID = (long)sqlDataReader["UserID"];
                user.FirstName = sqlDataReader["FirstName"] as string;
                user.LastName = sqlDataReader["LastName"] as string;
                user.Email = sqlDataReader["Email"] as string;
                user.City = sqlDataReader["City"] as string;
                user.UserName = sqlDataReader["UserName"] as string;
                user.Password = sqlDataReader["Password"] as string;
                user.RoleID = (byte)sqlDataReader["RoleID"];

                sqlDataReader.Close();
            }
            catch(Exception ex)
            {
                LogFile.DataFile(ex: ex);
                throw ex;
            }
            finally
            {
                if(connectionToSql!= null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
            }
            return user;
        }
    }
}

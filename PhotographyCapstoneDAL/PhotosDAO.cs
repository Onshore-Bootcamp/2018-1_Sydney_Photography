using PhotographyCapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyCapstoneDAL
{
    public class PhotosDAO
    {
        //consturctor for injection to connect to sql
        public PhotosDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
        //Method to upload photo to sql
        public void AddPhoto(PhotoDO photoDO)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ADD_PHOTO", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();

                storedProcedure.Parameters.AddWithValue("@PhotoName", photoDO.PhotoName);
                storedProcedure.Parameters.AddWithValue("@Height", photoDO.Height);
                storedProcedure.Parameters.AddWithValue("@Width", photoDO.Width);
                storedProcedure.Parameters.AddWithValue("@ExtensionType", photoDO.ExtensionType);
                storedProcedure.Parameters.AddWithValue("@Size", photoDO.Size);
                storedProcedure.Parameters.AddWithValue("@DateCreated", photoDO.DateCreated);
                storedProcedure.Parameters.AddWithValue("@Photo", photoDO.Photo);
                //stores relative path of photo to database
                storedProcedure.Parameters.AddWithValue("@Byte", photoDO.Byte);
                


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
        //method to view photos from database
        public List<PhotoDO> ViewAllPhotos()
        {
            List<PhotoDO> photoList = new List<PhotoDO>();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("READ_ALL_PHOTOS", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();
                SqlDataReader sqlDataReader = storedProcedure.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    PhotoDO photo = new PhotoDO();
                    photoList.Add(photo);
                    photo.PhotoID = Convert.ToInt32(sqlDataReader["PhotoID"]);
                    photo.PhotoName = sqlDataReader["PhotoName"] as string;
                    photo.Height = Convert.ToInt32(sqlDataReader["Height"]);
                    photo.Width = Convert.ToInt32(sqlDataReader["Width"]);
                    photo.ExtensionType = sqlDataReader["ExtensionType"] as string;
                    photo.Size = Convert.ToInt32(sqlDataReader["Size"]);
                    photo.DateCreated = (DateTime)sqlDataReader["DateCreated"];
                    photo.Photo = sqlDataReader["Photo"] as string;
                    photo.Byte = sqlDataReader["Byte"] as byte?[];
                }
                sqlDataReader.Close();
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
            return photoList;
        }
        //update photo row in datatbase
        public void UpdatePhoto(PhotoDO photoDO)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("UPDATE_PHOTO", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();

                storedProcedure.Parameters.AddWithValue("@PhotoID", photoDO.PhotoID);
                storedProcedure.Parameters.AddWithValue("@PhotoName", photoDO.PhotoName);
                storedProcedure.Parameters.AddWithValue("@Height", photoDO.Height);
                storedProcedure.Parameters.AddWithValue("@Width", photoDO.Width);
                storedProcedure.Parameters.AddWithValue("@ExtensionType", photoDO.ExtensionType);
                storedProcedure.Parameters.AddWithValue("@Size", photoDO.Size);
                storedProcedure.Parameters.AddWithValue("@DateCreated", photoDO.DateCreated);
                storedProcedure.Parameters.AddWithValue("@Photo", photoDO.Photo);
                storedProcedure.Parameters.AddWithValue("@Byte", photoDO.Byte);

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
        //method to view only one row out of photo table in data base
        public PhotoDO ReadPhotoById(int photoID)
        {
            PhotoDO photo = new PhotoDO();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("READ_PHOTO_BY_ID", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();

                storedProcedure.Parameters.AddWithValue("@PhotoID", photoID);
                SqlDataReader reader = storedProcedure.ExecuteReader();
                reader.Read();
                photo.PhotoID = (int)reader["PhotoID"];
                photo.PhotoName = reader["PhotoName"] as string;
                photo.Height = (int)reader["Height"];
                photo.Width = (int)reader["Width"];
                photo.ExtensionType = reader["ExtensionType"] as string;
                photo.Size = (int)reader["Size"];
                photo.DateCreated = (DateTime)reader["DateCreated"];
                photo.Photo = reader["Photo"] as string;
                photo.Byte = reader["Byte"] as byte?[];

                reader.Close();


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
            return photo;
        }
        // method to remove photo from photo table and juntion table
        public void DeletePhoto(int photoID)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("DELETE_PHOTO", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@PhotoID", photoID);

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

       
    }
}

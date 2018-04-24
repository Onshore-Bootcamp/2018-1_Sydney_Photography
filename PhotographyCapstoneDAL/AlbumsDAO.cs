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
    public class AlbumsDAO
    {
        public AlbumsDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
        public void AddAlbum(AlbumDO newAlbum)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ADD_ALBUM", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();

                storedProcedure.Parameters.AddWithValue("@AlbumName", newAlbum.AlbumName);
                storedProcedure.Parameters.AddWithValue("@AlbumType", newAlbum.AlbumType);
                storedProcedure.Parameters.AddWithValue("@UserID", newAlbum.UserID);


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

        public List<AlbumDO> ReadAllAlbums()
        {
            List<AlbumDO> albumsList = new List<AlbumDO>();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("READ_ALL_ALBUMS", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();
                SqlDataReader sqlDataReader = storedProcedure.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    AlbumDO album = new AlbumDO();
                    album.AlbumID = (int)sqlDataReader["AlbumID"];
                    album.AlbumName = sqlDataReader["AlbumName"] as string;
                    album.AlbumType = sqlDataReader["AlbumType"] as string;

                    if (sqlDataReader["UserId"] != DBNull.Value)
                    {
                        album.UserID = (long)sqlDataReader["UserID"];
                    }
                    else
                    {
                        //Nothing at this time
                    }

                    albumsList.Add(album);
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
            return albumsList;
        }

        public void UpdateAlbum(AlbumDO albumDO)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("UPDATE_ALBUM", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();

                storedProcedure.Parameters.AddWithValue("@AlbumID", albumDO.AlbumID);
                storedProcedure.Parameters.AddWithValue("@AlbumName", albumDO.AlbumName);
                storedProcedure.Parameters.AddWithValue("@AlbumType", albumDO.AlbumType);
                storedProcedure.Parameters.AddWithValue("@UserID", albumDO.UserID);

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

        public void DeleteAlbum(long albumID)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("DELETE_ALBUM", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                storedProcedure.Parameters.AddWithValue("@AlbumID", albumID);

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

        public AlbumDO ReadAlbumByID(int albumID)
        {
            AlbumDO album = new AlbumDO();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("READ_ALBUM_BY_ID", connectionToSql);
                storedProcedure.Parameters.AddWithValue("@AlbumID", albumID);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;

                connectionToSql.Open();
                SqlDataReader sqlDataReader = storedProcedure.ExecuteReader();

                sqlDataReader.Read();
                album.AlbumID = (int)sqlDataReader["AlbumID"];
                album.AlbumName = sqlDataReader["AlbumName"] as string;
                album.AlbumType = sqlDataReader["AlbumType"] as string;
                if (sqlDataReader["UserId"] != DBNull.Value)
                {
                    album.UserID = (long)sqlDataReader["UserID"];
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
            return album;
        }

        public List<AlbumDO> ViewPhotosInAlbum(int albumID)
        {
            List<AlbumDO> albumsList = new List<AlbumDO>();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("VIEW_PHOTOS_IN_ALBUM", connectionToSql);
                storedProcedure.Parameters.AddWithValue("@AlbumID", albumID);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();
                SqlDataReader sqlDataReader = storedProcedure.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    AlbumDO album = new AlbumDO();
                    albumsList.Add(album);
                    album.AlbumName = sqlDataReader["AlbumName"] as string;
                    album.AlbumType = sqlDataReader["AlbumType"] as string;
                    album.PhotoID = (int)sqlDataReader["PhotoID"];
                    album.Photo = sqlDataReader["Photo"] as string;
                    album.PhotoName = sqlDataReader["PhotoName"] as string;

                    if (sqlDataReader["UserId"] != DBNull.Value)
                    {
                        album.UserID = (long)sqlDataReader["UserID"];
                    }



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

            return albumsList;
        }


        public void AddPhotoToAlbum(int PhotoID, int AlbumID)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("ADD_PHOTO_TO_ALBUM", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();

                storedProcedure.Parameters.AddWithValue("@PhotoID", PhotoID);
                storedProcedure.Parameters.AddWithValue("@AlbumID", AlbumID);

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

        public List<PhotoAlbumDO> ReadAllJunctions()
        {
            List<PhotoAlbumDO> photoAlbumsList = new List<PhotoAlbumDO>();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            try
            {
                connectionToSql = new SqlConnection(_connectionString);
                storedProcedure = new SqlCommand("READ_ALL_JUNCTIONS", connectionToSql);
                storedProcedure.CommandType = System.Data.CommandType.StoredProcedure;
                connectionToSql.Open();
                SqlDataReader sqlDataReader = storedProcedure.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    PhotoAlbumDO photoAlbum = new PhotoAlbumDO();
                    photoAlbum.PhotoAlbumID = (long)sqlDataReader["PhotoAlbumID"];
                    photoAlbum.PhotoID = (int)sqlDataReader["PhotoID"];
                    photoAlbum.AlbumID = (int)sqlDataReader["AlbumID"];

                    photoAlbumsList.Add(photoAlbum);
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

            return photoAlbumsList;
        }
    }
}






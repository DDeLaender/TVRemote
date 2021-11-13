using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace TVRemote.repositories
{
    //RegistryRepository logs button clicks
    class RegistryRepository
    {
        // properties
        const string _connectionStringTvRemote = "Data source=localhost; Initial Catalog=TvRemote; User ID=sa; Password=123456789";
        const string _connectionStringMaster = "Data source=localhost; Initial Catalog=master; User ID=sa; Password=123456789";

        //methods
        // validates if the repository exists
        public bool ValidateRepositoryExists()
        {
            string sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES " +
                         "WHERE TABLE_SCHEMA = 'dbo' " +
                         "AND TABLE_NAME='registry'";
            int count;
            using (var connection = new SqlConnection(_connectionStringTvRemote))
            using (var command = new SqlCommand (sql, connection))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException e)
                {
                    return false;
                }
                var reader = command.ExecuteReader();
                reader.Read();
                var record = (IDataRecord)reader;  //typecast reader to record 
                count = record.GetInt32(0);
            }
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // creating the registry table
        public void CreateRepository()
        {
            string sql = "CREATE DATABASE TvRemote ";
            using (var connection = new SqlConnection(_connectionStringMaster))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.ExecuteReader();
                connection.Close();
            }

            Thread.Sleep(5000); //necessary because it takes time to make a database. db must exist before you can make a table...

            sql = "CREATE TABLE Registry(ButtonName varchar(50), TimeClicked datetime)";
            using (var connection = new SqlConnection(_connectionStringTvRemote))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.ExecuteReader();
            }
        }


        // register a button click to the table
        public void RegisterButtonClick()
        {

        }

        // gets time in msec since last click
        public void GetTimeSinceLastClick()
        {

        }
    }
}

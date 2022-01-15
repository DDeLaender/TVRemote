using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using TVRemote.models;

namespace TVRemote.repositories
{
    //RegistryRepository logs button clicks
    public class RegistryRepository : BaseRepository
    {
        public override string TableName
        {
            get
            {
                return "registry";
            }
        }

        // creating the registry table
        public override void CreateRepository()
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

            sql = "CREATE TABLE Registry(Id int identity(1,1) primary key,ButtonType int, ButtonName int, TimeClicked datetime DEFAULT(getdate()))"; //timeclicked runns automatically 
            using (var connection = new SqlConnection(_connectionStringTvRemote))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.ExecuteReader();
            }
        }


        // register a button click to the table
        public void RegisterButtonClick(ButtonType buttonType, ButtonName buttonName)
        {
            string sql = @"INSERT INTO Registry(ButtonType, ButtonName) 
                         VALUES (@ButtonType, @ButtonName)";

            using (var connection = new SqlConnection(_connectionStringTvRemote))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@ButtonType", (int)buttonType);
                command.Parameters.AddWithValue("@ButtonName", (int)buttonName);
                command.ExecuteReader();
            }
        }
        public List<int> NumbersClicked()
        {
            string sql = @"
                WITH numbers AS ( 
	                SELECT * 
	                FROM Registry 
	                WHERE ButtonType = 3 AND DATEDIFF_BIG(MILLISECOND, TimeClicked, CURRENT_TIMESTAMP) <= 3000
                )
                SELECT ButtonName as NumbersClicked
                FROM numbers;";
            using(var connection = new SqlConnection(_connectionStringTvRemote))
            using(var command = new SqlCommand (sql, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                connection.Close();
                List<int> numbers = new List<int>();
                while (reader.Read())
                {
                    var number = reader.GetInt32(reader.GetOrdinal("NumbersClicked"));
                    numbers.Add(number);
                }
                return numbers;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using TVRemote.Configuration;

namespace TVRemote.repositories
{
    public abstract class BaseRepository
    {
        //fields
        protected const string _connectionStringTvRemote = "Data source=localhost; Initial Catalog=TvRemote; User ID=sa; Password=123456789";
        protected const string _connectionStringMaster = "Data source=localhost; Initial Catalog=master; User ID=sa; Password=123456789";
        private static dynamic singleton;
        //properties
        public abstract string TableName
        {
            get;
        }

        //Constructor
        public dynamic BaseRepository()
        { 
            if (singleton)
            {
                return singleton;
            }
            else
            {
                bool validationRequired = Configuration.Configuration.mode == Mode.Production;
                if (validationRequired)
                {
                    bool validateRepositoryExists = ValidateRepositoryExists(TableName);
                    if (!validateRepositoryExists)
                    {
                        CreateRepository();
                    }
                }

                singleton = this;
                return singleton;
            }
            
        }

        //methods
        //validates if the repository exists
        public bool ValidateRepositoryExists(string tableName)
        {
            string sql = @"SELECT COUNT(*) 
                         FROM INFORMATION_SCHEMA.TABLES 
                         WHERE TABLE_SCHEMA = 'dbo' 
                         AND TABLE_NAME='@TableName'";
            int count;
            using (var connection = new SqlConnection(_connectionStringTvRemote))
            using (var command = new SqlCommand(sql, connection))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    return false;
                }
                command.Parameters.AddWithValue("@TableName", tableName);
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
        public abstract void CreateRepository();

    }
}

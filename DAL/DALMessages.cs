using ArtPortfolioASPEdition.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ArtPortfolioASPEdition.DAL
{
    public class DALMessages
    {
        private IConfiguration configuration;

        public DALMessages(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        internal int addMessage(ContactDetails message)
        {
            //Create connection
            string connStr = configuration.GetConnectionString("MyConnString");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();


            //SQL query for adding messages to database
            string query = "INSERT INTO [dbo].[messages]([email],[name],[message])" +
                " VALUES( @email, @name, @description)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", message.name);
            cmd.Parameters.AddWithValue("@email", message.email);
            cmd.Parameters.AddWithValue("@description", message.message);

            //Query 
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            //creates user ID
            int uID = 1;

            conn.Close(); //Closes the connection

            return uID;
        }
    }
}

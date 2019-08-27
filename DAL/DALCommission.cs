using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ArtPortfolioASPEdition.Models;
using Microsoft.Extensions.Configuration;

namespace ArtPortfolioASPEdition.DAL
{
    public class DALCommission
    {
        private IConfiguration configuration;

        public DALCommission(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        internal int addCommission(CommissionDetails commission)
        {
            //Create connection
            string connStr = configuration.GetConnectionString("MyConnString");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();


            //Command for inserting data into dbo.Person table
            string query = "INSERT INTO [dbo].[CommissionsList]([email],[description])" +
                " VALUES( @email, @description)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@email", commission.email);
            cmd.Parameters.AddWithValue("@description", commission.description);

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

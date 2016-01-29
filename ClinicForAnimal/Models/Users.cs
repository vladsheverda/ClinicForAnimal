using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ClinicForAnimal.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get;set; }
        public string LastName { get; set; }
        public string  EmailId { get; set; }


        string stringConnection = ConfigurationManager.ConnectionStrings["ClinicForAnimal"].ConnectionString.ToString();
        public List<Users> Select()
        {
            using(SqlConnection connention = new SqlConnection(stringConnection))
            {
                connention.Open();
                SqlCommand command = new SqlCommand("Select UserName, Password From [dbo].[Users]",connention);
                IDataReader idr = command.ExecuteReader();
                List<Users> list = new List<Users>();
                while (idr.Read())
                {
                    list.Add(new Users
                    {
                        UserName = idr["UserName"].ToString(),
                        Password = idr["Password"].ToString()
                    });
                }
                return list;
            }
        }
        private IDataReader Insert(string userName,string password,string firstName,string lastName,string emailId)
        {
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec [dbo].[AddUser] @UserName,@Password,@FirstName,@LastName,@EmailId", connection);
                command.Parameters.AddWithValue("@UserName",userName);
                command.Parameters.AddWithValue("@Password",password);
                command.Parameters.AddWithValue("@FirstName",firstName);
                command.Parameters.AddWithValue("@LastName",lastName);
                command.Parameters.AddWithValue("EmailId",emailId);
                return command.ExecuteReader();
            }
        }
        public void AddUser(string userName,string password,string firstName,string lastName,string emailId)
        {
            Insert( userName, password,firstName, lastName,emailId);
        }
    }
}
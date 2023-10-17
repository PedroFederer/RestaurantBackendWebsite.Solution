using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using RestaurantBackendWebsite.Models.Entities;

namespace RestaurantBackendWebsite.Models.Infra
{
	public class dbHelper
	{
		private static string _connectionString= System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
		private static SqlConnection GetConnection() 
		{
			return new SqlConnection( _connectionString );
		}
		public static int CreateUsers(User users)
		{
			using (var db = GetConnection())
			{
				string qry = "INSERT INTO Users(Account,Password,Name)VALUES(@Account,@Password,@Name)";
				return db.Execute(qry, new { users.Account, users.Password, users.Name });
			}
		}
		public static int UpdateUsers(User users)
		{
			using (var db = GetConnection())
			{
				string qry = "Update Users SET Account=@Account,Name=@Name Where Id=@Id";
	
				return db.Execute(qry, new {Id = users.Id, Account =users.Account, Name=users.Name });
			}
		
		}
		public static IEnumerable<User> GetUserList()
		{
			using (var db = GetConnection())
			{
				string qry = "SELECT Id,Account, Name,Password FROM Users";
				return db.Query<User>(qry);
			}
		}
		public static User GetUserById(int? Id)
		{
			using (var db = GetConnection())
			{
				string qry = "SELECT Top(1)*FROM Users Where Id=@Id";
				return db.Query<User>(qry, new { Id}).FirstOrDefault();
			}
		}
		public static void DeleteUser(int Id)
		{
			using (var db = GetConnection())
			{
				string qry = "Delete FROM Users Where Id=@Id";
				db.Execute(qry, new { Id });
			}
		}
	}
}
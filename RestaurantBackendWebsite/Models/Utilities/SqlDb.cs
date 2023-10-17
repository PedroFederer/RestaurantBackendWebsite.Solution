﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.Utilities
{
	public class SqlDb
	{
		public string GetConnString(string key)
		{
			var setting = System.Configuration.ConfigurationManager
								.ConnectionStrings[key];

			if (setting == null)
			{
				throw new Exception($"config裡面找不到連線字串--{key}的設定，請您確認後再試一次");
			}
			return setting.ConnectionString;
		}
		public SqlConnection GetConnection(string key)
		{
			var connString = GetConnString(key);
			return new SqlConnection(connString);
		}
		public void UpdateOrDelete(string sql, SqlParameter[] parameters)
		{
			var db = new SqlDb();
			using (var conn = db.GetConnection("default"))
			{
				conn.Open();
				var command = new SqlCommand(sql, conn);
				if (parameters != null && parameters.Length > 0)
				{
					command.Parameters.AddRange(parameters);
				}
				command.ExecuteNonQuery();
			}
		}
	}
}
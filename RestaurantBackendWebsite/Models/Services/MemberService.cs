using RestaurantBackendWebsite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.Services
{
	public class MemberService
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString);
		SqlCommand cmd;
		SqlDataAdapter sda;
		DataTable dt;
		public List<MemberVm> GetMembers()
		{
			cmd = new SqlCommand("sp_select", con);
			cmd.CommandType = CommandType.StoredProcedure;
			sda = new SqlDataAdapter(cmd);
			dt = new DataTable();
			sda.Fill(dt);
			List<MemberVm>list=new List<MemberVm>();
			foreach (DataRow dr in dt.Rows)
			{
				list.Add(new MemberVm
				{
					Id = Convert.ToInt32(dr["Id"]),
					Name = dr["Name"].ToString(),
					Email = dr["Email"].ToString(),
					PhoneNumber = dr["PhoneNumber"].ToString(),
					Account = dr["Account"].ToString()
				});
			}
			return list;
		}
		
		
	}
}
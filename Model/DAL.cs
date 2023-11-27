using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SampleWebApp.Model
{
    public class DAL
    {
        public List<User> GetUsers(IConfiguration _configuration)
        {
            List<User> users = new List<User>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TblUsers", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        User user = new User();
                        user.Id = Convert.ToString(dt.Rows[i]["Id"]);
                        user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                        user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public int AddUser(User user, IConfiguration _configuration)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TblUsers VALUES('" + user.FirstName + "', '" + user.LastName + "')", con);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
            return i;
        }

        public User GetUser(string id, IConfiguration _configuration)
        {
            User user = new User();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TblUsers WHERE ID = '" + id + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    user.Id = Convert.ToString(dt.Rows[0]["Id"]);
                    user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                }
            }
            return user;
        }

        public int UpdateUser(User user, IConfiguration _configuration)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlCommand cmd = new SqlCommand("Update TblUsers SET FirstName = '" + user.FirstName + "', LastName = '" + user.LastName + "' WHERE ID = '" + user.Id + "'", con);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
            return i;
        }

        public int DeleteUser(string id, IConfiguration _configuration)
        {   
            int i = 0;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM TblUsers WHERE ID = '" + id + "'", con);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
            return i;
        }

    }
}

using idkhelp.Pages.clients;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace idkhelp.Pages
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> listUser = new List<UserInfo>();
        public bool loginFlag;
        public void OnGet()
        {
            try
            {
                //String connectionString = "Server=tcp:testl3db.database.windows.net,1433;Initial Catalog=testdbl3;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                String connectionString = "Server=tcp:bdtestalejo.database.windows.net,1433;Initial Catalog=bdtestalejo;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.user = reader.GetString(1);
                                userInfo.pass = reader.GetString(2);

                                listUser.Add(userInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.ToString());
            }
        }

        public void OnPost() 
        {
            string userForm = Request.Form["user"];
            string passForm = Request.Form["pass"];

            try
            {
                //String connectionString = "Server=tcp:testl3db.database.windows.net,1433;Initial Catalog=testdbl3;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                String connectionString = "Server=tcp:bdtestalejo.database.windows.net,1433;Initial Catalog=bdtestalejo;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.user = reader.GetString(1);
                                userInfo.pass = reader.GetString(2);

                                listUser.Add(userInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.ToString());
            }

            for (int i = 0; i < listUser.Count(); i++)
            {
                if (userForm.Equals(listUser[i].user))
                {
                    if (passForm.Equals(listUser[i].pass))
                    {
                        loginFlag = true;
                        break;
                    }
                }
            }

            if (loginFlag)
            {
                Response.Redirect("/Alumnos/Index");
            }
            
        }

    }
    public class UserInfo
    {
        public String id;
        public String user;
        public String pass;
    }
}

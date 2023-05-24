using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace idkhelp.Pages.clients
{
    public class IndexCursos : PageModel
    {
        public List<CursosInfo> listCurso = new List<CursosInfo>();
        
        public void OnGet()
        {
            try
            {
                //String connectionString = "Server=tcp:testl3db.database.windows.net,1433;Initial Catalog=testdbl3;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                String connectionString = "Server=tcp:bdtestalejo.database.windows.net,1433;Initial Catalog=bdtestalejo;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Cursos";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CursosInfo cursosInfo = new CursosInfo();
                                cursosInfo.id = "" + reader.GetInt32(0);
                                cursosInfo.nombreCurso = reader.GetString(1);
                                cursosInfo.temario = reader.GetString(2);

                                listCurso.Add(cursosInfo);
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
    }

    public class CursosInfo
    {
        public String id;
        public String nombreCurso;
        public String temario;
    }
}

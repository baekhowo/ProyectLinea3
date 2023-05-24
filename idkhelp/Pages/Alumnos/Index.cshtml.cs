using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace idkhelp.Pages.clients
{
    public class IndexModel : PageModel
    {
        public List<AlumnoInfo> listAlumno = new List<AlumnoInfo>();
        
        public void OnGet()
        {
            try
            {
                //String connectionString = "Server=tcp:testl3db.database.windows.net,1433;Initial Catalog=testdbl3;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                String connectionString = "Server=tcp:bdtestalejo.database.windows.net,1433;Initial Catalog=bdtestalejo;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Alumnos";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AlumnoInfo alumnoInfo = new AlumnoInfo();
                                alumnoInfo.id = "" + reader.GetInt32(0);
                                alumnoInfo.nombre = reader.GetString(1);
                                alumnoInfo.apellido = reader.GetString(2);
                                alumnoInfo.sexo = reader.GetString(3);
                                alumnoInfo.fechaNace = reader.GetDateTime(4).ToString();
                                alumnoInfo.estado = "" + reader.GetInt32(5);

                                listAlumno.Add(alumnoInfo);
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

    public class AlumnoInfo
    {
        public String id;
        public String nombre;
        public String apellido;
        public String sexo;
        public String fechaNace;
        public String estado;
    }
}

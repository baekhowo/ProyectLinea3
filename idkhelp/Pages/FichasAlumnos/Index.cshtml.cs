using Azure.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Reflection.PortableExecutable;

namespace idkhelp.Pages.clients
{
    public class IndexFichaAlumnos : PageModel
    {
        public List<FichaAlumnoInfo> listFichaAlumno = new List<FichaAlumnoInfo>();
        public string nombreA { get; set; }

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                //String connectionString = "Server=tcp:testl3db.database.windows.net,1433;Initial Catalog=testdbl3;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                String connectionString = "Server=tcp:bdtestalejo.database.windows.net,1433;Initial Catalog=bdtestalejo;Persist Security Info=False;User ID=admtest;Password=Pass123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select Alumnos.id, Alumnos.nombreAlumno, Alumnos.apellidoAlumno," +
                                 "Alumnos.sexo, Alumnos.fechaNace, Cursos.nombreCurso from Alumnos," +
                                 "Cursos, Fichas where  Fichas.idAlumno = Alumnos.id and Fichas.idCurso = Cursos.id and Fichas.idAlumno = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FichaAlumnoInfo fichaAlumnoInfo = new FichaAlumnoInfo();
                                fichaAlumnoInfo.id = "" + reader.GetInt32(0);
                                fichaAlumnoInfo.nombreAlumno = reader.GetString(1);
                                fichaAlumnoInfo.apellidoAlumno = reader.GetString(2);
                                fichaAlumnoInfo.sexo = reader.GetString(3);
                                fichaAlumnoInfo.fechaNace = "" + reader.GetDateTime(4);
                                fichaAlumnoInfo.nombreCurso = reader.GetString(5);

                                listFichaAlumno.Add(fichaAlumnoInfo);
                                nombreA = fichaAlumnoInfo.nombreAlumno;
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

    public class FichaAlumnoInfo
    {
        public String id;
        public String nombreAlumno;
        public String apellidoAlumno;
        public String sexo;
        public String fechaNace;
        public String nombreCurso;
    }
}

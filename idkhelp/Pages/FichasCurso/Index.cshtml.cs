using Azure.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Reflection.PortableExecutable;

namespace idkhelp.Pages.clients
{
    public class IndexFichaCurso : PageModel
    {
        public List<FichaCursoInfo> listFichaCurso = new List<FichaCursoInfo>();
        //string lastCharacter = "";
        
        public string nombreC { get; set; }
        public string temarioC { get; set; }

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
                                 "Cursos.nombreCurso, Cursos.temario from Alumnos, Cursos, Fichas" +
                                 "where Fichas.idAlumno = Alumnos.id and Fichas.idCurso = Cursos.id and Fichas.idCurso=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FichaCursoInfo fichaCursoInfo = new FichaCursoInfo();
                                fichaCursoInfo.id = "" + reader.GetInt32(0);
                                fichaCursoInfo.nombreAlumno = reader.GetString(1);
                                fichaCursoInfo.apellidoAlumno = reader.GetString(2);
                                fichaCursoInfo.nombreCurso = reader.GetString(3);
                                fichaCursoInfo.temario = reader.GetString(4);

                                listFichaCurso.Add(fichaCursoInfo);
                                nombreC = fichaCursoInfo.nombreCurso;
                                temarioC= fichaCursoInfo.temario;
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

    public class FichaCursoInfo
    {
        public String id;
        public String nombreAlumno;
        public String apellidoAlumno;
        public String nombreCurso;
        public String temario;
    }
}

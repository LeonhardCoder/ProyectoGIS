using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ProyectoGIS
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        static string connstring = String.Format("Server={0};Port={1};" +
           "User Id={2};Password={3};Database={4};",
           "localhost", "5432", "postgres",
           "root", "demopf_db");

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int InsertUbicacion(string Longitud, string latitud, string id)
        {
            int retRecord = 0;
            try
            {

                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // sql statement

                string coord = String.Format("ST_SetSRID(ST_MakePoint({0} , {1}), 4326)", Longitud.Replace(',', '.'), latitud.Replace(',', '.'));

                string sql = "INSERT INTO HISTORIAL(IDTRANSPORT, geom, longitud , latitud) " +
                             String.Format("Values ({0}, {1} , '{2}' , '{3}'  )",
                                id,
                                coord,
                                Longitud.Replace(',', '.'),
                                latitud.Replace(',', '.')
                                );

                // Execute command
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                retRecord = command.ExecuteNonQuery();

                // since we only showing the result we don't need connection anymore
                conn.Close();
            }

            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                Console.WriteLine(msg.ToString());
                retRecord = -1;
            }
            return retRecord;
        }

        [WebMethod]
        public int Login(String user, string password)
        {
            return 0;
        }
    }
}

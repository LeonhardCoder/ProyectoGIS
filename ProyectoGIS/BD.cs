using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProyectoGIS
{
    public static class BD
    {
        // PostgeSQL-style connection string
        static string connstring = String.Format("Server={0};Port={1};" +
            "User Id={2};Password={3};Database={4};",
            "localhost", "5432", "postgres",
            "root", "demopf_db");

        #region FUNCIONES https://logicalerrors.wordpress.com/2015/09/19/how-to-insert-into-query-a-postgis-spatial-database-from-c/
        public static void InsertInTable(string nombre, string Direccion, int aforo, string lat, string lon)
        {
            try
            {

                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // sql statement

                string coord = String.Format("ST_SetSRID(ST_MakePoint({0} , {1}), 4326)",  lon.Replace(',','.'), lat.Replace(',','.'));

                string sql = "INSERT INTO SUCURSAL(name_branch, addres_branch,aforo, geom, longitud , latitud) " +
                             String.Format("Values ('{0}', '{1}' , {2} , {3} , '{4}','{5}' )",
                                 nombre,
                                Direccion,
                                aforo,
                                coord,
                                 lon.Replace(',', '.'),
                                lat.Replace(',', '.')
                                );

                // Execute command
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                Int32 rowsaffected = command.ExecuteNonQuery();

                // since we only showing the result we don't need connection anymore
                conn.Close();
            }

            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                Console.WriteLine(msg.ToString());

            }
        }

        public static void UpdateInTable(string nombre, string Direccion, int aforo, string lat, string lon)
        {
            try
            {

                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // sql statement
                
                string sql = "UPDATE  SUCURSAL SET " +
                            String.Format(" name_branch = '{0}', addres_branch = '{1}' , aforo={2} " +
                                    " WHERE longitud = '{3}' AND latitud = '{4}'",
                            nombre,
                            Direccion,
                            aforo,
                            lon,
                            lat
                            );

                // Execute command
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                Int32 rowsaffected = command.ExecuteNonQuery();

                // since we only showing the result we don't need connection anymore
                conn.Close();
            }

            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                Console.WriteLine(msg.ToString());

            }
        }

        public static void DeleteInTable(string lat, string lon)
        {
            try
            {

                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // sql statement

                string sql = "DELETE FROM SUCURSAL WHERE " +
                                         String.Format(" longitud = '{0}' AND latitud = '{1}'",
                                         lon,
                                         lat
                                         );

                // Execute command
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                Int32 rowsaffected = command.ExecuteNonQuery();

                // since we only showing the result we don't need connection anymore
                conn.Close();
            }

            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                Console.WriteLine(msg.ToString());

            }
        }

        public static DataTable GetTableOne(string lat, string lon)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {

                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // sql statement 
                string sql = "SELECT idbranch, name_branch , aforo ,CAST ( geom AS text), longitud, latitud FROM SUCURSAL"
                    + String.Format(" WHERE latitud = '{0}' AND longitud = '{1}'",
                                         lat,
                                         lon
                                         );

                NpgsqlCommand cmd = new NpgsqlCommand(sql);
                cmd.AllResultTypesAreUnknown = true;

                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

                // since we only showing the result we don't need connection anymore
                conn.Close();

                ds.Reset();
                da.Fill(ds); // filling DataSet with result from NpgsqlDataAdapter
                dt = ds.Tables[0]; // since it C# DataSet can handle multiple tables, we will select first
                return dt;
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                Console.WriteLine(msg.ToString());
                return (dt);
            }
        }

        public static DataTable GetTable()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {

                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // sql statement 
                string sql = "SELECT SELECT idbranch, name_branch , aforo ,CAST ( geom AS text), longitud, latitud FROM SUCURSAL";

                NpgsqlCommand cmd = new NpgsqlCommand(sql);
                cmd.AllResultTypesAreUnknown = true;

                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

                // since we only showing the result we don't need connection anymore
                conn.Close();

                ds.Reset();
                da.Fill(ds); // filling DataSet with result from NpgsqlDataAdapter
                dt = ds.Tables[0]; // since it C# DataSet can handle multiple tables, we will select first
                return dt;
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                Console.WriteLine(msg.ToString());
                return (dt);
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Restore_Data
{
    public class Connection
    {
        private static Connection obj = null;
        private static readonly object mylockobject = new object();
        public static Connection getInstance()
        {
            lock (mylockobject)
            {
                if (obj == null)
                {
                    obj = new Connection();
                }

            }
            return obj;
        }
        public static SqlConnection connect()
        {
            
            string s = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security = true;", Serveur.NomServeur, Serveur.NomDatabase);
            SqlConnection con = new SqlConnection(s);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            else
            {
                con.Close();
            }
            return con;

        }
        public static SqlConnection disconnect()
        {
            string s = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security = true;", Serveur.NomServeur, Serveur.NomDatabase);
            SqlConnection con = new SqlConnection(s);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                con.Open();
            }
            return con;

        }

        public SqlConnection con = new SqlConnection(string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security = true;", Serveur.NomServeur, Serveur.NomDatabase));
        public SqlConnection OpenServer()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
        public SqlConnection closeserver()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }

    }
}

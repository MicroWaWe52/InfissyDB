using Infissy.DBEntities;
using System;
using System.Configuration;
using System.Data.OleDb;

namespace Infissy
{
    public class DBcaller
    {
        private static string cs = ConfigurationManager.ConnectionStrings["InfissyCS"].ConnectionString;

        public static bool Register(string usern, string passw, string fName, string email)
        {
            var user = new Utente(usern, passw, fName, email);
            var conn = new OleDbConnection(cs);
            conn.Open();
            int rows=0;
            try
            {
                var registerComm = new OleDbCommand("INSERT INTO Utenti ( usern, passw, fname, email ) " +
                    "VALUES(@u, @p, @f, @e);", conn);
                registerComm.Parameters.Add(new OleDbParameter("@u", user.Usern));
                registerComm.Parameters.Add(new OleDbParameter("@p", user.Passw));
                registerComm.Parameters.Add(new OleDbParameter("@f", user.Fname));
                registerComm.Parameters.Add(new OleDbParameter("@e", user.Email));
                rows = registerComm.ExecuteNonQuery();
                conn.Close();

            }
            catch(Exception)
            {
                conn.Close();
                conn.Dispose();
            }
            return (rows > 0) ? true : false;


        }

        public static Utente Login(string usern, string passw)
        {
            var conn = new OleDbConnection(cs);
            Utente user = null;
            conn.Open();
            try
            {
                var loginComm = new OleDbCommand("SELECT * FROM utenti WHERE usern=@u AND passw=@p;", conn);
                loginComm.Parameters.Add(new OleDbParameter("@u", usern));
                loginComm.Parameters.Add(new OleDbParameter("@p", passw));
                var reader = loginComm.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new Utente(Convert.ToInt32(reader["IDUtente"]), reader["usern"].ToString(), reader["passw"].ToString(), reader["fname"].ToString(), reader["email"].ToString());
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return (user != null) ? user : null;
        }

    }
}
using Infissy.DBEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;

namespace Infissy
{
    public class DBcaller
    {
        private static readonly string cs = ConfigurationManager.ConnectionStrings["InfissyCS"].ConnectionString;

        public static bool Register(string usern, string passw, string fName, string email)
        {
            var user = new Utente(usern, passw, fName, email);
            var conn = new OleDbConnection(cs);
            conn.Open();
            int rows = 0;
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
            catch (Exception)
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
            return user ?? null;
        }

        public static List<Carta> GetCarteFromMazzo(int idMazzo)
        {
            var conn = new OleDbConnection(cs);
            List<Carta> mazzo = new List<Carta>();
            conn.Open();
            try
            {
                var getCarte = new OleDbCommand(
                    "SELECT Carte.* , MazzoCarta.IDMazzoCarta " +
                    "FROM Carte " +
                    "INNER JOIN MazzoCarta " +
                    "ON Carte.IDCard = MazzoCarta.CartaId " +
                   $"WHERE MazzoCarta.MazzoID = {idMazzo};",
                    conn);


                var reader = getCarte.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var IdCa = (int)reader[0];
                        var Tite = reader[1].ToString();
                        var Desc = reader[2].ToString();
                        var ReCy = (int)reader[3];
                        var Effe = reader[4].ToString();
                        var Type = (int)reader[5];
                        var AbVa = (int)reader[6];
                        var Pval = (int)reader[7];
                        var Prog = (bool)reader[8];
                        var Popu = (int)reader[9];
                        var Mate = (int)reader[10];
                        var Mony = (int)reader[11];
                        var IDMC = (int)reader[12];
                        mazzo.Add(new Carta(IdCa, Tite, Desc, ReCy, Effe, Type, AbVa, Pval, Prog, Popu, Mate, Mony, IDMC));
                    }

                }
                return mazzo;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

        }
        public static void AddQueue(int idUtente, string IP, int port)
        {
            var conn = new OleDbConnection(cs);
            conn.Open();
            try
            {
                var queueComm = new OleDbCommand(
                    "INSERT INTO Queue (IDUtente,IP,Port) " +
                   $"VALUES({idUtente},'{IP}',{port});"
                    , conn);
                queueComm.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {
                conn.Close();
                conn.Dispose();
            }


        }
        public static QueueUser[] CheckQueue()
        {
            var conn = new OleDbConnection(cs);
            conn.Open();
            try
            {
                var QUsers = new QueueUser[2];
                var queueComm = new OleDbCommand(
                    "SELECT TOP 2 *" +
                    "From Queue"
                    , conn);
                var reader = queueComm.ExecuteReader();
                try
                {
                    for (int i = 0; i < 2; i++)
                    {
                        reader.Read();
                        var id = (int)reader["IDUtente"];
                        var ip = reader["ip"].ToString();
                        var port = (int)reader["port"];
                        QUsers[i] = new QueueUser(id, ip, port);
                    }
                    conn.Close();
                    return QUsers ?? null;
                }
                catch
                {
                    conn.Close();
                    return null;
                }
                

            }
            catch (Exception)
            {
                conn.Close();
                conn.Dispose();
            }
            conn.Close();
            return null;
        }
        public static void AddPendingQueue(PendingQueue Queue)
        {
            var conn = new OleDbConnection(cs);
            conn.Open();
            try
            {
                var queueComm = new OleDbCommand(
                    "INSERT INTO PendingQueue (Utente1,IP1,Port1,Utente2,IP2,Port2) " +
                   $"VALUES({Queue.Utente1.IDUtente},'{Queue.Utente1.IP}',{Queue.Utente1.Port},{Queue.Utente2.IDUtente},'{Queue.Utente2.IP}',{Queue.Utente2.Port});"
                    , conn);
                queueComm.ExecuteNonQuery();
                var remove = new OleDbCommand(
                    "DELETE FROM Queue " +
                   $"WHERE IDUtente={Queue.Utente1.IDUtente} OR IDUtente={Queue.Utente2.IDUtente}"
                   , conn);
                remove.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {
                conn.Close();
                conn.Dispose();
            }
        }
        public static PendingQueue AskQueue(int idutente)
        {
            var conn = new OleDbConnection(cs);
            conn.Open();
            try
            {
                var queueComm = new OleDbCommand(
                    "SELECT * FROM PendingQueue " +
                    $"WHERE Utente1={idutente} OR Utente2={idutente};"
                    , conn);
                var reader = queueComm.ExecuteReader();

                if (!reader.HasRows) return null;
                reader.Read();
                var utente1 = new QueueUser((int)reader["Utente1"], reader["IP1"].ToString(), (int)reader["Port1"]);
                var utente2 = new QueueUser((int)reader["Utente2"], reader["IP2"].ToString(), (int)reader["Port2"]);
                var queue = new PendingQueue(utente1, utente2);

                conn.Close();
                return queue;
            }
            catch (Exception)
            {
                conn.Close();
                conn.Dispose();
            }
            return null;
        }
        public static void EndQueue(int idutente)
        {
            var conn = new OleDbConnection(cs);
            conn.Open();
            try
            {
                var queueComm = new OleDbCommand(
                    $"DELETE FROM PendingQueue WHERE utente1={idutente} OR utente2={idutente};"
                    , conn);
                queueComm.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
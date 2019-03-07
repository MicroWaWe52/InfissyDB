using System;

namespace Infissy.DBEntities
{
    public class PendingQueue
    {
        public PendingQueue(QueueUser utente1, QueueUser utente2)
        {
            Utente1 = utente1 ?? throw new ArgumentNullException(nameof(utente1));
            Utente2 = utente2 ?? throw new ArgumentNullException(nameof(utente2));
        }

        public QueueUser Utente1 { get; set; }
        public QueueUser Utente2 { get; set; }

        public QueueUser Opponenent(int utente)
        {
            return Utente1.IDUtente == utente ? Utente2 : Utente1;
        }
        public override string ToString()
        {
            return $"{Utente1.ToString()};{Utente2.ToString()}";
        }
    }

    public class QueueUser
    {
        public QueueUser()
        { }

        public QueueUser(int iDUtente, string iP, int port)
        {
            IDUtente = iDUtente;
            IP = iP ?? throw new ArgumentNullException(nameof(iP));
            Port = port;
        }

        public int IDUtente { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }

        public override string ToString()
        {
            return $"{IDUtente};{IP};{Port}";
        }
    }
}
using System;

namespace Infissy.DBEntities
{
    public class Utente
    {
        public Utente(string usern, string passw, string fname, string email)
        {
            Usern = usern ?? throw new ArgumentNullException(nameof(usern));
            Passw = passw ?? throw new ArgumentNullException(nameof(passw));
            Fname = fname ?? throw new ArgumentNullException(nameof(fname));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public Utente(int iD,string uSern, string passw, string fname, string email) : this(uSern, passw, fname, email)
        {
            ID = iD;
        }

        public string Usern { get; set; }
        public string Passw { get; set; }
        public string Fname { get; set; }
        public string Email { get; set; }
        public int ID { get; set; }

        public override string ToString()
        {
            return $"{ID};{Usern};{Passw};{Fname};{Email}";
        }
        public static implicit operator string(Utente u)
        {
            return u.ToString();
        }
    }
}
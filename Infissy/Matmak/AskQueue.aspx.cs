using System;

namespace Infissy.Matmak
{
    public partial class AskQueue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var utente = Convert.ToInt32(Request.QueryString["utente"]);
            var queue = DBcaller.AskQueue(utente);
            int host=(queue.Utente1.IDUtente > queue.Utente2.IDUtente)?queue.Utente1.IDUtente:queue.Utente2.IDUtente;
            var isHost = host == utente;
            Response.Write($"#{queue.Opponenent(utente)};{isHost}#");
        }
    }
}
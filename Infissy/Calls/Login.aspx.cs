using System;
using System.Web.UI;

namespace Infissy
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var usern = Request.QueryString["usern"];
            var passw = Request.QueryString["passw"];
            var utente = DBcaller.Login(usern, passw);
            Response.Write($"#{utente}#");
        }
    }
}
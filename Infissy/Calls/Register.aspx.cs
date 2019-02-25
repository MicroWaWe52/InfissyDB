using System;

namespace Infissy.Calls
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var usern = Request.QueryString["usern"];
            var passw = Request.QueryString["passw"];
            var fname = Request.QueryString["fname"];
            var email = Request.QueryString["email"];
            var result = DBcaller.Register(usern, passw, fname, email);
            Response.Write($"#{result}#");
        }
    }
}
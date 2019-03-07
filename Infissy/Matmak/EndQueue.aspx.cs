using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infissy.Matmak
{
    public partial class EndQueue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var utente = Request.QueryString["utente"].ToInt();
            DBcaller.EndQueue(utente);
        }
    }
}
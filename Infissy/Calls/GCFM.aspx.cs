using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infissy.Calls
{
    public partial class GCFM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var idMazzo =int.Parse( Request.QueryString["idMazzo"]);
            var result = DBcaller.GetCarteFromMazzo(idMazzo);
            Response.Write($"#{result.MazzoToString()}#");
            var x = result.MazzoToString();

        }
    }
}
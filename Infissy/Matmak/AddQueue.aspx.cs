using Infissy.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infissy.Matmak
{
    public partial class AddQueue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var utente = Request.QueryString["utente"].ToInt();
            var ip = Request.QueryString["ip"];
            var port = Request.QueryString["port"].ToInt();
            DBcaller.AddQueue(utente, ip, port);
            var queue = DBcaller.CheckQueue();
            if (queue!=null)
            {
                var pending = new PendingQueue(queue[0], queue[1]);
                MatchMaking(pending);
            }
        }
        public void MatchMaking(PendingQueue queue)
        {
            DBcaller.AddPendingQueue(queue);
        }
    }
}
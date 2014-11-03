/*
 * phosphorus five, copyright 2014 - Mother Earth, Jannah, Gaia
 * phosphorus five is licensed as mitx11, see the enclosed LICENSE file for details
 */

namespace phosphorus.five.applicationpool
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.UI;
    using phosphorus.core;
    using phosphorus.ajax.core;
    using pf = phosphorus.ajax.widgets;

    public partial class Default : AjaxPage
    {
        protected override void OnInit (EventArgs e)
        {
            ApplicationContext context = Loader.Instance.CreateApplicationContext ();
            Node node = new Node (null, "");
            context.Raise ("pf.execute", node);
            base.OnInit (e);
        }
    }
}

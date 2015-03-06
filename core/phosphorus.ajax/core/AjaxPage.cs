/*
 * phosphorus five, copyright 2014 - Mother Earth, Jannah, Gaia
 * phosphorus five is licensed as mitx11, see the enclosed LICENSE file for details
 */

using System;
using System.Collections.Generic;
using System.Web.UI;
using phosphorus.ajax.core.internals;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable MemberCanBeProtected.Global

namespace phosphorus.ajax.core
{
    /// <summary>
    ///     Helper class for implementing core Ajax functionality on your page.
    /// 
    ///     Inherit all your ASP.NET Pages from this class in your solution to allow for them to have Ajax functionality.
    ///     If you do not wish to inherit from this class,
    ///     you can implement the <see cref="phosphorus.ajax.core.IAjaxPage">IAjaxPage</see> interface on your page instead,
    ///     and create an instance of the <see cref="phosphorus.ajax.core.Manager" /> yourself, during the
    ///     initialization of your page.
    /// </summary>
    public class AjaxPage : Page, IAjaxPage
    {
        private readonly List<string> _javaScriptFilesToPush = new List<string> ();
        private readonly List<string> _stylesheetFilesToPush = new List<string> ();
        private PageStatePersister _statePersister;

        /// <summary>
        ///     Maximum number of ViewState entries in Session.
        /// 
        ///     If this is zero, ViewState will not be stored in Session,
        ///     but sent back and forth between client and browser as usual. This might be a security issue for you, in 
        ///     addition to that it increases the size of all your HTTP requests significantly. Unless you know what you are
        ///     doing, you should always store the ViewState on the server, having a positive value, as small as possible,
        ///     of this property.
        /// 
        ///     The higher this number is, the more memory your serer is going to consume. The lower this number is, the
        ///     less number of consecutive open windows the end user can have towards your application at the same time.
        /// 
        ///     If you set this number to "-1", then an infinite amount of state objects per session will be stored on your
        ///     server, which opens up your server to a whole range of difficulties, such as easily draining your server for
        ///     all its memory by simply pressing CTRL+R hundreds of times, etc. For some intranet sites though, this might
        ///     be a useful value, if you know you can trust your users not to sabotage your site.
        /// 
        ///     Useful and safe values for this property, probably ranges from anything from 5 to 20, depending upon the amount
        ///     of memory you have, and what type of site you're creating. If your site is a "single page Ajax application", then
        ///     having "1" as your value will be OK. If your site has no Ajax functionality almost at all, then you can also 
        ///     probably come away with a value of "1" for your page.
        /// 
        ///     If your site however mixes Ajax functionality with multiple URLs, or your users frequently opens up more than
        ///     one window to your site, then you should probably increase this number beyond "1", since otherwise every time
        ///     a user opens up a new window to your site, he will invalidate the state for all previously opened windows, and
        ///     break their Ajax functionality as he does.
        /// 
        ///     I recommend "5" to "10" as a general rule of thumb for this value, unless you know what you're doing.
        /// </summary>
        /// <value>The number of valid viewstate entries for each session.</value>
        public int ViewStateSessionEntries { get; set; }

        /// <summary>
        /// Gets the page state persister.
        /// </summary>
        /// <value>The page state persister.</value>
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                if (ViewStateSessionEntries == 0)
                    return base.PageStatePersister;
                return _statePersister ?? (_statePersister = new StatePersister (this, ViewStateSessionEntries));
            }
        }

        /// <summary>
        ///     Returns the ajax manager for your page.
        /// </summary>
        /// <value>the ajax manager</value>
        public Manager Manager { get; private set; }

        /// <summary>
        ///     Registers JavaScript file for page, that will be included on the client-side.
        /// </summary>
        /// <param name="url">url to JavaScript to register</param>
        public void RegisterJavaScriptFile (string url)
        {
            if (ViewState ["__pf_js_files"] == null)
                ViewState ["__pf_js_files"] = new List<string> ();
            var lst = ViewState ["__pf_js_files"] as List<string>;
            if (!lst.Contains (url)) {
                lst.Add (url);
                _javaScriptFilesToPush.Add (url);
            }
        }
        
        /// <summary>
        ///     Registers stylesheet file for page, that will be included on the client-side.
        /// </summary>
        /// <param name="url">url to stylesheet to register</param>
        protected void RegisterStylesheetFile (string url)
        {
            if (ViewState ["__pf_css_files"] == null)
                ViewState ["__pf_css_files"] = new List<string> ();
            var lst = ViewState ["__pf_css_files"] as List<string>;
            if (!lst.Contains (url)) {
                lst.Add (url);
                _stylesheetFilesToPush.Add (url);
            }
        }

        /*
         * returns the JavaScript file URL's we need to push to client during this request
         */
        List<string> IAjaxPage.JavaScriptFilesToPush
        {
            get { return _javaScriptFilesToPush; }
        }

        /*
         * returns the JavaScript file URL's we need to push to client during this request
         */
        List<string> IAjaxPage.StylesheetFilesToPush
        {
            get { return _stylesheetFilesToPush; }
        }

        protected override void OnPreInit (EventArgs e)
        {
            Manager = new Manager (this);
            base.OnPreInit (e);
        }
    }
}
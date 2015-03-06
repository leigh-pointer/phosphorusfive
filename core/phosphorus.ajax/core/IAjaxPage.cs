/*
 * phosphorus five, copyright 2014 - Mother Earth, Jannah, Gaia
 * phosphorus five is licensed as mitx11, see the enclosed LICENSE file for details
 */

using System.Collections.Generic;

namespace phosphorus.ajax.core
{
    /// <summary>
    ///     Interface for all your pages that uses the phosphorus.ajax library.
    /// 
    ///     Instead of implementing this interface yourself, you can inherit from the 
    ///     <see cref="phosphorus.ajax.core.AjaxPage">AjaxPage</see>, which takes care of everything automatically for you.
    /// </summary>
    public interface IAjaxPage
    {
        /// <summary>
        ///     Returns the manager for your page.
        /// </summary>
        /// <value>The manager.</value>
        Manager Manager { get; }

        /// <summary>
        ///     Returns the list of JavaScript files that was added during this request, and must be pushed back to client somehow.
        /// </summary>
        /// <value>The JavaScript files to push back to client.</value>
        List<string> JavaScriptFilesToPush { get; }

        /// <summary>
        ///     Returns the list of Stylesheet files that was added during this request, and must be pushed back to client somehow.
        /// </summary>
        /// <value>The CSS files to push back to client.</value>
        List<string> StylesheetFilesToPush { get; }

        /// <summary>
        ///     Registers a JavaScript file to be included on to the client-side.
        /// </summary>
        /// <param name="url">URL to JavaScript file.</param>
        void RegisterJavaScriptFile (string url);
    }
}
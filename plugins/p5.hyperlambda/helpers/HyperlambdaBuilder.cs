/*
 * Phosphorus Five, copyright 2014 - 2016, Thomas Hansen, thomas@gaiasoul.com
 * 
 * This file is part of Phosphorus Five.
 *
 * Phosphorus Five is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License version 3, as published by
 * the Free Software Foundation.
 *
 *
 * Phosphorus Five is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Phosphorus Five.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * If you cannot for some reasons use the GPL license, Phosphorus
 * Five is also commercially available under Quid Pro Quo terms. Check 
 * out our website at http://gaiasoul.com for more details.
 */

using System.Text;
using System.Collections.Generic;
using p5.core;

namespace p5.hyperlambda.helpers
{
    /// <summary>
    ///     Class encapsulating internals of creation of Hyperlambda
    /// </summary>
    public class HyperlambdaBuilder
    {
        readonly ApplicationContext _context;
        readonly IEnumerable<Node> _nodes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HyperlambdaBuilder" /> class
        /// </summary>
        /// <param name="context">Application context object</param>
        /// <param name="nodes">Nodes to convert into Hyperlambda</param>
        public HyperlambdaBuilder (ApplicationContext context, IEnumerable<Node> nodes)
        {
            _context = context;
            _nodes = nodes;
        }

        /// <summary>
        ///     Parses and retrieves the Hyperlambda
        /// </summary>
        /// <value>hyperlambda</value>
        public string Hyperlambda
        {
            get
            {
                var builder = new StringBuilder ();
                Nodes2Hyperlisp (builder, _nodes, 0);
                return builder.ToString ().TrimEnd ();
            }
        }

        /*
         * Recursively invoked for each "level" in node hierarchy
         */
        void Nodes2Hyperlisp (StringBuilder builder, IEnumerable<Node> nodes, int level)
        {
            foreach (var idxNode in nodes) {
                var idxLevel = level;
                while (idxLevel-- > 0) {
                    builder.Append ("  ");
                }
                AppendName (builder, idxNode);
                AppendType (builder, idxNode);
                AppendValue (builder, idxNode);
                builder.Append ("\r\n");
                Nodes2Hyperlisp (builder, idxNode.Children, level + 1);
            }
        }

        /*
         * Appends node's name to Hyperlambda StringBuilder output
         */
        void AppendName (StringBuilder builder, Node node)
        {
            if (node.Name.Contains ("\n")) {
                builder.Append (string.Format (@"@""{0}""", node.Name.Replace (@"""", @"""""")));
            } else if ((node.Name == "" && node.Value == null) || 
                       node.Name.Contains (":") || node.Name.Trim () != node.Name) {
                builder.Append (string.Format (@"""{0}""", node.Name.Replace (@"""", @"\""")));
            } else {
                builder.Append (string.Format ("{0}", node.Name));
            }
        }

        /*
         * Appends node's type to Hyperlambda StringBuilder output
         */
        void AppendType (StringBuilder builder, Node node)
        {
            if (node.Value == null)
                return; // no type information here

            var type = node.Value.GetType ();
            if (type == typeof (string))
                return; // String is "default" type information

            builder.Append (
                string.Format (":{0}",
                    _context.RaiseEvent (
                        ".p5.hyperlambda.get-type-name." + node.Value.GetType (),
                        new Node ()).Get<string> (_context)));
        }

        /*
         * Appends node's value to Hyperlambda StringBuilder output
         */
        void AppendValue (StringBuilder builder, Node node)
        {
            if (node.Value == null)
                return; // Nothing to append here.

            var value = (node.Value is byte [] ? Utilities.Base64Encode (_context, node.Value as byte []) : Utilities.Convert2String (_context, node.Value)) ?? "";
            if (value.Contains ("\n") || value.Contains ("\"")) {
                builder.Append (string.Format (@":@""{0}""", value.Replace (@"""", @"""""")));
            } else if (value.Contains (":") || value.Trim () != value) {
                builder.Append (string.Format (@":""{0}""", value.Replace (@"""", @"\""")));
            } else {
                builder.Append (string.Format (":{0}", value));
            }
        }
    }
}

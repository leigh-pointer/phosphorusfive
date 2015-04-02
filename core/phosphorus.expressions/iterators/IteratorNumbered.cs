/*
 * Phosphorus.Five, Copyright 2014 - 2015, Thomas Hansen - thomas@magixilluminate.com
 * Phosphorus.Five is licensed under the terms of the MIT license.
 * See the enclosed LICENSE file for details.
 */

using System.Linq;
using System.Collections.Generic;
using phosphorus.core;

namespace phosphorus.expressions.iterators
{
    public class IteratorNumbered : Iterator
    {
        private readonly int _number;

        public IteratorNumbered (int number)
        {
            _number = number;
        }

        public override IEnumerable<Node> Evaluate
        {
            get { return from idxCurrent in Left.Evaluate where idxCurrent.Children.Count > _number select idxCurrent [_number]; }
        }
    }
}
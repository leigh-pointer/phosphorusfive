/*
 * Phosphorus.Five, Copyright 2014 - 2015, Thomas Hansen - thomas@magixilluminate.com
 * Phosphorus.Five is licensed under the terms of the MIT license.
 * See the enclosed LICENSE file for details.
 */

using System;
using phosphorus.core;

namespace phosphorus.expressions
{
    public static class NodeExtensions
    {
        public static T GetExChildValue<T> (this Node node, string name, ApplicationContext context, T defaultValue = default(T))
        {
            return defaultValue;
        }
    }
}

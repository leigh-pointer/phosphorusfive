/*
 * Phosphorus Five, copyright 2014 - 2015, Thomas Hansen, phosphorusfive@gmail.com
 * Phosphorus Five is licensed under the terms of the MIT license, see the enclosed LICENSE file for details
 */

using System;
using NUnit.Framework;
using p5.core;

namespace p5.unittests.plugins
{
    /// <summary>
    ///     [p5.html.xxx] unit tests
    /// </summary>
    [TestFixture]
    public class Html : TestBase
    {
        public Html ()
            : base ("p5.types", "p5.hyperlisp", "p5.lambda", "p5.html")
        { }

        /// <summary>
        ///     Converts some HTML to p5.lambda, verifying [p5.html.html2lambda] works as it should.
        /// </summary>
        [Test]
        public void Html2Lambda01 ()
        {
            Node node = ExecuteLambda (@"p5.html.html2lambda:@""<html>
    <head>
        <title>foo bar</title>
    </head>
    <body>
        <form>
            <input type=""""text"""" id=""""foobar"""" />
            <textarea id=""""barfoo"""">success textarea</textarea>
            <div id=""""div1"""">
                <p id=""""par1"""">success 1</p>
            </div>
        </form>
        <p>success2</p>
    </body>
</html>""");
            Assert.AreEqual (1, node [0].Count);
            Assert.AreEqual ("doc", node [0] [0].Name);
            Assert.AreEqual (1, node [0] [0].Count);
            Assert.AreEqual ("html", node [0] [0] [0].Name);
            Assert.AreEqual (2, node [0] [0] [0].Count);
            Assert.AreEqual ("head", node [0] [0] [0] [0].Name);
            Assert.AreEqual ("title", node [0] [0] [0] [0] [0].Name);
            Assert.AreEqual ("foo bar", node [0] [0] [0] [0] [0].Value);
            Assert.AreEqual ("body", node [0] [0] [0] [1].Name);
            Assert.AreEqual ("form", node [0] [0] [0] [1] [0].Name);
            Assert.AreEqual ("input", node [0] [0] [0] [1] [0] [0].Name);
            Assert.AreEqual ("@type", node [0] [0] [0] [1] [0] [0] [0].Name);
            Assert.AreEqual ("text", node [0] [0] [0] [1] [0] [0] [0].Value);
            Assert.AreEqual ("@id", node [0] [0] [0] [1] [0] [0] [1].Name);
            Assert.AreEqual ("foobar", node [0] [0] [0] [1] [0] [0] [1].Value);
            Assert.AreEqual ("textarea", node [0] [0] [0] [1] [0] [1].Name);
            Assert.AreEqual ("success textarea", node [0] [0] [0] [1] [0] [1].Value);
            Assert.AreEqual ("@id", node [0] [0] [0] [1] [0] [1] [0].Name);
            Assert.AreEqual ("div", node [0] [0] [0] [1] [0] [2].Name);
            Assert.AreEqual ("@id", node [0] [0] [0] [1] [0] [2] [0].Name);
            Assert.AreEqual ("div1", node [0] [0] [0] [1] [0] [2] [0].Value);
            Assert.AreEqual ("p", node [0] [0] [0] [1] [0] [2] [1].Name);
            Assert.AreEqual ("success 1", node [0] [0] [0] [1] [0] [2] [1].Value);
            Assert.AreEqual ("@id", node [0] [0] [0] [1] [0] [2] [1] [0].Name);
            Assert.AreEqual ("par1", node [0] [0] [0] [1] [0] [2] [1] [0].Value);
            Assert.AreEqual ("p", node [0] [0] [0] [1] [1].Name);
            Assert.AreEqual ("success2", node [0] [0] [0] [1] [1].Value);
        }
        
        /// <summary>
        ///     Converts some HTML to p5.lambda and back again, verifying the results are what we started with.
        /// </summary>
        [Test]
        public void Html2Lambda02 ()
        {
            Node node = ExecuteLambda (@"p5.html.html2lambda:@""<html>
    <head>
        <title>foo bar</title>
    </head>
    <body>
        <form>
            <input type=""""text"""" id=""""foobar""""></input>
            <textarea id=""""barfoo"""">success textarea</textarea>
            <div id=""""div1"""">
                <p id=""""par1"""">success 1</p>
            </div>
        </form>
        <p>success2</p>
    </body>
</html>""
p5.html.lambda2html:x:/-/*/*");
            Assert.AreEqual (node [0].Value, node [1].Value);
        }
        
        /// <summary>
        ///     Converts some HTML to p5.lambda, containing special characters, 
        ///     verifying [p5.html.html2lambda] works as it should.
        /// </summary>
        [Test]
        public void Html2Lambda03 ()
        {
            Node node = ExecuteLambda (@"p5.html.html2lambda:@""<html>
    <head>
        <title>foo &lt;bar</title>
    </head>
    <body>
        <form>
            <input type=""""text"""" id=""""foobar"""" />
            <textarea id=""""barfoo"""">success&gt;textarea</textarea>
            <div id=""""div1"""">
                <p id=""""par1"""">success&amp;1</p>
            </div>
        </form>
        <p>success2</p>
    </body>
</html>""");
            Assert.AreEqual (1, node [0].Count);
            Assert.AreEqual ("doc", node [0] [0].Name);
            Assert.AreEqual (1, node [0] [0].Count);
            Assert.AreEqual ("html", node [0] [0] [0].Name);
            Assert.AreEqual (2, node [0] [0] [0].Count);
            Assert.AreEqual ("head", node [0] [0] [0] [0].Name);
            Assert.AreEqual ("title", node [0] [0] [0] [0] [0].Name);
            Assert.AreEqual ("foo <bar", node [0] [0] [0] [0] [0].Value);
            Assert.AreEqual ("body", node [0] [0] [0] [1].Name);
            Assert.AreEqual ("form", node [0] [0] [0] [1] [0].Name);
            Assert.AreEqual ("input", node [0] [0] [0] [1] [0] [0].Name);
            Assert.AreEqual ("@type", node [0] [0] [0] [1] [0] [0] [0].Name);
            Assert.AreEqual ("text", node [0] [0] [0] [1] [0] [0] [0].Value);
            Assert.AreEqual ("@id", node [0] [0] [0] [1] [0] [0] [1].Name);
            Assert.AreEqual ("foobar", node [0] [0] [0] [1] [0] [0] [1].Value);
            Assert.AreEqual ("textarea", node [0] [0] [0] [1] [0] [1].Name);
            Assert.AreEqual ("success>textarea", node [0] [0] [0] [1] [0] [1].Value);
            Assert.AreEqual ("@id", node [0] [0] [0] [1] [0] [1] [0].Name);
            Assert.AreEqual ("div", node [0] [0] [0] [1] [0] [2].Name);
            Assert.AreEqual ("@id", node [0] [0] [0] [1] [0] [2] [0].Name);
            Assert.AreEqual ("div1", node [0] [0] [0] [1] [0] [2] [0].Value);
            Assert.AreEqual ("p", node [0] [0] [0] [1] [0] [2] [1].Name);
            Assert.AreEqual ("success&1", node [0] [0] [0] [1] [0] [2] [1].Value);
            Assert.AreEqual ("@id", node [0] [0] [0] [1] [0] [2] [1] [0].Name);
            Assert.AreEqual ("par1", node [0] [0] [0] [1] [0] [2] [1] [0].Value);
            Assert.AreEqual ("p", node [0] [0] [0] [1] [1].Name);
            Assert.AreEqual ("success2", node [0] [0] [0] [1] [1].Value);
        }
        
        /// <summary>
        ///     Converts some HTML to p5.lambda and back again, containing special characters,
        ///     verifying the results are what we started with.
        /// </summary>
        [Test]
        public void Html2Lambda04 ()
        {
            Node node = ExecuteLambda (@"p5.html.html2lambda:@""<html>
    <head>
        <title>foo&lt;&amp;&gt;bar</title>
    </head>
    <body>
        <form>
            <input type=""""text"""" id=""""foobar""""></input>
            <textarea id=""""barfoo"""">success textarea</textarea>
            <div id=""""div1"""">
                <p id=""""par1"""">success 1</p>
            </div>
        </form>
        <p>success2</p>
    </body>
</html>""
p5.html.lambda2html:x:/-/*/*");
            Assert.AreEqual (node [0].Value, node [1].Value);
        }
    }
}

/*
 * phosphorus five, copyright 2014 - Mother Earth, Jannah, Gaia
 * phosphorus five is licensed as mit, see the enclosed LICENSE file for details
 */

using System;
using NUnit.Framework;
using phosphorus.core;

namespace phosphorus.unittests
{
    /// <summary>
    /// unit tests for testing the [append] lambda keyword
    /// </summary>
    [TestFixture]
    public class Branching : TestBase
    {
        public Branching ()
            : base ("phosphorus.lambda", "phosphorus.types", "phosphorus.hyperlisp")
        { }

        /// <summary>
        /// verifies [if] works when given constant
        /// </summary>
        [Test]
        public void If01 ()
        {
            Node result = ExecuteLambda (@"if:foo
  set:@/../?value
    source:success");
            Assert.AreEqual ("success", result.Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing two constant strings that are equal
        /// </summary>
        [Test]
        public void If02 ()
        {
            Node result = ExecuteLambda (@"if:foo
  =:foo
  lambda
    set:@/../?value
      source:success");
            Assert.AreEqual ("success", result.Value);
        }

        /// <summary>
        /// verifies [if] works when comparing two constant strings that are not equal
        /// </summary>
        [Test]
        public void If03 ()
        {
            Node result = ExecuteLambda (@"_result:success
if:foo
  =:bar
  lambda
    set:@/../*/_result/?value
      source:error");
            Assert.AreEqual ("success", result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing one constant to one expression,
        /// and result should yield true
        /// </summary>
        [Test]
        public void If04 ()
        {
            Node result = ExecuteLambda (@"_result:foo
if:@/../*/_result/?value
  =:foo
  lambda
    set:@/../*/_result/?value
      source:success");
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing one constant to one expression,
        /// and result should yield false
        /// </summary>
        [Test]
        public void If05 ()
        {
            Node result = ExecuteLambda (@"_result:success
if:@/../*/_result/?value
  =:foo
  lambda
    set:@/../*/_result/?value
      source:error");
            Assert.AreEqual ("success", result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing one constant to one expression,
        /// and result should yield true, and expression is on right hand side 
        /// of comparison
        /// </summary>
        [Test]
        public void If06 ()
        {
            Node result = ExecuteLambda (@"_result:error
if:error
  =:@/../*/_result/?value
  lambda
    set:@/../*/_result/?value
      source:success");
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing one constant to one expression,
        /// and result should yield false, and expression is on right hand side 
        /// of comparison
        /// </summary>
        [Test]
        public void If07 ()
        {
            Node result = ExecuteLambda (@"_result:success
if:foo
  =:@/../*/_result/?value
  lambda
    set:@/../*/_result/?value
      source:error");
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing two expressions,
        /// and result should yield true
        /// </summary>
        [Test]
        public void If08 ()
        {
            Node result = ExecuteLambda (@"_result1:foo
_result2:foo
if:@/../*/_result1/?value
  =:@/../*/_result2/?value
  lambda
    set:@/../*/_result1/?value
      source:success");
            Assert.AreEqual ("success", result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing two expressions,
        /// and result should yield false
        /// </summary>
        [Test]
        public void If09 ()
        {
            Node result = ExecuteLambda (@"_result1:success
_result2:foo
if:@/../*/_result1/?value
  =:@/../*/_result2/?value
  lambda
    set:@/../*/_result1/?value
      source:error");
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing two expressions with different types returned,
        /// and result should yield false
        /// </summary>
        [Test]
        public void If10 ()
        {
            Node result = ExecuteLambda (@"_result1:int:5
_result2:5
if:@/../*/_result1/?value
  =:@/../*/_result2/?value
  lambda
    set:@/../*/_result1/?value
      source:error");
            Assert.AreEqual (5, result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing two expressions returning integer values,
        /// and result should yield true
        /// </summary>
        [Test]
        public void If11 ()
        {
            Node result = ExecuteLambda (@"_result1:int:5
_result2:int:5
if:@/../*/_result1/?value
  =:@/../*/_result2/?value
  lambda
    set:@/../*/_result1/?value
      source:success");
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing two expressions returning integer values,
        /// and result should yield false
        /// </summary>
        [Test]
        public void If12 ()
        {
            Node result = ExecuteLambda (@"_result1:int:5
_result2:int:6
if:@/../*/_result1/?value
  =:@/../*/_result2/?value
  lambda
    set:@/../*/_result1/?value
      source:error");
            Assert.AreEqual (5, result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing two expressions,
        /// returning multiple results, and result should yield true
        /// </summary>
        [Test]
        public void If13 ()
        {
            Node result = ExecuteLambda (@"_result1
  foo1:bar1
  foo2:bar2
_result2
  foo1:bar1
  foo2:bar2
if:@/../*/_result1/*/?value
  =:@/../*/_result2/*/?value
  lambda
    set:@/../*/_result1/?value
      source:success");
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing two expressions,
        /// returning multiple results, and result should yield false
        /// </summary>
        [Test]
        public void If14 ()
        {
            Node result = ExecuteLambda (@"_result1:success
  foo1:bar1
  foo2:bar2
_result2
  foo1:bar1
  foo2:ERROR
if:@/../*/_result1/*/?value
  =:@/../*/_result2/*/?value
  lambda
    set:@/../*/_result1/?value
      source:error");
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing two expressions,
        /// returning multiple results, with multiple different types,
        /// and result should yield true
        /// </summary>
        [Test]
        public void If15 ()
        {
            Node result = ExecuteLambda (@"_result1
  foo1:bar1
  foo2:int:5
_result2
  foo1:bar1
  foo2:int:5
if:@/../*/_result1/*/?value
  =:@/../*/_result2/*/?value
  lambda
    set:@/../*/_result1/?value
      source:success");
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing two expressions,
        /// returning multiple results, with multiple different types,
        /// and result should yield false
        /// </summary>
        [Test]
        public void If16 ()
        {
            Node result = ExecuteLambda (@"_result1:success
  foo1:bar1
  foo2:int:5
_result2
  foo1:bar1
  foo2:int:6
if:@/../*/_result1/*/?value
  =:@/../*/_result2/*/?value
  lambda
    set:@/../*/_result1/?value
      source:error");
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing two expressions,
        /// returning multiple results, where node graph is different
        /// in expression results
        /// </summary>
        [Test]
        public void If17 ()
        {
            Node result = ExecuteLambda (@"_result1:success
  foo1:bar1
  foo2:int:5
_result2
  foo1:bar1
  foo2:int:5
    error
if:@/../*/_result1/*/?node
  =:@/../*/_result2/*/?node
  lambda
    set:@/../*/_result1/?value
      source:error");
            Assert.AreEqual ("success", result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing two expressions,
        /// returning multiple results, where node graph is different
        /// in expression results, yet results should be similar anyway
        /// </summary>
        [Test]
        public void If18 ()
        {
            Node result = ExecuteLambda (@"_result1:error
  foo1:bar1
  foo2:int:5
_result2
  foo1:bar1
  foo2:int:5
    error
if:@/../*/_result1/*/?value
  =:@/../*/_result2/*/?value
  lambda
    set:@/../*/_result1/?value
      source:success");

            // note, this comparison should yield true, since we're comparing the node's 'values',
            // which should be similar, since our [error] node above in [_result2] has no value
            Assert.AreEqual ("success", result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing two expressions,
        /// returning multiple results, where node graph is different
        /// in expression results, yet results should be similar anyway
        /// </summary>
        [Test]
        public void If19 ()
        {
            Node result = ExecuteLambda (@"_result1:error
  foo1:bar1
  foo2:5
_result2
  foo1:bar1
  empty:
  foo2:int:5
    error:
if:@/../*/_result1/*/?value
  =:@/../*/_result2/*/?value
  lambda
    set:@/../*/_result1/?value
      source:success");

            // note, this comparison should yield true, since we're comparing the node's 'values',
            // which should be similar, since our [error] node above in [_result2] has no value
            Assert.AreEqual ("success", result [0].Value);
        }

        /// <summary>
        /// verifies [if] works when comparing one count expression to a constant
        /// </summary>
        [Test]
        public void If20 ()
        {
            Node result = ExecuteLambda (@"_result
  foo1:bar1
  foo2:int:5
if:@/../*/_result/*/?count
  =:int:2
  lambda
    set:@/../*/_result/?value
      source:success");
            Assert.AreEqual ("success", result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing an expression returning a node,
        /// to a constant node
        /// </summary>
        [Test]
        public void If21 ()
        {
            Node result = ExecuteLambda (@"_result
  foo1:bar1
  foo2:int:5
if:@/../*/_result/?node
  =:node:@""_result
  foo1:bar1
  foo2:int:5""
  lambda
    set:@/../*/_result/?value
      source:success");
            Assert.AreEqual ("success", result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing an expression with 'null'
        /// </summary>
        [Test]
        public void If22 ()
        {
            Node result = ExecuteLambda (@"_result:success
if:@/../*/_result/?value
  =
  lambda
    set:@/../*/_result/?value
      source:error");
            Assert.AreEqual ("success", result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing a constant with 'null'
        /// </summary>
        [Test]
        public void If23 ()
        {
            Node result = ExecuteLambda (@"_result:success
if:foo
  =
  lambda
    set:@/../*/_result/?value
      source:error");
            Assert.AreEqual ("success", result [0].Value);
        }
        
        /// <summary>
        /// verifies [if] works when comparing two constants with formatting expressions
        /// </summary>
        [Test]
        public void If24 ()
        {
            Node result = ExecuteLambda (@"_result:error
if:{0}o
  :fo
  =:{0}o
    :fo
  lambda
    set:@/../*/_result/?value
      source:success");
            Assert.AreEqual ("success", result [0].Value);
        }
    }
}
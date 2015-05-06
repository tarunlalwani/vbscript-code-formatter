/*
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TARLABS.VBScriptFormatter
{
    [TestClass]
    public class TestFormatter
    {
        private TokenReader _vbScript;
        
        [TestInitialize]
        public void LoadClass()
        {
            _vbScript = new TokenReader {IndentationString = "\t", StartIndentString = ""};
        }

        [TestMethod]
        public void TestNotOperatorWithNumber()
        {
            _vbScript.FormatCode("x=not          - 2");
            Assert.AreEqual(_vbScript.FormattedCode, @"x = Not -2");
        }

        [TestMethod]
        public void TestNotOperatorWithFunc()
        {
            _vbScript.FormatCode("x=not          func   ( )  ");
            Assert.AreEqual(_vbScript.FormattedCode, @"x = Not func()");
        }

        [TestMethod]
        public void TestMultipleOperators()
        {
            _vbScript.FormatCode("x=-2");
            Assert.AreEqual(_vbScript.FormattedCode, @"x = -2");

            _vbScript.FormatCode("x=-2+2");
            Assert.AreEqual(_vbScript.FormattedCode, @"x = -2 + 2");

            _vbScript.FormatCode("x=3-2");
            Assert.AreEqual(_vbScript.FormattedCode, @"x = 3 - 2");

            _vbScript.FormatCode("x=-func()");
            Assert.AreEqual(_vbScript.FormattedCode, @"x = -func()");

            _vbScript.FormatCode("x=test-func()");
            Assert.AreEqual(_vbScript.FormattedCode, @"x = test - func()");

            _vbScript.FormatCode("x=test         mod    func()");
            Assert.AreEqual(_vbScript.FormattedCode, @"x = test Mod func()");
        }
    }

}
*/

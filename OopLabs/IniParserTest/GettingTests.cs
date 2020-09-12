using Microsoft.VisualStudio.TestTools.UnitTesting;
using IniParser;

namespace IniParserTest
{
    [TestClass]
    public class GettingTests
    {
        [TestMethod]
        public void TestIntGetting()
        {
            var parser = new IniParser.IniParser("lab1.data.ini");
            var data = parser.Parse();

            var x = data.TryGetInt("COMMON", "StatisterTimeMs");

            Assert.AreEqual(5000, x);
        }

        [TestMethod]
        public void TestDoubleGetting()
        {
            var parser = new IniParser.IniParser("lab1.data.ini");
            var data = parser.Parse();

            var x = data.TryGetDouble("ADC_DEV", "BufferLenSecons");

            Assert.AreEqual(0.65, x);
        }

        [TestMethod]
        public void TestStringGetting()
        {
            var parser = new IniParser.IniParser("lab1.data.ini");
            var data = parser.Parse();

            var x = data.TryGetString("COMMON", "DiskCachePath");

            Assert.AreEqual("/sata/panorama", x);
        }

        [TestMethod]
        public void TestGenericGetting()
        {
            var parser = new IniParser.IniParser("lab1.data.ini");
            var data = parser.Parse();

            var x = data.TryGet<int>("NCMD", "EnableChannelControl");

            Assert.AreEqual(1, x);
        }
    }
}

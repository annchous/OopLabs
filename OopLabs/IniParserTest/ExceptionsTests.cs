using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IniParserTest
{
    [TestClass]
    public class ExceptionsTests
    {
        [TestMethod]
        public void TestWrongSection()
        {
            var parser = new IniParser.IniParser("lab1.data.ini");
            var data = parser.Parse();

            var ex = Assert.ThrowsException<IniParser.IniParserException.SectionNotFound>(() => data.TryGetInt("COMMO", "StatisterTimeMs"));
            Assert.AreEqual("Section COMMO was not found!", ex.Message);
        }

        [TestMethod]
        public void TestWrongPropertyKey()
        {
            var parser = new IniParser.IniParser("lab1.data.ini");
            var data = parser.Parse();

            var ex = Assert.ThrowsException<IniParser.IniParserException.PropertyKeyNotFound>(() => data.TryGetInt("COMMON", "StatisterTime"));
            Assert.AreEqual("Property StatisterTime was not found!", ex.Message);
        }

        [TestMethod]
        public void TestWrongParameterValueType()
        {
            var parser = new IniParser.IniParser("lab1.data.ini");
            var data = parser.Parse();

            var ex = Assert.ThrowsException<IniParser.IniParserException.BadValueCast>(() => data.TryGet<int>("COMMON", "DiskCachePath"));
            Assert.AreEqual("Type of /sata/panorama can not be converted to Int32!", ex.Message);
        }

        [TestMethod]
        public void TestFileNotFound()
        {
            var parser = new IniParser.IniParser("lab1.in");

            var ex = Assert.ThrowsException<IniParser.IniParserException.FileNotFound>(() => parser.ReadData());
            Assert.AreEqual("This file was not found!", ex.Message);
        }

        [TestMethod]
        public void WrongFileFormat()
        {
            var parser = new IniParser.IniParser("test.in");

            var ex = Assert.ThrowsException<IniParser.IniParserException.WrongFileFormat>(() => parser.ReadData());
            Assert.AreEqual("Wrong file format! It should be '.ini'!", ex.Message);
        }
    }
}

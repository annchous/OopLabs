﻿using System;
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

            var ex = Assert.ThrowsException<IniParser.IniParserException.WrongParameterValueType>(() => data.TryGet<double>("COMMON", "StatisterTimeMs"));
            Assert.AreEqual("Type of StatisterTimeMs in section COMMON is not Double!", ex.Message);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.Tests
{
    [TestClass()]
    public class PictureConverterTests
    {
        [TestMethod()]
        public void ConvertTest()
        {
            var converter = new PictureConverter();
            var inputs = converter.Convert(@"C:\Users\dima-\source\repos\NeuralNetwork\NeuralNetworkTests\Images\Parazited.png");
            converter.Save("d:\\image.png", inputs);
        }
    }
}
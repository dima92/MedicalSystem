using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetworks.Tests
{
    [TestClass()]
    public class NeuralNetworkTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {
            var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
            var inputs = new double[,]
            {
                // Результат - Пациент болен - 1
                //             Пациент Здоров - 0

                // Неправильная температура T
                // Хороший возраст A
                // Курит S
                // Правильно питается F
                //T  A  S  F
                { 0, 0, 0, 0 },
                { 0, 0, 0, 1 },
                { 0, 0, 1, 0 },
                { 0, 0, 1, 1 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 1 },
                { 0, 1, 1, 0 },
                { 0, 1, 1, 1 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 1 },
                { 1, 0, 1, 0 },
                { 1, 0, 1, 1 },
                { 1, 1, 0, 0 },
                { 1, 1, 0, 1 },
                { 1, 1, 1, 0 },
                { 1, 1, 1, 1 }
            };

            var topology = new Topology(4, 1, 0.1, 2);
            var neuralNetwork = new NeuralNetwork(topology);
            var difference = neuralNetwork.Learn(outputs, inputs, 10000);
            var results = new List<double>();
            for (int i = 0; i < outputs.Length; i++)
            {
                var row = NeuralNetwork.GetRow(inputs, i);
                var res = neuralNetwork.Predict(row).Output;
                results.Add(res);
            }
            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void RecognizeImages()
        {
            var size = 1000;
            var parazitizedPath = @"C:\Users\dima-\OneDrive\Desktop\cell_images\Parasitized\";
            var unparazitizedPath = @"C:\Users\dima-\OneDrive\Desktop\cell_images\Uninfected\";

            var converter = new PictureConverter();
            var testParazitizedImageInput = converter.Convert(@"C:\Users\dima-\source\repos\NeuralNetwork\NeuralNetworkTests\Images\Parazited.png");
            var testUnParazitizedImageInput = converter.Convert(@"C:\Users\dima-\source\repos\NeuralNetwork\NeuralNetworkTests\Images\Unparazited.png");

            var topology = new Topology(testParazitizedImageInput.Length, 1, 0.1, testParazitizedImageInput.Length / 2);
            var neuralNetwork = new NeuralNetwork(topology);

            double[,] parazitizedInputs = GetData(parazitizedPath, converter, testParazitizedImageInput, size);
            neuralNetwork.Learn(new double[] { 1 }, parazitizedInputs, 1);

            double[,] unparazitizedInputs = GetData(unparazitizedPath, converter, testUnParazitizedImageInput, size);
            neuralNetwork.Learn(new double[] { 0 }, unparazitizedInputs, 1);

            var par = neuralNetwork.Predict(testParazitizedImageInput.Select(t => (double)t).ToArray());
            var unpar = neuralNetwork.Predict(testUnParazitizedImageInput.Select(t => (double)t).ToArray());

            Assert.AreEqual(1, Math.Round(par.Output, 2));
            Assert.AreEqual(0, Math.Round(unpar.Output, 2));
        }

        private static double[,] GetData(string parazitizedPath, PictureConverter converter, double[] testImageInput, int size)
        {
            var images = Directory.GetFiles(parazitizedPath);
            var result = new double[size, testImageInput.Length];
            for (int i = 0; i < size; i++)
            {
                var image = converter.Convert(images[i]);
                for (int j = 0; j < image.Length; j++)
                {
                    result[i, j] = image[j];
                }
            }
            return result;
        }
    }
}
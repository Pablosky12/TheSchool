using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Fibonacci
{
    [TestClass]
    public class FibonacciTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GetFibonacciForZero()
        {
            /*
             * Given Zero as input it should return 0.
             */
            int input = 0;
            var result = Fibonacci.FibonacciHelper.GetFibonacci(input);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void GetFibonacciLessThan2ButGreaterThanZero()
        {
            /*
             * Given Zero as input it should return 0.
             */
            int input = 1;
            var result = Fibonacci.FibonacciHelper.GetFibonacci(input);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests.xml", "serie", DataAccessMethod.Sequential)]
        public void GetFibonacciSeriesDataDriven()
        {
            /*
             * Given Zero as input it should return 0.
             */
            int input = Convert.ToInt32(TestContext.DataRow["input"]);
            int output = Convert.ToInt32(TestContext.DataRow["output"]);

            var result = FibonacciHelper.GetFibonacci(input);

            Assert.AreEqual(result, output);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    public static class FibonacciHelper
    {
        /// <summary>
        /// This method will return the fibonacci number for an integer.
        /// </summary>
        /// <param name="n">Input number</param>
        /// <returns>Fibonacci number based on 'n'.</returns>
        public static int GetFibonacci(int n)
        {
            ////////////////////////////////////////////
            //Your Fibonaci's code here..
            int num1 = 1;
            int num2 = 0;
            int exit = 0;
            if (n != 0)
            {
                for (int i = 1; i <= n; i++)
                {
                    exit = num1;
                    num1 = num1 + num2;
                    num2 = exit;
                }
            }
            return exit;

            ////////////////////////////////////////////
        }
    }
}

/*
BSD 3-Clause License

Copyright (c) 2019-2024, Alastair Lundy
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Diagnostics;
using System.Globalization;

using SecureRNGKit.Exceptions;

using SecureRNGKit.SyntacticSugar.Common;

// ReSharper disable once CheckNamespace
namespace SecureRNGKit{
    /// <summary>
    /// A class that provides additional functionality using SecureRNGWrapper.
    /// Notice: The namespace in this class will be updated to AluminiumTech.DevKit.SecureRNGKit.SyntacticSugar in Version 3.0 of SecureRNGKit. 
    /// </summary>
    public class SecureRandom
    {
        private readonly SecureRNGWrapper _secureRngWrapper;

        private readonly long _internalTimeLimit = 500;
        
        public SecureRandom()
        {
            _secureRngWrapper = new SecureRNGWrapper();
        }

        #region Random Int Generation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maximum"></param>
        /// <param name="allowNegativeNumbers"></param>
        /// <returns></returns>
        protected int NextIntWorker(int maximum, bool allowNegativeNumbers)
        {
            try
            {
                if (maximum >= int.MaxValue || maximum < int.MinValue)
                {
                    maximum = int.MaxValue;
                }

                //
                return ((int)NextDouble(Convert.ToDouble(int.MinValue), Convert.ToDouble(maximum), allowNegativeNumbers));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Generates a new Random 32 Bit Integer
        /// </summary>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public int NextInt(int maximum)
        {
            //true
            return NextInt(int.MinValue, maximum);
        }

        /// <summary>
        /// Generates a new Random 32 Bit Integer
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="allowNegativeNumbers"></param>
        /// <returns></returns>
        public int NextInt(int minimum = int.MinValue, int maximum = int.MaxValue, bool allowNegativeNumbers = true)
        {
            try{
                if (maximum < minimum){
                    throw new ArgumentException("Maximum was lower than the argument specified for minimum");
                }
                else if (minimum > maximum)
                {
                    throw new ArgumentException("Minimum was higher than the argument specified for maximum.");
                }
                else if (minimum < int.MinValue || minimum > int.MaxValue || maximum > int.MaxValue || maximum < int.MinValue)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (minimum.Equals(maximum))
                {
                    throw new ArgumentException("Minimum and Maximum can't be set to the same argument value.");
                }

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                while (stopwatch.ElapsedMilliseconds < _internalTimeLimit){
                    var l = NextIntWorker(maximum, allowNegativeNumbers);

                    if (l >= minimum && l <= maximum){
                        return l;
                    }
                }
               
                throw new TimeLimitException();
            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Generates an int array of the specified size
        /// </summary>
        /// <param name="intArraySize">The desired size of the array.</param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public int[] NextIntArray(int intArraySize, int minimum = int.MinValue, int maximum = int.MaxValue, bool AllowNegativeNumbers = true)
        {
            int[] array = new int[intArraySize];

            for (int i = 0; i < intArraySize; i++){
                array[i] = NextInt(minimum, maximum, AllowNegativeNumbers);
            }
            return array;
        }
        #endregion

        #region Double Random Generation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maximum"></param>
        /// <param name="allowNegativeNumbers"></param>
        /// <returns></returns>ss
        protected double NextDoubleWorker(double maximum, bool allowNegativeNumbers){
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                double worker = Convert.ToDouble(_secureRngWrapper.NextByte() / 255.0) * maximum;
                
                if (allowNegativeNumbers)
                {
                    var secureCoinToss = new SecureRandomCoinToss().CoinTossToInt();
                    
                    if (secureCoinToss == 0)
                    {
                        //Leave it as is if it's heads.
                    }
                    else if (secureCoinToss == 1)
                    {
                        //Assume the number is negative because we only normally generate positives.
                        worker *= (-1);
                    }
                }
                
                stopwatch.Stop();
#if DEBUG
                Console.WriteLine("Took " + stopwatch.ElapsedMilliseconds + " ms to complete double worker.");
#endif
                return worker;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Generates a random 64 Bit Double precision floating point number between 0.0 and 1.0. 
        /// This can generate negative numbers.
        /// </summary>
        /// <returns></returns>
        public double NextDouble(bool allowNegativeNumbers = false)
        {
            return NextDouble(0.0, 1.0, allowNegativeNumbers);
        }

        /// <summary>
        /// Generates a new Random 64 Bit Double precision floating point number
        /// </summary>
        /// <returns></returns>
        public double NextDouble(double minimum, double maximum, bool allowNegativeNumbers = true)
        {
            try{
                if (maximum < minimum){
                    throw new ArgumentException("Maximum was lower than the argument specified for minimum");
                }
                else if (minimum > maximum)
                {
                    throw new ArgumentException("Minimum was higher than the argument specified for maximum.");
                }
                else if (minimum < double.MinValue || minimum > double.MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (maximum > double.MaxValue || maximum < double.MinValue)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (minimum.Equals(maximum))
                {
                    throw new ArgumentException();
                }

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                while (stopwatch.ElapsedMilliseconds < _internalTimeLimit)
                {
                    var d = NextDoubleWorker(maximum, allowNegativeNumbers);

                    if (d >= minimum && d <= maximum){
                        return d;
                    }
                }

                stopwatch.Stop();

                throw new TimeLimitException();
            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Generates a Double array of the specified size
        /// </summary>
        /// <param name="arraySize"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public double[] NextDoubleArray(int arraySize, double minimum = double.MinValue, double maximum = double.MaxValue, bool AllowNegativeNumbers = true)
        {
            double[] array = new double[arraySize];

            for (int i = 0; i < arraySize; i++){
                //AllowNegativeNumbers
                array[i] = NextDouble(minimum, maximum, AllowNegativeNumbers);
            }
            return array;
        }
        #endregion

        #region Long Random Generation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maximum"></param>
        /// <param name="allowNegativeNumbers"></param>
        /// <returns></returns>
        protected long NextLongWorker(long maximum, bool allowNegativeNumbers = true){
            try
            {
                return (long)NextDoubleWorker((double)maximum, allowNegativeNumbers);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        public long NextLong(long maximum)
        {
            //true
            return NextLong(long.MinValue, maximum);
        }

        /// <summary>
        /// Generates a new Random 64 Bit Integer
        /// </summary>
        /// <returns></returns>
        public long NextLong(long minimum = long.MinValue, long maximum = long.MaxValue, bool allowNegativeNumbers = true)
        {
            try{
                if (maximum < minimum){
                    throw new ArgumentException("Maximum was lower than the argument specified for minimum");
                }
                else if (minimum > maximum)
                {
                    throw new ArgumentException("Minimum was higher than the argument specified for maximum.");
                }
                else if (minimum < long.MinValue || minimum > long.MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (maximum > long.MaxValue || maximum < long.MinValue)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (minimum.Equals(maximum))
                {
                    throw new ArgumentException();
                }

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                while (stopwatch.ElapsedMilliseconds < _internalTimeLimit)
                {
                    var l = NextLongWorker(maximum, allowNegativeNumbers);
           //         Console.WriteLine("Generated Long number is " + l);

                    if (l >= minimum && l <= maximum){
             //           Console.WriteLine("Completed Long number generation within " + stopwatch.ElapsedTicks + " ticks");
                        return l;
                    }
                    else
                    {
                   //     Console.WriteLine("Generated Long Number is lower than " + minimum + " or higher than maximum " + maximum);
                    }
                }
                
                stopwatch.Stop();

                throw new TimeLimitException();
            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Generates a new Random 64 Bit Integer precision floating point number
        /// </summary>
        /// <param name="arraySize"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public long[] NextLongArray(int arraySize, long minimum = long.MinValue, long maximum = long.MaxValue, bool AllowNegativeNumbers = true)    {
            long[] array = new long[arraySize];

            for (int i = 0; i < arraySize; i++){
                //
                array[i] = NextLong(minimum, maximum, AllowNegativeNumbers);
            }
            return array;
        }
        #endregion
    }
}
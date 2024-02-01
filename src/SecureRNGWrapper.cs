/*
BSD 3-Clause License

Copyright (C) 2019-2024  Alastair Lundy
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
using System.Security.Cryptography;

namespace SecureRNGKit {
    
    /// <summary>
    /// A wrapper class for the RNGCryptoServiceProvider class
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SecureRNGWrapper
    {
        protected System.Security.Cryptography.RandomNumberGenerator _randomNumberGenerator;
        
        public SecureRNGWrapper()
        {
            _randomNumberGenerator = RandomNumberGenerator.Create();
        }

        /// <summary>
        ///  Generate a byte array of the specified size  and fills it with random numbers.
        /// </summary>
        /// <param name="byteArraySize"></param>
        /// <param name="canGenerateNumberZero">Whether or not the number 0 should be allowed to be generated.</param>
        /// <returns></returns>
        public byte[] NextByteArray(int byteArraySize, bool canGenerateNumberZero = true)
        {
            if (byteArraySize <= 0) throw new ArgumentOutOfRangeException(nameof(byteArraySize));
            
            byte[] array = new byte[byteArraySize];
            return (byte[])NextByteArray(array, canGenerateNumberZero);
        }

        /// <summary>
        /// Fills a specified byte array with random numbers.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="canGenerateNumberZero"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte[] NextByteArray(byte[] byteArray, bool canGenerateNumberZero = true){
            try
            {
                if (canGenerateNumberZero)
                {
                    _randomNumberGenerator.GetBytes(byteArray);
                }
                else
                {
                    _randomNumberGenerator.GetNonZeroBytes(byteArray);
                }

                return byteArray;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Generates a new Random Byte between the minimum and maximum numbers you specify above 0 and less than 255.
        /// If you do not specify a maximum value, the number 255 will be picked.
        /// If you do not specify a minimum value, the number 0 will be picked.
        /// </summary>
        /// <returns></returns>
        public byte NextByte(byte minimum = byte.MinValue, byte maximum = byte.MaxValue, bool canGenerateNumberZero = true)
        {
            try{
                if (maximum < minimum){
                    throw new ArgumentException("Maximum was lower than the argument specified for minimum");
                }
                else if (minimum > maximum)
                {
                    throw new ArgumentException("Minimum was higher than the argument specified for maximum.");
                }
                else if(minimum < byte.MinValue || maximum > byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (minimum.Equals(maximum))
                {
                    throw new ArgumentException();
                }

                byte b = NextByteArray(1, canGenerateNumberZero)[0];

                if (b >= minimum && b <= maximum && maximum <= (byte)255) return b;

                while (b < minimum || b > maximum){
                     b = NextByteArray(1, canGenerateNumberZero)[0];

                    if (b >= minimum && b <= maximum && maximum <= (byte)255){
                        return b;
                    }
                }

                return b;
            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}
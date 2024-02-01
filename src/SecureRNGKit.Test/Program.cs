/*
SecureRNGKit
Copyright (C) 2019-2024  Alastair Lundy
   
This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
USA
*/

using System;
using SecureRNGKit;

namespace SecureRNGKit.Test{
    class Program{
        static void Main(string[] args){
            SecureRandom rng = new SecureRandom();

            try
            {
                for (int i = 0; i < (1.0 * 1000 * 1000); i++)
                {
                    Console.WriteLine("Starting again...");
                    Console.WriteLine(i + " 1)" + rng.NextInt());
                    Console.WriteLine(i + " 2)" + rng.NextInt());
                    Console.WriteLine(i + " 3)" + rng.NextInt(0, 1000));
                    Console.WriteLine(i + " 4)" + rng.NextInt(0, 10000));
                    Console.WriteLine(i + " 5)" + rng.NextInt(0, 20000));

                    Console.WriteLine(i + " 6)" + rng.NextInt(1000, (1 * 1000 * 1000)));

                    Console.WriteLine(i + " 7)" + rng.NextInt(0, 100000));
                    Console.WriteLine(i + " 8)" + rng.NextLong((100 * 1000), (200 * 1000)));
                    Console.WriteLine(i + " 9)" + rng.NextLong((100 * 1000), (500 * 1000)));
                    Console.WriteLine(i + " 10)" + rng.NextLong((100 * 1000), (1 * 1000 * 1000)));
                    Console.WriteLine(i + " 11)" + rng.NextLong((1 * 1000 * 1000), (5 * 1000 * 1000)));
                    Console.WriteLine(i + " 12)" + rng.NextLong((1 * 1000 * 1000), (10 * 1000 * 1000)));
                    Console.WriteLine(i + " 13)" + rng.NextLong((1 * 1000 * 1000), (50 * 1000 * 1000)));
     
                    Console.WriteLine(i + " 14)" + rng.NextDouble());
                    Console.WriteLine(i + " 15)" + rng.NextDouble());
                    Console.WriteLine(i + " 16)" + rng.NextDouble(0, 10 * 1000 * 1000));
                    

                    Console.WriteLine("                                            ");

                    int[] int_arr = rng.NextIntArray(50);
                    long[] long_arr = rng.NextLongArray(50);
                    double[] double_arr = rng.NextDoubleArray(50);
                  //  decimal[] decimal_arr = rng.NextDecimalArray(50);

                    for (int i2 = 0; i2 < int_arr.Length; i2++)
                    {
                        Console.WriteLine("Int Array item test " + i2 + ": " + int_arr[i2]);
                        Console.WriteLine("Long Array item test " + i2 + ": " + long_arr[i2]);
                        Console.WriteLine("Double Array item test " + i2 + ": " + double_arr[i2]);
                    //    Console.WriteLine("Decimal Array item test " + i2 + ": " + decimal_arr[i2]);
                    }

                    Console.WriteLine("                                            ");
                    Console.WriteLine("                                            ");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            Console.WriteLine("Ended");
            Console.ReadLine();
        }
    }
}
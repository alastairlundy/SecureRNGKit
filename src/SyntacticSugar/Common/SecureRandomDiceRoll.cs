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

namespace SecureRNGKit.SyntacticSugar.Common
{
    /// <summary>
    /// A simple class to implement a Secure Random Dice Roller which uses our Secure Random class.
    /// </summary>
    public class SecureRandomDiceRoll
    {
        protected SecureRandom _secureRandom;

        public SecureRandomDiceRoll()
        {
            _secureRandom = new SecureRandom();
        }

        /// <summary>
        /// Simulates rolling a 6 sided Dice with possible values of between 1 and 6 inclusive.
        /// </summary>
        /// <returns></returns>
        public int RollDice()
        {
            return _secureRandom.NextInt(1, 6);
        }

        /// <summary>
        /// Rolls 2 Dice at once and returns the results.
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int> RollTwoDice()
        {
            return new Tuple<int, int>(RollDice(), RollDice());
        }
        
    }
}
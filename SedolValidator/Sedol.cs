using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SedolValidator
{
    public class Sedol
    {        
        private const int CHECK_DIGIT_INDEX = 6;
        private const int SEDOL_LENGTH = 7;
        private const int USER_DEFINED_CHAR_INDEX = 0;
        private const char USER_DEFINED_CHAR = '9';        
        private readonly List<int> sedolWeights = new List<int> { 1, 3, 1, 7, 3, 9 };
        private readonly string input;

        public Sedol(string input)
        {
            this.input = input;            
        }

        /// <summary>
        /// calculated as (ASCAI code -55) for A-Z and (ASCAI code - 48 for numbers)
        /// </summary>
        /// <param name="inputChar"></param>
        /// <returns></returns>
        public int Code(char inputChar)
        {
            if (Char.IsLetter(inputChar))
                return Char.ToUpper(inputChar) - 55;
            return inputChar - 48;
        }

        public bool IsAlphaNumeric
        {
            get { return Regex.IsMatch(input, "^[a-zA-Z0-9]*$"); }
        }

        /// <summary>
        /// Calculated as 10-(weightedSum Mod(%) 10)
        /// </summary>
        public char CheckDigit
        {
            get {
                var codes = input.Take(SEDOL_LENGTH - 1).Select(Code).ToList();
                var weightedSum = sedolWeights.Zip(codes, (weight, code) => weight * code).Sum();
                return Convert.ToChar(((10 - (weightedSum % 10))).ToString(CultureInfo.InvariantCulture));
            }
        }

        public bool IsValidLength
        {
            get { return input?.Length == SEDOL_LENGTH; }
        }

        public bool IsUserDefined
        {
            get { return input[USER_DEFINED_CHAR_INDEX] == USER_DEFINED_CHAR; }
        }

        public bool HasValidCheckDigit
        {
            get { return input[CHECK_DIGIT_INDEX] == CheckDigit; }
        }
    }
}

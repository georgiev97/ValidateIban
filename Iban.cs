using System;
using System.Text;

namespace ValidateIban
{
    public class Iban
    {
        public static bool ValidateIban(string iban)
        {
            const int asciCode = 55; //Constant for ascii code for foreaching the string
            const int firstFourSymbols = 4;// index for the first four elements in the string
            iban = iban.Trim(); // trim the string
            iban=iban.ToUpper(); // making all char's capital
            var ibanLenght = iban.Length;

                                // first step for validation
                                      // check if the iban is exactly 22 symbols

                if (ibanLenght != 22)
                {
                    throw  new ArgumentException("IBAN must be 22 symbols!");
                }


                                // moving first 4 char's in the string to the lest four
            string newIban =
                iban.Substring(firstFourSymbols, ibanLenght - firstFourSymbols) +
                iban.Substring(0,firstFourSymbols);

            StringBuilder ibanSb = new StringBuilder();

            foreach (var character in newIban)   //make every char into digit in the string using ascii code
            {
                int value;
                if (char.IsLetter(character))
                {
                    value = character - asciCode;
                }
                else
                {
                    value = int.Parse(character.ToString());

                }

                ibanSb.Append(value);

            }

            string ibanSumString = ibanSb.ToString();

            var ibanSum = int.Parse(ibanSumString.Substring(0, 1));

            for (var i = 1; i < ibanSumString.Length; i++) // calculate all digits in the string
                                                           // and mod by 97 for result 1
            {
                var v = int.Parse(ibanSumString.Substring(i, 1));

                ibanSum *= 10;
                ibanSum += v;
                ibanSum %= 97;
            }

            Console.WriteLine(ibanSum %=97);


            return ibanSum == 1 ;

        }
    }
}
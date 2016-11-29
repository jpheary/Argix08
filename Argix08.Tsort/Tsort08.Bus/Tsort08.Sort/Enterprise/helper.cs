//	File:	helper.cs
//	Author:	J. Heary
//	Date:	05/08/07
//	Desc:	
//	Rev:	09/24/07 (jph)- added CheckDigitMod10_2() to calculate UPS check digits.
//	---------------------------------------------------------------------------
using System;

namespace Tsort.Enterprise {
    //
    internal class Helper {
        //Interface
        static Helper() { }
        private Helper() { }
        public static bool ValidateMod10CheckDigit(string input) {
            //
            return CheckDigitMod10(input.Substring(0,input.Length - 1)) == input.Substring(input.Length - 1,1);
        }
        public static string CheckDigitMod10(string input) {
            //Perform Mod10 check digit validation used in barcoding
            //Ref: http://en.wikipedia.org/wiki/Luhn_algorithm
            //1. Sum the odd-digits from left to right and multiply the total by 3
            //2. Sum the even-digits from left to right and add to the total in step 1
            //3. The check digit is correct if sum % 10 = 0 
            string checksum="";
            int[] digits = new int[input.Length];
            for(int i=0;i<input.Length;i++)
                digits[i] = DigitValue(input[i]);

            int sum=0;
            for(int i=1;i<=digits.Length;i++) {
                if(i%2 == 1) digits[i-1] *= 3;
                sum += digits[i-1];
            }
            int iCheckSumValue = 10 - sum % 10;
            if(iCheckSumValue == 10)
                checksum = "0";
            else
                checksum = iCheckSumValue.ToString();
            return checksum;
        }
        public static string CheckDigitMod10_2(string input) {
            //Perform Mod10 check digit validation used in barcoding
            //Ref: UPS Guide To Labeling
            //1. Sum the even-digits from left to right and multiply the total by 2
            //2. Sum the odd-digits from left to right and add to the total in step 1
            //3. The check digit is correct if sum % 10 = 0 
            string checksum="";
            int[] digits = new int[input.Length];
            for(int i=0;i<input.Length;i++)
                digits[i] = DigitValue(input[i]);

            int sum=0;
            for(int i=1;i<=digits.Length;i++) {
                if(i%2 == 0) digits[i-1] *= 2;
                sum += digits[i-1];
            }
            int iCheckSumValue = 10 - sum % 10;
            if(iCheckSumValue == 10)
                checksum = "0";
            else
                checksum = iCheckSumValue.ToString();
            return checksum;
        }
        public static string CheckDigitMod11(string input) {
            string checksum="";
            int iWeightedValue=0;
            for(int i=1;i<=input.Length;i++) {
                int iValue = DigitValue(input[i-1]);
                iWeightedValue += ((8-i) * iValue);
            }
            int iCheckSumValue = 11 - iWeightedValue % 11;
            if(iCheckSumValue == 10)
                checksum = "X";
            else if(iCheckSumValue == 11)
                checksum = "0";
            else
                checksum = iCheckSumValue.ToString();
            return checksum;
        }
        public static int DigitValue(char aCharacter) {
            // Answer an Integer corresponding to the numerical radix of
            // the receiver. Return 0-9 if the receiver is $0-$9, and
            // 10-35 if it is $A-$Z; otherwise return -1.
            // DigitValues is initialized to contain the character value + 1
            // of the character if the character is a digit and 0 otherwise. 


            if(Char.IsDigit(aCharacter)) return aCharacter - '0';
            if(Char.IsUpper(aCharacter)) return aCharacter - 'A' + 10;
            return -1;
        }

    }
}

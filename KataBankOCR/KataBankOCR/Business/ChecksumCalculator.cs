using System;
using WebGrease;

namespace KataBankOCR.Business
{
    public class ChecksumCalculator
    {
        public bool CalculateCheckSum(string input)
        {
            int multiplier = 9;
            int returnValue = 0;
            foreach (char c in input)
            {
                var value = c - '0';
                returnValue += value*multiplier;
                multiplier--;
            }
            return (returnValue % 11 == 0);
        }
    }
}
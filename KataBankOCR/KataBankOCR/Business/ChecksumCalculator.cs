namespace KataBankOCR.Business
{
    public interface IChecksumCalculator
    {
        bool DoesChecksumPass(string input);
    }

    public class ChecksumCalculator : IChecksumCalculator
    {
        public bool DoesChecksumPass(string input)
        {
            int multiplier = 9;
            int returnValue = 0;
            foreach (char c in input)
            {
                var value = c - '0';
                returnValue += value*multiplier;
                multiplier--;
            }
            return (returnValue%11 == 0);
        }
    }
}
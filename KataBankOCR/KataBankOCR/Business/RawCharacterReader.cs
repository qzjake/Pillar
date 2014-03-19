#region

using System;

#endregion

namespace KataBankOCR.Business
{
    public interface IRawCharacterReader
    {
        string GetRawDigit(string input, int x, int y);
    }

    /// <summary>
    /// Given a OCR string read 3x3 digit at x,y provided
    /// </summary>
    public class RawCharacterReader : IRawCharacterReader
    {
        public string GetRawDigit(string input, int x, int y)
        {
            var contents = input;
            var splitContents = contents.Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);

            if (4*y + 2 > splitContents.Length)
                return null;

            return
                splitContents[0 + 4*y].Substring(x*3, 3) +
                splitContents[1 + 4*y].Substring(x*3, 3) +
                splitContents[2 + 4*y].Substring(x*3, 3);
        }
    }
}
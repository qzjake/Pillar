#region

using System.Collections.Generic;
using System.IO;

#endregion

namespace KataBankOCR.Business
{
    public class KataBankOcr
    {
        private readonly IFileUtilities _fileUtilities;
        private readonly IRawCharacterReader _rawCharacterReader;

        public KataBankOcr(IFileUtilities fileUtilies, IRawCharacterReader rawCharacterReader)
        {
            _fileUtilities = fileUtilies;
            _rawCharacterReader = rawCharacterReader;
        }

        public KataBankOcr()
        {
            _fileUtilities = new FileUtilities();
        }


        public KataBankOcr(Stream file)
            : this()
        {
            File = file;
        }

        private Stream File { get; set; }

        public string FileContents
        {
            get { return _fileUtilities.ReadFileToString(File); }
        }

        public string[] GetAccountNumbers()
        {
            return null;
        }

        /// <summary>
        /// Read numbers as described: http://codingdojo.org/cgi-bin/index.pl?KataBankOCR
        /// numbers are 9 characters (3x3 grid) drawn with _ and |
        /// </summary>
        /// <param name="x">horizontal location in grid to find the number</param>
        /// <param name="y">vertical location in grid to find the number</param>
        /// <returns>Digit read</returns>
        /// <seealso cref="http://codingdojo.org/cgi-bin/index.pl?KataBankOCR"/>
        /// <remarks>Assumes data is always clean.</remarks>
        /// <remarks>Chose performance over readability.</remarks>
        public char GetValueAtPosition(int x, int y)
        {
            var text = _rawCharacterReader.GetRawDigit(FileContents, x, y);
            if (text == null)
                return '\0';
            // assumes only valid data will be read
            // favored performance over readability
            if (text[3] == '|')
            {
                if (text[6] == '|')
                {
                    if (text[4] == ' ')
                    {
                        return '0';
                    }
                    return text[5] == ' ' ? '6' : '8';
                }
                if (text[1] == ' ')
                {
                    return '4';
                }
                return text[5] == ' ' ? '5' : '9';
            }

            if (text[4] == '_')
            {
                return text[6] == '|' ? '2' : '3';
            }

            return text[1] == ' ' ? '1' : '7';
        }

        public string GetLine(int y)
        {
            var returnValue = "";
            for (var x = 0; x < 9; x++)
            {
                var temp = GetValueAtPosition(x, y);
                if (temp == '\0')
                {
                    return null;
                }
                returnValue += temp;
            }
            return returnValue;
        }

        public string[] ProcessFile()
        {
            var returnValue = new List<string>(500);
            var i = 0;
            do
            {
                var accountNumber = GetLine(i);

                if (accountNumber == null)
                    return returnValue.ToArray();

                returnValue.Add(accountNumber);
                i++;
            } while (true);
        }
    }
}
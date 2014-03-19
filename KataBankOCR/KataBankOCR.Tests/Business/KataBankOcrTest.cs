#region

using System.Collections.Generic;
using System.IO;
using System.Text;
using FakeItEasy;
using KataBankOCR.Business;
using NUnit.Framework;

#endregion

namespace KataBankOCR.Tests.Business
{
    internal class KataBankOcrTest
    {
        private static readonly string[] RawDigits =
        {
            " _ " +
            "| |" +
            "|_|",
            "   " +
            "  |" +
            "  |",
            " _ " +
            " _|" +
            "|_ ",
            " _ " +
            " _|" +
            " _|",
            "   " +
            "|_|" +
            "  |",
            " _ " +
            "|_ " +
            " _|",
            " _ " +
            "|_ " +
            "|_|",
            " _ " +
            "  |" +
            "  |",
            " _ " +
            "|_|" +
            "|_|",
            " _ " +
            "|_|" +
            " _|"
        };


        [Test]
        public void Exists()
        {
            var sut = new KataBankOcr();

            Assert.Pass();
        }

        [Test]
        public void AcceptsFile()
        {
            var file = A.Fake<Stream>();
            var sut = new KataBankOcr(file);

            Assert.Pass();
        }

        [Test]
        public void ReturnsArrayOfAccountNumbers()
        {
            var file = A.Fake<Stream>();
            var sut = new KataBankOcr(file);
            string[] accountNumbers = sut.GetAccountNumbers();

            Assert.Pass();
        }

        [Test]
        public void GetsStringFromFile()
        {
            const string fileString = "the stream";
            var fileContents = Encoding.UTF8.GetBytes(fileString);
            var file = new MemoryStream(fileContents);
            var sut = new KataBankOcr(file);
            Assert.AreEqual(fileString, sut.FileContents);
        }

        [Test]
        [TestCase(0, '0')]
        [TestCase(1, '1')]
        [TestCase(2, '2')]
        [TestCase(3, '3')]
        [TestCase(4, '4')]
        [TestCase(5, '5')]
        [TestCase(6, '6')]
        [TestCase(7, '7')]
        [TestCase(8, '8')]
        [TestCase(9, '9')]
        public void CanReadNumber(int digit, char expectedResult)
        {
            var fileUtil = A.Dummy<IFileUtilities>();
            var rawCharacterReader = A.Fake<IRawCharacterReader>();

            A.CallTo(() => rawCharacterReader.GetRawDigit(A<string>.Ignored, A<int>.Ignored, A<int>.Ignored))
                .Returns(RawDigits[digit]);

            var sut = new KataBankOcr(fileUtil, rawCharacterReader);

            var value = sut.GetValueAtPosition(0, 0);

            Assert.AreEqual(expectedResult, value);
        }

        [Test]
        [TestCase(0, "000000000")]
        [TestCase(1, "111111111")]
        [TestCase(10, null)]
        public void CanReadLine(int y, string expected)
        {
            var fileUtil = A.Dummy<IFileUtilities>();
            var rawCharacterReader = A.Fake<IRawCharacterReader>();

            var values = new List<string>();

            string rawDigit = y < 10 ? RawDigits[y] : null;

            for (var i = 0; i < 9; i++)
            {
                values.Add(rawDigit);
            }

            var valueArray = values.ToArray();
            A.CallTo(() => rawCharacterReader.GetRawDigit(A<string>.Ignored, A<int>.Ignored, A<int>.Ignored))
                .ReturnsNextFromSequence(valueArray);

            var sut = new KataBankOcr(fileUtil, rawCharacterReader);

            Assert.AreEqual(expected, sut.GetLine(y));
        }

        [Test]
        public void CanReadFile()
        {
            var expected = new string[2] {"000000000", "111111111"};
            var fileUtil = A.Dummy<IFileUtilities>();
            var rawCharacterReader = A.Fake<IRawCharacterReader>();

            var values = new List<string>();
            for (var i = 0; i < 9; i++)
            {
                values.Add(RawDigits[0]);
            }
            for (var i = 0; i < 9; i++)
            {
                values.Add(RawDigits[1]);
            }
            values.Add(null);

            A.CallTo(() => rawCharacterReader.GetRawDigit(A<string>.Ignored, A<int>.Ignored, A<int>.Ignored))
                .ReturnsNextFromSequence(values.ToArray());
            var sut = new KataBankOcr(fileUtil, rawCharacterReader);

            Assert.AreEqual(expected, sut.ProcessFile());
        }       

        [Test]
        public void CanDetectChecksum()
        {
        }

    }
}
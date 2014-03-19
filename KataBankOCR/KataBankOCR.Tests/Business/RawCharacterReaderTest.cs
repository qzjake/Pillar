using System.IO;
using FakeItEasy;
using KataBankOCR.Business;
using NUnit.Framework;

namespace KataBankOCR.Tests.Business
{
    internal class RawCharacterReaderTest
    {
        [Test]
        [TestCase(0, 0, "123234345")]
        [TestCase(1, 0, "456567678")]
        [TestCase(8, 0, "opqpqrqrs")]
        [TestCase(0, 1, "567678789")]
        [TestCase(0, 2, null)]
        public void CanGetPosition(int x, int y, string expected)
        {
            var sut = new RawCharacterReader();

            const string value = "1234567890abcdefghijklmnopq\n" +
                                 "234567890abcdefghijklmnopqr\n" +
                                 "34567890abcdefghijklmnopqrs\n" +
                                 "4567890abcdefghijklmnopqrst\n" +
                                 "567890abcdefghijklmnopqrstu\n" +
                                 "67890abcdefghijklmnopqrstuv\n" +
                                 "7890abcdefghijklmnopqrstuvw\n" +
                                 "890abcdefghijklmnopqrstuvwx\n";
            
            
            var positionContents = sut.GetRawDigit(value, x, y);
            Assert.AreEqual(expected, positionContents);
        }
    }
}
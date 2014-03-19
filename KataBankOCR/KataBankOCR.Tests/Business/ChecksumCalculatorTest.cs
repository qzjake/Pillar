using KataBankOCR.Business;
using NUnit.Framework;

namespace KataBankOCR.Tests.Business
{
    public class ChecksumCalculatorTest
    {

        [Test]
        [TestCase("345882865", true)]
        [TestCase("664371495", false)]
        public void IsChecksumCorrect(string input, bool expected)
        {
            var sut = new ChecksumCalculator();
            
            Assert.AreEqual(expected, sut.DoesChecksumPass(input));
        }
    }
}

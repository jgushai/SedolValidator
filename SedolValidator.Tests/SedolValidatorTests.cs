
using NUnit.Framework;
using SedolValidatorInterfaces;

namespace SedolValidator.Tests
{
    [TestFixture]
    public class SedolValidatorTests
    {
        /// <summary>
        /// **Scenario:**  Null, empty string or string other than 7 characters long
        /// </summary>
        /// <param name="sedol"></param>
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("12345678")]
        [TestCase("123456")]
        public void InvalidLengthSedols(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, ValidationDetailsConstants.INVALID_INPUT_STRING_LENGTH);
            AssertValidationResult(expected, actual);
        }

        /// <summary>
        /// **Scenario:**  Invalid Checksum non user defined SEDOL
        /// </summary>
        /// <param name="sedol"></param>
        [TestCase("1234567")]
        public void NonUserDefinedSedolsWithInvalidChecksum(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, ValidationDetailsConstants.INVALID_CHECKSUM);
            AssertValidationResult(expected, actual);
        }

        /// <summary>
        /// **Scenario:**  Valid non user define SEDOL
        /// </summary>
        /// <param name="sedol"></param>
        [Test]
        [TestCase("0709954")]
        [TestCase("B0YBKJ7")]
        public void ValidNonUserDefinedSedols(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, true, false, null);
            AssertValidationResult(expected, actual);
        }

        /// <summary>
        /// **Scenario:**  Invalid Checksum User defined SEDOL
        /// </summary>
        /// <param name="sedol"></param>
        [TestCase("9123451")]
        [TestCase("9ABCDE8")]
        public void UserDefinedSedolsWithInvalidChecksum(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, true, ValidationDetailsConstants.INVALID_CHECKSUM);
            AssertValidationResult(expected, actual);
        }


        /// <summary>
        /// **Scenario** Invaid characters found
        /// </summary>
        /// <param name="sedol"></param>
        [TestCase("9123_51")]
        [TestCase("VA.CDE8")]
        public void SedolsWithInvalidCharacters(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, ValidationDetailsConstants.INVALID_CHARACTER);
            AssertValidationResult(expected, actual);
        }

        /// <summary>
        /// **Scenario:**  Valid non user define SEDOL
        /// </summary>
        /// <param name="sedol"></param>
        [Test]
        [TestCase("9123458")]
        [TestCase("9ABCDE1")]
        public void ValidNUserDefinedSedols(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, true, true, null);
            AssertValidationResult(expected, actual);
        }



        private static void AssertValidationResult(ISedolValidationResult expected, ISedolValidationResult actual)
        {
            Assert.AreEqual(expected.InputString, actual.InputString, "Input String Failed");
            Assert.AreEqual(expected.IsValidSedol, actual.IsValidSedol, "Is Valid Sedol Failed");
            Assert.AreEqual(expected.IsUserDefined, actual.IsUserDefined, "Is User Defined Failed");
            Assert.AreEqual(expected.ValidationDetails, actual.ValidationDetails, "Validation Details Failed");
        }

    }
}

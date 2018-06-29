using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionTests
{
    [TestClass]
    public class StringArrayExtensionsTests
    {
        [TestMethod]
        public void IsSquareShouldReturnFalseOnEmptyStringArray()
        {
            string[] array = new string[0];
            Assert.IsFalse(array.IsSquare());
        }

        [TestMethod]
        public void IsSquareShouldReturnFalseOnNullStringArray()
        {
            string[] array = null;
            Assert.IsFalse(array.IsSquare());
        }

        [TestMethod]
        public void IsSquareShouldReturnTrueOnStringArray2x2Size()
        {
            string[] array = new string[] { "AA", "AA" };
            Assert.IsTrue(array.IsSquare());
        }

        [TestMethod]
        public void IsSquareShouldReturnFalseOnStringArray2x1Size()
        {
            string[] array = new string[] { "AA", "A" };
            Assert.IsFalse(array.IsSquare());
        }

        [TestMethod]
        public void IsSquareShouldReturnFalseOnStringArray1x2Size()
        {
            string[] array = new string[] { "AA", "A" };
            Assert.IsFalse(array.IsSquare());
        }

        [TestMethod]
        public void IsSquareShouldReturnTrueOnStringArray3x3Size()
        {
            string[] array = new string[] { "AAA", "AAA", "AAA" };
            Assert.IsTrue(array.IsSquare());
        }

        [TestMethod]
        public void IsSquareShouldReturnFalseOnStringArray2x3Size()
        {
            string[] array = new string[] { "AAA", "AAA" };
            Assert.IsFalse(array.IsSquare());
        }

        [TestMethod]
        public void IsSquareShouldReturnFalseOnStringArrayWithInvalidLengthInBeginOf3x3Size()
        {
            string[] array = new string[] { "AA", "AAA", "AAA" };
            Assert.IsFalse(array.IsSquare());
        }

        [TestMethod]
        public void IsSquareShouldReturnFalseOnStringArrayWithInvalidLengthInMiddleOf3x3Size()
        {
            string[] array = new string[] { "AAA", "AA", "AAA" };
            Assert.IsFalse(array.IsSquare());
        }

        [TestMethod]
        public void IsSquareShouldReturnFalseOnStringArrayWithInvalidLengthInEndOf3x3Size()
        {
            string[] array = new string[] { "AAA", "AAA", "AAAA" };
            Assert.IsFalse(array.IsSquare());
        }

        [TestMethod]
        public void SearchHorizontalOccurrencesShouldReturnZeroArrayCaseOne()
        {
            string[] stringArray = new string[] { "AAAA", "CCCC", "CCCC", "CCCC" };
            Assert.AreEqual(stringArray.CountHorizontalOcurrences("BBB"), 0);
        }

        [TestMethod]
        public void SearchHorizontalOccurrencesShouldReturnOneCaseOne()
        {
            string[] stringArray = new string[] { "ABBB", "CCCC", "CCCC", "CCCC" };
            Assert.AreEqual(stringArray.CountHorizontalOcurrences("BBB"), 1);
        }

        [TestMethod]
        public void SearchHorizontalOccurrencesShouldReturnOneCaseTwo()
        {
            string[] stringArray = new string[] { "AAAA", "CCCC", "BBBC", "CCCC" };
            Assert.AreEqual(stringArray.CountHorizontalOcurrences("BBB"), 1);
        }

        [TestMethod]
        public void SearchHorizontalOccurrencesShouldReturnOneCaseThree()
        {
            string[] stringArray = new string[] { "AAAA", "CCCC", "AAAC", "CBBB" };
            Assert.AreEqual(stringArray.CountHorizontalOcurrences("BBB"), 1);
        }

        [TestMethod]
        public void SearchHorizontalOccurrencesShouldReturnThreeCaseOne()
        {
            string[] stringArray = new string[] { "ABBB", "CBBB", "BBBC", "CCCC" };
            Assert.AreEqual(stringArray.CountHorizontalOcurrences("BBB"), 3);
        }

        [TestMethod]
        public void SearchHorizontalOccurrencesShouldReturnThreeCaseTwo()
        {
            string[] stringArray = new string[] { "ABBB", "CACA", "BBBC", "CBBB" };
            Assert.AreEqual(stringArray.CountHorizontalOcurrences("BBB"), 3);
        }

        [TestMethod]
        public void SearchHorizontalOccurrencesShouldReturnFourCaseOne()
        {
            string[] stringArray = new string[] { "ABBB", "BBBB", "BBBC", "CBBB" };
            Assert.AreEqual(stringArray.CountHorizontalOcurrences("BBB"), 4);
        }

        [TestMethod]
        public void SearchVerticalOccurrencesShouldReturnZeroArrayCaseOne()
        {
            string[] stringArray = new string[] { "AAAA", "CCCC", "CCCC", "CCCC" };
            Assert.AreEqual(stringArray.CountVerticalOcurrences("BBB"), 0);
        }

        [TestMethod]
        public void SearchVerticalOccurrencesShouldReturnOneCaseOne()
        {
            string[] stringArray = new string[] { "ABBB", "CCBC", "CCBC", "CCCC" };
            Assert.AreEqual(stringArray.CountVerticalOcurrences("BBB"), 1);
        }

        [TestMethod]
        public void SearchVerticalOccurrencesShouldReturnOneCaseTwo()
        {
            string[] stringArray = new string[] { "AAAA", "CCBC", "BBBC", "CCBC" };
            Assert.AreEqual(stringArray.CountVerticalOcurrences("BBB"), 1);
        }

        [TestMethod]
        public void SearchVerticalOccurrencesShouldReturnOneCaseThree()
        {
            string[] stringArray = new string[] { "AAAB", "CCCB", "AAAB", "CBBB" };
            Assert.AreEqual(stringArray.CountVerticalOcurrences("BBB"), 1);
        }

        [TestMethod]
        public void SearchVerticalOccurrencesShouldReturnThreeCaseOne()
        {
            string[] stringArray = new string[] { "ABBB", "CBBB", "BBBB", "CBBB" };
            Assert.AreEqual(stringArray.CountVerticalOcurrences("BBB"), 3);
        }

        [TestMethod]
        public void SearchVerticalOccurrencesShouldReturnThreeCaseTwo()
        {
            string[] stringArray = new string[] { "BABB", "BABB", "BCBB", "CABC" };
            Assert.AreEqual(stringArray.CountVerticalOcurrences("BBB"), 3);
        }

        [TestMethod]
        public void SearchVerticalOccurrencesShouldReturnFourCaseOne()
        {
            string[] stringArray = new string[] { "BAAA", "BBBB", "BBBB", "ABBB" };
            Assert.AreEqual(stringArray.CountVerticalOcurrences("BBB"), 4);
        }

        [TestMethod]
        public void SearchDiagonalOccurrencesShouldReturnZeroArrayCaseOne()
        {
            string[] stringArray = new string[] { "AAAA", "CCCC", "CCCC", "CCCC" };
            Assert.AreEqual(stringArray.CountDiagonalOcurrences("BBB"), 0);
        }

        [TestMethod]
        public void SearchDiagonalOccurrencesShouldReturnOneCaseOne()
        {
            string[] stringArray = new string[] { "ABBB", "CCBC", "CCBB", "CCCC" };
            Assert.AreEqual(stringArray.CountDiagonalOcurrences("BBB"), 1);
        }

        [TestMethod]
        public void SearchDiagonalOccurrencesShouldReturnOneCaseTwo()
        {
            string[] stringArray = new string[] { "BAAA", "CBBC", "BBBC", "CCBC" };
            Assert.AreEqual(stringArray.CountDiagonalOcurrences("BBB"), 1);
        }

        [TestMethod]
        public void SearchDiagonalOccurrencesShouldReturnOneCaseThree()
        {
            string[] stringArray = new string[] { "AAAA", "CCBB", "ABAB", "BBBB" };
            Assert.AreEqual(stringArray.CountDiagonalOcurrences("BBB"), 1);
        }

        [TestMethod]
        public void SearchDiagonalOccurrencesShouldReturnThreeCaseOne()
        {
            string[] stringArray = new string[] { "BBAA", "BBBB", "ABBB", "CCBC" };
            Assert.AreEqual(stringArray.CountDiagonalOcurrences("BBB"), 3);
        }

        [TestMethod]
        public void SearchDiagonalOccurrencesShouldReturnThreeCaseTwo()
        {
            string[] stringArray = new string[] { "AABA", "ABBB", "BBBA", "BBAA" };
            Assert.AreEqual(stringArray.CountDiagonalOcurrences("BBB"), 3);
        }
    }
}

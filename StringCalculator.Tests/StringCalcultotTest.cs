using System;
using NUnit.Framework;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class StringCalcultorTest
    {
        private CStringCalculator _stringCalculator;

        [SetUp]
        public void SetUp()
        {
            _stringCalculator = new CStringCalculator();
        }

        public class CStringCalculator
        {
            public int Add(string numbers)
            {
                //if (string.IsNullOrEmpty(numbers)) return 0;
                //else
                {
                    string substringNumbers;
                    substringNumbers = numbers.Contains("\n") ? numbers.Substring(numbers.LastIndexOf("\n", StringComparison.Ordinal)) : numbers;

                    string[] strarrNumbers = substringNumbers.Split(',');
                    int dwCalculateAdd = 0;
                    try
                    {
                        foreach (var t in strarrNumbers) dwCalculateAdd += int.Parse(t);
                    }
                    catch (FormatException)
                    {   
                           return 0;
                    }

                    return dwCalculateAdd;
                }
            }
        }

        [TestCase("", 0)]
        public void Add_EmptyString_ReturnZero(string actual, int expected)
        {
            //Arrange
            
     
            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1",1)]
        [TestCase("2", 2)]
        public void Add_SingleNumber_ReturnThisNumber(string actual, int expected)
        {
            //Arrange
           

            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1,2",3)]
        [TestCase("5,6", 11)]
        public void Add_DoubleNumbers_ReturnFinalNumber(string actual, int expected)
        {
            //Arrange

            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("0,1,2,3,4,5,6,7,8,9", 45)]
        [TestCase("10,11,12,13,14,15,16,17,18,19", 145)]
        public void Add_ManyNumbers_ReturnFinalNumber(string actual, int expected)
        {
            //Arrange

            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("1\n2,3", 5)]
        [TestCase("1,\n", 0)]
        [TestCase("1,\n,\n", 0)]
        [TestCase("1,\n,\n3", 3)]
        public void Add_NewLineNumbers_ReturnNewLineFinalNumber(string actual, int expected)
        {
            //Arrange

            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));

        }
    }

}

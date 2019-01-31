using System;
using System.Collections;
using NUnit.Framework;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class StringCalcultorTest
    {
        private StringCalculator _stringCalculator;

        [SetUp]
        public void SetUp()
        {
            _stringCalculator = new StringCalculator();
        }

        [TearDown]
        public void TearDown() {}

        public class StringCalculator
        {
            public int Add(string numbers)
            {
                var arrListNewDilimiter = new List<string> {","};

               arrListNewDilimiter.AddRange(GetAllowedDelimeters(numbers));

                var NewLineSymbol = "\n";

                numbers = numbers.Contains(NewLineSymbol) ? numbers.Substring(numbers.LastIndexOf(NewLineSymbol, StringComparison.Ordinal)) : numbers;

                string[] strarrNumbers = numbers.Split(arrListNewDilimiter.ToArray(), StringSplitOptions.None);

                int dwCalculateAdd = 0;
                try
                {
                    foreach (var t in strarrNumbers)
                    {
                        if (int.Parse(t) < 0) { throw new ArgumentException("Negative Number"); }

                        if (int.Parse(t) >= 1000) { continue; }

                        dwCalculateAdd += int.Parse(t);
                    }
                }
                catch (ArgumentException)
                {
                    return -1;
                }

                catch (FormatException)
                {
                    return 0;
                }

                return dwCalculateAdd;
            }

            private static List<string> GetAllowedDelimeters(string numbers)
            {
                List<string> delimeters = new List<string>();

                if (numbers.Contains("//"))
                {
                    Regex regex = new Regex(@"((?<=\[)([^]]+)(?=\]))|((?<=\//)(.))");
                    MatchCollection matches = regex.Matches(numbers);
                    foreach (Match match in matches)
                        delimeters.Add(match.Value);
                }

                return delimeters;
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

        [TestCase("2", 2)]
        public void Add_SingleNumber_ReturnThisNumber(string actual, int expected)
        {
            //Arrange


            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("5,6", 11)]
        public void Add_DoubleNumbers_ReturnFinalNumber(string actual, int expected)
        {
            //Arrange


            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

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

        [TestCase("//;\n1;2", 3)]
        public void Add_NewDilimiterOfNumbers_ReturnFinalNumber(string actual, int expected)
        {
            //Arrange


            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("//;\n-1;2", -1)]
        public void Add_NegativeNumbers_ReturnNegativeResult(string actual, int expected)
        {
            //Arrange


            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("//;\n1000;2", 2)]
        public void Add_LessThousandNumbers_ReturnLessResult(string actual, int expected)
        {
            //Arrange


            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("//[***]\n1***2***3", 6)]
        public void Add_DelimiterWithAnyLength_ReturnResult(string actual, int expected)
        {
            //Arrange


            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("//[*][%]\n1*2%3", 6)]
        public void Add_MultiplyDelimiter_ReturnResult(string actual, int expected)
        {
            //Arrange


            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("//[**!**][%&.]\n1**!**7%&.8", 16)]
        public void Add_MultiplyDelimiterWithAnyLength_ReturnResult(string actual, int expected)
        {
            //Arrange


            //Act
            int result = _stringCalculator.Add(actual);

            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }

}

using Xunit;
using System;
using System.Collections.Generic;
using CSharp_learn;

namespace CSarp_learn.Tests
{
    public class FractionTest
    {
        [Fact]
        public void Ctor()
        {
            bool exc = false;
            try
            {
                Fraction actual = new Fraction(1, 2);
                Assert.NotNull(actual);
                Fraction bad = new Fraction(1, 0);
                Assert.True(exc);
            } catch (ArgumentException ex)
            {
                exc = true;
            }
            Assert.True(exc);
        }
        [Theory]
        [MemberData("PlusTestCases")]
        public void Plus(Fraction a, Fraction b, Fraction expected)
        {
            Assert.Equal(expected, a + b);
        }

        [Theory]
        [MemberData("MinusTestCases")]
        public void Minus(Fraction a, Fraction b, Fraction expected)
        {
            Assert.Equal(expected, a - b);
        }

        [Theory]
        [MemberData("MultiplyTestCases")]
        public void Multiply(Fraction a, Fraction b, Fraction expected)
        {
            Assert.Equal(expected, a * b);
        }

        [Theory]
        [MemberData("DivideTestCases")]
        public void Divide(Fraction a, Fraction b, Fraction expected)
        {
            Assert.Equal(expected, a / b);
        }
        [Theory]
        [MemberData("ParseTestCases")]
        public void NormalParse(string data, Fraction? expected, bool valid_data, string? errorMsg)
        {
            var actual = Fraction.Parse(data);
            if (actual.Fraction is null)
            {
                Assert.False(valid_data);
                Assert.Equal(errorMsg, actual.ErrorMassage);
            } 
            else
            {
                Assert.True(valid_data);
                Assert.Equal(expected, actual.Fraction);
            }
        }
        public static IEnumerable<Fraction[]> PlusTestCases()
        {
            // целые
            yield return new Fraction[] { new(1), new(3), new(4) };              // 1
            yield return new Fraction[] { new(1, 2), new(3), new(7, 2) };        // 2
            yield return new Fraction[] { new(5), new(1, 5), new(26, 5) };       // 3
            // одинаковый знаменатель
            yield return new Fraction[] { new(5, 8), new(3, 8), new(1) };        // 4
            yield return new Fraction[] { new(2, 3), new(3, 3), new(5, 3) };     // 5
            // разный знаменталь
            yield return new Fraction[] { new(2, 6), new(1, 3), new(2, 3) };     // 6
            yield return new Fraction[] { new(1, 2), new(2, 3), new(7, 6) };     // 7
            // отрицательные с положительными
            yield return new Fraction[] { new(-2, 6), new(2, 3), new(1, 3) };    // 8
            yield return new Fraction[] { new(1, -2), new(2, 3), new(1, 6) };    // 9
            yield return new Fraction[] { new(2, 6), new(1, -3), new(0, 1) };    // 10
            yield return new Fraction[] { new(1, 2), new(-2, 3), new(-1, 6) };   // 11
            // отрицательные
            yield return new Fraction[] { new(-1, 5), new(-3, 5), new(-4, 5) };  // 12
            yield return new Fraction[] { new(-2, 3), new(-1, 4), new(-11,12) }; // 13
        }
        public static IEnumerable<Fraction[]> MinusTestCases()
        {
            // целые
            
            yield return new Fraction[] { new(3), new(2), new(1) };             // 1
            yield return new Fraction[] { new(5), new(15, 4), new(5, 4) };      // 2
            yield return new Fraction[] { new(6, 5), new(1), new(1, 5) };       // 3
            // одинаковый знаменатель
            yield return new Fraction[] { new(5, 8), new(3, 8), new(2, 8) };    // 4
            yield return new Fraction[] { new(3, 4), new(3, 4), new(0) };       // 5
            // разный знаменталь
            yield return new Fraction[] { new(2, 6), new(1, 3), new(0) };       // 6
            yield return new Fraction[] { new(2, 3), new(1, 2), new(1, 6) };    // 7
            // отрицательные с положительными
            yield return new Fraction[] { new(-2, 6), new(2, 3), new(-1) };     // 8
            yield return new Fraction[] { new(1, -2), new(2, 3), new(-7, 6) };  // 9
            yield return new Fraction[] { new(4, 6), new(1, -3), new(1) };      // 10
            yield return new Fraction[] { new(1, 2), new(-2, 3), new(7, 6) };   // 11
            // отрицательные
            yield return new Fraction[] { new(-1, 5), new(-3, 5), new(2, 5) };  // 12
            yield return new Fraction[] { new(-2, 3), new(-1, 2), new(-1, 6) }; // 13
        }
        public static IEnumerable<Fraction[]> MultiplyTestCases()
        {
            // целые

            yield return new Fraction[] { new(3), new(2), new(6) };             // 1
            yield return new Fraction[] { new(2), new(15, 4), new(15, 2) };     // 2
            yield return new Fraction[] { new(6, 5), new(1), new(6, 5) };       // 3
            // одинаковый знаменатель
            yield return new Fraction[] { new(5, 8), new(3, 2), new(15, 16) };  // 4
            yield return new Fraction[] { new(3, 4), new(3, 4), new(9, 16) };   // 5
            // разный знаменталь
            yield return new Fraction[] { new(2, 6), new(1, 3), new(1, 9) };    // 6
            yield return new Fraction[] { new(2, 3), new(1, 2), new(1, 3) };    // 7
            // отрицательные с положительными
            yield return new Fraction[] { new(-2, 6), new(2, 3), new(-2, 9) };  // 8
            yield return new Fraction[] { new(4, 6), new(-6, 2), new(-2) };     // 9
            // отрицательные
            yield return new Fraction[] { new(-1, 5), new(-3, 5), new(3, 25) }; // 10
            yield return new Fraction[] { new(-2, 3), new(-1, 2), new(1, 3) };  // 11
        }
        public static IEnumerable<Fraction[]> DivideTestCases()
        {
            // целые

            yield return new Fraction[] { new(3), new(2), new(3, 2) };          // 1
            // одинаковый знаменатель
            yield return new Fraction[] { new(5, 8), new(3, 8), new(5, 3) };    // 2
            yield return new Fraction[] { new(3, 4), new(3, 4), new(1) };       // 3
            // разный знаменталь
            yield return new Fraction[] { new(2, 6), new(1, 3), new(1) };       // 4
            yield return new Fraction[] { new(2, 3), new(1, 2), new(4, 3) };    // 5
            // отрицательные с положительными
            yield return new Fraction[] { new(-2, 6), new(2, 3), new(-1, 2) };  // 6
            yield return new Fraction[] { new(1, 2), new(-2, 3), new(-3, 4) };  // 9
            // отрицательные
            yield return new Fraction[] { new(-1, 5), new(-3, 5), new(1, 3) };  // 10
            yield return new Fraction[] { new(-2, 3), new(-1, 2), new(4, 3) };  // 11
        }

        public static IEnumerable<object?[]> ParseTestCases()
        {
            //normal
            yield return new object?[] { "1", new Fraction(1), true, null };                   // 1
            yield return new object?[] { "1/5", new Fraction(1, 5), true, null };              // 2
            yield return new object?[] { "-5/6", new Fraction(-5, 6), true, null };            // 3
            yield return new object?[] { "6/-5", new Fraction(-6, 5), true, null };            // 4
            yield return new object?[] { "-5/-10", new Fraction(1, 2), true, null };           // 5

            //abnormal
            yield return new object?[] { null, null, false, "Нахуй null!" };                   // 6
            yield return new object?[] { "4/5/6", null, false, "Нахуй корявые дроби!" };       // 7

            yield return new object?[] { "/", null, false, "Нахуй корявый числитель!" };       // 8
            yield return new object?[] { "2.5/", null, false, "Нахуй корявый числитель!" };    // 9
            yield return new object?[] { "/6", null, false, "Нахуй корявый числитель!" };      // 10

            yield return new object?[] { "5/", null, false, "Нахуй корявый знаменатель!" };    // 11
            yield return new object?[] { "5/d", null, false, "Нахуй корявый знаменатель!" };   // 12
            yield return new object?[] { "5/2.4", null, false, "Нахуй корявый знаменатель!" }; // 13

            yield return new object?[] { "5.5", null, false, "Нахуй корявую дробь!" };         // 14
            yield return new object?[] { "5t", null, false, "Нахуй корявую дробь!" };          // 15
        }

    }
}
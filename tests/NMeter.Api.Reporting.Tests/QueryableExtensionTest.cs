using System.Text.Json;
using NMeter.Api.Reporting.Extensions;

namespace NMeter.Api.Reporting.Tests
{
    [TestClass]
    public class QueryableExtensionTest
    {
        private record Person(string Name, int Age);

        [TestMethod]
        public void FilterBy_Integer_Equals_Positive_Test()
        {
            var persons = new List<Person>
            {
                new Person("Tim Tayler", 30),
                new Person("Al Pachino", 70)
            }
            .AsQueryable();

            var person = persons.FilterBy("age", 30).FirstOrDefault();

            Assert.AreEqual(new Person("Tim Tayler", 30), person);
        }

        [TestMethod]
        public void FilterBy_Integer_Equals_Negative_Test()
        {
            var persons = new List<Person>
            {
                new Person("Tim Tayler", 30),
                new Person("Al Pachino", 70)
            }
            .AsQueryable();

            var person = persons.FilterBy("age", 31).FirstOrDefault();

            Assert.AreEqual(null, person);
        }

        [TestMethod]
        public void FilterBy_String_Contains_Positive_Test()
        {
            var persons = new List<Person>
            {
                new Person("Tim Tayler", 30),
                new Person("Al Pachino", 70)
            }
            .AsQueryable();

            var person = persons.FilterBy("name", "yle").FirstOrDefault();

            Assert.AreEqual(new Person("Tim Tayler", 30), person);
        }

        [TestMethod]
        public void FilterBy_String_Contains_Negative_Test()
        {
            var persons = new List<Person>
            {
                new Person("Tim Tayler", 30),
                new Person("Al Pachino", 70)
            }
            .AsQueryable();

            var person = persons.FilterBy("name", "qqq").FirstOrDefault();

            Assert.AreEqual(null, person);
        }

        [TestMethod]
        public void FilterBy_Empty_Source_Negative_Test()
        {
            var persons = new List<Person>()
            .AsQueryable();

            var person = persons.FilterBy("name", "qqq").FirstOrDefault();

            Assert.AreEqual(null, person);
        }

        [TestMethod]
        public void FilterBy_Empty_Property_Name_Negative_Test()
        {
            var persons = new List<Person>
            {
                new Person("Tim Tayler", 30),
                new Person("Al Pachino", 70)
            }
            .AsQueryable();

            var result = persons.FilterBy("", "qqq");

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void FilterBy_Filter_Value_Null_Negative_Test()
        {
            var persons = new List<Person>
            {
                new Person("Tim Tayler", 30),
                new Person("Al Pachino", 70)
            }
            .AsQueryable();

            var result = persons.FilterBy("name", null!);

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void FilterBy_No_Property_Negative_Test()
        {
            var persons = new List<Person>
            {
                new Person("Tim Tayler", 30),
                new Person("Al Pachino", 70)
            }
            .AsQueryable();

            var result = persons.FilterBy("noProp", 13);

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void FilterBy_Wrong_Value_Type_Negative_Test()
        {
            var persons = new List<Person>
            {
                new Person("Tim Tayler", 30),
                new Person("Al Pachino", 70)
            }
            .AsQueryable();

            var result = persons.FilterBy("age", 30L);

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void Sort_By_String_Field_Asc_Positive_Test()
        {
            var persons = new List<Person>
            {
                new Person("Anton Chigurh", 41),
                new Person("Al Pachino", 70),
                new Person("Tim Tayler", 30),
                new Person("Iaac Azimov", 63)
            }
            .AsQueryable();

            var result = persons.SortBy("Name");
            var actual = JsonSerializer.Serialize<List<Person>>(result.ToList());
            var expected = JsonSerializer.Serialize<List<Person>>(
                new List<Person>
                {
                    new Person("Al Pachino", 70),
                    new Person("Anton Chigurh", 41),
                    new Person("Iaac Azimov", 63),
                    new Person("Tim Tayler", 30),
                }
            );

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_By_String_Field_Desc_Positive_Test()
        {
            var persons = new List<Person>
            {
                new Person("Anton Chigurh", 41),
                new Person("Al Pachino", 70),
                new Person("Tim Tayler", 30),
                new Person("Iaac Azimov", 63)
            }
            .AsQueryable();

            var result = persons.SortBy("Name", true);
            var actual = JsonSerializer.Serialize<List<Person>>(result.ToList());
            var expected = JsonSerializer.Serialize<List<Person>>(
                new List<Person>
                {
                    new Person("Tim Tayler", 30),
                    new Person("Iaac Azimov", 63),
                    new Person("Anton Chigurh", 41),
                    new Person("Al Pachino", 70),
                }
            );

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_By_Int_Field_Asc_Positive_Test()
        {
            var persons = new List<Person>
            {
                new Person("Anton Chigurh", 41),
                new Person("Al Pachino", 70),
                new Person("Tim Tayler", 30),
                new Person("Iaac Azimov", 63)
            }
            .AsQueryable();

            var result = persons.SortBy("Age");
            var actual = JsonSerializer.Serialize<List<Person>>(result.ToList());
            var expected = JsonSerializer.Serialize<List<Person>>(
                new List<Person>
                {
                    new Person("Tim Tayler", 30),
                    new Person("Anton Chigurh", 41),
                    new Person("Iaac Azimov", 63),
                    new Person("Al Pachino", 70),
                }
            );

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_By_Int_Field_Desc_Positive_Test()
        {
            var persons = new List<Person>
            {
                new Person("Anton Chigurh", 41),
                new Person("Al Pachino", 70),
                new Person("Tim Tayler", 30),
                new Person("Iaac Azimov", 63)
            }
            .AsQueryable();

            var result = persons.SortBy("Age", true);
            var actual = JsonSerializer.Serialize<List<Person>>(result.ToList());
            var expected = JsonSerializer.Serialize<List<Person>>(
                new List<Person>
                {
                    new Person("Al Pachino", 70),
                    new Person("Iaac Azimov", 63),
                    new Person("Anton Chigurh", 41),
                    new Person("Tim Tayler", 30),
                }
            );

            Assert.AreEqual(expected, actual);
        }
    }
}
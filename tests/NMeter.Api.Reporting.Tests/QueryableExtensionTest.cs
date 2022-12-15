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
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orient.Client;

namespace Orient.Tests.Query
{
    [TestClass]
    public class SqlGenerateSelectQueryTests
    {
        [TestMethod]
        public void ShouldGenerateSelectAlsoNthAsQuery()
        {
            string generatedQuery = new OSqlSelect()
                .Select("foo").As("Foo")
                .Also("bar").As("Bar")
                .Also("baq").Nth(0).As("Baq")
                .From("TestClass")
                .ToString();

            string query =
                "SELECT foo AS Foo, " +
                "bar AS Bar, " +
                "baq[0] AS Baq " +
                "FROM TestClass";

            Assert.AreEqual(generatedQuery, query);
        }

        [TestMethod]
        public void ShouldGenerateSelectWhereLimitQuery()
        {
            string generatedQuery = new OSqlSelect()
                .Select()
                .From("TestClass")
                .Where("foo").Equals("whoa")
                .Or("foo").NotEquals(123)
                .And("foo").Lesser(1)
                .And("foo").LesserEqual(2)
                .And("foo").Greater(3)
                .And("foo").GreaterEqual(4)
                .And("foo").Like("%whoa%")
                .And("foo").IsNull()
                .And("foo").Contains("johny")
                .And("foo").Contains("name", "johny")
                .And("foo").Between(1,2)
                .And("foo").In(new[]{1,2})
                .Limit(5)
                .ToString();

            string query =
                "SELECT " +
                "FROM TestClass " +
                "WHERE foo = 'whoa' " +
                "OR foo != 123 " +
                "AND foo < 1 " +
                "AND foo <= 2 " +
                "AND foo > 3 " +
                "AND foo >= 4 " +
                "AND foo LIKE '%whoa%' " +
                "AND foo IS NULL " +
                "AND foo CONTAINS 'johny' " +
                "AND foo CONTAINS (name = 'johny') " +
                "AND foo BETWEEN 1 AND 2 "+
                "AND foo IN [1,2] "+
                "LIMIT 5";

            Assert.AreEqual(generatedQuery, query);
        }

        [TestMethod]
        public void ShouldGenerateSelectFromDocumentOridQuery()
        {
            ODocument document = new ODocument();
            document.ORID = new ORID(8, 0);

            string generatedQuery = new OSqlSelect()
                .Select("foo", "bar")
                .From(document)
                .ToString();

            string query =
                "SELECT foo, bar " +
                "FROM #8:0";

            Assert.AreEqual(generatedQuery, query);
        }

        [TestMethod]
        public void ShouldGenerateSelectFromDocumentOClassNameQuery()
        {
            ODocument document = new ODocument();
            document.OClassName = "TestClass";

            string generatedQuery = new OSqlSelect()
                .Select("foo", "bar")
                .From(document)
                .ToString();

            string query =
                "SELECT foo, bar " +
                "FROM TestClass";

            Assert.AreEqual(generatedQuery, query);
        }

        [TestMethod]
        public void ShouldGenerateSelectFromOrderBySkipLimitQuery()
        {
            string generatedQuery = new OSqlSelect()
                .Select()
                .From("TestClass")
                .OrderBy("foo", "bar")
                .Skip(5)
                .Limit(10)
                .ToString();

            string query =
                "SELECT " +
                "FROM TestClass " +
                "ORDER BY foo, bar " +
                "SKIP 5 " +
                "LIMIT 10";

            Assert.AreEqual(generatedQuery, query);
        }

        [TestMethod]
        public void ShouldGenerateSelectFromOrderByAscendingQuery()
        {
            string generatedQuery = new OSqlSelect()
                .Select()
                .From("TestClass")
                .OrderBy("foo")
                .Ascending()
                .ToString();

            string query =
                "SELECT " +
                "FROM TestClass " +
                "ORDER BY foo ASC";

            Assert.AreEqual(generatedQuery, query);
        }

        [TestMethod]
        public void ShouldGenerateSelectFromOrderByDescendingQuery()
        {
            string generatedQuery = new OSqlSelect()
                .Select()
                .From("TestClass")
                .OrderBy("foo")
                .Descending()
                .ToString();

            string query =
                "SELECT " +
                "FROM TestClass " +
                "ORDER BY foo DESC";

            Assert.AreEqual(generatedQuery, query);
        }
    }
}

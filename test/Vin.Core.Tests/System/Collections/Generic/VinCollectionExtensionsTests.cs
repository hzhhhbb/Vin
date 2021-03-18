using Shouldly;
using Xunit;

namespace System.Collections.Generic
{
    public class VinCollectionExtensionsTests
    {
        [Fact]
        public void IsNullOrEmptyTest()
        {
            ICollection<int> source = null;

            source.IsNullOrEmpty().ShouldBe(true);
        }

        [Fact]
        public void AddIfNotContainsTest()
        {
            ICollection<int> source = new List<int>();
            source.Add(1);

            source.AddIfNotContains(2).ShouldBe(true);
            source.AddIfNotContains(2).ShouldBe(false);
            source.Count.ShouldBe(2);
        }

        [Fact]
        public void AddIfNotContainsTest1()
        {
            ICollection<int> source = new List<int>();
            source.Add(1);
            source.Add(2);

            ICollection<int> items = new List<int>();
            items.Add(1);
            items.Add(2);
            items.Add(3);

            source.AddIfNotContains(items).ShouldContain(3);
            source.Count.ShouldBe(3);
            source.ShouldContain(3);
        }

        [Fact]
        public void AddIfNotContainsTest2()
        {
            ICollection<int> source = new List<int>();
            source.Add(1);
            source.Add(2);
            var item = 3;

            source.AddIfNotContains(u => u == item, () => item).ShouldBe(true);
            source.AddIfNotContains(u => u == item, () => item).ShouldBe(false);
        }

        [Fact]
        public void RemoveAllTest()
        {
            ICollection<int> source = new List<int>();
            source.Add(1);
            source.Add(2);
            source.RemoveAll(u => u != 3);

            source.ShouldBeEmpty();
        }

        [Fact]
        public void RemoveAllTest1()
        {
            ICollection<int> source = new List<int>();
            source.Add(1);
            source.Add(2);
            source.Add(4);

            ICollection<int> items = new List<int>();
            items.Add(1);
            items.Add(2);
            items.Add(3);

            source.RemoveAll(items);
            source.Count.ShouldBe(1);
        }
    }
}
using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Vin
{
    public class Check_Tests
    {
        [Fact]
        public void NotNull_Test()
        {
            string str = null;
            Should.Throw(() => Check.NotNull(str, nameof(str)), typeof(ArgumentNullException));

            str = "Vincent";
            Check.NotNull(str, nameof(str)).ShouldBe(str);
        }

        [Fact]
        public void NotNull_Test1()
        {
            string str = null;
            Should.Throw(() => Check.NotNull(str, nameof(str), "null"), typeof(ArgumentNullException), "null");

            str = "Vincent";
            Check.NotNull(str, nameof(str), "not null").ShouldBe(str);
        }

        [Fact]
        public void NotNull_Test2()
        {
            string str = null;
            Should.Throw(() => Check.NotNull(str, nameof(str), 0), typeof(ArgumentException));

            str = "Vincent";
            Should.Throw(() => Check.NotNull(str, nameof(str), 6), typeof(ArgumentException));
            Should.Throw(() => Check.NotNull(str, nameof(str), 7, 8), typeof(ArgumentException));
            Check.NotNull(str, nameof(str), 7, 6).ShouldBe(str);
        }

        [Fact]
        public void NotNullOrWhiteSpace_Test()
        {
            string str = null;
            Should.Throw(() => Check.NotNullOrWhiteSpace(str, nameof(str)), typeof(ArgumentException));

            str = "Vincent";
            Should.Throw(() => Check.NotNullOrWhiteSpace(str, nameof(str), 6), typeof(ArgumentException));
            Should.Throw(() => Check.NotNullOrWhiteSpace(str, nameof(str), 7, 8), typeof(ArgumentException));
            Check.NotNullOrWhiteSpace(str, nameof(str), 7, 6).ShouldBe(str);
        }

        [Fact]
        public void NotNullOrEmpty_Test()
        {
            string str = null;
            Should.Throw(() => Check.NotNullOrEmpty(str, nameof(str)), typeof(ArgumentException));

            str = "Vincent";
            Should.Throw(() => Check.NotNullOrEmpty(str, nameof(str), 6), typeof(ArgumentException));
            Should.Throw(() => Check.NotNullOrEmpty(str, nameof(str), 7, 8), typeof(ArgumentException));
            Check.NotNullOrEmpty(str, nameof(str), 7, 6).ShouldBe(str);
        }

        [Fact]
        public void NotNullOrEmpty_Test1()
        {
            ICollection<int> collection = null;
            Should.Throw(() => Check.NotNullOrEmpty(collection, nameof(collection)), typeof(ArgumentException));

            collection = new List<int>();
            collection.Add(1);
            Check.NotNullOrEmpty(collection, nameof(collection)).ShouldBe(collection);
        }

        [Fact]
        public void AssignableTo_Test()
        {
            var type = typeof(MyClass1);
            Check.AssignableTo<ClassBase>(type, nameof(type)).ShouldBe(type);

            Should.Throw(() => Check.AssignableTo<OtherClass>(type, nameof(type)).ShouldBe(type), typeof(ArgumentException));

            type = typeof(ClassBase);
            Should.Throw(() => Check.AssignableTo<MyClass1>(type, nameof(type)).ShouldBe(type), typeof(ArgumentException));
        }

        [Fact]
        public void Length_Test()
        {
            string str = "Vincent";
            Check.Length(str, nameof(str), 7);
            Should.Throw(() =>Check.Length(str, nameof(str), 6),typeof(ArgumentException));
            Should.Throw(() =>Check.Length(str, nameof(str), 7,8),typeof(ArgumentException));

            str = string.Empty;
            Should.Throw(() =>Check.Length(str, nameof(str), 7,8),typeof(ArgumentException));
        }

        public class MyClass1 : ClassBase
        {
        }

        public abstract class ClassBase
        {
        }

        public class OtherClass
        {
        }
    }
}
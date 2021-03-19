using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit.Abstractions;

namespace System
{
    public class VinTypeExtensions_Tests
    {
        public VinTypeExtensions_Tests(ITestOutputHelper output)
        {
            this.Output = output;
        }

        public ITestOutputHelper Output { get; set; }

        [Fact()]
        public void GetFullNameWithAssemblyName_Test()
        {
            this.GetType().GetFullNameWithAssemblyName().ShouldBe("System.VinTypeExtensions_Tests, Vin.Core.Tests");
        }

        [Fact()]
        public void GetBaseClasses_Test()
        {
            var baseClasses = typeof(MyClass).GetBaseClasses(includeObject: false);
            baseClasses.Length.ShouldBe(2);
            baseClasses[0].ShouldBe(typeof(MyBaseClass1));
            baseClasses[1].ShouldBe(typeof(MyBaseClass2));
        }

        [Fact()]
        public void GetBaseClasses_Test1()
        {
            var baseClasses = typeof(MyClass).GetBaseClasses(typeof(MyBaseClass1));
            baseClasses.Length.ShouldBe(1);
            baseClasses[0].ShouldBe(typeof(MyBaseClass2));
        }

        public abstract class MyBaseClass1
        {

        }

        public class MyBaseClass2 : MyBaseClass1
        {

        }

        public class MyClass : MyBaseClass2
        {

        }
    }
}
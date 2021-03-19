using System.Text;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace System
{
    public class VinStringExtensionsTests
    {
        public VinStringExtensionsTests(ITestOutputHelper output)
        {
            this.Output = output;
        }

        public ITestOutputHelper Output { get; set; }

        [Fact]
        public void EnsureEndsWithTest()
        {
            "Vincent!".EnsureEndsWith('!').ShouldBe("Vincent!");
            "Vincent".EnsureEndsWith('!').ShouldBe("Vincent!");
            string s = null;
            Should.Throw(() => s.EnsureEndsWith('!'), typeof(ArgumentNullException));
        }

        [Fact]
        public void EnsureStartsWithTest()
        {
            "Vincent".EnsureStartsWith('V').ShouldBe("Vincent");
            "incent".EnsureStartsWith('V').ShouldBe("Vincent");
            string s = null;
            Should.Throw(() => s.EnsureStartsWith('!'), typeof(ArgumentNullException));
        }

        [Fact]
        public void IsNullOrEmptyTest()
        {
            string.Empty.IsNullOrEmpty().ShouldBeTrue();
            ((string) null).IsNullOrEmpty().ShouldBeTrue();
            "   ".IsNullOrEmpty().ShouldBeFalse();
            "Vincent".IsNullOrEmpty().ShouldBeFalse();
        }

        [Fact]
        public void IsNullOrWhiteSpaceTest()
        {
            string.Empty.IsNullOrWhiteSpace().ShouldBeTrue();
            ((string) null).IsNullOrWhiteSpace().ShouldBeTrue();
            "   ".IsNullOrWhiteSpace().ShouldBeTrue();
            "Vincent".IsNullOrWhiteSpace().ShouldBeFalse();
        }

        [Fact]
        public void LeftTest()
        {
            string s = null;
            Should.Throw(() => s.Left(0), typeof(ArgumentNullException));

            "Vincent".Left(1).ShouldBe("V");

            Should.Throw(() => "Vincent".Left(8), typeof(ArgumentOutOfRangeException));
            Should.Throw(() => "Vincent".Left(-1), typeof(ArgumentOutOfRangeException));

            "中文博大精深啊".Left(2).ShouldBe("中文");
        }

        [Fact]
        public void NormalizeLineEndingsTest()
        {
            var str = "This\r\n is a\r test \n string";
            str = str.NormalizeLineEndings();
            str.SplitToLines().Length.ShouldBe(4);
            this.Output.WriteLine(str);
        }

        [Fact]
        public void NthIndexOfTest()
        {
            const string str = "This is a test string";

            str.NthIndexOf('i', 0).ShouldBe(-1);
            str.NthIndexOf('i', 1).ShouldBe(2);
            str.NthIndexOf('i', 2).ShouldBe(5);
            str.NthIndexOf('i', 3).ShouldBe(18);
            str.NthIndexOf('i', 4).ShouldBe(-1);
        }

        [Fact]
        public void RemovePostFixTest()
        {
            //null case
            (null as string).RemovePostFix("Test").ShouldBeNull();

            //Simple case
            "MyTestAppService".RemovePostFix("AppService").ShouldBe("MyTest");
            "MyTestAppService".RemovePostFix("Service").ShouldBe("MyTestApp");

            //Multiple postfix (orders of postfixes are important)
            "MyTestAppService".RemovePostFix("AppService", "Service").ShouldBe("MyTest");
            "MyTestAppService".RemovePostFix("Service", "AppService").ShouldBe("MyTestApp");

            //Ignore case
            "TestString".RemovePostFix(StringComparison.OrdinalIgnoreCase, "string").ShouldBe("Test");
            "TestString".RemovePostFix(StringComparison.OrdinalIgnoreCase, "string1", "string").ShouldBe("Test");
            //Unmatched case
            "MyTestAppService".RemovePostFix("Unmatched").ShouldBe("MyTestAppService");

            "TestString".RemovePostFix()
        }

        [Fact]
        public void RemovePreFixTest()
        {
            (null as string).RemovePreFix("Test").ShouldBeNull();
            "".RemovePreFix("Test").ShouldBeEmpty();

            "".RemovePreFix("").ShouldBeEmpty();

            //Ignore case
            "TestString".RemovePreFix(StringComparison.OrdinalIgnoreCase).ShouldBe("TestString");
            "TestString".RemovePreFix(StringComparison.OrdinalIgnoreCase, "test").ShouldBe("String");
            "TestString".RemovePreFix(StringComparison.OrdinalIgnoreCase, "test1", "test").ShouldBe("String");

            "TestString".RemovePreFix("TT").ShouldBe("TestString");
            "TestString".RemovePreFix("").ShouldBe("TestString");
        }

        [Fact]
        public void ReplaceFirstTest()
        {
            "Test string".ReplaceFirst("s", "X").ShouldBe("TeXt string");
            "Test test test".ReplaceFirst("test", "XX").ShouldBe("Test XX test");
            "Test test test".ReplaceFirst("test", "XX", StringComparison.OrdinalIgnoreCase).ShouldBe("XX test test");
        }

        [Fact]
        public void RightTest()
        {
            string s = null;
            Should.Throw(() => s.Right(0), typeof(ArgumentNullException));

            "Vincent".Right(1).ShouldBe("t");

            Should.Throw(() => "Vincent".Right(8), typeof(ArgumentOutOfRangeException));
            Should.Throw(() => "Vincent".Right(-1), typeof(ArgumentOutOfRangeException));
        }


        [Fact]
        public void ToCamelCaseTest()
        {
            (null as string).ToCamelCase().ShouldBe(null);
            "HelloWorld".ToCamelCase().ShouldBe("helloWorld");
            "Istanbul".ToCamelCase().ShouldBe("istanbul");
            "中文博大精深".ToCamelCase().ShouldBe("中文博大精深");
        }

        [Fact]
        public void ToSentenceCaseTest()
        {
            (null as string).ToSentenceCase().ShouldBe(null);
            "HelloWorld".ToSentenceCase().ShouldBe("Hello world");
            "HelloIsparta".ToSentenceCase().ShouldBe("Hello isparta");
            "ThisIsSampleSentence".ToSentenceCase().ShouldBe("This is sample sentence");
            "thisIsSampleSentence".ToSentenceCase().ShouldBe("this is sample sentence");
            "中文博大精深".ToSentenceCase().ShouldBe("中文博大精深");
        }

        [Fact]
        public void ToKebabCaseTest()
        {
            (null as string).ToKebabCase().ShouldBe(null);
            "helloMoon".ToKebabCase().ShouldBe("hello-moon");
            "HelloWorld".ToKebabCase().ShouldBe("hello-world");
            "HelloIsparta".ToKebabCase().ShouldBe("hello-isparta");
            "ThisIsSampleText".ToKebabCase().ShouldBe("this-is-sample-text");
            "中文博大精深".ToKebabCase().ShouldBe("中文博大精深");
        }

        [Fact]
        public void ToSnakeCaseTest()
        {
            (null as string).ToSnakeCase().ShouldBe(null);
            "helloMoon".ToSnakeCase().ShouldBe("hello_moon");
            "HelloWorld".ToSnakeCase().ShouldBe("hello_world");
            "HelloIsparta".ToSnakeCase().ShouldBe("hello_isparta");
            "ThisIsSampleText".ToSnakeCase().ShouldBe("this_is_sample_text");
        }

        [Fact]
        public void ToPascalCaseTest()
        {
            (null as string).ToPascalCase().ShouldBe(null);
            "helloWorld".ToPascalCase().ShouldBe("HelloWorld");
            "istanbul".ToPascalCase().ShouldBe("Istanbul");
        }

        [Fact]
        public void ToPascalCase_CurrentCulture_Test()
        {
            "istanbul".ToPascalCase(true).ShouldBe("Istanbul");
        }

        [Fact]
        public void ToEnumTest()
        {
            "MyValue1".ToEnum<MyEnum>().ShouldBe(MyEnum.MyValue1);
            "MyValue2".ToEnum<MyEnum>().ShouldBe(MyEnum.MyValue2);
            "myValue2".ToEnum<MyEnum>(true).ShouldBe(MyEnum.MyValue2);
        }

        [Fact]
        public void ToMd5Test()
        {
            var str = "Vincent";
            var md5 = str.ToMd5();
            md5.ShouldBe("D3FACF360F0B4F2D570C093E7E130210");
        }

        [Fact]
        public void TruncateTest()
        {
            const string str = "This is a test string";
            const string nullValue = null;

            str.Truncate(7).ShouldBe("This is");
            str.Truncate(0).ShouldBe("");
            str.Truncate(100).ShouldBe(str);

            nullValue.Truncate(5).ShouldBe(null);
        }

        [Fact]
        public void TruncateFromBeginningTest()
        {
            const string str = "This is a test string";
            const string nullValue = null;

            str.TruncateFromBeginning(7).ShouldBe(" string");
            str.TruncateFromBeginning(0).ShouldBe("");
            str.TruncateFromBeginning(100).ShouldBe(str);

            nullValue.Truncate(5).ShouldBe(null);
        }

        [Fact]
        public void TruncateWithPostfixTest()
        {
            const string str = "This is a test string";
            const string nullValue = null;

            str.TruncateWithPostfix(3).ShouldBe("...");
            str.TruncateWithPostfix(12).ShouldBe("This is a...");
            str.TruncateWithPostfix(0).ShouldBe("");
            str.TruncateWithPostfix(100).ShouldBe(str);

            nullValue.Truncate(5).ShouldBe(null);

            str.TruncateWithPostfix(3, "~").ShouldBe("Th~");
            str.TruncateWithPostfix(12, "~").ShouldBe("This is a t~");
            str.TruncateWithPostfix(0, "~").ShouldBe("");
            str.TruncateWithPostfix(100, "~").ShouldBe(str);

            nullValue.TruncateWithPostfix(5, "~").ShouldBe(null);
        }

        [Theory]
        [InlineData("")]
        [InlineData("MyStringİ")]
        public void GetBytesTest(string str)
        {
            this.Output.WriteLine(str);
            var bytes = str.GetBytes();
            bytes.ShouldNotBeNull();
            bytes.Length.ShouldBeGreaterThanOrEqualTo(str.Length);
            Encoding.UTF8.GetString(bytes).ShouldBe(str);
        }

        [Theory]
        [InlineData("")]
        [InlineData("MyString")]
        public void GetBytes_With_Encoding_Test(string str)
        {
            var bytes = str.GetBytes(Encoding.ASCII);
            bytes.ShouldNotBeNull();
            bytes.Length.ShouldBeGreaterThanOrEqualTo(str.Length);
            Encoding.ASCII.GetString(bytes).ShouldBe(str);
        }

        private enum MyEnum
        {
            MyValue1,
            MyValue2
        }
    }
}
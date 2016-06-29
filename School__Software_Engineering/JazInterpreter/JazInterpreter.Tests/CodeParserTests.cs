using NUnit.Framework;

namespace JazInterpreter.Tests
{
    [TestFixture]
    public class CodeParserTests
    {
        CodeParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new CodeParser();
        }
    }
}

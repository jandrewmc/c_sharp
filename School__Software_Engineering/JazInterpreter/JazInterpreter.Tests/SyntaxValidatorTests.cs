using JazInterpreter.Interfaces;
using NUnit.Framework;

namespace JazInterpreter.Tests
{
    public class SyntaxValidatorTests
    {
        private ISyntaxValidator syntaxValidator;

        [SetUp]
        public void SetUp()
        {
            syntaxValidator = new SyntaxValidator();
        }

        [Test]
        public void ValidateChecksGotoInstructions()
        {
            string[,] code = { { "goto", "foo" }, { "label", "bar" } };


            Assert.Throws<SyntaxError>(() => syntaxValidator.Validate(code));
        }

        [Test]
        public void ValidateChecksCallInstructions()
        {
            string[,] code = { { "call", "foo" }, { "label", "bar" } };


            Assert.Throws<SyntaxError>(() => syntaxValidator.Validate(code));
        }

        [Test]
        public void ValidateChecksGoFalseInstructions()
        {
            string[,] code = { { "gofalse", "foo" }, { "label", "bar" } };


            Assert.Throws<SyntaxError>(() => syntaxValidator.Validate(code));
        }

        [Test]
        public void ValidateChecksGoTrueInstructions()
        {
            string[,] code = { { "gotrue", "foo" }, { "label", "bar" } };


            Assert.Throws<SyntaxError>(() => syntaxValidator.Validate(code));
        }

        [Test]
        public void ValidateThrowsASyntaxErrorWhenOnlyOneOfTheLabelsDoesNotExist()
        {
            string[,] code = { { "gotrue", "foo" }, { "label", "foo" }, { "gofalse", "baz" }, { "label", "whatsthis" } };


            Assert.Throws<SyntaxError>(() => syntaxValidator.Validate(code));
        }

        [Test]
        public void ValidateThrowsAnExceptionWhenThereIsAnotherBeginWithoutAnEnd()
        {
            string[,] code = { { "begin", "" }, { "begin", "" } };

            Assert.Throws<SyntaxError>(() => syntaxValidator.Validate(code));
        }

        [Test]
        public void ValidateThrowsAnExceptionWhenThereIsAnotherEndWithoutABegin()
        {
            string[,] code = { { "end", "" }, { "end", "" } };

            Assert.Throws<SyntaxError>(() => syntaxValidator.Validate(code));
        }
    }
}
using System.Collections;
using NUnit.Framework;

namespace JazInterpreter.Tests
{
    [TestFixture]
    public class StackManipulationTests
    {
        private TestStackManipulation module;

        [SetUp]
        public void SetUp()
        {
            module = new TestStackManipulation();
        }

        [Test]
        public void PushPushesTheItemToTheStack()
        {
            int itemToPush = 20;

            module.Push(itemToPush);

            Assert.That(module.InternalStack.Pop(), Is.EqualTo(itemToPush));
        }

        [Test]
        public void RValuePushesTheIdentifiersValueToTheStack()
        {
            var expected = 100;
            Identifier identifier = new Identifier
            {
                Value = expected	
            };

            module.RValue(identifier);

            Assert.That(module.InternalStack.Pop(), Is.EqualTo(expected));
        }

        [Test]
        public void LValuePushesTheIdentifierToTheStack()
        {
            Identifier expected = new Identifier();

            module.LValue(expected);

            Assert.That(module.InternalStack.Pop(), Is.SameAs(expected));
        }

        [Test]
        public void PopThrowsAnExceptionWhenTheStackIsEmpty()
        {
            Assert.Throws<UnderflowException>(() => module.Pop());
        }

        [Test]
        public void PeekReturnsTheTopElementOnTheStack()
        {
            int expected = 800;
            module.Push(expected);

            int actual = (int)module.Peek();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void PeekThrowsAnExceptionWhenTheStackIsEmpty()
        {
            Assert.Throws<UnderflowException>(() => module.Peek());
        }

        [Test]
        public void ColonEqualsAssignsTheMostRecentlyPushedValueToTheMostPreviouslyPushedIdentifier()
        {
            Identifier identifier = new Identifier();
            int expected = 50;
            module.LValue(identifier);
            module.Push(expected);

            module.ColonEquals();

            Assert.That(identifier.Value, Is.EqualTo(expected));
        }

        [Test]
        public void ColonEqualsThrowsAMissingLValueExceptionWhenTheSecondMostElementIsNotAnIdentifier()
        {
            module.Push(5);
            module.Push(6);

            Assert.Throws<MissingLValueException>(() => module.ColonEquals());
        }

        [Test]
        public void CopyCopiesTheTopElementOfTheStack()
        {
            int value = 10;
            module.Push(value);

            module.Copy();

            Assert.That(module.InternalStack.Pop(), Is.EqualTo(value));
            Assert.That(module.InternalStack.Pop(), Is.EqualTo(value));
        }
    }

    class TestStackManipulation : StackManipulation
    {
        public Stack InternalStack => Stack;
    }
}


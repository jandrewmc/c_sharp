using JazInterpreter.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace JazInterpreter.Tests
{
    [TestFixture]
    public class ArithmeticOperatorsTests
    {
        private IStackManipulation stackManipulation;
        private IArithmeticOperators operators;

        [SetUp]
        public void SetUp()
        {
            stackManipulation = MockRepository.GenerateMock<IStackManipulation>();
            operators = new ArithmeticOperators(stackManipulation);
        }

        [Test]
        public void AddAddsTheTopTwoValuesOnTheStack()
        {
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(5);
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(15);

            operators.Add();


            stackManipulation.AssertWasCalled(x => x.Push(Arg<int>.Is.Equal(20)));
        }

        [Test]
        public void SubtractSubtractsTheTopTwoValuesOnTheStack()
        {
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(10);
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(50);

            operators.Subtract();

            stackManipulation.AssertWasCalled(x => x.Push(Arg<int>.Is.Equal(40)));
        }

        [Test]
        public void MultiplyMultipliesTheTopTwoValuesOnTheStack()
        {
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(10);
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(20);

            operators.Multiply();

            stackManipulation.AssertWasCalled(x => x.Push(Arg<int>.Is.Equal(200)));
        }

        [Test]
        public void DivideDividesTheTopTwoValuesOnTheStack()
        {
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(10);
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(31);

            operators.Divide();

            stackManipulation.AssertWasCalled(x => x.Push(Arg<int>.Is.Equal(3)));
        }

        [Test]
        public void ModCalculatesTheModulusOfTheTopTwoValuesOnTheStack()
        {
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(10);
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(38);

            operators.Mod();

            stackManipulation.AssertWasCalled(x => x.Push(Arg<int>.Is.Equal(8)));
        }
    }
}
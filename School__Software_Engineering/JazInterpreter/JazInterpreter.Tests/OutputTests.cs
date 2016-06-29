using JazInterpreter.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace JazInterpreter.Tests
{
    public class OutputTests
    {
        private IStackManipulation stackManipulation;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            stackManipulation = MockRepository.GenerateMock<IStackManipulation>();
            output = new Output(stackManipulation);
        }

        // I can't test the output of Console.WriteLine :(
        [Test]
        public void PrintWritesTheTopOfTheStackToTheConsole()
        {
            int expected = 10;
            stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(expected);

            output.Print();

            stackManipulation.AssertWasCalled(x => x.Pop());
        }
    }
}


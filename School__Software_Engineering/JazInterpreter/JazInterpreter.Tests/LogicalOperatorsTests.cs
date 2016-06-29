using JazInterpreter.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace JazInterpreter.Tests
{
	[TestFixture]
	public class LogicalOperatorsTests
	{
		private IStackManipulation stackManipulation = MockRepository.GenerateMock<IStackManipulation>();
		private ILogicalOperators operators;

		[SetUp]
		public void SetUp() {
			operators = new LogicalOperators(stackManipulation);
		}

		[Test]
		public void AndPerformsALogicalAndOnTheTopTwoValues() {
			stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(1);
			stackManipulation.Expect(x => x.Peek()).Repeat.Once().Return(0);

			operators.And();

			stackManipulation.AssertWasCalled(x => x.Push(Arg<int>.Is.Equal(0)));
		}
	}
}


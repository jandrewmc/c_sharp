using System;
using JazInterpreter.Interfaces;

namespace JazInterpreter
{
    public class LogicalOperators : ILogicalOperators
    {
        private readonly IStackManipulation stackManipulation;

        public LogicalOperators(IStackManipulation stackManipulation)
        {
            this.stackManipulation = stackManipulation;
        }

        private Tuple<int, int> GetTopTwoValues()
        {
            int firstValue = PeekAndPop();
            int secondValue = PeekAndPop();

            return Tuple.Create(firstValue, secondValue);
        }

        private int PeekAndPop()
        {
            int value = (int)stackManipulation.Peek();
            stackManipulation.Pop();

            return value;
        }

        public void And()
        {
            var values = GetTopTwoValues();

            bool result = Convert.ToBoolean(values.Item1) && Convert.ToBoolean(values.Item2);

            stackManipulation.Push(Convert.ToInt32(result));
        }

        public void Not()
        {
            int value = PeekAndPop();

            bool result = !Convert.ToBoolean(value);

            stackManipulation.Push(Convert.ToInt32(result));
        }

        public void Or()
        {
            var values = GetTopTwoValues();

            bool result = Convert.ToBoolean(values.Item1) || Convert.ToBoolean(values.Item2);

            stackManipulation.Push(Convert.ToInt32(result));
        }
    }
}


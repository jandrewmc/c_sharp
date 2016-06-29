using System;
using JazInterpreter.Interfaces;

namespace JazInterpreter
{
    public class RelationalOperators : IRelationalOperators
    {
        private readonly IStackManipulation stackManipulation;

        public RelationalOperators(IStackManipulation stackManipulation)
        {
            this.stackManipulation = stackManipulation;
        }

        private Tuple<int, int> GetTopTwoValues()
        {
            int firstValue = PeekAndPop();
            int secondValue = PeekAndPop();

            return Tuple.Create(secondValue, firstValue);
        }

        private int PeekAndPop()
        {
            int value = (int)stackManipulation.Peek();
            stackManipulation.Pop();

            return value;
        }

        public void Equal()
        {
            var values = GetTopTwoValues();

            PushBooleanToStack(values.Item1 == values.Item2);
        }

        private void PushBooleanToStack(bool value)
        {
            stackManipulation.Push(Convert.ToInt32(value));
        }

        public void LessThanOrEqualTo()
        {
            var values = GetTopTwoValues();

            PushBooleanToStack(values.Item1 <= values.Item2);
        }

        public void GreaterThanOrEqualTo()
        {
            var values = GetTopTwoValues();

            PushBooleanToStack(values.Item1 >= values.Item2);
        }

        public void LessThan()
        {
            var values = GetTopTwoValues();

            PushBooleanToStack(values.Item1 < values.Item2);
        }

        public void GreaterThan()
        {
            var values = GetTopTwoValues();

            PushBooleanToStack(values.Item1 > values.Item2);
        }

        public void NotEqual()
        {
            var values = GetTopTwoValues();

            PushBooleanToStack(values.Item1 != values.Item2);
        }
    }
}


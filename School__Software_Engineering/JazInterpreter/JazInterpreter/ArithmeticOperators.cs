using System;
using JazInterpreter.Interfaces;

namespace JazInterpreter
{
    public class ArithmeticOperators : IArithmeticOperators
    {
        private readonly IStackManipulation stackManipulation;

        public ArithmeticOperators(IStackManipulation stackManipulation)
        {
            this.stackManipulation = stackManipulation;
        }

        public void Add()
        {
            var values = GetTopTwoValues();

            stackManipulation.Push(values.Item1 + values.Item2);
        }

        private Tuple<int, int> GetTopTwoValues()
        {
            int value1 = GetAndRemoveTopOfStack();
            int value2 = GetAndRemoveTopOfStack();

            return Tuple.Create(value2, value1);
        }

        private int GetAndRemoveTopOfStack()
        {
            int value = (int)stackManipulation.Peek();
            stackManipulation.Pop();

            return value;
        }

        public void Subtract()
        {
            var values = GetTopTwoValues();

            stackManipulation.Push(values.Item1 - values.Item2);
        }

        public void Multiply()
        {
            var values = GetTopTwoValues();

            stackManipulation.Push(values.Item1 * values.Item2);
        }

        public void Divide()
        {
            var values = GetTopTwoValues();

            stackManipulation.Push(values.Item1 / values.Item2);
        }

        public void Mod()
        {
            var values = GetTopTwoValues();

            stackManipulation.Push(values.Item1 % values.Item2);
        }
    }
}


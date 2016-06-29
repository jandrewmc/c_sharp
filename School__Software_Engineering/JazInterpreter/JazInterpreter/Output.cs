using System;
using JazInterpreter.Interfaces;

namespace JazInterpreter
{
    public class Output : IOutput
    {
        private readonly IStackManipulation stackManipulation;

        public Output(IStackManipulation stackManipulation)
        {
            this.stackManipulation = stackManipulation;
        }

        private int Peek()
        {
            int value = (int)stackManipulation.Peek();

            return value;
        }

        public void Print()
        {
            Console.WriteLine(Peek());
        }

        public void Show(string line)
        {
            Console.WriteLine(line);
        }
    }
}


using System;
using System.Collections;
using JazInterpreter.Interfaces;

namespace JazInterpreter
{
    public class StackManipulation : IStackManipulation
    {
        protected Stack Stack { get; set; }

        public StackManipulation()
        {
            Stack = new Stack();
        }

        public void Push(int value)
        {
            Stack.Push(value);
        }

        public void RValue(Identifier identifier)
        {
            Stack.Push(identifier.Value);
        }

        public void LValue(Identifier identifier)
        {
            Stack.Push(identifier);
        }

        public void Pop()
        {
            if (Stack.Count == 0)
            {
                throw new UnderflowException();
            }

            Stack.Pop();
        }

        public object Peek()
        {
            if (Stack.Count == 0)
            {
                throw new UnderflowException();
            }

            return Stack.Peek();
        }

		public int PeekAndPop()
		{
			int value = (int)Peek();
			Pop();

			return value;
		}

        public void ColonEquals()
        {
            try
            {
                int value = (int)Stack.Peek();
                Stack.Pop();
                Identifier identifier = (Identifier)Stack.Peek();
                Stack.Pop();

                identifier.Value = value;
            }
            catch (InvalidCastException)
            {
                throw new MissingLValueException();
            }
        }

        public void Copy()
        {
            Stack.Push(Stack.Peek());
        }
    }
}


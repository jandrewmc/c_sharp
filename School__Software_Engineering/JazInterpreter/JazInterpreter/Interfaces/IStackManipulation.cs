namespace JazInterpreter.Interfaces
{
    public interface IStackManipulation
    {
        void Push(int value);

        void RValue(Identifier identifier);

        void LValue(Identifier identifier);

        void Pop();

        object Peek();

        void ColonEquals();

        void Copy();
    }
}


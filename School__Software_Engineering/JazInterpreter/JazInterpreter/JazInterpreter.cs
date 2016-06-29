/*
 * Jaz Interpreter
 * By Mike Koch and J Andrew McCormick
 * Submitted 3/15/16
 * EECS 3550
 * 
 */

/*
 * This is our main module that handles getting the filename as well as
 * parsing the code and interpreting it.
 */

using System;
using System.IO;

namespace JazInterpreter
{
    class JazInterpreter
    {
		// This method gets the filename from the user, if one is not specified as an argument
		// then it asks the user to enter one.

        private string getFileName(string[] args)
        {
			string filename;
			while (true)
			{
				//get name of file to be read in
				if (args.Length == 0)
				{
					while (true)
					{
						Console.Write ("Please enter the location of the file you wish to interpret: ");
						filename = Console.ReadLine ();
						if (System.IO.File.Exists (filename))
						{
							break;
						}
						Console.WriteLine ("That file does not exist!");
					}
				} else
				{
					filename = args [0];
				}
				if (File.Exists(filename))
				{
					break;
				}
				else
				{
					Console.WriteLine ("The filename you specified does not exist!");
				}
			}
        	return filename;
        }

		// The main method.  Here we set-up the entire file and execute it.

        public static void Main(string[] args)
        {
			try
			{
	            StackManipulation stack = new StackManipulation();
				ArithmeticOperators arOp = new ArithmeticOperators(stack);
	            LogicalOperators loOp = new LogicalOperators(stack);
	            RelationalOperators relOp = new RelationalOperators(stack);
	            Output output = new Output(stack);

				// Get the filename, parse the code, and analyze it
	            string filename = (new JazInterpreter()).getFileName(args);
	            string[,] array = (new CodeParser()).Parse(filename);
	            (new Analyzer()).Analyze(array);

				// instruction pointer is used to decide where we are in the code
	            int instructionPointer = 0;

				// defines current variable scope
	            int currentLevel = 0;

				// flags for scoping
				bool isAfterBeginButBeforeCall = false;
				bool isAfterCallButBeforeEnd = false;

	            while (true)
	            {
	                switch (array[instructionPointer, 0])
	                {
	                    case "push":
	                        stack.Push(Int32.Parse(array[instructionPointer, 1]));
	                        break;
	                    case "rvalue":
							if (isAfterCallButBeforeEnd)
							{
								Identifier identifier = SymbolsTable.VariableTable[currentLevel + 1].Find(x => x.Name == array[instructionPointer, 1]);
								stack.RValue(identifier);		
							}
							else
							{
						   		Identifier identifier = SymbolsTable.VariableTable[currentLevel].Find(x => x.Name == array[instructionPointer, 1]);
	                       		stack.RValue(identifier);	
							}
	                        break;
	                    case "lvalue":
							if (isAfterBeginButBeforeCall)
							{
								Identifier identifier2 = SymbolsTable.VariableTable[currentLevel + 1].Find(x => x.Name == array[instructionPointer, 1]);
								stack.LValue(identifier2);		
							}
							else
							{
								Identifier identifier2 = SymbolsTable.VariableTable[currentLevel].Find(x => x.Name == array[instructionPointer, 1]);
								stack.LValue(identifier2);	
							}
	                        break;
	                    case "pop":
	                        stack.Pop();
	                        break;
	                    case ":=":
	                        stack.ColonEquals();
	                        break;
	                    case "copy":
	                        stack.Copy();
	                        break;
	                    case "label":
	                        break;
	                    case "goto":
	                        instructionPointer = ControlFlow.Goto(array[instructionPointer, 1]);
	                        break;
	                    case "gofalse":
	                        if (!Convert.ToBoolean(stack.Peek()))
	                            instructionPointer = ControlFlow.Gofalse(array[instructionPointer, 1]);
	                        stack.Pop();
	                        break;
	                    case "gotrue":
	                        if (Convert.ToBoolean(stack.Peek()))
	                            instructionPointer = ControlFlow.Gotrue(array[instructionPointer, 1]);
	                        stack.Pop();
	                        break;
	                    case "halt":
	                        return;
	                    case "+":
	                        arOp.Add();
	                        break;
	                    case "-":
	                        arOp.Subtract();
	                        break;
	                    case "*":
	                        arOp.Multiply();
	                        break;
	                    case "/":
	                        arOp.Divide();
	                        break;
	                    case "div":
	                        arOp.Mod();
	                        break;
	                    case "&":
	                        loOp.And();	
	                        break;
	                    case "!":
	                        loOp.Not();
	                        break;
	                    case "|":
	                        loOp.Or();
	                        break;
	                    case "<>":
	                        relOp.NotEqual();				
	                        break;
	                    case "<=":
	                        relOp.LessThanOrEqualTo();
	                        break;
	                    case ">=":
	                        relOp.GreaterThanOrEqualTo();
	                        break;
	                    case "<":
	                        relOp.LessThan();
	                        break;
	                    case ">":
	                        relOp.GreaterThan();
	                        break;
	                    case "=":
	                        relOp.Equal();
	                        break;
	                    case "print":
	                        output.Print();
	                        break;
	                    case "show":
	                        output.Show(array[instructionPointer, 1]);
	                        break;
					case "begin":
						isAfterBeginButBeforeCall = true;
						SymbolsTable.AddLevel (currentLevel);

	                        break;
					case "end":
							//need to remove level
						SymbolsTable.VariableTable.RemoveAt (currentLevel + 1);
							isAfterBeginButBeforeCall = false;
							isAfterCallButBeforeEnd = false;
	                        break;
					case "return":
						//return needs to decrement the leve and restore everything
						currentLevel--;
						instructionPointer = stack.PeekAndPop ();
						isAfterBeginButBeforeCall = false;
						isAfterCallButBeforeEnd = true;
	                        break;
					case "call":
						//call needs to save the current instruction pointer, set the instruction pointer to the next level,
						//increment the level, reset the boolean flags
						stack.Push (instructionPointer);
						isAfterBeginButBeforeCall = false;
						isAfterCallButBeforeEnd = false;
						currentLevel++;
						instructionPointer = SymbolsTable.LabelTable [array [instructionPointer, 1]];

	                        break;
					case "":
						break;
	                    default:
	                        Console.WriteLine("YOU DID SOMETHING VERY BAD");
	                        break;
	                }
	                instructionPointer++;
	            }
			} catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }
    }
}

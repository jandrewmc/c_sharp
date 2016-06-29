// validates the syntax of the code read in from the file
// Simple things like: there exists a label for go's and calls
// matching begin and end statements,
// and that the instructions given are valid

using System;
using System.Collections.Generic;
using JazInterpreter.Interfaces;

namespace JazInterpreter
{
    public class SyntaxValidator : ISyntaxValidator
    {
		public List<string> validInstructions;
        private static void CheckForMatchingLabels(string[,] code)
        {
            for (int i = 0; i < code.GetLength(0); i++)
            {
                if (code[i, 0] == "goto" || code[i, 0] == "call" || code[i, 0] == "gofalse" || code[i, 0] == "gotrue")
                {
                    string labelToLookFor = code[i, 1];
                    bool found = false;
                    for (int j = 0; j < code.GetLength(0); j++)
                    {
                        if (code[j, 0] == "label")
                        {
                            if (code[j, 1] == labelToLookFor)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (!found)
                    {
                        throw new SyntaxError("No label found that matches " + labelToLookFor);
                    }
                }
            }
        }

        private static void CheckForMatchingBeginAndEndStatements(string[,] code)
        {
            int count = 0;
            int callCount = 0;
            for (int i = 0; i < code.GetLength(0); i++)
            {
                if (code[i, 0] == "begin")
                    count++;
                if (count == 1 && code[i, 0] == "call")
                    callCount++;
                if (code[i, 0] == "end")
                {
                    count--;
                    if (callCount > 1)
                    {
                        throw new SyntaxError("More than one call contained in a begin/end block");
                    }
                    callCount = 0;
                }

                if (count > 1)
                {
                    throw new SyntaxError("Found successive begin statements without a corresponding end statement");
                }
                if (count < 0)
                {
                    throw new SyntaxError("Found and additional end statement without a preceeding begin statement");
                }
            }
            if (count != 0)
            {
                //TODO: Throw an Error
                Console.WriteLine("You majorly broke it");
            }
        }

		private void CreateListOfValidInstructions()
		{
			validInstructions = new List<string> ();
			validInstructions.Add ("push");
			validInstructions.Add ("rvalue");
			validInstructions.Add ("lvalue");
			validInstructions.Add ("pop");
			validInstructions.Add (":=");
			validInstructions.Add ("copy");
			validInstructions.Add ("label");
			validInstructions.Add ("goto");
			validInstructions.Add ("gofalse");
			validInstructions.Add ("gotrue");
			validInstructions.Add ("halt");
			validInstructions.Add ("+");
			validInstructions.Add ("-");
			validInstructions.Add ("*");
			validInstructions.Add ("/");
			validInstructions.Add ("div");
			validInstructions.Add ("&");
			validInstructions.Add ("!");
			validInstructions.Add ("|");
			validInstructions.Add ("<>");
			validInstructions.Add ("<=");
			validInstructions.Add (">=");
			validInstructions.Add ("<");
			validInstructions.Add (">");
			validInstructions.Add ("=");
			validInstructions.Add ("print");
			validInstructions.Add ("show");
			validInstructions.Add ("begin");
			validInstructions.Add ("end");
			validInstructions.Add ("return");
			validInstructions.Add ("call");
			validInstructions.Add ("");
		}

		public void CheckForValidInstructions(string[,] code)
		{
			for (int i = 0; i < code.GetLength(0); i++)
			{
				string instruction = code [i, 0];
				if (!validInstructions.Exists (x => x == instruction))
					throw new SyntaxError ("Invalid instruction: " + instruction + "\non line: " + (i + 1) );
			}
		}

        public void Validate(string[,] code)
        {
			CreateListOfValidInstructions ();
            CheckForMatchingLabels(code);
            CheckForMatchingBeginAndEndStatements(code);
			CheckForValidInstructions (code);
        }
    }
}


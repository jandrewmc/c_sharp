// Builds the symbols table up front and
// and adds the ability to add levels to it.

using System.Collections.Generic;
using JazInterpreter.Interfaces;

namespace JazInterpreter
{
	public class SymbolsTable : ISymbolsTable
	{
		public static List<List<Identifier>> VariableTable;
		public static Dictionary<string, int> LabelTable;

		public static void InitializeSymbolsTable()
		{
			VariableTable = new List<List<Identifier>> ();
			LabelTable = new Dictionary<string, int> ();			
		}

		public static void AddLevel(int currentLevel)
		{
			int addedLevel = currentLevel + 1;

			VariableTable.Add(new List<Identifier>());

			foreach(var identifier in VariableTable[currentLevel])
				VariableTable[addedLevel].Add((Identifier)identifier.Clone());
		}

		private static void BuildVariableTable(string[,] code)
		{
			AddLevel (0);

			//every lvalue and rvalue in the table
			for (int i = 0; i < code.GetLength(0); i++)
			{
				if (code[i,0] == "lvalue" || code[i, 0] == "rvalue")
				{

					if(!VariableTable[0].Exists(x=>x.Name == code[i, 1]))
						VariableTable [0].Add (new Identifier {
							Value = 0,
							Name = code [i, 1]
						});
				}
			}
		}

		private static void BuildLabelTable(string[,] code)
		{
			//every label in the table needs an entry in the symbols table
			for (int i = 0; i < code.GetLength(0); i++)
			{
				if (code[i, 0] == "label")
				{
					//add the name of the label and the first line of code after the label
					LabelTable.Add (code [i, 1], i);
				}
			}
		}

		public static void BuildSymbolTable(string[,] code)
		{
			InitializeSymbolsTable ();

			BuildVariableTable (code);
			BuildLabelTable (code);
		}
	}
}


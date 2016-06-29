// Custom exception for missing LValue

using System;

namespace JazInterpreter
{
	public class MissingLValueException : BaseException
	{
		public MissingLValueException() : base("MISSING_LVALUE_EXCEPTION", 
			                                      "You cannot call := unless you have an IDENTIFIER as the second top-most element " +
			                                      "and an INTEGER as the top-most element.", 
			                                      ExitCode.EXIT_CODE_MISSING_LVALUE_EXCEPTION)
		{
		}
	}
}


// custom stack underflow exception

using System;

namespace JazInterpreter
{
	public class UnderflowException : BaseException
	{
		public UnderflowException() : base("UNDERFLOW_EXCEPTION", 
			                                  "You cannot pop off the stack when the stack is empty.", 
			                                  ExitCode.EXIT_CODE_UNDERFLOW_EXCEPTION)
		{
		}
	}
}


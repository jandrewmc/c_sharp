// custom value to store the variables 

using System;

namespace JazInterpreter
{
	public class Identifier : ICloneable
	{
		public int Value { get; set; }
		public string Name { get; set; }
		public object Clone()
		{
			return new Identifier {
				Value = 0,
				Name = Name
			};
		}
	}
}


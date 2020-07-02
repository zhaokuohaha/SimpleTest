using System;
using Xunit;

namespace SimpleTests
{
	public class UnitTest1
	{

		[Fact]
		public void Test1()
		{
			string str = "a"; 
			str += "c" + "d";
			string stfr1 = "a" + "b";
		}
	}
}

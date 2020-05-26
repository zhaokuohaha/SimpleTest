using System;
using Xunit;

namespace SimpleTests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			byte a = 255;
			a += 5;
		}
		
		int f(int n)
		{
			int t = 0;
			int a1 = 5;
			if(n%2 == 0)
			{
				int a = 6;
				t += a++;
			}
			else
			{
				int a = 7;
				t += a++;
			}
			return t + a1++;
		}

		int f2()
		{
			int a = 1;
			return 1 + a++;
		}
	}


	struct A
	{
		public string Name;
	}
	struct B
	{
		public int Age { get; set; }
	}


}

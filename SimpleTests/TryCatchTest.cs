using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTests
{
	/// <summary>
	/// 测试一下TryCatch的性能
	/// </summary>
	public class TryCatchTest
	{
        public void TryCatchTest1(string[] args)
        {
            int j = 0;
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    j = i;
                    throw new System.Exception();
                }
                catch { }
            }
            System.Console.Write(j);
            return;
        }
	}
}

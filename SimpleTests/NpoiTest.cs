using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Npoi.Mapper;
using Npoi.Mapper.Attributes;
using NPOI.SS.Formula.Functions;
using System.Linq;
using System.IO;

namespace SimpleTests
{
	public class NpoiTest
	{
		[Fact]
		public void TestNpoiPut()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "test.xlsx");
			var u1 = new[] { new NUser { Name = "U1", Age = 12 } };
			var u2 = new[] { new NUser { Name = "U2", Age = 13 } };

			foreach(var userBatch in new[] { u1, u2 })
			{
				var mapper = new Mapper();
				mapper.Save(path, userBatch, 0, !File.Exists(path));
			}

			var data = (new Mapper(path)).Take<NUser>();
			Assert.Null(data.Last().Value.Name);

		}
	}

	public class NUser
	{
		[Column("MyName")]
		public string Name { get; set; }
		public int Age { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Text.Json;

namespace SimpleTests.Stream
{
	public class StreamTest
	{
		[Fact]
		public async Task TestUsing()
		{
			string a, b;
			using(var stream = new MemoryStream())
			{
				using(var writer = new BinaryWriter(stream))
				{
					a = JsonSerializer.Serialize(stream.GetBuffer());
				}
				b = JsonSerializer.Serialize(stream.GetBuffer());
			}
			Assert.Equal(b, a);
		}
	}
}

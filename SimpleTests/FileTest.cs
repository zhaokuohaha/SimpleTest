using Microsoft.AspNetCore.Mvc;
using SimpleTests.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SimpleTests
{
	public class FileTest
	{
		[Fact]
		public async Task DownloadFiles()
		{
			var filesUrl = new[]{ "https://github.com/favicon.ico"};
			var bytes = await new FileHelper().ZipFiles(filesUrl);
			await File.WriteAllBytesAsync("./icons.zip", bytes);
		}

		[Fact]
		public async Task TestStream()
		{
			byte[] b1, b2;
			string s1, s2;
			using (var stream = new MemoryStream())
			{
				using (var zip = new ZipArchive(stream, ZipArchiveMode.Create))
				{
					var entry = zip.CreateEntry("a", CompressionLevel.Optimal);

					//using (var entryStream = entry.Open())
					//{
					//	entryStream.Write(new[] { (byte)1 });
					//}

					b1 = stream.GetBuffer();
					s1 = JsonSerializer.Serialize(stream.GetBuffer());
				}
				b2 = stream.GetBuffer();
				s2 = JsonSerializer.Serialize(stream.GetBuffer());
			}
			Assert.NotEqual(b1, b2);
		}
	}
}

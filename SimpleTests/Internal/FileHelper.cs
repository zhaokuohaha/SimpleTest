using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleTests.Internal
{
	public class FileHelper
	{
		public async Task<byte[]> ZipFiles(IEnumerable<string> paths)
		{
			byte[] result;
			var downloadTasks = paths.Select(async url =>
			{
				try
				{
					return new
					{
						Name = Path.GetFileName(url),
						Data = await new HttpClient().GetStreamAsync(url)
						//Data = await new HttpClient().GetByteArrayAsync(url) // 方案2, 读字节数组5
					};
				}
				catch
				{
					throw new Exception("下载文件失败");
				}
			});

			using (var zipStream = new MemoryStream())
			{
				using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create))
				{
					var filesData = await Task.WhenAll(downloadTasks);
					foreach (var file in filesData)
					{
						var entry = zip.CreateEntry(file.Name, CompressionLevel.Optimal);
						using (var entryStream = entry.Open())
						{
							await file.Data.CopyToAsync(entryStream);
							//await entryStream.WriteAsync(file.Data, 0, file.Data.Length);
						}
					}
				}
				result = zipStream.GetBuffer();
			}
			return result;
		}
	}
}

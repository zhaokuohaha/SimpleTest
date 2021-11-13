using Xunit;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Json;
using Newtonsoft.Json;

namespace SimpleTests.MemoryCache
{
	public class MemoryCacheTests
	{
		class User { public string Name { get; set; }}
		
		/// <summary>
		/// 使用扩展方法, 方便理解
		/// </summary>
		[Fact]
		public void TestMemoryObject()
		{
			var cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

			cache.Set("user", new User { Name = "Foo" });
			var f = cache.Get<User>("user");
			Assert.Equal("Foo", f.Name);

			f.Name = "Bar";
			var b = cache.Get<User>("user");
			Assert.Equal("Bar", b.Name);
		}


		/// <summary>
		/// 不使用扩展方法, 直接使用 Caching.Memory 包里面提供的方法
		/// </summary>
		[Fact]
		public void TestMemoryObject2()
		{
			var cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

			var entry = cache.CreateEntry("user");
			entry.Value = new User { Name = "Foo" };
			

			cache.TryGetValue("user", out User f);
			Assert.Equal("Foo", f.Name);

			f.Name = "Bar";
			cache.TryGetValue("user", out User b);
			Assert.Equal("Bar", b.Name);
		}

		/// <summary>
		/// System.Runtime.Caching 测试, 效果一样
		/// 另外: 没有实现 IMemoryCache
		/// </summary>
		[Fact]
		public void TestRunTimeCache()
		{
			var cache = new System.Runtime.Caching.MemoryCache("sys");
			cache.Add("user", new User { Name = "Foo" }, DateTime.Now.AddDays(1));

			User f = (User)cache.Get("user");
			Assert.Equal("Foo", f.Name);

			f.Name = "Bar";
			var b = (User)cache.Get("user");
			Assert.Equal("Bar", b.Name);
		}

		/// <summary>
		/// 自定义扩展, 转成字符串后缓存, Bar 更改没有生效
		/// </summary>
		[Fact]
		public void TestMyExtension()
		{
			var cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

			cache.MySet("user", new User { Name = "Foo" });
			var f = cache.MyGet<User>("user");
			Assert.Equal("Foo", f.Name);

			f.Name = "Bar";
			var b = cache.MyGet<User>("user");
			Assert.Equal("Foo", b.Name);
		}
	}

	public static class MemoryCacheExtensions
	{
		class Entry<T>
		{
			public T Value { get; set; }
		}

		public static T MyGet<T>(this IMemoryCache cache, object key)
		{
			var res = cache.TryGetValue(key, out string entryStr);
			if (!res)
				return default;

			var entry = JsonConvert.DeserializeObject<Entry<T>>(entryStr);
			return entry.Value;
		}

		public static bool MySet<T>(this IMemoryCache cache, object key, T value)
		{
			var entry = new Entry<T> { Value = value };
			try
			{
				cache.Set(key, JsonConvert.SerializeObject(entry));
				return true;
			}
			catch 
			{ 
				return false;
			}
		}
	}

}

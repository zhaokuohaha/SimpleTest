using Xunit;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace SimpleTests
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
			var cache = new MemoryCache(new MemoryCacheOptions());

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
			var cache = new MemoryCache(new MemoryCacheOptions());

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
	}
}

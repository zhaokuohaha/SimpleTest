using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;
using Xunit;

namespace SimpleTests.ValueAndRefType
{
	/// <summary>
	/// 测试值类型和引用类型
	/// </summary>
	public class ValueAndRefTypeTest
	{
		[Fact]
		public void TestMethodParam()
		{
			var p = new Profile { Id = 1, User = new User { Name = "Foo" } };
			ChangeUser(p);

			Assert.Equal(1, p.Id);
			Assert.Equal("FooBar", p.User.Name);

			RefChangeUser(ref p);
			Assert.Equal(11, p.Id);
			Assert.Equal("FooBarBar", p.User.Name);
		}

		private void ChangeUser(Profile p)
		{
			p.Id += 10;
			p.User.Name += "Bar";
		}

		private void RefChangeUser(ref Profile p)
		{
			p.Id += 10;
			p.User.Name += "Bar";
		}
	}

	struct Profile
	{
		public int Id { get; set; }
		public User User { get; set; }
	}
	class User { public string Name { get; set; } }
}

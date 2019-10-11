using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimpleTests
{
	public class IEnumerableTest
	{
		/// <summary>
		/// 直接初始化 Score 数组， 修改数组中元素的值生效
		/// </summary>
		[Fact]
		public void ChangeWorkTest()
		{
			var data = new[] { new Score { Value = 1 } };
			foreach (var d in data)
				d.Value = 2;
			foreach (var d in data)
				Assert.Equal(2, d.Value);
		}

		/// <summary>
		/// 通过初始化的 Score 数组调用 Select 返回一个 Linq 查询， 修改元素的值生效,
		/// 因为该 linq 查询的原始数据就是 Score 数组本身
		/// </summary>
		[Fact]
		public void ChangeWorkTest2()
		{
			var data = (new[] { new Score { Value = 1 } }).Select(d => d);
			foreach (var d in data)
				d.Value = 2;
			foreach (var d in data)
				Assert.Equal(2, d.Value);
		}

		/// <summary>
		/// 通过数组调用 Select 返回一个 Linq 查询， 修改元素的值， 不生效。
		/// 因为第一个 foreach 中运算生成的 d 修改不会影响第二次运算生成的值， 原始数据是 int 数组
		/// </summary>
		[Fact]
		public void ChangeNotWorkTest()
		{
			var data = (new[] { 1 }).Select(d => new Score { Value = d });
			foreach (var d in data)
				d.Value = 2;
			foreach (var d in data)
				Assert.Equal(1, d.Value);
		}

		public class Score
		{
			public int Value { get; set; }
		}
	}

}

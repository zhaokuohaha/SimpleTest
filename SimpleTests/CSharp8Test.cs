using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleTests
{
	public class CSharp8Test
	{
		/// <summary>
		/// 默认接口方法
		/// </summary>
		[Fact]
		public void TestDefaultInterfaceMethod()
		{
			IShape r = new Rect { Name = "aaa" };
			IShape t = new Triangle { Name = "bbb" };
			Assert.Equal("this is aaa", r.SayHi());
			Assert.Equal("这是 bbb", t.SayHi());
		}

		/// <summary>
		/// Switch 表达式
		/// </summary>
		[Fact]
		public void TestSwitchExpression()
		{
				var c = (Color)0;
				string rgb1;
				switch (c)
				{
					case Color.Red:
						rgb1 = "f00";
						break;
					case Color.Green:
						rgb1 = "0f0";
						break;
					case Color.Blue:
						rgb1 = "00f";
						break;
					default:
						throw new ArgumentException();
				}

				string rgb2 = c switch
				{
					Color.Red => "f00",
					Color.Green => "0f0",
					Color.Blue => "00f",
					_ => throw new ArgumentException()
				};

				Assert.Equal(rgb1, rgb2);
		}

		/// <summary>
		/// 属性模式
		/// </summary>
		[Fact]
		public void TestPropertyPatterns()
		{
			var t = new ColorInfo { Color = Color.Red };
			_ = t switch
			{
				{ Color: Color.Red } => t.Chinese = "红色",
				{ Color: Color.Green } => t.Chinese = "绿色",
				{ Color: Color.Blue } => t.Chinese = "蓝色"
			};
			Assert.Equal("红色", t.Chinese);
		}

		/// <summary>
		/// 元组模式
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="result"></param>
		[Theory]
		[InlineData(Color.Red, Color.Blue, Color.Purple)]
		public void TuplePatterns(Color a, Color b, Color result)
		{
			var res =  (a, b) switch
			{
				(Color.Red, Color.Green) => Color.Yellow,
				(Color.Red, Color.Blue) => Color.Purple,
				(Color.Green, Color.Blue) => Color.Turq,
				_ => throw new ArgumentException()
			};
			Assert.Equal(res, result);
		}

		/// <summary>
		/// 可空引用类型
		/// </summary>
		[Fact]
		public void TestNullableRefenenceTypes()
		{
#nullable enable
			string? a = null;
			Assert.Equal(0, a?.Length??0);
			a = "111";
			Assert.Equal(3, a!.Length);

			string b = null;
			Assert.Null(b);
#nullable disable

		}

		/// <summary>
		/// 异步流
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestAsyncStream()
		{
			static async IAsyncEnumerable<int> GenerateSequence()
			{
				for (int i = 0; i < 20; i++)
				{
					await Task.Delay(100);
					yield return i;
				}
			}

			var res = 0;
			await foreach (var number in GenerateSequence())
			{
				Assert.Equal(res++, number);
			}
		}

		/// <summary>
		/// 索引和范围
		/// </summary>
		/// <returns></returns>
		[Fact]
		public void TestIndexAndRange()
		{
			var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			Assert.Equal(9, arr[^1]);
			// 单独访问 arr[^0] 会报错
			// 范围选择为左闭右开区间
			Assert.Equal(1, arr[0..^0][0]);
			Assert.Equal(9, arr[0..^0][^1]);
			Assert.Equal(2, arr[1..^1][0]);
			Assert.Equal(8, arr[1..^1][^1]);
		}

		[Fact]
		public void TestStackalloc()
		{
			unsafe
			{
				// 默认会返回 int* 类型
				var numbers = stackalloc[] { 1, 2, 3, 4, 5, 6 };
			}
			Span<int> numbers2 = stackalloc[] { 1, 2, 3, 4, 5, 6 };

		}
	}

	ref struct Point
	{
		public int X { get; set; }
		public int Y { get; set; }
		public void Dispose() { }
	}

	class ColorInfo
	{
		public Color Color { get; set; }
		public string Chinese { get; set; }
	}
	public enum Color
	{
		Red,
		Green,
		Blue,
		Yellow,
		Turq,
		Purple
	}
	interface IShape
	{
		public string Name { get; set; }
		public string SayHi()
		{
			return $"this is {Name}";
		}
	}
	class Rect : IShape
	{
		public string Name { get; set; }
	}

	class Triangle : IShape
	{
		public string Name { get; set; }
		public string SayHi()
		{
			return $"这是 {Name}";
		}
	}
}

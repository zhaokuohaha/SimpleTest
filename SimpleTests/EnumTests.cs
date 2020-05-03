using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace SimpleTests
{
	public class EnumTests
	{
		[Fact]
		public void TestDescription()
		{
			Assert.Equal("男性", Sex.Man.GetDescription());
			Assert.Equal("Man", Sex.Man.ToString());
		}

		[Fact]
		public void TestFlag()
		{
			
			Assert.Equal("A, C", ((OperateFlag)5).ToString());
			Assert.Equal("9", ((OperateFlag)9).ToString());

			Assert.True(((OperateFlag)6).HasFlag(OperateFlag.B));
			Assert.False(((OperateFlag)6).HasFlag((OperateFlag)3));
			Assert.True(((OperateFlag)7).HasFlag((OperateFlag)3));

			Assert.True(((OperateFlag)9).HasFlag(OperateFlag.A));
			Assert.False(OperateFlag.A.HasFlag((OperateFlag)9));

			var f1 = OperateFlag.A | OperateFlag.B;
			Assert.Equal(OperateFlag.A, f1 & OperateFlag.A);
			Assert.False(OperateFlag.A == f1);
			var f2 = Enum.Parse<OperateFlag>("A, B");
			Assert.Equal(f1, f2);
		}

		[Fact]
		public void TestNoneFlog()
		{
			Assert.Equal("A", OperateFlag.A.ToString());
			Assert.Equal("5", ((Operate)5).ToString());

			Assert.True(((Operate)6).HasFlag(Operate.B));
			Assert.False(((Operate)6).HasFlag((Operate)3));
			Assert.True(((Operate)7).HasFlag((Operate)3));

			Assert.True(((Operate)9).HasFlag(Operate.A));
			Assert.False(Operate.A.HasFlag((Operate)9));

			var f1 = Operate.A | Operate.B;
			Assert.Equal(Operate.A, f1 & Operate.A);
			var f2 = Enum.Parse<Operate>("A, B");
			Assert.Equal(f1, f2);
		}

		[Fact]
		public void TestDefault()
		{
			Assert.Equal(3, (int)FlagDefault.D);
		}

		[Fact]
		public void TestStrigValue()
		{
			Assert.Equal("A", CharValue.A.ToString());
			Assert.Equal((short)'a', (short)CharValue.A); // ASCII
			Assert.Equal(98, (short)CharValue.B); // ASCII

			Assert.Equal("AAA", StrigEnum.A);
		}
	}

	public static class EnumExtension
	{
		public static string GetDescription(this Enum e)
		{
			
			var fi = e.GetType().GetField(e.ToString());
			var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (attributes.Length > 0)
			{
				return attributes[0].Description;
			}
			return e.ToString();
		}
	}


	enum Sex
	{
		[Description("男性")]
		Man,
		[Description("女性")]
		Woman
	}

	[Flags]
	enum OperateFlag
	{
		A = 1,
		B = 2,
		C = 4
	}
	enum Operate
	{
		A = 1,
		B = 2,
		C = 4,
	}

	[Flags]
	enum FlagDefault { A, B, C, D }
	enum EnumDefault { A, B, C, D }


	enum CharValue
	{
		A = 'a',
		B = 'b',
		C = 'c'
	}

	class StrigEnum
	{
		public static string A => "AAA";
		public static string B => "BBB";
		public static string C => "CCC";
	}


	class StringEnumBase
	{
		protected readonly string _value;
		public StringEnumBase()
		{
			_value = string.Empty;
		}
		public StringEnumBase(string value)
		{
			_value = value;
		}
		public static implicit operator string(StringEnumBase d)
		{
			return d._value;
		}
		public static implicit operator StringEnumBase(string d)
		{
			return new StringEnumBase(d);
		}
		public override string ToString()
		{
			return this._value;
		}
	}

	public struct StrEnum
	{
		public string s;

		public static implicit operator StrEnum(string value)
		{
			return new StrEnum() { s = value};
		}
	}
}

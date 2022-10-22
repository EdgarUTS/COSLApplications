using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace COSLApplications.Shared
{
	public class EnumDisplayName
	{
		
		public string? Name { get; set; }
		public EnumDisplayName() { }
		public EnumDisplayName(Type enumType, int num)
		{
			//Type enumType = enu.GetType();
			string? enumValue = Enum.GetName(enumType, num);
			MemberInfo? member = enumType.GetMember(enumValue)[0];

			var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
			string? outString = ((DisplayAttribute)attrs[0]).Name;
			Name = outString;
		}
		public EnumDisplayName(Enum value)
		{
			Type enumType = value.GetType();
			var enumValue = Enum.GetName(enumType, value);
			MemberInfo ? member = enumType.GetMember(enumValue)[0];

			var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
			string? outString = ((DisplayAttribute)attrs[0]).Name;
			Name = outString;
		}

		public string getEnumDisplayName(Enum value)
		{
			Type enumType = value.GetType();
			var enumValue = Enum.GetName(enumType, value);
			MemberInfo? member = enumType.GetMember(enumValue)[0];

			var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
			string? outString = ((DisplayAttribute)attrs[0]).Name;
			return outString;
		}
	}
}

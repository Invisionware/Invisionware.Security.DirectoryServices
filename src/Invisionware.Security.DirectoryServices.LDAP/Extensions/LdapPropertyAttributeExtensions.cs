using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invisionware.Security.DirectoryServices.LDAP
{
	public static class LdapPropertyAttributeExtensions
	{
		public static IEnumerable<string> GetLdapPropertyNames(this object obj)
		{
			return GetLdapPropertyNames(obj.GetType());
		}

		public static IEnumerable<string> GetLdapPropertyNames(this Type type)
		{
			var attribs = type.GetProperties().SelectMany(x => x.GetCustomAttributes(typeof(LdapPropertyAttribute), true));

			return attribs.Cast<LdapPropertyAttribute>().Select(x => x.Name);
		}
	}
}

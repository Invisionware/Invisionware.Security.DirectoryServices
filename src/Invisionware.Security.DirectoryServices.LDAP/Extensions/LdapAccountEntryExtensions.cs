using Invisionware.Security.DirectoryServices.LDAP.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invisionware.Security.DirectoryServices.LDAP.Extensions
{
	public static class LdapAccountEntryExtensions
	{
		public static string ToLdapQuery(this ILdapAccountEntry accountEntry)
		{
			var ldapQuery = new LdapQueryCondition() { ConditionType = LdapQueryConditionTypes.And };

			// Get the type of the result object
			Type ldapAccountEntryType = accountEntry.GetType();

			// Get a list of all of the properties we need to bind to
			var propertiesWithMyAttribute = ldapAccountEntryType.GetPropertiesWithAttribute<LdapPropertyAttribute>(true);

			foreach (var tp in propertiesWithMyAttribute)
			{
				var attr = tp.GetAttributeOfType<LdapPropertyAttribute>().First();

				if (attr.Name == "*") continue; // skip the wildcard as we cannot use it in a query

				var value = tp.GetValue(accountEntry);
				if (value != null)
				{
					Type vt = value.GetType();

					if (value is string && !string.IsNullOrEmpty(value.ToString()))
					{
						ldapQuery.EqualTo(attr.Name, value);
					}
					else if ((vt is IEnumerable) || (vt.IsGenericType && vt.GetGenericTypeDefinition().GetInterfaces().Any(x => x.IsAssignableFrom(typeof(IEnumerable)))))
					{
						var lq = ldapQuery.Or();

						foreach (var v in value as IEnumerable)
						{
							lq.EqualTo(attr.Name, v);
						}
					} 
				}
			}

			return ldapQuery.Build().ToString();
		}
	}
}

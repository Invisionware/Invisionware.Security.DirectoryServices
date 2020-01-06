using Invisionware.Security.DirectoryServices.LDAP.Query;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;

namespace Invisionware.Security.DirectoryServices.LDAP.Extensions
{
	public static class DirectorySearcherExtensions
	{
		public static SearchResultCollection FindAllByUserName(this DirectorySearcher searcher, string accountFilder = "*", string givenNameFilter = "*", string surnameFilter = "*", string emailFilter = "*")
		{
			var filter = new LdapFilter(LdapQueryFilterBuilder.LdapFilterExpression.AND);
			filter.Add(new LdapQueryFilterCondition("objectClass", LdapQueryFilterCondition.LdapFilterOperators.EQUALTO, "user"));
			filter.Add(new LdapQueryFilterCondition("objectClass", LdapQueryFilterCondition.LdapFilterOperators.EQUALTO, "user"));

			searcher.Filter = $"(&(objectClass=user)(&(sAMAccountName={accountFilder})(givenName={givenNameFilter})(sn={surnameFilter})(Mail={emailFilter})))";

			return searcher.FindAll();
		}

		public static SearchResultCollection FindAll(this DirectorySearcher searcher, LdapFilter filter)
		{
			var filterString = LdapQueryFilterBuilder.GetFilter(filter, out string error);

			searcher.Filter = filterString;

			return searcher.FindAll();
		}
	}
}

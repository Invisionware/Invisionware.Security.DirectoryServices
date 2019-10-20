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
			searcher.Filter = $"(&(objectClass=user)(&(sAMAccountName={accountFilder})(givenName={givenNameFilter})(sn={surnameFilter})(Mail={emailFilter})))";

			return searcher.FindAll();
		}
	}
}

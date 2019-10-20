using FluentAssertions;
using Invisionware.Security.DirectoryServices.LDAP.Extensions;
using NUnit.Framework;
using System;

namespace Invisionware.Security.DirectoryServices.LDAP.Tests.Extensions
{
	[TestFixture(Category = "", Description = "Implements Unit Tests for LdapAccountEntryExtensions")]
	public class LdapAccountEntryExtensionsTests
	{
		[Test]
		public void ToLdapQuery_ExpectedBehavior()
		{
			var account = new LdapAccountEntry { AccountName = "sanderson", FirstName = "Shawn", LastName = "Anderson" };
			account.EmailAddressAliases.Add("shawn@eye-catcher.com");

			var result = account.ToLdapQuery();

			result.Should().Be("(&(sAMAccountName=sanderson)(givenName=Shawn)(sn=Anderson)(|(proxyAddresses=shawn@eye-catcher.com)))");
		}
	}
}

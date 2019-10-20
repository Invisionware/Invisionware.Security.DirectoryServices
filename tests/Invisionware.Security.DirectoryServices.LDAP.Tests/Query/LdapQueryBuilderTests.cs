using FluentAssertions;
using Invisionware.Security.DirectoryServices.LDAP.Query;
using NUnit.Framework;

namespace Invisionware.Security.DirectoryServices.LDAP.Tests.Query
{
	[TestFixture(Category = "", Description = "Implements Unit Tests for LdapQueryBuilder")]
	public class LdapQueryBuilderTests
	{
		[Test]
		public void CreateComplex1_Pass()
		{
			// Arrange
			var ldapQueryBuilder = new LdapQueryCondition()
											.And()
											.EqualTo("objectClass", "user")
											.Or()
											.StartsWith("cn", "shawn")
											.StartsWith("cn", "joe").Build();

			// Act
			var result = ldapQueryBuilder.ToString();

			// Assert
			result.Should().Be("(&(objectClass=user)(|(cn=shawn*)(cn=joe*)))");
		}

		[Test]
		public void CreateComplex2_Pass()
		{
			var ldapQuery = new LdapQueryCondition()
								.And()
								.EqualTo("objectClass", "user")
								.And()
								.EqualTo("sAMAccountName", "sanderson")
								.EqualTo("givenName", "Shawn")
								.EqualTo("sn", "Anderson")
								.EqualTo("Mail", "sanderson@eye-catcher.com").Build();

			var result = ldapQuery.ToString();

			result.Should().Be("(&(objectClass=user)(&(sAMAccountName=sanderson)(givenName=Shawn)(sn=Anderson)(Mail=sanderson@eye-catcher.com)))");

		}
	}
}

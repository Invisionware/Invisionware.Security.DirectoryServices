using FluentAssertions;
using Invisionware.Security.DirectoryServices.LDAP;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Invisionware.Security.DirectoryServices.LDAP.Tests.Managers
{
	[TestFixture(Category = "", Description = "Implements Unit Tests for LdapAccountManager")]
	public class LdapAccountManagerTests
	{
		private LdapServerConnection _connection;

		private LdapConnectionManager _connectionManager;


		[SetUp]
		public void Setup()
		{
			var appSettings = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings;

			if (!appSettings.Settings.AllKeys.Contains("LdapUserPassword"))
			{
				_connection = new LdapServerConnection
				{
					Server = appSettings.Settings["LdapServer"].Value
				};
			}
			else
			{
				_connection = new LdapServerConnection
				{
					Server = appSettings.Settings["LdapServer"].Value,
					Credentials = new System.Net.NetworkCredential(appSettings.Settings["LdapUserName"].Value, appSettings.Settings["LdapUserPassword"].Value, appSettings.Settings["LdapUserDomain"].Value)
				};
			}

			_connectionManager = new LdapConnectionManager(_connection);
		}

		[Test]
		public void FindUsers_ExpectedSingleUser()
		{
			// Arrange
			var accountManager = _connectionManager.CreateAccountManager();
			string accountFilder = "sanderson";
			string givenNameFilter = "*";
			string surnameFilter = "*";
			string emailFilter = "*";

			// Act
			var result = accountManager.FindUsers(
				accountFilder,
				givenNameFilter,
				surnameFilter,
				emailFilter);

			// Assert
			result.Should().NotBeNullOrEmpty();
			result.Should().ContainSingle();
		}

		[Test]
		public void FindUsers_ExpecgedMultipleleUser()
		{
			// Arrange
			var accountManager = _connectionManager.CreateAccountManager();
			string accountFilder = "a*";
			string givenNameFilter = "*";
			string surnameFilter = "*";
			string emailFilter = "*";

			// Act
			var result = accountManager.FindUsers(
				accountFilder,
				givenNameFilter,
				surnameFilter,
				emailFilter);

			// Assert
			result.Should().NotBeNullOrEmpty();
			result.Should().HaveCountGreaterOrEqualTo(3);
		}

		[Test]
		public void FindUserByEmail_ExpectedValidAccount()
		{
			// Arrange
			var accountManager = _connectionManager.CreateAccountManager();
			string emailAddress = "sanderson@eye-catcher.com";

			// Act
			var result = accountManager.FindUserByEmail(
				emailAddress);

			// Assert
			result.Should().NotBeNull();
			result.PrimaryEmailAddress.Should().Be(emailAddress);
		}

		[Test]
		public void FindUserByAccountName_ExpectedValidAccount()
		{
			// Arrange
			var accountManager = _connectionManager.CreateAccountManager();
			string accountName = "sanderson";

			// Act
			var result = accountManager.FindUserByAccountName(
				accountName);

			// Assert
			result.Should().NotBeNull();
			result.AccountName.Should().Be(accountName);
		}

		[Test]
		public void FindUserByFilterString_ExpectedSingleAccount()
		{
			// Arrange
			var accountManager = _connectionManager.CreateAccountManager();
			string ldapFilter = "(sAMAccountName=sanderson)";

			// Act
			var result = accountManager.FindUserByFilterString(
				ldapFilter);

			// Assert
			result.Should().NotBeNullOrEmpty();
			result.Should().ContainSingle();
		}
	}
}

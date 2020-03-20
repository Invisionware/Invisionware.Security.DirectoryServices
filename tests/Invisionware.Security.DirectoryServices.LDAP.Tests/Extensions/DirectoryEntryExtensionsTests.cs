using FluentAssertions;
using Invisionware.Security.DirectoryServices.LDAP;
using Invisionware.Security.DirectoryServices.LDAP.Query;
using NUnit.Framework;
using System;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;

namespace Invisionware.Security.DirectoryServices.LDAP.Tests.Extensions
{
	[TestFixture(Category = "", Description = "Implements Unit Tests for DirectoryEntryExtensions")]
	public class DirectoryEntryExtensionsTests
	{
		private DirectoryEntry _directoryEntry;
		private LdapServerConnection _connection;

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

			if (_connection.Credentials != null && !string.IsNullOrEmpty(_connection.Credentials.Password))
			{
				_directoryEntry = new DirectoryEntry(_connection.ServerUrl, $"{_connection.Credentials.Domain}\\{_connection.Credentials.UserName}", _connection.Credentials.Password);
			}
			else
			{
				_directoryEntry = new DirectoryEntry(_connection.ServerUrl);
			}
		}

		//[Test] // Disabled for Azure DevOps Builds
		//TODO Need to fix to allow it to work in Azure DevOps
		public void ToAccountEntry_Valid()
		{
			using (var ds = new DirectorySearcher(_directoryEntry))
			{
				ds.Filter = new LdapQueryCondition().And().EqualTo("objectClass", "user").EqualTo("sAMAccountName", "sanderson").Build().ToString();
				ds.Filter.Should().NotBeNullOrEmpty();

				foreach (SearchResult searchResult in ds.FindAll())
				{
					DirectoryEntry de = searchResult.GetDirectoryEntry();

					var accountEntry = de.ToAccountEntry();

					accountEntry.Should().NotBeNull();
					accountEntry.FirstName.Should().Be("Shawn");
				}
			}
		}
	}
}

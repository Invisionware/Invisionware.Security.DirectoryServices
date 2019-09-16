// ***********************************************************************
// Assembly         : Invisionware.Security.DirectoryServices.LDAP
// Author           : sanderson
// Created          : 09-16-2019
//
// Last Modified By : sanderson
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="DirectoryEntryExtensions.cs" company="Invisionware">
//     Copyright © 2019 Invisionware
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.DirectoryServices;

namespace Invisionware.Security.DirectoryServices.LDAP
{
	/// <summary>
	/// Class DirectoryEntryExtensions.
	/// </summary>
	internal static class DirectoryEntryExtensions
	{
		/// <summary>
		/// Converts to accountentry.
		/// </summary>
		/// <param name="de">The de.</param>
		/// <returns>LdapAccountEntry.</returns>
		public static LdapAccountEntry ToAccountEntry(this DirectoryEntry de)
		{
			if (de == null) return null;

			var result = new LdapAccountEntry
			{
				AccountName = de.Properties["sAMAccountName"].Value.ToString(),
				UserPrincipal = de.Properties["userPrincipalName"].Value.ToString(),
				DisplayName = de.Properties["displayName"].Value.ToString(),
				PrimaryEmailAddress = new LdapEmailAddressEntry { Address = de.Properties["mail"].Value.ToString().Replace("smtp:", "").Replace("SMTP:","") },
				FirstName = de.Properties["givenname"].Value.ToString(),
				LastName = de.Properties["sn"].Value.ToString(),
			};

			// Add the "proxyaddresses" entries.
			if (de.Properties.Contains("proxyaddresses"))
			{
				foreach (object property in de.Properties["proxyaddresses"])
				{
					if (property.ToString().StartsWith("x500", System.StringComparison.OrdinalIgnoreCase)) continue;

					result.EmailAddressAliases.Add(new LdapEmailAddressEntry { Address = property.ToString().Replace("smtp:","").Replace("SMTP:", "") });
				}
			}

			return result;
		}
	}
}

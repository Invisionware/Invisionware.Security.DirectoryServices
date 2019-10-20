// ***********************************************************************
// Assembly         : Invisionware.Security.DirectoryServices.LDAP
// Author           : sanderson
// Created          : 09-16-2019
//
// Last Modified By : sanderson
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="LdapAccountEntry.cs" company="Invisionware">
//     Copyright © 2019 Invisionware
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Diagnostics;

namespace Invisionware.Security.DirectoryServices.LDAP
{
	public interface ILdapAccountEntry
	{
		/// <summary>
		/// Gets or sets the name of the account.
		/// </summary>
		/// <value>The name of the account.</value>
		[LdapProperty("sAMAccountName")]
		string AccountName { get; set; }
		/// <summary>
		/// Gets or sets the user principal.
		/// </summary>
		/// <value>The user principal.</value>
		[LdapProperty("userPrincipalName")]
		string UserPrincipal { get; set; }
		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>The display name.</value>
		[LdapProperty("displayName")]
		string DisplayName { get; set; }
		/// <summary>
		/// Gets or sets the primary email address.
		/// </summary>
		/// <value>The primary email address.</value>
		[LdapProperty("mail")]
		string PrimaryEmailAddress { get; set; }
		/// <summary>
		/// Gets or sets the email address aliases.
		/// </summary>
		/// <value>The email address aliases.</value>
		[LdapCollectionPropertyAttribute("proxyAddresses", ElementType = typeof(string))]
		IList<string> EmailAddressAliases { get; set; }
		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>The first name.</value>
		[LdapProperty("givenName")]
		string FirstName { get; set; }
		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>The last name.</value>
		[LdapProperty("sn")]
		string LastName { get; set; }

		/// <summary>
		/// Gets or sets the properties.
		/// </summary>
		/// <value>The properties.</value>
		[LdapDictionaryPropertyAttribute("*")]
		IDictionary<string, object> Properties { get; set; }
	}
}
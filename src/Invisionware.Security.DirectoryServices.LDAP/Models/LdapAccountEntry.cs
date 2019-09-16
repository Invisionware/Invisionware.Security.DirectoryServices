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
	/// <summary>
	/// Class LdapAccountEntry.
	/// </summary>
	[DebuggerDisplay("AccountName={AccountName},DisplayName={DisplayName},PrimaryEmailAddres={PrimaryEmailAddress}")]
	public class LdapAccountEntry
	{
		/// <summary>
		/// Gets or sets the name of the account.
		/// </summary>
		/// <value>The name of the account.</value>
		public string AccountName { get; set; }
		/// <summary>
		/// Gets or sets the user principal.
		/// </summary>
		/// <value>The user principal.</value>
		public string UserPrincipal { get; set; }
		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>The display name.</value>
		public string DisplayName { get; set; }
		/// <summary>
		/// Gets or sets the primary email address.
		/// </summary>
		/// <value>The primary email address.</value>
		public LdapEmailAddressEntry PrimaryEmailAddress { get; set; }
		/// <summary>
		/// Gets or sets the email address aliases.
		/// </summary>
		/// <value>The email address aliases.</value>
		public IList<LdapEmailAddressEntry> EmailAddressAliases { get; set; } = new List<LdapEmailAddressEntry>();
		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>The first name.</value>
		public string FirstName { get; set; }
		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>The last name.</value>
		public string LastName { get; set; }
	}
}

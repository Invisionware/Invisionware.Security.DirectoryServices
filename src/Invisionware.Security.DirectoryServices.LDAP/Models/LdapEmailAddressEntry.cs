// ***********************************************************************
// Assembly         : Invisionware.Security.DirectoryServices.LDAP
// Author           : sanderson
// Created          : 09-16-2019
//
// Last Modified By : sanderson
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="LdapEmailAddressEntry.cs" company="Invisionware">
//     Copyright © 2019 Invisionware
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Diagnostics;

namespace Invisionware.Security.DirectoryServices.LDAP
{
	/// <summary>
	/// Class LdapEmailAddressEntry.
	/// </summary>
	[DebuggerDisplay("Address={Address}")]
	public class LdapEmailAddressEntry
	{
		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		public string Address { get; set; }
	}
}

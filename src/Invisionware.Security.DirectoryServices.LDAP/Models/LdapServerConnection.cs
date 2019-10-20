// ***********************************************************************
// Assembly         : Invisionware.Security.DirectoryServices.LDAP
// Author           : sanderson
// Created          : 09-16-2019
//
// Last Modified By : sanderson
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="LdapConnectionManager.cs" company="Invisionware">
//     Copyright © 2019 Invisionware
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Net;

namespace Invisionware.Security.DirectoryServices.LDAP
{
	/// <summary>
	/// Class LdapServerConnection.
	/// </summary>
	public class LdapServerConnection
	{
		/// <summary>
		/// Gets or sets the server.
		/// </summary>
		/// <value>The server.</value>
		public string Server { get; set; }
		/// <summary>
		/// Gets the server URL.
		/// </summary>
		/// <value>The server URL.</value>
		public string ServerUrl => string.Format("{0}://{1}", UseSecure ? "LDAPS" : "LDAP", Server);

		/// <summary>
		/// Gets or sets the credentials.
		/// </summary>
		/// <value>The credentials.</value>
		public NetworkCredential Credentials { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [use secure].
		/// </summary>
		/// <value><c>true</c> if [use secure]; otherwise, <c>false</c>.</value>
		public bool UseSecure { get; set; } = false;
	}
}

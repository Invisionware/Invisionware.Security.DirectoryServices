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
using System;
using System.DirectoryServices;
using System.Net;

namespace Invisionware.Security.DirectoryServices.LDAP
{
	/// <summary>
	/// Class LdapConnectionManager.
	/// </summary>
	public class LdapConnectionManager : IDisposable
	{
		/// <summary>
		/// The connection
		/// </summary>
		private LdapServerConnection _connection;

		/// <summary>
		/// Initializes a new instance of the <see cref="LdapConnectionManager"/> class.
		/// </summary>
		/// <param name="connection">The connection.</param>
		public LdapConnectionManager(LdapServerConnection connection)
		{
			_connection = connection;
		}

		/// <summary>
		/// Connects this instance.
		/// </summary>
		public void Connect()
		{
			if (IsConnected) return;

			if (_connection.Credentials != null && !string.IsNullOrEmpty(_connection.Credentials.Password))
			{
				DirectoryEntry = new DirectoryEntry(_connection.ServerUrl, $"{_connection.Credentials.Domain}\\{_connection.Credentials.UserName}", _connection.Credentials.Password);
			}
			else
			{
				DirectoryEntry = new DirectoryEntry(_connection.ServerUrl);
			}			
		}

		/// <summary>
		/// Disconnects this instance.
		/// </summary>
		public void Disconnect()
		{
			if (!IsConnected) return;

			DirectoryEntry?.Dispose();
			DirectoryEntry = null;
		}

		/// <summary>
		/// Gets a value indicating whether this instance is connected.
		/// </summary>
		/// <value><c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
		internal bool IsConnected => DirectoryEntry != null;

		/// <summary>
		/// Gets the directory entry.
		/// </summary>
		/// <value>The directory entry.</value>
		internal DirectoryEntry DirectoryEntry { get; private set; }

		public LdapAccountManager CreateAccountManager()
		{
			return new LdapAccountManager(this);
		}

		public void Dispose()
		{			
			Disconnect();
		}
	}
}

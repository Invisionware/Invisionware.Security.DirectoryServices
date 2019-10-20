// ***********************************************************************
// Assembly         : Invisionware.Security.DirectoryServices.LDAP
// Author           : sanderson
// Created          : 09-16-2019
//
// Last Modified By : sanderson
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="LdapAccountManager.cs" company="Invisionware">
//     Copyright © 2019 Invisionware
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;

namespace Invisionware.Security.DirectoryServices.LDAP
{
	/// <summary>
	/// Class LdapAccountManager.
	/// </summary>
	public class LdapAccountManager
	{
		/// <summary>
		/// The connection manager
		/// </summary>
		private LdapConnectionManager _connectionManager;
		/// <summary>
		/// The maximum page size
		/// </summary>
		private int _maxPageSize = 1000;

		/// <summary>
		/// Initializes a new instance of the <see cref="LdapAccountManager"/> class.
		/// </summary>
		/// <param name="connectionManager">The connection manager.</param>
		internal LdapAccountManager(LdapConnectionManager connectionManager)
		{
			_connectionManager = connectionManager;
		}

		/// <summary>
		/// Finds the users.
		/// </summary>
		/// <param name="accountFilder">The account filder.</param>
		/// <param name="givenNameFilter">The given name filter.</param>
		/// <param name="surnameFilter">The surname filter.</param>
		/// <param name="emailFilter">The email filter.</param>
		/// <returns>IList&lt;LdapAccountEntry&gt;.</returns>
		public IList<ILdapAccountEntry> FindUsers(string accountFilder = "*", string givenNameFilter = "*", string surnameFilter = "*", string emailFilter = "*")
		{
			string filter = $"(&(objectClass=user)(&(sAMAccountName={accountFilder})(givenName={givenNameFilter})(sn={surnameFilter})(Mail={emailFilter})))";

			var results = FindUserByFilterString(filter);

			return results;
		}

		/// <summary>
		/// Finds the user by email.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		/// <returns>LdapAccountEntry.</returns>
		public ILdapAccountEntry FindUserByEmail(string emailAddress)
		{
			string filter = $"(&(objectClass=user)(Mail={emailAddress}))";

			var results = FindUserByFilterString(filter);

			return results.FirstOrDefault();
		}

		/// <summary>
		/// Finds the name of the user by account.
		/// </summary>
		/// <param name="accountName">Name of the account.</param>
		/// <returns>LdapAccountEntry.</returns>
		public ILdapAccountEntry FindUserByAccountName(string accountName)
		{
			string filter = $"(&(objectClass=user)(sAMAccountName={accountName}))";

			var results = FindUserByFilterString(filter);

			return results.FirstOrDefault();
		}

		/// <summary>
		/// Finds the user by filter string.
		/// </summary>
		/// <param name="ldapFilter">The LDAP filter.</param>
		/// <returns>IList&lt;LdapAccountEntry&gt;.</returns>
		public IList<ILdapAccountEntry> FindUserByFilterString(string ldapFilter)
		{
			if (!_connectionManager.IsConnected) _connectionManager.Connect();

			using (DirectorySearcher mySearcher = new DirectorySearcher(_connectionManager.DirectoryEntry)
			{
				SearchScope = SearchScope.Subtree,
				Filter = $"(&(objectClass=user)({ldapFilter}))",
				Sort = new SortOption("sAMAccountName", SortDirection.Ascending),
				PageSize = _maxPageSize
			}
			)
			{

				var props = typeof(LdapAccountEntry).GetLdapPropertyNames();

				foreach (var p in props)
				{
					mySearcher.PropertiesToLoad.Add(p);
				}

				var results = new List<ILdapAccountEntry>();

				foreach (SearchResult resEnt in mySearcher.FindAll())
				{
					DirectoryEntry de = resEnt.GetDirectoryEntry();

					results.Add(de.ToAccountEntry());
				}

				return results;
			}
		}
	}
}

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
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace Invisionware.Security.DirectoryServices.LDAP
{
	/// <summary>
	/// Class DirectoryEntryExtensions.
	/// </summary>
	public static class DirectoryEntryExtensions
	{
		/// <summary>
		/// Converts to accountentry.
		/// </summary>
		/// <param name="de">The de.</param>
		/// <returns>LdapAccountEntry.</returns>
		public static ILdapAccountEntry ToAccountEntry(this DirectoryEntry de)
		{
			if (de == null) return null;

			// Get the result object
			ILdapAccountEntry result = Invisionware.IoC.Resolver.IsSet && Invisionware.IoC.Resolver.IsRegistered<ILdapAccountEntry>() ? Invisionware.IoC.Resolver.Resolve<ILdapAccountEntry>() : new LdapAccountEntry();

			// Get the type of the result object
			Type ldapAccountEntryType = result.GetType();

			// Get a list of all of the properties we need to bind to
			var propertiesWithMyAttribute = ldapAccountEntryType.GetPropertiesWithAttribute<LdapPropertyAttribute>(true);
			
			foreach (var tp in propertiesWithMyAttribute)
			{
				var attr = tp.GetAttributeOfType<LdapPropertyAttribute>().First();

				if (attr.Name == "*") continue; // skip the wildcard catch all as we handle it later

				if (de.Properties.Contains(attr.Name))
				{
					var deValue = de.Properties[attr.Name];

					if (attr is LdapCollectionPropertyAttribute)
					{
						object cln = tp.GetValue(result);

						var m = tp.PropertyType.GetMethod("Add") ?? tp.PropertyType.GetMethod("Insert");
						foreach (var v in deValue)
						{
							var p = m.Name == "Insert" ? new[] { 0, Convert.ChangeType(v, ((LdapCollectionPropertyAttribute)attr).ElementType) } : new[] { Convert.ChangeType(v, ((LdapCollectionPropertyAttribute)attr).ElementType) };

							m.Invoke(cln, p);
						}
					}
					else if (attr is LdapDictionaryPropertyAttribute)
					{
						object dct = tp.GetValue(result);

						tp.PropertyType.GetMethod("Add").Invoke(dct, new[] { deValue.PropertyName, Convert.ChangeType(deValue.Value, ((LdapCollectionPropertyAttribute)attr).ElementType) });
					}
					else
					{
						tp.SetValue(result, deValue.Value);
					}
				}
			}

			var propertyAllValues = propertiesWithMyAttribute.Where(x => x.GetAttributeOfType<LdapDictionaryPropertyAttribute>()?.Any(y => y.Name == "*") == true);

			if (propertyAllValues != null)
			{
				var pc = propertyAllValues.First();
				Type tColl = typeof(IDictionary<,>);
				if (pc.PropertyType.IsGenericType && tColl.IsAssignableFrom(pc.PropertyType.GetGenericTypeDefinition()) || pc.PropertyType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == tColl))
				{
					object cln = pc.GetValue(result);

					foreach (PropertyValueCollection pvc in de.Properties)
					{
						pc.PropertyType.GetMethod("Add").Invoke(cln, new[] { pvc.PropertyName, pvc.Value });
					}
				}
			}

			return result;
		}
	}
}

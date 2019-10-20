using System;
using System.Collections.Generic;
using System.Text;

namespace Invisionware.Security.DirectoryServices.LDAP
{
	[AttributeUsage(AttributeTargets.Property)]
	public class LdapPropertyAttribute : Attribute
	{
		public LdapPropertyAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class LdapCollectionPropertyAttribute : LdapPropertyAttribute
	{
		public LdapCollectionPropertyAttribute(string name) : base(name)
		{
			Name = name;
		}

		public LdapCollectionPropertyAttribute(string name, Type collectionType) : base(name)
		{
			ElementType = collectionType;
		}

		public Type ElementType { get; set; } = typeof(string);
	}

	public class LdapDictionaryPropertyAttribute : LdapPropertyAttribute
	{
		public LdapDictionaryPropertyAttribute(string name) : base(name)
		{
			Name = name;
		}

		public Type KeyType { get; set; } = typeof(string);
		public Type ValueType { get; set; } = typeof(object);
	}
}

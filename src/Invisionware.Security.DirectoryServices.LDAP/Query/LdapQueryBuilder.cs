using System;
using System.Collections.Generic;
using System.Text;

namespace Invisionware.Security.DirectoryServices.LDAP.Query
{
	//public class LdapQueryBuilder
	//{
	//	public IList<LdapQueryCondition> WithCondition => new List<LdapQueryCondition>();

	//	public string Build()
	//	{
	//		var sb = new StringBuilder();

	//		sb.Append("(");
	//		foreach (var c in WithCondition)
	//		{
	//			sb.Append(c.ToString());
	//		}
	//		sb.Append(")");

	//		return sb.ToString();
	//	}
	//}

	//public static class LdapQueryBuilderExtensions
	//{
	//	public static LdapQueryCondition And(this LdapQueryBuilder builder)
	//	{
	//		return AddCondition(builder, LdapQueryConditionTypes.And);
	//	}

	//	public static LdapQueryCondition Or(this LdapQueryBuilder builder)
	//	{
	//		return AddCondition(builder, LdapQueryConditionTypes.Or);
	//	}

	//	private static LdapQueryCondition AddCondition(LdapQueryBuilder builder, LdapQueryConditionTypes type)
	//	{
	//		var condition = new LdapQueryCondition { ConditionType = type };

	//		builder.WithCondition.Add(condition);

	//		return condition;
	//	}
	//}
}

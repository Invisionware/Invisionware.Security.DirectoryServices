using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invisionware.Security.DirectoryServices.LDAP.Query
{
	public class LdapQueryCondition
	{
		public LdapQueryCondition Parent { get; set; }
		public IList<LdapQueryCondition> Conditions { get; set; } = new List<LdapQueryCondition>();
		public IList<LdapQueryConditionValue> Values { get; set; } = new List<LdapQueryConditionValue>();
		public LdapQueryConditionTypes ConditionType { get; set; } = LdapQueryConditionTypes.And;

		public override string ToString()
		{
			var sb = new StringBuilder();

			if (Parent != null || Values.Any())
			{
				sb.Append("(");
				sb.AppendFormat("{0}", ConditionType == LdapQueryConditionTypes.And ? "&" : "|");
			}

			foreach (var v in Values)
			{
				sb.Append(v.ToString());
			}

			foreach (var c in Conditions)
			{
				sb.Append(c.ToString());
			}

			if (Parent != null || Values.Any())
			{
				sb.Append(")");
			}

			return sb.ToString();
		}

		public LdapQueryCondition Build()
		{
			if (Parent == null) return this;

			var p = Parent;
			while (p.Parent != null)
			{
				p = p.Parent;
			}

			return p;
		}
	}

	public enum LdapQueryConditionTypes
	{
		And,
		Or
	}

}

using System.Linq;
using System.Text;

namespace Invisionware.Security.DirectoryServices.LDAP.Query
{
	public class LdapQueryConditionValue
	{
		public string Name { get; set; }
		public object Value { get; set; }

		public LdapQueryConditionValueComparisonTypes Comparison { get; set; } = LdapQueryConditionValueComparisonTypes.EqualTo;

		public override string ToString()
		{
			if (Comparison == LdapQueryConditionValueComparisonTypes.StartsWith)
				return $"({Name}={Value}*)";
			if (Comparison == LdapQueryConditionValueComparisonTypes.NotEqualTo)
				return $"(!{Name}={Value}*)";

			return $"({Name}{ComparisonTypeToString()}{Value})";
		}

		private string ComparisonTypeToString()
		{
			switch (this.Comparison)
			{
				case LdapQueryConditionValueComparisonTypes.GreaterThan: return ">";
				case LdapQueryConditionValueComparisonTypes.GreaterThanOrEqualTo: return ">=";
				case LdapQueryConditionValueComparisonTypes.LessThan: return "<";
				case LdapQueryConditionValueComparisonTypes.LessThanOrEqualto: return "<=";
				case LdapQueryConditionValueComparisonTypes.EndsWith: return "=*";
				case LdapQueryConditionValueComparisonTypes.StartsWith: return "=";
				case LdapQueryConditionValueComparisonTypes.EqualTo: return "=";
				default: return "=";
			}
		}
	}

	public enum LdapQueryConditionValueComparisonTypes
	{
		EqualTo,
		NotEqualTo,
		GreaterThan,
		GreaterThanOrEqualTo,
		LessThan,
		LessThanOrEqualto,
		StartsWith,
		EndsWith
	}

}

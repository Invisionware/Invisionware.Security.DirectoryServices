namespace Invisionware.Security.DirectoryServices.LDAP.Query
{
	public static class LdapQueryConditionExtensions
	{
		#region Condition
		public static LdapQueryCondition And(this LdapQueryCondition condition)
		{
			return AddCondition(condition, LdapQueryConditionTypes.And);
		}

		public static LdapQueryCondition Or(this LdapQueryCondition condition)
		{
			return AddCondition(condition, LdapQueryConditionTypes.Or);
		}

		private static LdapQueryCondition AddCondition(LdapQueryCondition parentCondition, LdapQueryConditionTypes type)
		{
			var condition = new LdapQueryCondition() { ConditionType = type, Parent = parentCondition };

			parentCondition.Conditions.Add(condition);

			return condition;
		}
		#endregion Condition

		#region Values
		public static LdapQueryCondition EqualTo<T>(this LdapQueryCondition condition, string name, T value)
		{
			return CreateConditionValue(condition, LdapQueryConditionValueComparisonTypes.EqualTo, name, value);
		}

		public static LdapQueryCondition NotEqualTo<T>(this LdapQueryCondition condition, string name, T value)
		{
			return CreateConditionValue(condition, LdapQueryConditionValueComparisonTypes.NotEqualTo, name, value);
		}

		public static LdapQueryCondition GreaterThan<T>(this LdapQueryCondition condition, string name, T value)
		{
			return CreateConditionValue(condition, LdapQueryConditionValueComparisonTypes.GreaterThan, name, value);
		}

		public static LdapQueryCondition GreaterThanOrEqualTo<T>(this LdapQueryCondition condition, string name, T value)
		{
			return CreateConditionValue(condition, LdapQueryConditionValueComparisonTypes.GreaterThanOrEqualTo, name, value);
		}

		public static LdapQueryCondition LessThan<T>(this LdapQueryCondition condition, string name, T value)
		{
			return CreateConditionValue(condition, LdapQueryConditionValueComparisonTypes.GreaterThan, name, value);
		}

		public static LdapQueryCondition LessThanOrEqualTo<T>(this LdapQueryCondition condition, string name, T value)
		{
			return CreateConditionValue(condition, LdapQueryConditionValueComparisonTypes.GreaterThanOrEqualTo, name, value);
		}

		public static LdapQueryCondition StartsWith<T>(this LdapQueryCondition condition, string name, T value)
		{
			return CreateConditionValue(condition, LdapQueryConditionValueComparisonTypes.StartsWith, name, value);
		}

		public static LdapQueryCondition EndsWith<T>(this LdapQueryCondition condition, string name, T value)
		{
			return CreateConditionValue(condition, LdapQueryConditionValueComparisonTypes.EndsWith, name, value);
		}

		private static LdapQueryCondition CreateConditionValue<T>(LdapQueryCondition condition, LdapQueryConditionValueComparisonTypes type, string name, T value)
		{
			var cv = new LdapQueryConditionValue { Name = name, Value = value, Comparison = type };

			condition.Values.Add(cv);
			
			return condition;
		}
		#endregion Values
	}
}

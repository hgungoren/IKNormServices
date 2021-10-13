using Serendip.IK.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Serendip.IK.Action.Common
{
    public class FilterHelper
    {
        public static LambdaExpression GetExpression(FilterRule rule, string type)
        {
            var assembly = typeof(BaseEntity).Assembly;
            var param = Expression.Parameter(assembly.GetType(type), "x");
            var rawExp = CreateCondition(rule.Rules, rule.Condition, param);
            var delegateType = typeof(Func<,>).MakeGenericType(assembly.GetType(type), typeof(bool));
            var exp = Expression.Lambda(delegateType, rawExp, param);

            return exp;
        }

        public static Expression CreateCondition(List<FilterRule> rules, string conditionOperation, ParameterExpression param)
        {
            Expression temp = null;
            foreach (var rule in rules)
            {
                if (rule.Rules != null && rule.Rules.Any())
                {
                    var innerExpression = CreateCondition(rule.Rules, rule.Condition, param);
                    if (innerExpression == null) continue;
                    if (temp != null)
                    {
                        var ruleCondition = string.IsNullOrEmpty(rule.Condition) ? conditionOperation : rule.Condition;
                        temp = ruleCondition.ToLower() == "and"
                                  ? Expression.And(temp, innerExpression)
                                  : Expression.Or(temp, innerExpression);
                    }
                    else
                    {
                        temp = innerExpression;
                    }
                }
                else
                {

                    Expression lowValue = null;
                    Expression highValue = null;
                    Expression lowCondition;
                    Expression highCondition;

                    var property = Expression.Property(param, rule.Id);
                    object rawValue = null;


                    switch (rule.Type.ToLower())
                    {
                        case "string":
                            if (property.Type == typeof(Guid) || property.Type == typeof(Guid?))
                            {
                                rawValue = Guid.Parse(rule.Value.First());
                            }
                            else if (property.Type == typeof(long) || property.Type == typeof(long?))
                            {
                                rawValue = Convert.ToInt64(rule.Value.First());
                            }
                            else
                            {
                                rawValue = rule.Value.First();
                            }
                            break;
                        case "integer":

                            if (rule.Value.Count() == 1)
                            {
                                if (property.Type == typeof(int) || property.Type == typeof(int?))
                                {
                                    rawValue = Convert.ToInt32(rule.Value.First());

                                }
                                else if (property.Type == typeof(long) || property.Type == typeof(long?))
                                {
                                    rawValue = Convert.ToInt64(rule.Value.First());

                                }
                            }
                            else
                            {
                                if (property.Type == typeof(int) || property.Type == typeof(int?))
                                {
                                    lowValue = Expression.Constant(Convert.ToInt32(rule.Value[0]));
                                    highValue = Expression.Constant(Convert.ToInt32(rule.Value[1]));
                                }
                                else if (property.Type == typeof(long) || property.Type == typeof(long?))
                                {
                                    lowValue = Expression.Constant(Convert.ToInt64(rule.Value[0]));
                                    highValue = Expression.Constant(Convert.ToInt64(rule.Value[1]));
                                }
                            }
                            break;
                        case "double":
                            if (rule.Value.Count() == 1)
                            {
                                if (property.Type == typeof(double) || property.Type == typeof(double?))
                                {
                                    rawValue = Convert.ToDouble(rule.Value.First());

                                }
                                else if (property.Type == typeof(decimal) || property.Type == typeof(decimal?))
                                {
                                    rawValue = Convert.ToDecimal(rule.Value.First());
                                }
                            }
                            else
                            {
                                if (property.Type == typeof(double) || property.Type == typeof(double?))
                                {
                                    lowValue = Expression.Constant(Convert.ToDouble(rule.Value[0]));
                                    highValue = Expression.Constant(Convert.ToDouble(rule.Value[1]));
                                }
                                else if (property.Type == typeof(decimal) || property.Type == typeof(decimal?))
                                {
                                    lowValue = Expression.Constant(Convert.ToDecimal(rule.Value[0]));
                                    highValue = Expression.Constant(Convert.ToDecimal(rule.Value[1]));
                                }
                            }
                            break;
                        case "date":
                        case "time":
                        case "datetime":
                            if (rule.Value.Count() == 1)
                            {
                                rawValue = Convert.ToDateTime(rule.Value.First());
                            }
                            else
                            {
                                lowValue = Expression.Constant(Convert.ToDateTime(rule.Value[0]));
                                highValue = Expression.Constant(Convert.ToDateTime(rule.Value[1]));
                            }
                            break;
                        case "boolean":

                            rawValue = Convert.ToBoolean(rule.Value);
                            break;
                    }

                    var value = Expression.Constant(rawValue, property.Type);
                    Expression condition = null;
             

                    var containsMethod = rule.Type == "string" ? typeof(string).GetMethod("Contains", new[] { typeof(string) }) : typeof(IList).GetMethod("Contains", new[] { typeof(string) });

                    switch (rule.Operator.ToLower())
                    {
                        case "equal":
                            condition = Expression.Equal(property, value);
                            break;
                        case "not_equal":
                            condition = Expression.NotEqual(property, value);
                            break;
                        case "in":
                        case "contains":
                            condition = Expression.AndAlso(Expression.NotEqual(property, Expression.Constant(null, property.Type)), Expression.Call(property, containsMethod, value));
                            break;
                        case "not_in":
                        case "not_contains":
                            condition = Expression.AndAlso(Expression.NotEqual(property, Expression.Constant(null, property.Type)), Expression.Not(Expression.Call(property, containsMethod, value)));
                            break;
                        case "less":
                            condition = Expression.LessThan(property, value);
                            break;
                        case "less_or_equal":
                            condition = Expression.LessThanOrEqual(property, value);
                            break;
                        case "greater":
                            condition = Expression.GreaterThan(property, value);
                            break;
                        case "greater_or_equal":
                            condition = Expression.GreaterThanOrEqual(property, value);
                            break;
                        case "between":
                            lowCondition = Expression.GreaterThanOrEqual(property, lowValue);
                            highCondition = Expression.LessThanOrEqual(property, highValue);
                            condition = Expression.AndAlso(lowCondition, highCondition);
                            break;
                        case "not_between":
                            lowCondition = Expression.GreaterThanOrEqual(property, lowValue);
                            highCondition = Expression.LessThanOrEqual(property, highValue);
                            condition = Expression.Not(Expression.AndAlso(lowCondition, highCondition));
                            break;
                        case "begins_with":
                            throw new Exception("Operation BeginWith is not implemented..");
                        case "not_begins_with":
                            throw new Exception("Operation Not BeginWith is not implemented..");
                        case "ends_with":
                            throw new Exception("Operation Not ends_with is not implemented..");
                        case "not_ends_with":
                            throw new Exception("Operation Not not_ends_with is not implemented..");
                        case "is_empty":
                            condition = Expression.Equal(property, Expression.Constant(""));
                            break;
                        case "is_not_empty":
                            condition = Expression.NotEqual(property, Expression.Constant(""));
                            break;
                        case "is_null":
                            condition = Expression.Equal(property, Expression.Constant(null));
                            break;
                        case "is_not_null":
                            condition = Expression.NotEqual(property, Expression.Constant(null));
                            break;
                    }
                    if (condition == null) continue;
                    ;
                    if (temp != null)
                    {
                        var ruleCondition = string.IsNullOrEmpty(rule.Condition) ? conditionOperation : rule.Condition;
                        temp = ruleCondition.ToLower() == "and"
                            ? Expression.And(temp, condition)
                            : Expression.Or(temp, condition);
                    }
                    else
                    {
                        temp = condition;
                    }
                }
            }
            return temp;
        }
    }
}

using KutnoAPI.Models;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace MesInternalApi.Extensions
{
    public class ConditionParser
    {
        public static Dictionary<string, object> ParsePostgres(out string whereString, List<SearchCondition> conditions, object baseObject)
        {
            var where_strings = new List<string>(); 
            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            int counter = 0;
            foreach(SearchCondition condition in conditions)
            {
                string new_string = string.Empty;
                var new_dict = ParsePostgresCondition(ref counter, condition, out new_string, baseObject, true);
                paramDict = paramDict.Concat(new_dict).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                where_strings.Add(new_string);
            }
            if (where_strings.Count > 0)
                whereString = " WHERE " + String.Join(" AND ", where_strings);
            else
                whereString = string.Empty;
            return paramDict;
        }

        private static Dictionary<string, object> ParsePostgresCondition(ref int counter, SearchCondition c, out string where_string, object baseObject, bool searchAlternatives = false)
        {
            ++counter;
            where_string = string.Empty;
            Dictionary<string, object> paramDict = new Dictionary<string, object>();

            if (c.Operator is eOperators.IN)
            {

                var column_type = baseObject.GetType().GetProperty(c.Column).GetValue(baseObject).GetType();


                where_string = c.Column + " = ANY(@" + c.Column + counter.ToString() + ")";
                var values = c.Value.Split(',')
                    .Select(v => TypeDescriptor.GetConverter(column_type).ConvertFromString(v))
                    .ToArray();

                Array destinationArray = null;
                if (column_type.IsEnum)
                {
                    destinationArray = Array.CreateInstance(0.GetType(), values.Length);
                    Array.Copy(values.Select(e => (int)e).ToArray(), destinationArray, values.Length);
                }
                else
                {
                    destinationArray = Array.CreateInstance(column_type, values.Length);
                    Array.Copy(values, destinationArray, values.Length);

                }
                paramDict.Add("@" + c.Column + counter.ToString(), destinationArray);
            }
            else
            {
                where_string = (c.Column + " " + c.eOperatorString() + " @" + c.Column + counter.ToString());
                paramDict.Add("@" + c.Column + counter.ToString(), TypeDescriptor.GetConverter(baseObject.GetType().GetProperty(c.Column).GetValue(baseObject).GetType()).ConvertFromString(c.Value));
            }
            if(searchAlternatives)
            {
                var cWithAlternatives = c as SearchConditionWithAlternatives;
                if (cWithAlternatives is not null)
                {
                    var or_strings = new List<string>();
                    foreach (var cond in cWithAlternatives.AlternativeConditions)
                    {
                        var new_string = string.Empty;
                        var new_dict = ParsePostgresCondition(ref counter, cond, out new_string, baseObject);
                        where_string = where_string + " OR " + new_string;
                        paramDict = paramDict.Concat(new_dict).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    }
                    where_string = "(" + where_string + ")";
                }
            }
            return paramDict;
        }

        public static Dictionary<string, object> ParseMSSQL(out string whereString, List<SearchCondition> conditions, object baseObject)
        {
            var where_strings = new List<string>();
            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            int counter = 0;
            foreach (SearchCondition condition in conditions)
            {
                string new_string = string.Empty;
                var new_dict = ParseMSSQLCondition(ref counter, condition, out new_string, baseObject, true);
                paramDict = paramDict.Concat(new_dict).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                where_strings.Add(new_string);
            }
            if (where_strings.Count > 0)
                whereString = " WHERE " + String.Join(" AND ", where_strings);
            else
                whereString = string.Empty;
            return paramDict;
        }

        private static Dictionary<string, object> ParseMSSQLCondition(ref int counter, SearchCondition c, out string where_string, object baseObject, bool searchAlternatives = false)
        {
            ++counter;
            where_string = string.Empty;
            Dictionary<string, object> paramDict = new Dictionary<string, object>();

            if ("Date".Equals(c.Column))
            {
                where_string = "CAST([Date] as DATE) " + c.eOperatorString() + " @" + c.Column;
            }
            else
            {
                where_string = "[" + c.Column + "] " + c.eOperatorString() + " @" + c.Column;
            }
            if (c.Operator is eOperators.IN)
            {
                var values = c.Value.Split(',')
                    .Select(v => TypeDescriptor.GetConverter(baseObject.GetType().GetProperty(c.Column).GetValue(baseObject).GetType()).ConvertFromString(v))
                    .ToArray();
                Array destinationArray = Array.CreateInstance(baseObject.GetType().GetProperty(c.Column).GetValue(baseObject).GetType(), values.Length);
                Array.Copy(values, destinationArray, values.Length);
                paramDict.Add("@" + c.Column, destinationArray);
            } if(c.Operator is eOperators.ILIKE)
            {
                where_string = "[" + c.Column + "] collate SQL_Latin1_General_CP1_CI_AS like @" + c.Column;
                paramDict.Add("@" + c.Column, TypeDescriptor.GetConverter(baseObject.GetType().GetProperty(c.Column).GetValue(baseObject).GetType()).ConvertFromString(c.Value));
            }
            else
            {
                paramDict.Add("@" + c.Column, TypeDescriptor.GetConverter(baseObject.GetType().GetProperty(c.Column).GetValue(baseObject).GetType()).ConvertFromString(c.Value));
            }
            if (searchAlternatives)
            {
                var cWithAlternatives = c as SearchConditionWithAlternatives;
                if (cWithAlternatives is not null)
                {
                    var or_strings = new List<string>();
                    foreach (var cond in cWithAlternatives.AlternativeConditions)
                    {
                        var new_string = string.Empty;
                        var new_dict = ParseMSSQLCondition(ref counter, cond, out new_string, baseObject);
                        where_string = where_string + " OR " + new_string;
                        paramDict = paramDict.Concat(new_dict).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    }
                    where_string = "(" + where_string + ")";
                }
            }
            return paramDict;
        }
    }
}

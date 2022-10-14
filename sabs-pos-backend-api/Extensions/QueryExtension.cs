using CoreLib.Sql;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;

namespace sabs_pos_backend_api
{
    public class QF
    {
        public static string EM(string field)
        {
            return string.Format("{1}{0}em", QueryDevide.FUNC_PART, field);
        }

        public static string NEM(string field)
        {
            return string.Format("{1}{0}nem", QueryDevide.FUNC_PART, field);
        }

        public static string EQ(string field, object value)
        {
            return string.Format("{1}{0}eq{0}{2}", QueryDevide.FUNC_PART, field, value);
        }

        public static string NEQ(string field, object value)
        {
            return string.Format("{1}{0}neq{0}{2}", QueryDevide.FUNC_PART, field, value);
        }

        public static string IN(string field, IEnumerable<object> values)
        {
            return string.Format("{1}{0}in{0}{2}", QueryDevide.FUNC_PART, field, string.Join(QueryDevide.VALUE, values));
        }

        public static string NIN(string field, IEnumerable<object> values)
        {
            return string.Format("{1}{0}nin{0}{2}", QueryDevide.FUNC_PART, field, string.Join(QueryDevide.VALUE, values));
        }

        public static string LW(string field, object value)
        {
            return string.Format("{1}{0}lw{0}{2}", QueryDevide.FUNC_PART, field, value);
        }

        public static string SW(string field, object value)
        {
            return string.Format("{1}{0}sw{0}{2}", QueryDevide.FUNC_PART, field, value);
        }

        public static string EW(string field, object value)
        {
            return string.Format("{1}{0}ew{0}{2}", QueryDevide.FUNC_PART, field, value);
        }

        public static string BT(string field, object value1, object value2)
        {
            return string.Format("{1}{0}bt{0}{2}", QueryDevide.FUNC_PART, field, value1 + QueryDevide.VALUE + value2);
        }

        public static string GT(string field, object value)
        {
            return string.Format("{1}{0}gt{0}{2}", QueryDevide.FUNC_PART, field, value);
        }

        public static string GTE(string field, object value)
        {
            return string.Format("{1}{0}gte{0}{2}", QueryDevide.FUNC_PART, field, value);
        }

        public static string LT(string field, object value)
        {
            return string.Format("{1}{0}lt{0}{2}", QueryDevide.FUNC_PART, field, value);
        }

        public static string LTE(string field, object value)
        {
            return string.Format("{1}{0}lte{0}{2}", QueryDevide.FUNC_PART, field, value);
        }

        public static string EM(string table, string field)
        {
            return string.Format("{2}{1}{3}{0}em", QueryDevide.FUNC_PART, table, field);
        }

        public static string NEM(string table, string field)
        {
            return string.Format("{2}{1}{3}{0}nem", QueryDevide.FUNC_PART, table, field);
        }

        public static string EQ(string table, string field, object value)
        {
            return string.Format("{2}{1}{3}{0}eq{0}{4}", QueryDevide.FUNC_PART, QueryDevide.FIELD_PART, table, field, value);
        }

        public static string NEQ(string table, string field, object value)
        {
            return string.Format("{2}{1}{3}{0}neq{0}{4}", QueryDevide.FUNC_PART, table, field, value);
        }

        public static string IN(string table, string field, IEnumerable<object> values)
        {
            return string.Format("{2}{1}{3}{0}in{0}{4}", QueryDevide.FUNC_PART, table, field, string.Join(QueryDevide.VALUE, values));
        }

        public static string NIN(string table, string field, IEnumerable<object> values)
        {
            return string.Format("{2}{1}{3}{0}nin{0}{4}", QueryDevide.FUNC_PART, table, field, string.Join(QueryDevide.VALUE, values));
        }

        public static string LW(string table, string field, object value)
        {
            return string.Format("{2}{1}{3}{0}lw{0}{4}", QueryDevide.FUNC_PART, table, field, value);
        }

        public static string SW(string table, string field, object value)
        {
            return string.Format("{2}{1}{3}{0}sw{0}{4}", QueryDevide.FUNC_PART, table, field, value);
        }

        public static string EW(string table, string field, object value)
        {
            return string.Format("{2}{1}{3}{0}ew{0}{4}", QueryDevide.FUNC_PART, table, field, value);
        }

        public static string BT(string table, string field, object value1, object value2)
        {
            return string.Format("{2}{1}{3}{0}bt{0}{4}", QueryDevide.FUNC_PART, table, field, value1 + QueryDevide.VALUE + value2);
        }

        public static string GT(string table, string field, object value)
        {
            return string.Format("{2}{1}{3}{0}gt{0}{4}", QueryDevide.FUNC_PART, table, field, value);
        }

        public static string GTE(string table, string field, object value)
        {
            return string.Format("{2}{1}{3}{0}gte{0}{4}", QueryDevide.FUNC_PART, table, field, value);
        }

        public static string LT(string table, string field, object value)
        {
            return string.Format("{2}{1}{3}{0}lt{0}{4}", QueryDevide.FUNC_PART, table, field, value);
        }

        public static string LTE(string table, string field, object value)
        {
            return string.Format("{2}{1}{3}{0}lte{0}{4}", QueryDevide.FUNC_PART, table, field, value);
        }
    }

    public class QI
    {
        public static string IC(string table, params string[] fields)
        {
            return table + QueryDevide.FUNC_PART + string.Join(QueryDevide.FIELD, fields);
        }
    }

    public class QS
    {
        public static string ASC(string field)
        {
            return field + QueryDevide.FUNC_PART + SqlConstant.SortType.Asc;
        }

        public static string ASC(string table, string field)
        {
            var tablePrefix = string.IsNullOrEmpty(table) ? "" : table + QueryDevide.FIELD_PART;
            return tablePrefix + field + QueryDevide.FUNC_PART + SqlConstant.SortType.Asc;
        }

        public static string DESC(string field)
        {
            return field + QueryDevide.FUNC_PART + SqlConstant.SortType.Desc;
        }

        public static string DESC(string table, string field)
        {
            var tablePrefix = string.IsNullOrEmpty(table) ? "" : table + QueryDevide.FIELD_PART;
            return tablePrefix + field + QueryDevide.FUNC_PART + SqlConstant.SortType.Desc;
        }
    }

    public static class QueryExtension
    {
        public static string Merge(this string func, string otherFunc)
        {
            return func + QueryDevide.FUNC + otherFunc;
        }

        public static string Merge(this IEnumerable<string> funcs)
        {
            return string.Join(QueryDevide.FUNC, funcs);
        }

        public static ISqlQueryable<T> BuildCursor<T>(this ISqlQueryable<T> queryable, string cursor) where T : class
        {
            if (string.IsNullOrEmpty(cursor) || cursor == "*")
                return queryable;

            var fields = cursor.Split(QueryDevide.FIELD);
            return queryable.Select(fields);
        }

        public static ISqlQueryable<T> BuildFilter<T>(this ISqlQueryable<T> queryable, string filter) where T : class
        {
            if (string.IsNullOrEmpty(filter))
                return queryable;

            var filterInfos = filter.Split(QueryDevide.FUNC);
            foreach (var filterInfo in filterInfos)
            {
                var filterParts = filterInfo.getFilterPart();
                if (filterParts.Length <= 1)
                    throw new ResponseException(ResponseCode.INVALID_VALUE, "Filter is invalid format");

                var filterType = filterParts[1];
                if (!filterType.StartsWith("$"))
                    filterType = "$" + filterType;

                object filterValue = null;
                if (!(new string[] { "$em", "$nem" }).Contains(filterType) && !string.IsNullOrEmpty(filterParts[2]))
                {
                    var vals = filterParts[2].Split(QueryDevide.VALUE);
                    if (vals.Length > 1)
                    {
                        var arr = new JArray();
                        foreach (var val in vals)
                        {
                            if (val.ToLower() == "null")
                                arr.Add(null);
                            else
                                arr.Add(JToken.FromObject(val));
                        }
                        filterValue = arr;
                    }
                    else
                    {
                        if (vals[0].ToLower() != "null")
                            filterValue = JToken.FromObject(vals[0]);
                    }
                }

                if (filterType == "$em")
                    filterType = SqlConstant.OperatorType.Equal;
                if (filterType == "$nem")
                    filterType = SqlConstant.OperatorType.NotEqual;

                var filterFields = filterParts[0].Split(QueryDevide.FIELD);
                if (filterFields.Length > 1)
                {
                    var fieldArr = new List<string>();
                    foreach (var filterField in filterFields)
                    {
                        if (filterField.Contains(QueryDevide.FIELD_PART))
                            fieldArr.Add("$" + filterField);
                        else
                            fieldArr.Add(filterField);
                    }
                    queryable = queryable.Match("$arr:" + string.Join(",", fieldArr), filterValue, filterType);
                }
                else
                {
                    var fieldParts = filterFields[0].Split(QueryDevide.FIELD_PART);
                    if (fieldParts.Length > 1)
                        queryable = queryable.Match(fieldParts[0], fieldParts[1], filterValue, filterType);
                    else
                        queryable = queryable.Match(fieldParts[0], filterValue, filterType);
                }
            }

            return queryable;
        }

        public static ISqlQueryable<T> BuildInclude<T>(this ISqlQueryable<T> queryable, string include) where T : class
        {
            if (string.IsNullOrEmpty(include))
                return queryable;

            var includeInfos = include.Split(QueryDevide.FUNC);
            foreach (var includeInfo in includeInfos)
            {
                var table = "";
                var fields = new string[] { };
                var includeParts = includeInfo.Split(QueryDevide.FUNC_PART);
                if (includeParts.Length > 1)
                {
                    table = includeParts[0];
                    fields = includeParts[1].Split(QueryDevide.FIELD);
                }
                else
                    fields = includeParts[0].Split(QueryDevide.FIELD);

                var includeFields = new List<SqlIncludeField>();
                foreach (var field in fields)
                {
                    var sliptChar = " ";
                    if (field.Contains("[AS]"))
                        sliptChar = "[AS]";
                    if (field.Contains("[as]"))
                        sliptChar = "[as]";

                    var fieldInfos = field.Split(sliptChar);
                    includeFields.Add(new SqlIncludeField { Name = fieldInfos[0], Alias = fieldInfos.Length > 1 ? fieldInfos[1] : "" });
                }
                queryable = queryable.Include(table, includeFields);
            }

            return queryable;
        }

        public static ISqlQueryable<T> BuildSort<T>(this ISqlQueryable<T> queryable, string sort) where T : class
        {
            if (string.IsNullOrEmpty(sort))
                return queryable;

            var sortInfos = sort.Split(QueryDevide.FUNC);
            foreach (var sortInfo in sortInfos)
            {
                var sortParts = sortInfo.Split(QueryDevide.FUNC_PART);
                var sortType = sortParts.Length > 1 ? sortParts[1] : SqlConstant.SortType.Asc;
                var sortFields = sortParts[0].Split(QueryDevide.FIELD);
                foreach (var sortField in sortFields)
                {
                    var fieldParts = sortField.Split(QueryDevide.FIELD_PART);
                    if (fieldParts.Length > 1)
                        queryable = queryable.Sort(fieldParts[0], fieldParts[1], sortType);
                    else
                        queryable = queryable.Sort(fieldParts[0], sortType);
                }
            }

            return queryable;
        }

        static string[] getFilterPart(this string filterInfo)
        {
            var parts = new List<string>();

            var devideIndex = filterInfo.IndexOf(QueryDevide.FUNC_PART);
            if (devideIndex != -1)
            {
                parts.Add(filterInfo.Substring(0, devideIndex));
                filterInfo = filterInfo.Remove(0, devideIndex + 1);
            }

            devideIndex = filterInfo.IndexOf(QueryDevide.FUNC_PART);
            if (devideIndex != -1)
            {
                parts.Add(filterInfo.Substring(0, devideIndex));
                filterInfo = filterInfo.Remove(0, devideIndex + 1);
            }
            else
            {
                parts.Add(filterInfo);
                filterInfo = "";
            }

            parts.Add(filterInfo);

            return parts.ToArray();
        }
    }
}

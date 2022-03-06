using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Annotachan {
    public static class DbExtensions {
        public static void LoadFromRow(this object value, SqlResultRow row) {
            if (value == null) {
                return;
            }
            if (row == null) {
                return;
            }
            value.SetValues(row);
        }
        public static T Create<T>(this SqlResultRow row) {
            var obj = ConstructorCache<T>.CreateInstance();
            obj.SetValues(row);
            return obj;
        }

        public static T Create<T, T0>(this SqlResultRow row, T0 param0) {
            var obj = ConstructorCache<T, T0>.CreateInstance(param0);
            obj.SetValues(row);
            return obj;
        }
        public static T Create<T, T0, T1>(this SqlResultRow row, T0 param0, T1 param1) {
            var obj = ConstructorCache<T, T0, T1>.CreateInstance(param0, param1);
            obj.SetValues(row);
            return obj;
        }
        public static T Create<T, T0, T1, T2>(this SqlResultRow row, T0 param0, T1 param1, T2 param2) {
            var obj = ConstructorCache<T, T0, T1, T2>.CreateInstance(param0, param1, param2);
            obj.SetValues(row);
            return obj;
        }
        public static T Create<T, T0, T1, T2, T3>(this SqlResultRow row, T0 param0, T1 param1, T2 param2, T3 param3) {
            var obj = ConstructorCache<T, T0, T1, T2, T3>.CreateInstance(param0, param1, param2, param3);
            obj.SetValues(row);
            return obj;
        }
        public static T Create<T, T0, T1, T2, T3, T4>(this SqlResultRow row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4) {
            var obj = ConstructorCache<T, T0, T1, T2, T3, T4>.CreateInstance(param0, param1, param2, param3, param4);
            obj.SetValues(row);
            return obj;
        }
        public static T Create<T, T0, T1, T2, T3, T4, T5>(this SqlResultRow row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) {
            var obj = ConstructorCache<T, T0, T1, T2, T3, T4, T5>.CreateInstance(param0, param1, param2, param3, param4, param5);
            obj.SetValues(row);
            return obj;
        }
        public static T Create<T, T0, T1, T2, T3, T4, T5, T6>(this SqlResultRow row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) {
            var obj = ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6>.CreateInstance(param0, param1, param2, param3, param4, param5, param6);
            obj.SetValues(row);
            return obj;
        }

        public static T Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(this SqlResultRow row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) {
            var obj = ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6, T7>.CreateInstance(param0, param1, param2, param3, param4, param5, param6, param7);
            obj.SetValues(row);
            return obj;
        }

        #region ConstructorCache
        private static class ConstructorCache<T> {
            static ConstructorCache() {
                ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, Type.EmptyTypes, null);
            }
            private static readonly ConstructorInfo ConstructorInfo;
            public static T CreateInstance() {
                return (T)ConstructorInfo.Invoke(null);
            }
        }
        private static class ConstructorCache<T, T0> {
            static ConstructorCache() {
                ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0) }, null);
            }
            private static readonly ConstructorInfo ConstructorInfo;
            public static T CreateInstance(T0 param0) {
                return (T)ConstructorInfo.Invoke(new object[] { param0 });
            }
        }
        private static class ConstructorCache<T, T0, T1> {
            static ConstructorCache() {
                ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1) }, null);
            }
            private static readonly ConstructorInfo ConstructorInfo;
            public static T CreateInstance(T0 param0, T1 param1) {
                return (T)ConstructorInfo.Invoke(new object[] { param0, param1 });
            }
        }
        private static class ConstructorCache<T, T0, T1, T2> {
            static ConstructorCache() {
                ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2) }, null);
            }
            private static readonly ConstructorInfo ConstructorInfo;
            public static T CreateInstance(T0 param0, T1 param1, T2 param2) {
                return (T)ConstructorInfo.Invoke(new object[] { param0, param1, param2 });
            }
        }
        private static class ConstructorCache<T, T0, T1, T2, T3> {
            static ConstructorCache() {
                ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3) }, null);
            }
            private static readonly ConstructorInfo ConstructorInfo;
            public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3) {
                return (T)ConstructorInfo.Invoke(new object[] { param0, param1, param2, param3 });
            }
        }
        private static class ConstructorCache<T, T0, T1, T2, T3, T4> {
            static ConstructorCache() {
                ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4) }, null);
            }
            private static readonly ConstructorInfo ConstructorInfo;
            public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3, T4 param4) {
                return (T)ConstructorInfo.Invoke(new object[] { param0, param1, param2, param3, param4 });
            }
        }
        private static class ConstructorCache<T, T0, T1, T2, T3, T4, T5> {
            static ConstructorCache() {
                ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) }, null);
            }
            private static readonly ConstructorInfo ConstructorInfo;
            public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) {
                return (T)ConstructorInfo.Invoke(new object[] { param0, param1, param2, param3, param4, param5 });
            }
        }
        private static class ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6> {
            static ConstructorCache() {
                ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) }, null);
            }
            private static readonly ConstructorInfo ConstructorInfo;
            public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) {
                return (T)ConstructorInfo.Invoke(new object[] { param0, param1, param2, param3, param4, param5, param6 });
            }
        }
        private static class ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6, T7> {
            static ConstructorCache() {
                ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) }, null);
            }
            private static readonly ConstructorInfo ConstructorInfo;
            public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) {
                return (T)ConstructorInfo.Invoke(new object[] { param0, param1, param2, param3, param4, param5, param6, param7 });
            }
        }
        #endregion
        #region FieldInfoCache
        private static class FieldInfoCache<T> {
            static FieldInfoCache() {
                FieldInfo = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Fields = new ConcurrentDictionary<FieldInfo, DbColumnAttribute>(FieldInfo.ToDictionary(x => x, x => x.GetCustomAttributes(typeof(DbColumnAttribute), false)).
                    Where(x => x.Value != null && x.Value.Length > 0).
                    ToDictionary(x => x.Key, x => x.Value[0] as DbColumnAttribute));
            }
            private static readonly FieldInfo[] FieldInfo;
            public static readonly ConcurrentDictionary<FieldInfo, DbColumnAttribute> Fields;
        }
        #endregion
        #region PropertyInfoCache
        private static class PropertyInfoCache<T> {
            static PropertyInfoCache() {
                PropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                Properties = new ConcurrentDictionary<PropertyInfo, DbColumnAttribute>(PropertyInfo.ToDictionary(x => x, x => x.GetCustomAttributes(typeof(DbColumnAttribute), false)).
                    Where(x => x.Value != null && x.Value.Length > 0).
                    ToDictionary(x => x.Key, x => x.Value[0] as DbColumnAttribute));
            }
            private static readonly PropertyInfo[] PropertyInfo;
            public static readonly ConcurrentDictionary<System.Reflection.PropertyInfo, DbColumnAttribute> Properties;
        }
        #endregion

        public static void SetValues<T>(this T src, SqlResultRow row) {
            foreach (KeyValuePair<PropertyInfo, DbColumnAttribute> p in PropertyInfoCache<T>.Properties) {
                p.Key.SetValue(src, p.GetValue(row), null);
            }
            foreach (KeyValuePair<FieldInfo, DbColumnAttribute> f in FieldInfoCache<T>.Fields) {
                f.Key.SetValue(src, f.GetValue(row));
            }
        }


        private static object GetValue(this KeyValuePair<FieldInfo, DbColumnAttribute> f, SqlResultRow row) {
            return row[f.Value.ColumnName].GetValue(f.Key.FieldType, f.Value?.DateFormat);
        }

        private static object GetValue(this KeyValuePair<PropertyInfo, DbColumnAttribute> p, SqlResultRow row) {
            return row[p.Value.ColumnName].GetValue(p.Key.PropertyType, p.Value?.DateFormat);
        }
        private static Dictionary<Type, Dictionary<Enum, long>> _enumValues = new Dictionary<Type, Dictionary<Enum, long>>();
        private static Dictionary<Enum, long> GetEnumValues(Type t) {

            if (!_enumValues.ContainsKey(t)) {
                _enumValues.Add(t, EnumsNET.Enums.GetValues(t).ToDictionary(
                    e => e,
                    e => Convert.ChangeType(e, Enum.GetUnderlyingType(t)).ToLongN()).
                    Where(x => x.Value.HasValue).ToDictionary(e => (Enum)e.Key, e => e.Value.Value));
            }
            return _enumValues[t];
        }

        private static object GetValue(this object value, Type t, string format) {
            if (t.IsEnum) {
                if (value is string) {
                    return EnumsNET.Enums.GetValues(t).Where(x => x.ToString().Equals(value?.ToString(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                } else {
                    return GetEnumValues(t).Where(x => x.Value == value.ToLongN()).Select(x => x.Key).FirstOrDefault();
                }
            }
            if (t == typeof(DateTime)) {
                if (!string.IsNullOrEmpty(format)) {
                    return value.ToDateTime(format).Value;
                } else {
                    return value.ToDateTime().Value;
                }
            } else if (t == typeof(DateTime?)) {
                if (!string.IsNullOrEmpty(format)) {
                    return value.ToDateTime(format);
                } else {
                    return value.ToDateTime();
                }
            } else if (t == typeof(int)) {
                return value.ToIntN().Value;
            } else if (t == typeof(int?)) {
                return value.ToIntN();
            } else if (t == typeof(decimal)) {
                return value.ToDecimalN().Value;
            } else if (t == typeof(decimal?)) {
                return value.ToDecimalN();
            } else if (t == typeof(float)) {
                return value.ToFloatN().Value;
            } else if (t == typeof(float?)) {
                return value.ToFloatN();
            } else if (t == typeof(double)) {
                return value.ToDoubleN().Value;
            } else if (t == typeof(double?)) {
                return value.ToDoubleN();
            } else if (t == typeof(long)) {
                return value.ToLongN().Value;
            } else if (t == typeof(long?)) {
                return value.ToLongN();
            } else if (t == typeof(string)) {
                return value.ToString();
            } else if (t == typeof(bool)) {
                return "Y".Equals(value?.ToString(), StringComparison.OrdinalIgnoreCase);
            } else if (t == typeof(bool?)) {
                if (value == null) {
                    return null;
                } else {
                    return "Y".Equals(value.ToString(), StringComparison.OrdinalIgnoreCase);
                }
            } else {
                return Convert.ChangeType(value, t);
            }
        }

        public static int? ToIntN(this object value) {
            if (value == null || value == DBNull.Value) {
                return null;
            }
            int ret;
            return int.TryParse(value.ToString(), out ret) ? ret : (int?)null;
        }

        internal static int ToInt32(this object value, int defaultValue) {
            return value.ToIntN() ?? defaultValue;
        }

        internal static DateTime? ToDateTime(this object value) {
            if (value == null || value == DBNull.Value) {
                return null;
            }
            if (value is DateTime) {
                return (DateTime)value;
            }
            if (value is DateTime?) {
                return (DateTime?)value;
            }
            return null;
        }

        internal static DateTime? ToDateTime(this object value, string dateFormat) {
            if (value == null || value == DBNull.Value) {
                return null;
            }
            DateTime d;
            return DateTime.TryParseExact(value.ToString(), dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d) ? d : (DateTime?)null;
        }

        internal static decimal? ToDecimalN(this object value) {
            if (value == null || value == DBNull.Value) {
                return null;
            }
            decimal ret;
            return decimal.TryParse(value.ToString(), out ret) ? ret : (decimal?)null;
        }

        internal static decimal ToDecimal(this object value, decimal defaultValue) {
            return value.ToDecimalN() ?? defaultValue;
        }
        internal static float? ToFloatN(this object value) {
            if (value == null || value == DBNull.Value) {
                return null;
            }
            float ret;
            return float.TryParse(value.ToString(), out ret) ? ret : (float?)null;
        }

        internal static float ToFloat(this object value, float defaultValue) {
            return value.ToFloatN() ?? defaultValue;
        }

        internal static double? ToDoubleN(this object value) {
            if (value == null || value == DBNull.Value) {
                return null;
            }
            double ret;
            return double.TryParse(value.ToString(), out ret) ? ret : (double?)null;
        }

        internal static double ToDouble(this object value, double defaultValue) {
            return value.ToDoubleN() ?? defaultValue;
        }
        internal static long? ToLongN(this object value) {
            if (value == null || value == DBNull.Value) {
                return null;
            }
            long ret;
            return long.TryParse(value.ToString(), out ret) ? ret : (long?)null;
        }

        internal static long ToLong(this object value, long defaultValue) {
            return value.ToLongN() ?? defaultValue;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Domain
{
    public class DataBaseUtil
    {

        public static string GetProcedureName(string tableName, ProcedureType type)
        {
            return string.Format("{0}{1}", Enum.GetName(typeof(ProcedureType), (int)type), tableName);
        }

        private static Dictionary<Type, IList<PropertyInfo>> typeDictionary = new Dictionary<Type, IList<PropertyInfo>>();

        public static IList<PropertyInfo> GetPropertiesForType<T>()
        {
            var type = typeof(T);
            if (!typeDictionary.ContainsKey(typeof(T)))
            {
                typeDictionary.Add(type, type.GetProperties().ToList());
            }
            return typeDictionary[type];
        }

        public static IList<T> DataTableToList<T>(DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = GetPropertiesForType<T>();
            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        public static PropertyInfo GetIdProperty<T>()
        {
            IList<PropertyInfo> propertyInfos = GetPropertiesForType<T>();

            PropertyInfo IdProperty = (from PropertyInfo property in propertyInfos
                                       where property.GetCustomAttributes(typeof(IdentifierAttribute), true).Length > 0
                                       select property).First();
            return IdProperty;
        }

        //public static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        //{
        //    T item = new T();
        //    foreach (var property in properties)
        //    {
        //        if (row[property.Name] != DBNull.Value)
        //        {
        //            property.SetValue(item, row[property.Name], null);
        //        }
        //    }
        //    return item;
        //}

        public static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();

            foreach (var property in properties)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    object value = row[property.Name];

                    if (value == DBNull.Value)
                    {
                        // Assign null if the property is a reference or nullable type
                        if (!property.PropertyType.IsValueType || Nullable.GetUnderlyingType(property.PropertyType) != null)
                        {
                            property.SetValue(item, null);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Cannot assign null to non-nullable property {property.Name}");
                        }            
                    }
                    else
                    {
                        Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        object safeValue = Convert.ChangeType(value, targetType);
                        property.SetValue(item, safeValue);
                    }
                }
            }

            return item;
        }

    }
}

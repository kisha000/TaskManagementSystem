using System.Collections.Generic;
using System.Web;

namespace TaskManagementSystem.Common
{
    public class SessionCookieManager
    {
        public static void SetSessionValues(Dictionary<string, object> values)
        {
            foreach (var kvp in values)
            {
                HttpContext.Current.Session[kvp.Key] = kvp.Value;
            }
        }

        public static void SetSessionValues(object values)
        {
            var properties = values.GetType().GetProperties();
            foreach (var property in properties)
            {
                HttpContext.Current.Session[property.Name] = property.GetValue(values);
            }
        }

        public static T GetSessionValue<T>(string key)
        {
            object sessionValue = HttpContext.Current.Session[key];
            if (sessionValue != null && sessionValue is T)
            {
                return (T)sessionValue;
            }
            return default(T);
        }

        public static void RemoveSessionValue(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        public static void AbandonSession()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}

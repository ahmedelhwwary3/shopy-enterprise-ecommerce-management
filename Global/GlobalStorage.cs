using Enterprise_E_Commerce_Management_System.Global.Extension_Methods;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using System.Collections.Generic;

namespace Enterprise_E_Commerce_Management_System.Global
{
    public static class GlobalStorage
    {
        public static readonly string CartIdKey = "CartId";
        public static readonly string CartItemsCountKey = "CartItemsCount";

        public static readonly string CurrencyIdKey = "CurrencyId";

        public static readonly string CountryIdKey = "CountryId";
        public static readonly string CustomerIdKey = "CustomerId"; 
        public static readonly string ProductIdKey = "ProductId";
        public static readonly string PermissionKey = "Permission"; 

        public static readonly string CourierIdKey = "CourierId"; 
        public static readonly string OrdersCountKey = "CourierOrdersCount";

        public static int GetCookieCurrencyIdOrDefault(HttpContext context)
        {
            int? currencyId = storage.GetInt32FromCookies(storage.CurrencyIdKey, context);
            return currencyId.HasValue ? currencyId.Value : valid.DollarCurrencyId;
        }
        public static List<string> GetPermissionCodePolicies()
        {
            return Enum.GetValues(typeof(enPermissions))
                .Cast<enPermissions>()
                .Select(p=>p.GetPermissionCode())
                .ToList(); 
        }
        public static int? GetInt32FromSessionOrCookies(string key, HttpContext context)
        {
            int? cartId = context.Session.GetInt32(key);
            if (!cartId.HasValue)
            {
                if(!context.Request.Cookies.ContainsKey(key))
                    return null;

                string? cookie = context.Request.Cookies[key];
                if (!string.IsNullOrEmpty(cookie) &&
                    int.TryParse(cookie, out int Id))
                {
                    cartId = Id;
                    context.Session.SetInt32(key, Id);
                    return cartId;
                }
               
            }
            return cartId;
        }
        public static int? GetInt32FromSession(string key, HttpContext context)
        {
            return context.Session.GetInt32(key);
        
        }
        public static int? GetInt32FromCookies(string key, HttpContext context)
        {
            if (!context.Request.Cookies.ContainsKey(key))
                return null;

            var customerId = context.Request.Cookies[key];
            if (!string.IsNullOrEmpty(customerId) &&
                 int.TryParse(customerId, out int custId))
                return custId;

            return null;
        }
        public static void RefreshCookieDays(string key, int value, HttpContext context,int days)
        {
            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(days)
            };
            context.Response.Cookies.Append(key, $"{value}", options);
        }
        public static void RefreshCookieMinutes(string key, int value, HttpContext context, int minutes)
        {
            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            };
            context.Response.Cookies.Append(key, $"{value}", options);
        }
        public static void RefreshSession(string key, int value, HttpContext context)
        {
            context.Session.SetInt32(key, value);
        }
        public static void RefreshSession(string key, string value, HttpContext context)
        {
            context.Session.SetString(key, value);
        }
        public static void RefreshCookie(string key, string value, HttpContext context)
        {
            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddMonths
                (ValidationConstants.CustomerCookieMonths)
            };
            context.Response.Cookies.Append(key, $"{value}", options);
        }
        public static void RefreshCookie(string key, int value, HttpContext context)
        {
            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddMonths
                (ValidationConstants.CustomerCookieMonths)
            };
            context.Response.Cookies.Append(key, $"{value}", options);
        }
        public static void RemoveSession(string key,HttpContext context)
        {
            context.Session.Remove(key);
        }
        public static void RemoveCookie(string key, HttpContext context)
        {
            context.Response.Cookies.Delete(key);
        }
        public static void RemoveSessionAndCookie(string key, HttpContext context)
        {
            RemoveSession(key,context);
            RemoveCookie(key,context);
        }
    }
}

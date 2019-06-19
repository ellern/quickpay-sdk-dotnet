using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace QuickPay.SDK
{
    internal static class StringExtensions
    {
        public static Uri FormatUri(this string pattern, params object[] args) => new Uri(string.Format(CultureInfo.InvariantCulture, pattern, args), UriKind.Relative);

        /// <summary>
        /// Adds the specified parameter to the Query String.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramName">Name of the parameter to add.</param>
        /// <param name="paramValue">Value for the parameter to add.</param>
        /// <returns>Url with added parameter.</returns>
        public static Uri AddParameter(this Uri uri, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        ///     Adds query string value to an existing url, both absolute and relative URI's are supported.
        /// </summary>
        /// <example>
        /// <code>
        ///     // returns "www.domain.com/test?param1=val1&amp;param2=val2&amp;param3=val3"
        ///     new Uri("www.domain.com/test?param1=val1").ExtendQuery(new Dictionary&lt;string, string&gt; { { "param2", "val2" }, { "param3", "val3" } }); 
        /// 
        ///     // returns "/test?param1=val1&amp;param2=val2&amp;param3=val3"
        ///     new Uri("/test?param1=val1").ExtendQuery(new Dictionary&lt;string, string&gt; { { "param2", "val2" }, { "param3", "val3" } }); 
        /// </code>
        /// </example>
        /// <param name="uri"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Uri ExtendQuery(this Uri uri, IDictionary<string, string> values)
        {
            var baseUrl = uri.ToString();
            var queryString = string.Empty;
            if (baseUrl.Contains("?"))
            {
                var urlSplit = baseUrl.Split('?');
                baseUrl = urlSplit[0];
                queryString = urlSplit.Length > 1 ? urlSplit[1] : string.Empty;
            }

            var queryCollection = HttpUtility.ParseQueryString(queryString);
            foreach (var kvp in values ?? new Dictionary<string, string>())
            {
                queryCollection[kvp.Key] = kvp.Value;
            }
            var uriKind = uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative;
            return queryCollection.Count == 0
              ? new Uri(baseUrl, uriKind)
              : new Uri(string.Format("{0}?{1}", baseUrl, queryCollection), uriKind);
        }

        /// <summary>
        ///     Adds query string value to an existing url, both absolute and relative URI's are supported.
        /// </summary>
        /// <example>
        /// <code>
        ///     // returns "www.domain.com/test?param1=val1&amp;param2=val2&amp;param3=val3"
        ///     new Uri("www.domain.com/test?param1=val1").ExtendQuery(new { param2 = "val2", param3 = "val3" }); 
        /// 
        ///     // returns "/test?param1=val1&amp;param2=val2&amp;param3=val3"
        ///     new Uri("/test?param1=val1").ExtendQuery(new { param2 = "val2", param3 = "val3" }); 
        /// </code>
        /// </example>
        /// <param name="uri"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Uri ExtendQuery(this Uri uri, object values)
        {
            return ExtendQuery(uri, values.GetType().GetProperties().ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => { var value = propInfo.GetValue(values, null); return value != null ? value.ToString() : null; }
            ));
        }

        static readonly Regex _optionalQueryStringRegex = new Regex("\\{\\?([^}]+)\\}");

        public static Uri ExpandUriTemplate(this string template, object values)
        {
            var optionalQueryStringMatch = _optionalQueryStringRegex.Match(template);
            if (optionalQueryStringMatch.Success)
            {
                var expansion = string.Empty;
                var parameters = optionalQueryStringMatch.Groups[1].Value.Split(',');

                foreach (var parameter in parameters)
                {
                    var parameterProperty = values.GetType().GetProperty(parameter);
                    if (parameterProperty != null)
                    {
                        expansion += string.IsNullOrWhiteSpace(expansion) ? "?" : "&";
                        expansion += parameter + "=" +
                            Uri.EscapeDataString("" + parameterProperty.GetValue(values, new object[0]));
                    }
                }
                template = _optionalQueryStringRegex.Replace(template, expansion);
            }
            return new Uri(template);
        }
    }
}

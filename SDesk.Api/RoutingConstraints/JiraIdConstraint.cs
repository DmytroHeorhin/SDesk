using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Routing;

namespace SDesk.Api.RoutingConstraints
{
    public class JiraIdConstraint : IHttpRouteConstraint
    {
        public bool Match
            (
            HttpRequestMessage request, 
            IHttpRoute route, 
            string parameterName, 
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection
            )
        {
            object value;
            values.TryGetValue(parameterName, out value);           
            var input = Convert.ToString(value);
            var jiraIdStringRegex = new Regex("^" + CommonInfoContainer.JiraIdPrefix + "[1-9][0-9]*$", RegexOptions.IgnoreCase);
            return jiraIdStringRegex.IsMatch(input);
        }
    }
}
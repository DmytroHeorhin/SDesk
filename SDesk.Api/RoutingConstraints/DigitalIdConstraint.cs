using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Routing;

namespace SDesk.Api.RoutingConstraints
{
    public class DigitalIdConstraint : IHttpRouteConstraint
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
            if (string.IsNullOrEmpty(input))
                return true;
            var regex = new Regex("^[0-9]+$");
            return regex.IsMatch(input);
        }   
    }
}
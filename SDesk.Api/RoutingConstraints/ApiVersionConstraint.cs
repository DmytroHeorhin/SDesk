using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace SDesk.Api.RoutingConstraints
{
    internal class ApiVersionConstraint : IHttpRouteConstraint
    {
        public const string VersionHeaderName = "api-version";
        private const int DefaultVersion = 1;

        public ApiVersionConstraint(int allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion { get; }
                
        public bool Match
            (
            HttpRequestMessage request, 
            IHttpRoute route,
            string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection
            )
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                int version = GetVersionHeader(request) ?? DefaultVersion;
                if (version == AllowedVersion)
                {
                    return true;
                }
            }
            return false;
        }

        private int? GetVersionHeader(HttpRequestMessage request)
        {
            string versionAsString;
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues(VersionHeaderName, out headerValues) && headerValues.Count() == 1)
            {
                versionAsString = headerValues.First();
            }
            else
            {
                return null;
            }
            int version;
            if (versionAsString != null && int.TryParse(versionAsString, out version))
            {
                return version;
            }
            return null;
        }
    }
}
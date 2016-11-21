﻿using SDesk.Api.RoutingConstraints;
using System.Collections.Generic;
using System.Web.Http.Routing;

namespace SDesk.Api.RouteAttributes
{
    internal class VersionedRoute : RouteFactoryAttribute
    {
        public VersionedRoute(string template, int allowedVersion) : base(template)         
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion { get; }
                  
        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary
                {
                    {"api-version", new ApiVersionConstraint(AllowedVersion)}
                };
                return constraints;
            }
        }
    }

}
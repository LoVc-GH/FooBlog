﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.OData.Routing.Attributes
{
    /// <summary>
    /// Represents an attribute that can be placed on an action of an Controller to specify
    /// the OData URLs that the action handles.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ODataRouteAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ODataRouteAttribute"/> class.
        /// </summary>
        public ODataRouteAttribute()
            : this(pathTemplate: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataRouteAttribute"/> class.
        /// </summary>
        /// <param name="pathTemplate">The OData URL path template that this action handles.</param>
        public ODataRouteAttribute(string pathTemplate)
        {
            PathTemplate = pathTemplate ?? String.Empty;
        }

        /// <summary>
        /// Gets the OData URL path template that this action handles.
        /// </summary>
        public string PathTemplate { get; }

        /// <summary>
        /// Gets or sets the OData route with which to associate the attribute.
        /// </summary>
        public string ModelName { get; set; }
    }
}

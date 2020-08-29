﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.OData.Routing.Attributes
{
    /// <summary>
    /// The attribute to specify a controller to an Edm model using the model name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ODataModelAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ODataModelAttribute"/> class.
        /// </summary>
        public ODataModelAttribute()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataModelAttribute"/> class.
        /// </summary>
        /// <param name="model">The specified model name.</param>
        public ODataModelAttribute(string model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        /// <summary>
        /// Gets the specified model name.
        /// </summary>
        public string Model { get; }
    }
}

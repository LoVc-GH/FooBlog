﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Microsoft.AspNetCore.OData.Routing
{
    /// <summary>
    /// Use for debug for OData convention action discovery on <see cref="ApplicationModel" />.
    /// </summary>
    internal class ODataRoutingApplicationModelDebugProvider : IApplicationModelProvider
    {
        /// <inheritdoc />
        public int Order => -100;

        /// <inheritdoc />
        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
            foreach (var controller in context.Result.Controllers)
            {
                foreach (var action in controller.Actions)
                {
                    string parameters = string.Join(",", action.Parameters.Select(p => p.Name));

                    foreach (var selector in action.Selectors)
                    {
                        ODataRoutingMetadata metadata = selector.EndpointMetadata.OfType<ODataRoutingMetadata>().FirstOrDefault();
                        if (metadata == null)
                        {
                            continue;
                        }

                        /*{metadata.Template.Template}*/
                        Console.WriteLine($"{action.ActionMethod.Name}({parameters}) in {controller.ControllerName}Controller: '{selector.AttributeRouteModel.Template}' ");
                    }
                }
            }
        }

        /// <inheritdoc />
        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
        }
    }
}

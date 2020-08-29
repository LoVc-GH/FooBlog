﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNetCore.OData.Routing.Template
{
    /// <summary>
    /// Represents a template that could match an <see cref="IEdmAction"/>.
    /// </summary>
    public class ActionSegmentTemplate : ODataSegmentTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionSegmentTemplate" /> class.
        /// </summary>
        /// <param name="action">The Edm action.</param>
        public ActionSegmentTemplate(IEdmAction action)
            : this(action, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionSegmentTemplate" /> class.
        /// </summary>
        /// <param name="action">The Edm action.</param>
        /// <param name="navigationSource">Unqualified function/action call boolean value.</param>
        public ActionSegmentTemplate(IEdmAction action, IEdmNavigationSource navigationSource)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            NavigationSource = navigationSource;

            if (action.ReturnType != null)
            {
                IsSingle = action.ReturnType.TypeKind() != EdmTypeKind.Collection;
                EdmType = action.ReturnType.Definition;
            }
        }

        /// <inheritdoc />
        public override string Literal => Action.FullName();

        /// <inheritdoc />
        public override IEdmType EdmType { get; }

        /// <summary>
        /// Gets the wrapped Edm action.
        /// </summary>
        public IEdmAction Action { get; }

        /// <inheritdoc />
        public override IEdmNavigationSource NavigationSource { get; }

        /// <inheritdoc />
        public override ODataSegmentKind Kind => ODataSegmentKind.Action;

        /// <inheritdoc />
        public override bool IsSingle { get; }

        /// <inheritdoc />
        public override ODataPathSegment Translate(ODataTemplateTranslateContext context)
        {
            return new OperationSegment(Action, NavigationSource as IEdmEntitySetBase);
        }
    }
}

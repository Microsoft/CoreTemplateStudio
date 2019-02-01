﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CoreTemplateStudio.Api.Enumerables;

namespace CoreTemplateStudio.Api.Models
{
    public class ProjectTypeItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; } = @"C:\Some\Dummy\Path";

        public ProjectTypeItem(ShortProjectType type)
        {
            this.Name = EnumerablesHelper.GetDisplayName(type);
            this.Description = EnumerablesHelper.GetDescription(type);
        }
    }
}

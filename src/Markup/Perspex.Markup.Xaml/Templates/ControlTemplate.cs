// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Controls;
using Perspex.Controls.Templates;
using Perspex.Metadata;
using Perspex.Styling;

namespace Perspex.Markup.Xaml.Templates
{
    public class ControlTemplate : IControlTemplate
    {
        [Content]
        public TemplateContent Content { get; set; }

        public IControl Build(ITemplatedControl control)
        {
            return Content.Load();
        }
    }
}
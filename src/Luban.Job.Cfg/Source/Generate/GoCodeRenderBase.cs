﻿using Luban.Job.Cfg.Defs;
using Luban.Job.Common.Defs;
using Luban.Job.Common.Utils;
using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luban.Job.Cfg.Generate
{
    abstract class GoCodeRenderBase : CodeRenderBase
    {
        [ThreadStatic]
        private static Template t_constRender;

        public override string Render(DefConst c)
        {
            string package = "cfg";
            var template = t_constRender ??= Template.Parse(StringTemplateUtil.GetTemplateString("common/go/const"));
            var result = template.RenderCode(c, new Dictionary<string, object>() { ["package"] = package });
            return result;
        }

        [ThreadStatic]
        private static Template t_enumRender;

        public override string Render(DefEnum e)
        {
            string package = "cfg";
            var template = t_enumRender ??= Template.Parse(StringTemplateUtil.GetTemplateString("common/go/enum"));
            var result = template.RenderCode(e, new Dictionary<string, object>() { ["package"] = package });
            return result;
        }

    }
}

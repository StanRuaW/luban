﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luban.Job.Cfg.Generate
{
    interface IRender
    {
        void Render(GenContext ctx);
    }
}

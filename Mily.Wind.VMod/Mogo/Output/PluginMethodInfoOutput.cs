﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.VMod.Mogo.Output
{
    public class PluginMethodInfoOutput: IVMCastle
    {
        public List<PluginMethodMapperInfo> Detail { get; set; }
        public string DSCode { get; set; }
    }
}

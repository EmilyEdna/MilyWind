using Microsoft.AspNetCore.Http;
using Mily.Wind.Extens.InternalInterface;
using Mily.Wind.Extens.SystemConfig;
using Mily.Wind.VMod.Mogo.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mily.Wind.Logic.Plugin
{
    public interface IPluginLogic: ILogic
    {
        MilyMapperResult UploadPlugin(List<IFormFile> files);
        MilyMapperResult GetPluginPage(PluginInput input);
        MilyMapperResult AlterPlugin(PluginAlterInput input);
        MilyMapperResult GetPluginClassList(string input);
        MilyMapperResult GetPluginMethodList(string input);
        MilyMapperResult GetPluginExcuteList();
    }
}

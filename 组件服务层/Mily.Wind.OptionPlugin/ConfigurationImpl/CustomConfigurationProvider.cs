using Microsoft.Extensions.Configuration;
using Mily.Wind.OptionPlugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mily.Wind.OptionPlugin.ConfigurationImpl
{
    public class CustomConfigurationProvider : ConfigurationProvider
    {
        private ConfigurationOption option;
        public override void Load()
        {
            option = ConfigurationIoc.GetService<ConfigurationOption>(nameof(ConfigurationOption));

            Data = ConfigurationJsonUtity.GetConfig();

            SaveDisk();

            Reload();
        }

        private void Reload()
        {
            Timer timer = new Timer(_ =>
            {
                if (Data.Count == 0)
                    return;

                ConfigurationIoc.Set("OptionVersion", Data["OptionVersion"]);
                var OldVersion = ConfigurationIoc.GetService<string>("OptionVersion");

                var NewVersion = ConfigurationJsonUtity.GetVersion();
                if (NewVersion == "-1")
                    return;

                if (!OldVersion.Equals(NewVersion))
                {
                    Data = ConfigurationJsonUtity.GetConfig();
                    SaveDisk();
                    OnReload();
                }
            }, null, 60 * 1000, option.Interval * 1000);
        }

        private void SaveDisk()
        {
            if (Data.Count > 0)
                ConfigurationJsonUtity.WriteFile(Data);
        }

    }
}

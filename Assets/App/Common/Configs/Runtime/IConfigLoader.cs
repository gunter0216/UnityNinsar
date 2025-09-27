using App.Common.Utilities.Utility.Runtime;

namespace App.Common.Configs.Runtime
{
    public interface IConfigLoader
    {
        Optional<string> LoadConfig(string key);
    }
}
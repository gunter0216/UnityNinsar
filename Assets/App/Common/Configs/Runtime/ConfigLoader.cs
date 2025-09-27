using App.Common.AssetSystem.Runtime;
using App.Common.Utilities.Utility.Runtime;

namespace App.Common.Configs.Runtime
{
    public class ConfigLoader : IConfigLoader
    {
        private readonly IAssetManager m_AssetManager;

        private readonly AddressablesConfigLoader m_AddressablesConfigLoader;

        public ConfigLoader(IAssetManager assetManager)
        {
            m_AssetManager = assetManager;
            
            m_AddressablesConfigLoader = new AddressablesConfigLoader(m_AssetManager);
        }
        
        public Optional<string> LoadConfig(string key)
        {
            var config = m_AddressablesConfigLoader.Load(key);
            return config;
        }
    }
}
using App.Common.Configs.Runtime;
using App.Common.Utilities.Utility.Runtime;

namespace App.Core.Cubes.External.Config
{
    public class CubesConfigLoader
    {
        private const string m_AssetKey = "File1";
        
        private readonly IConfigLoader m_ConfigLoader;

        public CubesConfigLoader(IConfigLoader configLoader)
        {
            m_ConfigLoader = configLoader;
        }

        public Optional<string> Load()
        {
            return m_ConfigLoader.LoadConfig(m_AssetKey);
        }
    }
}
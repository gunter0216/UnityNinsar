using App.Common.Configs.Runtime;
using UnityEngine;

namespace App.Core.Cubes.External.Config
{
    public class CubesConfigController
    {
        private readonly IConfigLoader m_ConfigLoader;


        public CubesConfigController(IConfigLoader configLoader)
        {
            m_ConfigLoader = configLoader;
        }   

        public bool Initialize()
        {
            var loader = new CubesConfigLoader(m_ConfigLoader);
            var config = loader.Load();
            if (!config.HasValue)
            {
                Debug.LogError("[CubesConfigController] In method Initialize, error load Config.");
                return false;
            }

            var text = config.Value;

            return true;
        }
    }
}
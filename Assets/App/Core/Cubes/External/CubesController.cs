using App.Common.AssetSystem.Runtime;
using App.Common.Configs.Runtime;
using App.Common.Utilities.Utility.Runtime;
using App.Core.Cubes.External.Config;
using App.Core.Cubes.External.Presenter;
using App.Core.Cubes.External.Presenter.Fabric;

namespace App.Core.Cubes.External
{
    public class CubesController : IInitSystem
    {
        private readonly IConfigLoader m_ConfigLoader;
        private readonly IAssetManager m_AssetManager;

        private CubesConfigController m_ConfigController;
        private CubesPresenter m_Presenter;
        
        public CubesController(IConfigLoader configLoader)
        {
            m_ConfigLoader = configLoader;
        }

        public void Init()
        {
            m_ConfigController = new CubesConfigController(m_ConfigLoader);
            m_ConfigController.Initialize();

            m_Presenter = new CubesPresenter(new CubeViewCreator(m_AssetManager));
        }
    }
}
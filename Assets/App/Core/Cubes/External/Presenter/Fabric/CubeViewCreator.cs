using App.Common.AssetSystem.Runtime;
using App.Common.Utilities.Utility.Runtime;
using App.Core.Cubes.External.View;

namespace App.Core.Cubes.External.Presenter.Fabric
{
    public class CubeViewCreator
    {
        private const string m_AssetKey = "CubeView";
        
        private readonly IAssetManager m_AssetManager;

        public CubeViewCreator(IAssetManager assetManager)
        {
            m_AssetManager = assetManager;
        }

        public Optional<CubeView> Create()
        {
            var view = m_AssetManager.InstantiateSync<CubeView>(new StringKeyEvaluator(m_AssetKey));
            return view;
        }
    }
}
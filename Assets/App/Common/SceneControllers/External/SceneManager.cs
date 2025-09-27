using App.Common.AssetSystem.External;
using App.Common.AssetSystem.Runtime.Context;
using App.Common.SceneControllers.Runtime;
using DG.Tweening;

namespace App.Common.SceneControllers.External
{
    public class SceneManager : ISceneManager
    {
        private readonly AssetManager m_AssetManager;

        public SceneManager(AssetManager assetManager)
        {
            m_AssetManager = assetManager;
        }

        public void LoadScene(string sceneName)
        {
            m_AssetManager.UnloadContext(typeof(SceneAssetContext));
            DOTween.KillAll();

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
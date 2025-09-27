using App.Common.AssetSystem.Runtime;
using App.Common.Utilities.Utility.Runtime;
using UnityEngine;

namespace App.Common.Configs.Runtime
{
    public class AddressablesConfigLoader
    {
        private readonly IAssetManager m_AssetManager;

        public AddressablesConfigLoader(IAssetManager assetManager)
        {
            m_AssetManager = assetManager;
        }

        public Optional<string> Load(string localKey)
        {
            var keyEvaluator = new StringKeyEvaluator(localKey);
            var config = m_AssetManager.LoadSync<TextAsset>(keyEvaluator);
            if (!config.HasValue)
            {
                Debug.LogError($"[AddressablesConfigLoader] In method Load, cant load config {localKey}.");
                return Optional<string>.Fail();
            }

            var text = config.Value.text;
            m_AssetManager.UnloadAsset(keyEvaluator);
            
            return Optional<string>.Success(text);
        }
    }
}
using App.Core.Cubes.Runtime.Config;
using UnityEngine;

namespace App.Core.Cubes.Runtime.Services
{
    public class StartPositionStrategy : IStartPositionStrategy
    {
        private readonly ICubesConfigController m_ConfigController;

        public StartPositionStrategy(ICubesConfigController configController)
        {
            m_ConfigController = configController;
        }

        public Vector2Int GetStartPosition()
        {
            var x = Random.Range(0, m_ConfigController.GetWidth());
            var y = Random.Range(0, m_ConfigController.GetHeight());
            
            return new Vector2Int(x, y);
        }
    }
}
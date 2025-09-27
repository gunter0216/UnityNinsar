using App.Common.Utilities.Utility.Runtime;
using App.Core.Cubes.External.Presenter.Fabric;
using App.Core.Cubes.External.View;
using App.Core.Cubes.Runtime.Config;
using UnityEngine;

namespace App.Core.Cubes.External.Presenter
{
    public class CubesPresenter
    {
        private const float m_Offset = 1.5f;

        private readonly ICubesConfigController m_ConfigController;
        private readonly CubeViewCreator m_CubeViewCreator;

        private int m_DisplayedSize;
        private CubeView[,] m_Cubes;

        public CubesPresenter(ICubesConfigController configController, CubeViewCreator cubeViewCreator)
        {
            m_CubeViewCreator = cubeViewCreator;
            m_ConfigController = configController;
        }
        
        public void Initialize()
        {
            m_DisplayedSize = m_ConfigController.GetDisplayedSize();
            CreateCubes();
        }

        public void UpdateCubes(Color[,] grid)
        {
            if (grid.Length != m_DisplayedSize * m_DisplayedSize)
            {
                Debug.LogError("[CubesPresenter] In method UpdateCubes, invalid grid size.");
                return;
            }
            
            for (int i = 0; i < m_DisplayedSize; ++i)
            {
                for (int j = 0; j < m_DisplayedSize; ++j)
                {
                    var color = grid[j, i];
                    var cube = m_Cubes[j, i];
                    cube.SetColor(color);
                }
            }
        }

        private void CreateCubes()
        {
            m_Cubes = new CubeView[m_DisplayedSize, m_DisplayedSize];
            int half = m_DisplayedSize / 2;
            float offsetX = half * -m_Offset;
            float offsetY = half * m_Offset;
            for (int i = 0; i < m_DisplayedSize; ++i)
            {
                for (int j = 0; j < m_DisplayedSize; ++j)
                {
                    CreateCube(i, j, offsetX, offsetY);
                }
            }
        }

        private void CreateCube(int i, int j, float offsetX, float offsetY)
        {
            var positionX = offsetX + j * m_Offset;
            var positionZ = offsetY - i * m_Offset;
            var position = new Vector3(positionX, 0.0f, positionZ);
            var cube = CreateCube(position);
            if (!cube.HasValue)
            {
                return;
            }
                    
            m_Cubes[i, j] = cube.Value;
        }

        private Optional<CubeView> CreateCube(Vector3 position)
        {
            var viewOpt = m_CubeViewCreator.Create();
            if (!viewOpt.HasValue)
            {
                Debug.LogError("[CubesPresenter] In method Initialize, error create CubeView.");
                return Optional<CubeView>.Fail();
            }

            var view = viewOpt.Value;
            view.SetPosition(position);
            
            return Optional<CubeView>.Success(view);
        }
    }
}
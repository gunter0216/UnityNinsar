using App.Common.Utilities.Utility.Runtime;
using App.Core.Cubes.External.Presenter.Fabric;
using App.Core.Cubes.External.View;
using UnityEngine;

namespace App.Core.Cubes.External.Presenter
{
    public class CubesPresenter
    {
        private const int m_Size = 3;
        private readonly CubeViewCreator m_CubeViewCreator;
        
        private CubeView[,] m_Cubes;

        public CubesPresenter(CubeViewCreator cubeViewCreator)
        {
            m_CubeViewCreator = cubeViewCreator;
        }
        
        public void Initialize()
        {
            const float offset = 1.5f;
            m_Cubes = new CubeView[m_Size, m_Size];
            float start = (m_Size / 2) * -offset;
            for (int i = 0; i < m_Size; ++i)
            {
                for (int j = 0; j < m_Size; ++j)
                {
                    var positionX = start + i * offset;
                    var positionZ = start + j * offset;
                    var position = new Vector3(positionX, 0.0f, positionZ);
                    var cube = CreateCube(position);
                    if (!cube.HasValue)
                    {
                        return;
                    }
                    
                    m_Cubes[i, j] = cube.Value;
                }
            }
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
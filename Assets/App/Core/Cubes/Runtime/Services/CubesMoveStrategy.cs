using System.Collections.Generic;
using App.Core.Cubes.Runtime.Config;
using UnityEngine;

namespace App.Core.Cubes.Runtime.Services
{
    public class CubesMoveStrategy
    {
        private const int m_Size = CubesConstants.Size;

        private readonly CubesConfigController m_ConfigController;

        private char[,] m_Grid;
        private Vector2Int m_Center;
        private IReadOnlyList<string> m_Matrix;

        public CubesMoveStrategy(CubesConfigController configController)
        {
            m_ConfigController = configController;
        }

        public void Initialize()
        {
            m_Grid = new char[m_Size, m_Size];
            m_Matrix = m_ConfigController.GetMatrix();

            m_Center = GetRandomCenter();
            
            UpdateGrid();
        }

        public void Move(Vector2Int direction)
        {
            direction.y = -direction.y;
            m_Center += direction;

            var height = GetHeight();
            var width = GetWidth();

            m_Center.x = (m_Center.x + width) % width;
            m_Center.y = (m_Center.y + height) % height;

            UpdateGrid();
        }

        public char[,] GetGrid()
        {
            return m_Grid;
        }

        private void UpdateGrid()
        {
            int half = m_Size / 2;
            int height = GetHeight();
            int width = GetWidth();
            for (int i = 0; i < m_Size; ++i)
            {
                for (int j = 0; j < m_Size; ++j)
                {
                    int x = m_Center.x + i - half;
                    int y = m_Center.y + j - half;
                    x = (x + width) % width;
                    y = (y + height) % height;
                    char c = m_Matrix[y][x];
                    m_Grid[j, i] = c;
                }
            }
        }

        private int GetWidth()
        {
            return m_ConfigController.GetWidth();
        }

        private int GetHeight()
        {
            return m_ConfigController.GetHeight();
        }

        private Vector2Int GetRandomCenter()
        {
            var x = Random.Range(0, GetWidth());
            var y = Random.Range(0, GetHeight());
            return new Vector2Int(x, y);
        }
    }
}
using App.Core.Cubes.Runtime.Config;
using UnityEngine;

namespace App.Core.Cubes.Runtime.Services
{
    public class CharToColorMatrixConverter
    {
        private readonly int m_DisplayedSize;
        private readonly Color[,] m_Grid;

        public CharToColorMatrixConverter(ICubesConfigController configController)
        {
            m_DisplayedSize = configController.GetDisplayedSize();
            m_Grid = new Color[m_DisplayedSize, m_DisplayedSize];
        }
        
        public Color[,] Convert(char[,] charMatrix)
        {
            var converter = new CharToColorConverter();
            for (int i = 0; i < m_DisplayedSize; ++i)
            {
                for (int j = 0; j < m_DisplayedSize; ++j)
                {
                    var c = charMatrix[i, j];
                    var colorOpt = converter.Convert(c);
                    if (colorOpt.HasValue)
                    {
                        m_Grid[i, j] = colorOpt.Value;
                    }
                    else
                    {
                        Debug.LogError("[CharToColorMatrixConverter] In method Convert, failed to convert char: " + c);
                        m_Grid[i, j] = Color.clear;
                    }
                }
            }

            return m_Grid;
        }
    }
}
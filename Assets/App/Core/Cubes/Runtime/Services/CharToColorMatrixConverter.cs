using UnityEngine;

namespace App.Core.Cubes.Runtime.Services
{
    public class CharToColorMatrixConverter
    {
        private const int m_Size = CubesConstants.Size;
        
        private readonly Color[,] m_Grid;
        
        public CharToColorMatrixConverter()
        {
            m_Grid = new Color[m_Size, m_Size];
        }
        
        public Color[,] Convert(char[,] charMatrix)
        {
            var converter = new CharToColorConverter();
            for (int i = 0; i < m_Size; ++i)
            {
                for (int j = 0; j < m_Size; ++j)
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
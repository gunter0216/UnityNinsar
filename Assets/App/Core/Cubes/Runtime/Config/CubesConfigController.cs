using System.Collections.Generic;
using App.Common.Configs.Runtime;
using UnityEngine;

namespace App.Core.Cubes.Runtime.Config
{
    public class CubesConfigController : ICubesConfigController
    {
        private readonly IConfigLoader m_ConfigLoader;

        private string[] m_Matrix;

        public CubesConfigController(IConfigLoader configLoader)
        {
            m_ConfigLoader = configLoader;
        }   

        public bool Initialize()
        {
            var loader = new CubesConfigLoader(m_ConfigLoader);
            var config = loader.Load();
            if (!config.HasValue)
            {
                Debug.LogError("[CubesConfigController] In method Initialize, error load Config.");
                return false;
            }

            var text = config.Value;
            var rows = text.Trim().Split('\n');
            
            m_Matrix = new string[rows.Length];
            for (int i = 0; i < rows.Length; ++i)
            {
                m_Matrix[i] = rows[i].Trim();
            }

            return true;
        }

        public IReadOnlyList<string> GetMatrix()
        {
            return m_Matrix;
        }
        
        public int GetWidth()
        {
            return m_Matrix[0].Length;
        }
        
        public int GetHeight()
        {
            return m_Matrix.Length;
        }

        public int GetDisplayedSize()
        {
            return CubesConstants.Size;
        }
    }
}
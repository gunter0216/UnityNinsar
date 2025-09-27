using UnityEngine;

namespace App.Core.Cubes.External.View
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private Renderer m_Renderer;
        
        public void SetColor(Color color)
        {
            m_Renderer.material.color = color;
        }
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
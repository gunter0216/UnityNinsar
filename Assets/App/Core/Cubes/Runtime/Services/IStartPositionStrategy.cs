using UnityEngine;

namespace App.Core.Cubes.Runtime.Services
{
    public interface IStartPositionStrategy
    {
        Vector2Int GetStartPosition();
    }
}
using System.Collections.Generic;

namespace App.Core.Cubes.Runtime.Config
{
    public interface ICubesConfigController
    {
        IReadOnlyList<string> GetMatrix();
        int GetWidth();
        int GetHeight();
        int GetDisplayedSize();
    }
}
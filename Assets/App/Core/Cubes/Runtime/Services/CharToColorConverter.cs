using App.Common.Utilities.Utility.Runtime;
using UnityEngine;

namespace App.Core.Cubes.Runtime.Services
{
    public class CharToColorConverter
    {
        public Optional<Color> Convert(char c)
        {
            var color = ConvertInternal(c);
            if (color == Color.clear)
            {
                Debug.LogError("[CharToColorConverter] In method Convert, unsupported char: " + c);
                return Optional<Color>.Fail();
            }
            
            return Optional<Color>.Success(color);
        }

        private Color ConvertInternal(char c)
        {
            return c switch
            {
                '1' => Color.red,
                '2' => Color.yellow,
                '3' => Color.blue,
                '4' => new Color(0.5f, 0f, 0.5f),
                _ => Color.clear
            };
        }
    }
}
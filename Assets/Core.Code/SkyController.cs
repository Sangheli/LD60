using UnityEngine;

namespace Core.Code
{
    public class SkyController
    {
        public Color baseSkyColor;
        public Camera camera;

        public void Init(Camera camera)
        {
            this.camera = camera;
            baseSkyColor = camera.backgroundColor;
        }

        public void SetColor(Color color, bool reset) => camera.backgroundColor = reset ? baseSkyColor : color;
    }
}
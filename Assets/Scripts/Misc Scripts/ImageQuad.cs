using UnityEngine;

namespace TemporaryGameCompany
{
    public class ImageQuad : MonoBehaviour
    {
        [SerializeField] private Material _textureMaterial;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private FloatReference _pixelScale;

        [Tooltip("The desired y value for the bottom of the quad. Use 'Reposition' button.")]
        [SerializeField] private float _bottomY;

        public void UpdateMaterial()
        {
            // Update texture being used with renderer's texture. Make sure it isn't null.
            if ((_textureMaterial = _renderer.sharedMaterial) != null)
                // Resize quad.
                transform.localScale = new Vector3(_textureMaterial.mainTexture.width, _textureMaterial.mainTexture.height, _pixelScale) / _pixelScale;
        }

        public void Reposition()
        {
            // Position above _bottomY.
            transform.position = new Vector3(transform.position.x, _bottomY + _textureMaterial.mainTexture.height/_pixelScale/2, transform.position.z);
        }
    }
}

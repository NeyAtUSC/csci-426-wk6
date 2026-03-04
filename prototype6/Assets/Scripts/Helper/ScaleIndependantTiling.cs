using UnityEngine;

[ExecuteAlways]
public class ScaleIndependentTiling : MonoBehaviour
{
    public Renderer targetRenderer;
    public float tilesPerUnit = 1f;

    private void Update()
    {
        if (targetRenderer == null) return;

        Vector3 scale = transform.localScale;
        Vector2 tiling = new Vector2(
            scale.x * tilesPerUnit,
            scale.z * tilesPerUnit
        );

        targetRenderer.sharedMaterial.mainTextureScale = tiling;
    }
}
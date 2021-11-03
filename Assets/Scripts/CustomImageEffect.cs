using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// must be attached to objects which have Camera component
[RequireComponent(typeof(Camera)), ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour
{
    [SerializeField]
    private Shader m_shader;
    private Material m_material;

    private Camera m_camera;
    // Start is called before the first frame update
    void Start()
    {
        m_camera = GetComponent<Camera>();
        m_camera.depthTextureMode = DepthTextureMode.Depth;
        m_material = new Material(m_shader);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, m_material);
    }
}

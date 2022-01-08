using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RetroCamHandler : MonoBehaviour
{
    public GameObject pixelRenderer;
    public RenderTexture pixelTexture;
    public GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        if (Gameplay.retroCameraEnabled)
        {
            pixelRenderer.SetActive(true);
            Camera camera = cameraObject.GetComponent<Camera>();
            camera.targetTexture = pixelTexture;
            PostProcessLayer postProcessLayer = cameraObject.GetComponent<PostProcessLayer>();
            postProcessLayer.enabled = false;
        }
    }
}

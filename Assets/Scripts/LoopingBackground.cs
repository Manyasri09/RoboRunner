using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    // A public variable to control the speed of the background scroll
    public float backgroundSpeed;

    // A reference to the Renderer component of the background object
    public Renderer backgroundRenderer;

    // Update is called once per frame
    void Update()
    {
        // Continuously shift the main texture offset of the material
        // This creates the visual effect of the background scrolling horizontally.
        backgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles continuous input while mobile button is held down
/// Attach this to each mobile control button
/// </summary>
public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button Settings")]
    [Tooltip("1 for UP, -1 for DOWN")]
    public float inputValue = 1f;

    [Header("References")]
    public Player playerScript;

    private bool isPressed = false;

    // ──────────────────────────────
    void Update()
    {
        // Continuously send input while button is held
        if (isPressed && playerScript != null)
        {
            playerScript.SetVerticalInput(inputValue);
        }
    }

    // ──────────────────────────────
    // Called when button is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        Debug.Log($"<color=cyan>[MobileButton]</color> Button pressed! Input: {inputValue}");
    }

    // ──────────────────────────────
    // Called when button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        
        // Stop movement when button released
        if (playerScript != null)
        {
            playerScript.SetVerticalInput(0f);
        }
        
        Debug.Log($"<color=cyan>[MobileButton]</color> Button released! Input reset to 0");
    }
}
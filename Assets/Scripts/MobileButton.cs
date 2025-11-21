using UnityEngine;
using UnityEngine.EventSystems;


public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button Settings")]
    [Tooltip("1 for UP, -1 for DOWN")]
    public float inputValue = 1f;

    [Header("References")]
    public Player playerScript;

    private bool isPressed = false;

   
    void Update()
    {
        
        if (isPressed && playerScript != null)
        {
            playerScript.SetVerticalInput(inputValue);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        Debug.Log($"<color=cyan>[MobileButton]</color> Button pressed! Input: {inputValue}");
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        
  
        if (playerScript != null)
        {
            playerScript.SetVerticalInput(0f);
        }
        
        Debug.Log($"<color=cyan>[MobileButton]</color> Button released! Input reset to 0");
    }
}
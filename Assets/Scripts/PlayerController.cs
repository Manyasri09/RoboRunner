using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float playerSpeed = 5f;

    [Header("Animation References")]
    public Animator flyingAnimator;
    public GameObject bodyObj;
    public GameObject deathAnimatorObj;

    [Header("Audio")]
    public AudioClip deathSound;

    [Header("Mobile Controls (Optional)")]
    public GameObject mobileControls;   // Parent with Up & Down buttons

    private bool isAlive = true;
    private float moveY; // continuously updated input

    // ──────────────────────────────
    void Awake()
    {
        // Show/hide controls depending on platform
        if (mobileControls != null)
        {
#if UNITY_EDITOR
            // In Unity Editor: Check if Android platform is selected in Build Settings
            bool showInEditor = UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android;
            mobileControls.SetActive(showInEditor);
            
            if (showInEditor)
                Debug.Log("<color=cyan>[MobileControls]</color> EDITOR - Android platform selected - Buttons VISIBLE for testing");
            else
                Debug.Log("<color=cyan>[MobileControls]</color> EDITOR - Non-Android platform selected - Buttons HIDDEN");
#else
            // In actual builds: Only show on Android
            bool isAndroid = Application.platform == RuntimePlatform.Android;
            mobileControls.SetActive(isAndroid);
            
            if (isAndroid)
                Debug.Log("<color=cyan>[MobileControls]</color> Android build - Buttons VISIBLE");
            else
                Debug.Log("<color=cyan>[MobileControls]</color> Desktop build - Buttons HIDDEN");
#endif
        }
    }

    // ──────────────────────────────
    void Start()
    {
        if (bodyObj != null) bodyObj.SetActive(true);
        if (deathAnimatorObj != null) deathAnimatorObj.SetActive(false);
        if (flyingAnimator != null) flyingAnimator.enabled = true;
    }

    // ──────────────────────────────
    void Update()
    {
        if (!isAlive) return;

#if UNITY_STANDALONE || UNITY_EDITOR
        // 🖥️ Keyboard input (but allow mobile buttons to override in Editor)
        float keyboardInput = Input.GetAxisRaw("Vertical");
        
        // If there's keyboard input, use it; otherwise keep mobile input
        if (Mathf.Abs(keyboardInput) > 0.01f)
        {
            moveY = keyboardInput;
        }
        // If no keyboard input and no mobile input, moveY stays as set by mobile buttons
#endif
        
        // Mobile input is handled by SetVerticalInput() method called from buttons
    }

    // ──────────────────────────────
    void FixedUpdate()
    {
        if (!isAlive) return;

        // ✅ Apply movement every physics frame
        Vector3 moveDirection = new Vector3(0, moveY, 0) * playerSpeed * Time.fixedDeltaTime;
        transform.position += moveDirection;
        
        // Optional: Debug log to see if movement is being applied
        if (Mathf.Abs(moveY) > 0.01f)
        {
            Debug.Log($"<color=yellow>[Player FixedUpdate]</color> Moving with moveY = {moveY}, newPos = {transform.position}");
        }
    }

    // ──────────────────────────────
    // 📱 MOBILE CONTROLS
    public void SetVerticalInput(float inputY)
    {
        if (!isAlive) return;
        moveY = inputY;
        Debug.Log($"<color=green>[Player]</color> SetVerticalInput called — moveY = {moveY}");
    }

    // ☠️ DEATH HANDLING
    public void Die()
    {
        if (!isAlive) return;
        isAlive = false;

        Debug.Log("<color=red>[Player]</color> Died! Triggering death animation...");
        GameManager.Instance?.StopAllSystems();
        AudioManager.Instance?.DimMusic(0.25f, 1.2f);

        if (bodyObj != null)
            bodyObj.SetActive(false);

        if (deathAnimatorObj != null)
        {
            deathAnimatorObj.SetActive(true);
            deathAnimatorObj.transform.position = bodyObj.transform.position;

            Animator deathAnim = deathAnimatorObj.GetComponent<Animator>();
            if (deathAnim != null)
            {
                if (AudioManager.Instance != null && deathSound != null)
                    AudioManager.Instance.PlaySFX(deathSound, 1f);

                deathAnim.Play("dieing", 0, 0f);
                float len = deathAnim.GetCurrentAnimatorStateInfo(0).length;
                StartCoroutine(ShowGameOverAfterDelay(len));
            }
            else
                StartCoroutine(ShowGameOverAfterDelay(1.5f));
        }
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ScoreManager.Instance?.GameOver();
    }
}
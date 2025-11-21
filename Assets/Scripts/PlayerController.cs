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
    public GameObject mobileControls;  

    private bool isAlive = true;
    private float moveY; 


    void Awake()
    {
       
        if (mobileControls != null)
        {
#if UNITY_EDITOR
           
            bool showInEditor = UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android;
            mobileControls.SetActive(showInEditor);
            
            if (showInEditor)
                Debug.Log("<color=cyan>[MobileControls]</color> EDITOR - Android platform selected - Buttons VISIBLE for testing");
            else
                Debug.Log("<color=cyan>[MobileControls]</color> EDITOR - Non-Android platform selected - Buttons HIDDEN");
#else
            
            bool isAndroid = Application.platform == RuntimePlatform.Android;
            mobileControls.SetActive(isAndroid);
            
            if (isAndroid)
                Debug.Log("<color=cyan>[MobileControls]</color> Android build - Buttons VISIBLE");
            else
                Debug.Log("<color=cyan>[MobileControls]</color> Desktop build - Buttons HIDDEN");
#endif
        }
    }

    void Start()
    {
        if (bodyObj != null) bodyObj.SetActive(true);
        if (deathAnimatorObj != null) deathAnimatorObj.SetActive(false);
        if (flyingAnimator != null) flyingAnimator.enabled = true;
    }


    void Update()
    {
        if (!isAlive) return;

#if UNITY_STANDALONE || UNITY_EDITOR
        
        float keyboardInput = Input.GetAxisRaw("Vertical");
        
       
        if (Mathf.Abs(keyboardInput) > 0.01f)
        {
            moveY = keyboardInput;
        }
        
#endif
        
      
    }


    void FixedUpdate()
    {
        if (!isAlive) return;

        
        Vector3 moveDirection = new Vector3(0, moveY, 0) * playerSpeed * Time.fixedDeltaTime;
        transform.position += moveDirection;
        

        if (Mathf.Abs(moveY) > 0.01f)
        {
            Debug.Log($"<color=yellow>[Player FixedUpdate]</color> Moving with moveY = {moveY}, newPos = {transform.position}");
        }
    }


    public void SetVerticalInput(float inputY)
    {
        if (!isAlive) return;
        moveY = inputY;
        Debug.Log($"<color=green>[Player]</color> SetVerticalInput called — moveY = {moveY}");
    }

   
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
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    public void DimMusic(float targetVolume = 0.25f, float duration = 1.5f)
    {
        if (musicSource == null) return;
        StartCoroutine(DimMusicCoroutine(targetVolume, duration));
    }

    private IEnumerator DimMusicCoroutine(float targetVolume, float duration)
    {
        float startVolume = musicSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        musicSource.volume = targetVolume; 
    }

  
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}

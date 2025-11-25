using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip clip, Transform spawntransform, float volume) 
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawntransform.position, Quaternion.identity);
        
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);
    }
    public void PlayRandomSoundFXClip(AudioClip[] clip, Transform spawntransform, float volume)
    {
        int rand = Random.Range(0, clip.Length);
        AudioSource audioSource = Instantiate(soundFXObject, spawntransform.position, Quaternion.identity);

        audioSource.clip = clip[rand];
        audioSource.volume = volume;
        audioSource.Play();
        float cliplength = audioSource.clip.length;

        Destroy(audioSource.gameObject, cliplength);
    }
}

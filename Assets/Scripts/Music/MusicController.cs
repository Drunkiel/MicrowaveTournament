using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] audioClips;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            DrawNextAudioClip();
        }
    }

    void DrawNextAudioClip()
    {
        int num = (int)Mathf.Round(Random.Range(0, audioClips.Length));

        audioSource.clip = audioClips[num];
        audioSource.Play();
    }
}

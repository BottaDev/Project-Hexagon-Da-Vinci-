using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioClip playerDeadSound;
    public AudioClip click_01;
    public AudioClip click_02;
    public AudioClip infiniteProgressiveSong;
    public AudioClip infiniteSong;

    public AudioSource audioSource;

    int timesReproduced = 0;

    void Start()
    {
        StartCoroutine(PlayMusicLevel());
    }

    public void Click_01()
    {
        audioSource.clip = click_01;
        audioSource.Play();
    }

    public void Click_02()
    {
        audioSource.clip = click_02;
        audioSource.Play();
    }

    IEnumerator PlayMusicLevel()
    {
        if (audioSource.clip != null)
        {
            yield return new WaitForSeconds(audioSource.clip.length);

            audioSource.clip = infiniteProgressiveSong;
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            audioSource.loop = true;
            audioSource.clip = infiniteSong;
            audioSource.Play();
        }
    }

    public IEnumerator PlayerDeadSound()
    {
        timesReproduced++;

        if (timesReproduced == 1)
        {
            audioSource.Stop();
            audioSource.clip = playerDeadSound;
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            gameObject.SetActive(false);
        }
    }
}

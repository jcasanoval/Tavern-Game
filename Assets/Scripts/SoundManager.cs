using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static List<AudioSource> audioSources;
    public static AudioSource backgroundMusicAudioSource;
    public static AudioClip discChangeSound;
    private static GameObject father;

    public static void BecomeFather(GameObject you){
        father = you;
    }

    public static IEnumerator BackgroundMusicPlay(AudioClip audioClip)
    {
        if (backgroundMusicAudioSource == null)
        {
            backgroundMusicAudioSource = father.AddComponent<AudioSource>();
            backgroundMusicAudioSource.loop = true;
            backgroundMusicAudioSource.volume = 0.5f;
        }
        backgroundMusicAudioSource.clip = discChangeSound;
        backgroundMusicAudioSource.Play();
        while(backgroundMusicAudioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }

        backgroundMusicAudioSource.clip = audioClip;
        backgroundMusicAudioSource.Play();
    }

    public static void setDiscChangeSound(AudioClip audioClip)
    {
        discChangeSound = audioClip;
    }




}

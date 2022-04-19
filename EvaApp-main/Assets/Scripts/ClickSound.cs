using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip audioClip0;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;

    public void OnClick()
    {
        sound.PlayOneShot(sound.clip);
    }

    public void OnClick0()
    {
        sound.PlayOneShot(audioClip0);
    }

    public void OnClick1()
    {
        sound.PlayOneShot(audioClip1);
    }

    public void OnClick2()
    {
        sound.PlayOneShot(audioClip2);
    }

    public void OnClick3()
    {
        sound.PlayOneShot(audioClip3);
    }
}
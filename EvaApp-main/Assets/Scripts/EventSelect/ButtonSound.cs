using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource SEplayer;//AudioSource型の変数aを宣言 使用するAudioSourceコンポーネントをアタッチ必要

    [SerializeField] private AudioClip SeikaiSE;//AudioClip型の変数b1を宣言 使用するAudioClipをアタッチ必要
    [SerializeField] private AudioClip FuseikaiSE;//AudioClip型の変数b2を宣言 使用するAudioClipをアタッチ必要 

    //自作の関数1
    public void SE1()
    {
        SEplayer.PlayOneShot(SeikaiSE);
    }

    //自作の関数2
    public void SE2()
    {
        SEplayer.PlayOneShot(FuseikaiSE);
    }
}
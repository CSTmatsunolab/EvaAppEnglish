using UnityEngine;
using System.Collections;
 
public class MusicFadeOut : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip0;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    private GameObject Pdata;
    private string intro;
    /*public bool IsFade;
    public double FadeOutSeconds = 1.0;
    bool IsFadeOut = false;
    double FadeDeltaTime = 0;
    int ChangeMusic = MenuPanel.SendChangeMusic();
    */
 
    void Start()
    {
        PdataLoad();
        intro = Pdata.GetComponent<Player_Data>().PlayerData[1][8];
        if(intro == "0"){
            audioSource.clip = audioClip2;
        }
        else if(intro== "1"){
            audioSource.clip = audioClip3;
        }
        else{
            int answer = ESManagement.SendAnswer();
            if(answer == 0 ){
            audioSource.clip = audioClip0;
            }
            else if(answer == 1){
            audioSource.clip = audioClip1;
            }
        }
        audioSource.Play();
    }

    private void PdataLoad(){
        Pdata = GameObject.Find("Player_Data");
    }


}
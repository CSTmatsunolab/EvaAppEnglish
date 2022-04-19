using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    GameObject Pdata;
    public GameObject RButton;

    void Start(){
        PdataLoad();
        if(Pdata.GetComponent<Player_Data>().PlayerData[1][8]=="3"){
            RButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/QuizButton");
        }
    }

    private void PdataLoad(){
        Pdata = GameObject.Find("Player_Data");
    }
    // 選択画面に遷移
    public void ReturnButtonDown()
    {
        if(Pdata.GetComponent<Player_Data>().PlayerData[1][8]=="3"){
            SceneManager.LoadScene("QuizScene");
        }else{
            SceneManager.LoadScene("SelectScene");
        } 
    }
}
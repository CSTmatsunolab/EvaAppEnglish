using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    public GameObject Pdata;
    int introcheck;

    // 選択画面に遷移
    public void SelectButtonDown()
    {
        introcheck = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][8]); //イントロやってない：０、やってる：１
        Debug.Log(introcheck);
        if(introcheck == 0){        //イントロシナリオにいく
            SceneManager.LoadScene("EventScene");
        }
        else {
            SceneManager.LoadScene("SelectScene");
        }
    }
}
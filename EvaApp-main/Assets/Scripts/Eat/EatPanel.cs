using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class EatPanel : MonoBehaviour
{
    [SerializeField] GameObject Main;
    [SerializeField] GameObject Result;
    [SerializeField] GameObject Sleep;
    [SerializeField] GameObject Alert;
    void Start()
    {
        GameObject Pdata = GameObject.Find("Player_Data");
        int i = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][0]);
        if(i<4) {GoToAlert(); GoToMain();} //シーンが始まった時満腹3以下だとAlertパネルを表示,Mainパネルを非表示
        else    {BackToMain();}
        BackToResult();
        BackToSleep();
    }
    public void GoToAlert(){
        Alert.SetActive(true);//Alertパネルを表示
    }
    public void BackToAlert(){
        Alert.SetActive(false);//Alertパネルを非表示
    }
    public void BackToMain()
    {
        GameObject Pdata = GameObject.Find("Player_Data");
        int i = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][0]);
        BackToAlert();
        Main.SetActive(true);//Mainパネルを表示
    }
    public void GoToMain()
    {
        Main.SetActive(false);//Mainパネルを非表示
    }
    public void GoToResult()
    //public void EatButtonDown()
    {
        Result.SetActive(true);//Resultパネルを表示
        Main.SetActive(false);//Mainパネルを非表示
        Sleep.SetActive(false);//Sleepパネルを非表示
    }
    public void BackToResult()
    {
        Result.SetActive(false);//Resultパネルを非表示
    }
    public void BackToSleep()
    {
        Sleep.SetActive(false);//Sleepパネルを非表示
    }
    public void GoToSleep()
    {
        BackToResult();
        GameObject Pdata = GameObject.Find("Player_Data");
        int i = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][6]);

        if (i == 1) //昼、夜の判定、昼だったら
            {
                GoToSelect();
            }
        else if (i == 0)
            {
                Sleep.SetActive(true);//Sleepパネルを表示
            }
    }
    public void GoToSelect()
    {
        SceneManager.LoadScene("SelectScene");//選択シーンに遷移
    }
}

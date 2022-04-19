using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class DrinkButton : MonoBehaviour
{
    public Button DButton;

    [SerializeField] GameObject G1;
    [SerializeField] GameObject G2;
    [SerializeField] GameObject G3;
    [SerializeField] Text stock_text;
    GameObject Pdata;
    private string path;
    int x = 0;//水分
    int y = 0;//水の所持数

    void Start(){        
        Pdata = GameObject.Find("Player_Data");
        //水分x
        x = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][2]);
        //水の所持数(ペットボトル)y
        y = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][3]);
        //水の所持数が0のとき or 水分が満タンの時ボタン無効化
        Put();
    }

    public void DrinkButtonDown()//ドリンクパネルの閉じるボタンを押したときの処理
    {
        if(y>0){
            x=x+3;
            y=y-1;
            if(x>3) x=3;
            
            //string型にして格納
            string str=x.ToString();
            Pdata.GetComponent<Player_Data>().PlayerData[1][2]=str;
            str=y.ToString();
            Pdata.GetComponent<Player_Data>().PlayerData[1][3]=str;
        }
        Pdata.GetComponent<Player_Data>().CsvSave();
        Put();
    }

    private void DrinkCheck(){//水を飲むボタンの無効化
        if(y==0 || x==3){
            DButton.interactable=false;
        }
    }

    private void DrinkGauge(){//ドリンクゲージの増減処理
        int x = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][2]);
        G1.gameObject.SetActive(true);
        G2.gameObject.SetActive(true);
        G3.gameObject.SetActive(true);
        if(x==2){
            G3.gameObject.SetActive(false);
        }else if(x==1){
            G3.gameObject.SetActive(false);
            G2.gameObject.SetActive(false);
        }else if(x==0){
            G3.gameObject.SetActive(false);
            G2.gameObject.SetActive(false);
            G1.gameObject.SetActive(false);
        }
    }

    private void WaterStock()//水の残量表示
    {
        stock_text.text = "×"+Pdata.GetComponent<Player_Data>().PlayerData[1][3];
    }

    private void Put(){//処理まとめ
        DrinkCheck();
        DrinkGauge();
        WaterStock();
    }
}
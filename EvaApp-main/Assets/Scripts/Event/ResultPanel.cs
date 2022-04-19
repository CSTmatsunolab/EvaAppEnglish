using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class ResultPanel : MonoBehaviour
{
    private GameObject Pdata = null;
    private GameObject Edata = null;
    int index = 0;
    int Answer = 0;
    int HG = 0; //満腹ゲージ
    int RE = 0; //安心ゲージ
    int HGC = 0; //イベントによる満腹ゲージの変動値
    int REC = 0; //イベントによる安心ゲージの変動値
    public Text Result1;
    public Text Result2;
    private string path;
    

    void Start()
    {
        ResultCalculation();
    }

    private void PdataLoad()
    {
        Pdata = GameObject.Find("Player_Data");
    }

    private void EdataLoad()
    {
        Edata = GameObject.Find("EventData");
    }

    private void ResultMake()
    {
        PdataLoad();
        EdataLoad();

        int introcheck = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][8]);
        if(introcheck == 0||introcheck == 1){        //イントロシナリオにいく
            index = 1;
        }
        else if(introcheck==54){
            index = 54;
        }
        else{
            index = ESManagement.Send();
            Answer = ESManagement.SendAnswer();
        }
        HG = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][0]);
        RE = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][1]);

        if(Answer == 0)
        {
            HGC = int.Parse(Edata.GetComponent<Event_Data>().EventData[index][3]);
            REC = int.Parse(Edata.GetComponent<Event_Data>().EventData[index][4]);
        }
        else if(Answer == 1)
        {
            HGC = int.Parse(Edata.GetComponent<Event_Data>().EventData[index][5]);
            REC = int.Parse(Edata.GetComponent<Event_Data>().EventData[index][6]);
        }
    }

    private void ResultCalculation()
    {
        ResultMake();
        HGC = HG + HGC;
        if(HGC < 0)
        {
            HGC = 0;
        }
        else if(HGC > 10)
        {
            HGC = 10;
        }
        REC = RE + REC;
        if(REC < 0)
        {
            REC = 0;
        }else if(REC > 10)
        {
            REC = 10;
        }

        Pdata.GetComponent<Player_Data>().PlayerData[1][0] = HGC.ToString();
        Pdata.GetComponent<Player_Data>().PlayerData[1][1] = REC.ToString();
        Pdata.GetComponent<Player_Data>().CsvSave();

        if(index == 54){
            Result1.color = new Color(0.0f, 0.0f, 1.0f, 1.0f); //青
            Result2.color = new Color(0.0f, 0.0f, 1.0f, 1.0f); //青
            Result1.text = "水が増えた！";
            Result2.text = "食料が増えた！";
        }
        else{
            if (HG > HGC)
            {
                Result1.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); //赤
            }
            if (HG < HGC)
            {
                Result1.color = new Color(0.0f, 0.0f, 1.0f, 1.0f); //青
            }
            if (RE > REC)
            {
                Result2.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); //赤
            }
            if (RE < REC)
            {
                Result2.color = new Color(0.0f, 0.0f, 1.0f, 1.0f); //青
            }
            if (HG == HGC)
            {
                Result1.color = new Color(0.0f, 0.0f, 0.0f, 1.0f); //黒
            }
            if (RE == REC)
            {
                Result2.color = new Color(0.0f, 0.0f, 0.0f, 1.0f); //黒
            }
            
            Result1.text = ("満腹ゲージ：" + HG.ToString() + "→" + HGC.ToString());
            Result2.text = ("安心ゲージ：" + RE.ToString() + "→" + REC.ToString());

        }
        
    
    }

    public void NextButtonDown()
    {
        int flag = MenuPanel.Send();
        if(flag == 1){
            SceneManager.LoadScene("SelectScene");//選択画面に遷移
        }
        /*if(flag == 2){
            SceneManager.LoadScene("SelectScene");//選択画面に遷移
        }
        */
        else if(flag == 3){
            Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "2";
            SceneManager.LoadScene("SelectScene");//選択画面に遷移
        }
        else{
            SceneManager.LoadScene("EatScene");//食事画面に遷移
        }
    }
}

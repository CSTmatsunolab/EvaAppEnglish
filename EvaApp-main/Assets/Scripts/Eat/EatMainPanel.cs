using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class EatMainPanel : MonoBehaviour
{
    int x = 0;//食べる食料の数
    int i = 0;//食料の貯蓄量
    int j = 0;//まんぷくゲージ
    int k = 0;//時間
    int l = 0;//安心ゲージ
    int m = 0;//水分ゲージ
    private GameObject Pdata = null;
    public GameObject KText = null;
    public GameObject RText = null;
    Text ktext;
    private string path;
    // Start is called before the first frame update
    void Start()
    {
        Pdata = GameObject.Find("Player_Data");
        ktext=KText.GetComponent<Text>();
        x = 0;
        i = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][4]);
        Debug.Log(i);
        j = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][0]);
        k = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][5]);
        l = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][1]);
        m = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][2]);
        ktext.text= x.ToString() + "個";
    }

    public void PlusButtondown()    {
        if((i>0)&&(x<3))        {
            --i;//貯蓄を減らす
            ++x;//食べる量を増やす
            ktext.text= x.ToString() + "個";
        }
    }

    public void MinusButtondown()   {
        if(x>0) //食べる量が0以上の時のみ
        {
            ++i;//貯蓄を増やす
            --x;//食べる量を減らす
            ktext.text=x.ToString() + "個";
        } 
    }

    public void EatButtondown()
    {
        j = j + (x*3); //まんぷくゲージに加算
        Debug.Log(x);
        Debug.Log(i);
        if(j > 10){
            j = 10;
        }
        Pdata.GetComponent<Player_Data>().PlayerData[1][0]=j.ToString();//配列に格納
        Pdata.GetComponent<Player_Data>().PlayerData[1][4]=i.ToString();//配列に格納

        TimeCheck();
        ReduceWater();
        Result();
        Pdata.GetComponent<Player_Data>().CsvSave();
    }

    public void NotEatButtondown()
    {
        x=0;
        TimeCheck();
        ReduceWater();
        Result();
        Pdata.GetComponent<Player_Data>().CsvSave();
    }

    void Result()//食事結果の表示
    {
        Text rtext = RText.GetComponent<Text>();
        //rtext.text = "まんぷくゲージが"+x.ToString()+"回復した！";
        if(x==0)//食べた量が0の時に分岐
        {
            rtext.text = "何も食べなかった...";
        }
        else
        {
            rtext.text = ("満腹：" + (j - x).ToString() + "→" + j + Environment.NewLine +
                      "食料の数：" + (i + x).ToString() + "→" + i);
        }
    }

    void TimeCheck()//昼と夜の変更
    {
        if(Pdata.GetComponent<Player_Data>().PlayerData[1][6]=="0") //昼の場合
        {
            Pdata.GetComponent<Player_Data>().PlayerData[1][6]="1";
        }
        else
        {
            Pdata.GetComponent<Player_Data>().PlayerData[1][6]="0"; //夜の場合
            k=k+1;
            Pdata.GetComponent<Player_Data>().PlayerData[1][5]=k.ToString(); //日数の変更

            int manpuku = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][0]);
            int anshin = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][1]);
            int suibun = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][2]);
            int GameOverFlag = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][11]);
            int HaikyuCount = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][12]);
            if(HaikyuCount == 1)
            {
                HaikyuCount = 0;
                Pdata.GetComponent<Player_Data>().PlayerData[1][12]=HaikyuCount.ToString();
            }
            if(manpuku == 0||suibun == 0||anshin == 0)//満腹が0の時に起きる
            {
                GameOverFlag++;
                Pdata.GetComponent<Player_Data>().PlayerData[1][11]=GameOverFlag.ToString();
            }
        }
    }

    private void ReduceWater(){
        if(m>0){
            m=m-1;
            Debug.Log("水分-1操作");
            Pdata.GetComponent<Player_Data>().PlayerData[1][2]=m.ToString();//配列に格納
        }
    }

    public void GoToHungry()
    {
        //一日の終わり(=sleepの閉じるボタンを押した時)に満腹や安心の操作を行う
        //２日目以降の昼(一日の最初)に満腹-1       
        if(j>0){
            j = j -1 ;
            Debug.Log("満腹-1操作");
            Pdata.GetComponent<Player_Data>().PlayerData[1][0]=j.ToString();//配列に格納
        }
        else{    //0のまま寝て起きたら安心-1
            if(l>0){
                l = l -1;
                Debug.Log("安心-1操作");
                Pdata.GetComponent<Player_Data>().PlayerData[1][1]=l.ToString();//配列に格納
            }
        }
        GoToSelect();//選択シーンに遷移
    }

    public void GoToSelect()
    {
        SceneManager.LoadScene("SelectScene");//選択シーンに遷移
    }
    

}

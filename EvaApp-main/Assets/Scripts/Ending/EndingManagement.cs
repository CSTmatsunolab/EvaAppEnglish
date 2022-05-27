using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class EndingManagement : MonoBehaviour
{
    GameObject Pdata;
    GameObject Rdata;
    public Text ResultText;//結果のTextUI
    public Text ScoreText;//得点のTextUI
    public Text PName;//プレイヤーの名前のTextUI
    public Image ScoreImage;//ランクのImageUI
    //ランクのスプライト
    public Sprite S;
    public Sprite A;
    public Sprite B;
    public Sprite C;

    public GameObject Cloud;//Cloudパネル
    public GameObject Score;//Scoreパネル
    public GameObject Result;//Resultオブジェクト
    public GameObject Endroll;//エンドロールパネル
    public GameObject NextButton;//次へボタン
    public GameObject man;//走っている男のオブジェクト
    public GameObject dragon;//神龍のオブジェクト
    public GameObject kyukyu;//救急車のオブジェクト
    public Animator ScorePoints;//ScorePointsオブジェクトのアニメーターコントローラー
    int ScorePoint;//得点
    int EResultLine = 55;//EventResultの行数(1行目(項目名)を含む)
    
    // Start is called before the first frame update
    void Start()
    {
        TextSet();
    }

    private void PdataLoad(){
        Pdata = GameObject.Find("Player_Data");
    }

    private void RdataLoad(){
        Rdata = GameObject.Find("ResultData");
    }

    private void TextSet(){//CloudTextの文章変更
        Score.SetActive(false);
        Endroll.SetActive(false);
        PdataLoad();
        RdataLoad();
        
        int OpenedEvent = 0;
        for (int i = 0 ;i < EResultLine-1; i++){ //EventResult.csvの行数分、開放フラグを足すのを繰り返す 0行目は飛ばす       
            OpenedEvent += int.Parse(Rdata.GetComponent<Result_Data>().ResultData[i+1][1]);
        }

        ResultText.text = "Shelter name：" + Pdata.GetComponent<Player_Data>().PlayerData[1][9]+"\n"
                        + "Name：" + Pdata.GetComponent<Player_Data>().PlayerData[1][10]+"\n"
                        + "Days lapsed：" + Pdata.GetComponent<Player_Data>().PlayerData[1][5]+"days"+"\n"
                        + "Number of events opened：" + OpenedEvent+"\n"
                        + "Number of correct answers to quiz：" + (QuizManagement.Send()).ToString()+"questions";
        ScoreCalculation();
        ScoreOutput(ScorePoint);
    }

    private void ScoreCalculation(){//得点計算
        int Correct = QuizManagement.Send();
        int Manpuku = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][0]);
        int Stress = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][1]);
        int Water = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][2]);
        int OpenedEvent = 0;
        for (int i = 0 ;i < EResultLine-1; i++){ //EventResult.csvの行数分、開放フラグを足すのを繰り返す 0行目は飛ばす       
            OpenedEvent += int.Parse(Rdata.GetComponent<Result_Data>().ResultData[i+1][1]);
        }
        Correct = Correct * 1000;
        Manpuku = Cal(Manpuku);
        Stress = Cal(Stress);
        Water = Cal(Water);
        OpenedEvent = (OpenedEvent * 100) -300;    //イベントの開放数ぶんスコア100, オープニング+エンディング(スコア200+100)を除く
        ScorePoint = Correct + Manpuku + Stress + Water + OpenedEvent;
        ScoreText.text = "Evaluation point"+ScorePoint.ToString();
    } 

    public void CloudPanelClose(){//Cloudパネルの次へボタンを押したときの処理
        Cloud.SetActive(false);
        NextButton.SetActive(false);
        Score.SetActive(true);
        dragon.SetActive(false);
        kyukyu.SetActive(false);
        int a = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][8]);
        if(a==50){
            dragon.SetActive(true);
        }else if(a==49){
            man.SetActive(false);
            kyukyu.SetActive(true);
        }
        ScorePoints.SetTrigger("PanelOn");
        NextButton.SetActive(true);
    }

    private int Cal(int a){//ゲージの残量を100倍する
        a = a * 100;
        return a;
    }

    private void ScoreOutput(int a){//ScoreImageのスプライト変更
        if(a>=10500){
            ScoreImage.sprite = S;
        }else if(a>=8500){
            ScoreImage.sprite = A;
        }else if(a>=6500){
            ScoreImage.sprite = B;
        }else{
            ScoreImage.sprite = C;
        }
    }

    public void next(){//Scoreパネルの次へボタンを押したときの処理
        PName.text = Pdata.GetComponent<Player_Data>().PlayerData[1][10];

        Endroll.SetActive(true);
        Result.SetActive(false);
        Reset();
    }
    
    public void GoToTitle()
    {
        SceneManager.LoadScene("StartScene");//スタートシーンに遷移
    }
    private void EndCheck(){//エンディングシナリオのフラグ変更
        int a = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][8]);  
        Rdata.GetComponent<Result_Data>().ResultData[a][1]="1";

    }

    private void Reset(){//csvを初期状態に戻す
        RdataLoad();
        EndCheck();
        Pdata.GetComponent<Player_Data>().PlayerData[1][0] = "5";
        Pdata.GetComponent<Player_Data>().PlayerData[1][1] = "5";
        Pdata.GetComponent<Player_Data>().PlayerData[1][2] = "3";
        Pdata.GetComponent<Player_Data>().PlayerData[1][3] = "3";
        Pdata.GetComponent<Player_Data>().PlayerData[1][4] = "3";
        Pdata.GetComponent<Player_Data>().PlayerData[1][5] = "1";
        Pdata.GetComponent<Player_Data>().PlayerData[1][6] = "0";
        Pdata.GetComponent<Player_Data>().PlayerData[1][7] = "0";
        Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "0";
        Pdata.GetComponent<Player_Data>().PlayerData[1][9] = "避難所";
        Pdata.GetComponent<Player_Data>().PlayerData[1][10] = "You";
        Pdata.GetComponent<Player_Data>().PlayerData[1][11] = "0" ;
        Pdata.GetComponent<Player_Data>().PlayerData[1][12] = "0" ;
        // 被験者実験のため一度削除
        //for(int i=3;i<=48;i++){
        //    Rdata.GetComponent<Result_Data>().ResultData[i][1]="0";
        //}
        Pdata.GetComponent<Player_Data>().CsvSave();
        Rdata.GetComponent<Result_Data>().CsvSave();
        Destroy(Pdata);
        Destroy(GameObject.Find("EventData"));
        //Destroy(Rdata);
    }
}

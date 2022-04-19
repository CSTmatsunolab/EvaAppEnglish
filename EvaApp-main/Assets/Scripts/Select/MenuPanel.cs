using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Drink;
    [SerializeField] GameObject Event;
    [SerializeField] GameObject Explanation;
    [SerializeField] GameObject ChallengeTest;//防災テストへの誘導パネル
    [SerializeField] GameObject GameOver;//ゲームオーバーシーン
    [SerializeField] GameObject Haikyu;//配給時のパネル
    public Image explanation;
    public Image ChallengeImage;
    public Sprite test;
    public Sprite list;
    GameObject Pdata;
    static public int flag = 0;//行動するボタンからイベント選択画面に行くか、ランダムイベントからイベント選択画面に行くか
    static public int Randflag = 0;
    //static public int ChangeMusic = 0;
    int push = 0;//ボタンが押された回数
    private string path;
    static public int EC = 0;
    private int page = 0;//説明パネルのページ数

    void Start()
    {
        BackToMenu();
        BackToDrink();
        EventClose();
        GameOverClose();
        HaikyuClose();
        RandomEvent();
        CloseExplanation();
        OpenGaugeExplanation();
        GoChallengeTest();
        HaikyuEvent();
    }

    public void BackToMenu()
    {
        Menu.SetActive(false);//Menuパネルを非表示
    }

    public void GoToMenu()
    {
        Menu.SetActive(true);//Menuパネルを表示
    }

    public void GoToTitle()
    {
        PdataLoad();
        Destroy(Pdata);
        Destroy(GameObject.Find("EventData"));
        Destroy(GameObject.Find("ResultData"));
        SceneManager.LoadScene("StartScene");//スタートシーンに遷移
    }

    public void GoToDrink()
    {
        Drink.SetActive(true);//Drinkパネルを表示
    }

    public void BackToDrink()
    {
        Drink.SetActive(false);//Drinkパネルを非表示
    }

    public void GoToExplanation(){//説明パネル表示
        page = 0;
        Explanation.SetActive(true);
        BackToMenu();
    }

    public void GoPage(){//説明パネル右ボタン
        page++;
        if(page > 4)
            page = 0;
        explanation.sprite = Resources.Load<Sprite>("Sprites/Explanation"+page.ToString());
    }

    public void ReturnPage(){//説明パネル左ボタン
        page--;
        if(page < 0)
            page = 4;
        explanation.sprite = Resources.Load<Sprite>("Sprites/Explanation"+page.ToString());
    }

    public void CloseExplanation(){//説明パネル非表示
        Explanation.SetActive(false);
    }

    private void EventClose()
    {
        Event.SetActive(false);
    }

    private void GameOverClose()
    {
        GameOver.SetActive(false);
    }

    private void HaikyuClose()
    {
        Haikyu.SetActive(false);
    }

    private void ChallengeTestClose()
    {
        ChallengeTest.SetActive(false);
    }

    private void PdataLoad(){
        Pdata = GameObject.Find("Player_Data");
    }

    private void RandomEvent(){
        PdataLoad();
        int i = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][6]);//昼、夜の判定
        int GameOverFlag = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][11]);
        if(i == 0){
            Randflag = 0;
            if(GameOverFlag >= 2)//ゲームオーバーのフラグが2になったらゲームオーバー
            {
                Debug.Log("GameOver");
                GameOver.SetActive(true);
            }

        }
        if(i == 1)
        {
            int a = Random.Range(1,100);
            Debug.Log(a);
            if(Randflag == 0){
                if(a >= 33){
                    Randflag = 1;
                    Event.SetActive(true);
                }
            }    
        }
    }

    private void HaikyuEvent(){
        int Day = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][5]);
        int Time = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][6]);
        int WaterStock = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][3]);
        int FoodStock = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][4]);
        int HaikyuCount = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][12]);
        if(HaikyuCount == 0 && Day >= 2 && Day % 2 == 1 && Time == 0)
        {
            int rand = Random.Range(1,100);//配給ガチャ
            Debug.Log("配給日");
            if(rand > 50){
                WaterStock = WaterStock + 3;
                FoodStock = FoodStock + 3;
                HaikyuCount = HaikyuCount + 1;

                Pdata.GetComponent<Player_Data>().PlayerData[1][3]=WaterStock.ToString();
                Pdata.GetComponent<Player_Data>().PlayerData[1][4]=FoodStock.ToString();
                Pdata.GetComponent<Player_Data>().PlayerData[1][12]=HaikyuCount.ToString();
                Pdata.GetComponent<Player_Data>().CsvSave();
                Haikyu.SetActive(true);
            } 
        }
    }

    private void OpenGaugeExplanation() //起動時に説明パネル1を表示
    {
        PdataLoad();
        int x = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][5]);
        int y = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][6]);
        if(x == 1)
        {
            if(y == 0 && EC == 0)
            {
                Pdata.GetComponent<Player_Data>().CsvSave();
                GoToExplanation();
                EC = 1;
            }
        }
    }


    //行動するボタンからイベント選択画面に遷移する場合
    public void GoButtonDown(){
        flag = 0;
        //ChangeMusic = 1;
        SceneManager.LoadScene("EventSelectScene");//イベント選択画面に遷移
    }

    //ランダムイベントの進むボタンからイベント選択画面に遷移する場合
    public void GoButtonDown2(){
        flag = 1;
        Debug.Log(flag);
        //ChangeMusic = 1;
        SceneManager.LoadScene("EventSelectScene");//イベント選択画面に遷移
    }

    //ゲームオーバー時の進むボタンから遷移
    public void GoButtonDown3()
    {
        //flag = 2;
        //ChangeMusic = 1;
        Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "49";
        SceneManager.LoadScene("EventScene");//イベント選択画面に遷移
    }

    //配給のパネルから遷移
    public void GoHaikyuButtonDown()
    {
        flag = 3;
        Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "54";
        SceneManager.LoadScene("EventScene");//イベント画面に遷移
        /*
        //ChangeMusic = 1;
        SceneManager.LoadScene("EventSelectScene");//イベント選択画面に遷移
        */
    }

    private void GoChallengeTest()
    {
        PdataLoad();
        int a = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][5]);
        int b = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][6]);
        if ((a >= 8)&&(b == 0)){
            ChallengeTest.SetActive(true);
            ChallengeImage.sprite = test;//ChallengeTestパネルを表示 
        }else{
            ChallengeTest.SetActive(false);
        }
    }

    //チャレンジテストに挑戦するボタンからリスト誘導パネル
    public void YesChallengeTest()
    {
        Pdata.GetComponent<Player_Data>().PlayerData[1][8]="3";
        if (push == 0){
            ChallengeImage.sprite = list;
            push = 1;
        }else if (push == 1){
            SceneManager.LoadScene("ListScene");
        }
    }

    //チャレンジテストに挑戦しないボタンが押された場合にChallengeTestパネルを消す
    public void NotChallengeTest()
    {
        if (push == 0){
            ChallengeTest.SetActive(false);
            push = 1;
        }else if (push == 1){
            SceneManager.LoadScene("QuizScene");
        }
        
    }


    static public int Send(){
        return flag;
    }

    /*public static int SendChangeMusic()
    {
        return ChangeMusic;
    }*/
}

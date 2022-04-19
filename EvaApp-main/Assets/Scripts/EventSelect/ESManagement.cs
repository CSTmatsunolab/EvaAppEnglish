using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;//乱数生成のために追加
using UnityEngine.SceneManagement;
using System.IO;


public class ESManagement : MonoBehaviour
{
    [SerializeField] GameObject EventBackground;//イベント選択画面の背景画像UI
    [SerializeField] GameObject EventImage;//イベントの画像UI
    [SerializeField] GameObject TitleText;//イベントのタイトルのTextUI
    [SerializeField] GameObject EventText;//イベントの問題文のTextUI
    [SerializeField] GameObject ClearText;//正解の選択肢のTextUI
    [SerializeField] GameObject FailText;//不正解の選択肢のTextUI
    public Image Maru;//まるの画像
    public Image Batsu;//ばつの画像

    [SerializeField] private string spritesDirectory = "Sprites/Event";//イベント画像が保存されているフォルダへのパス
    private GameObject Rdata;//イベント閲覧フラグのオブジェクト

    public static int index = 0;//イベントのナンバー
    public static int Answer = 0;//イベントの回答
    private int Transform = 0;//

    [SerializeField] private AudioSource SEplayer;//EventSelectManagementのAudioSourceコンポーネント
    [SerializeField] private AudioClip SeikaiSE;//正解のSE
    [SerializeField] private AudioClip FuseikaiSE;//不正解のSE

    // Start is called before the first frame update
    void Start(){        
        index = rand();//乱数生成
        Debug.Log(index);//コンソールにイベントNo.を表示
        Transform = Random.Range(1,3);//1~2
        Debug.Log(Transform);
        EventLoad(index);
        Maru.gameObject.SetActive(false);
        Batsu.gameObject.SetActive(false);
    }

    //ResultData読み込み
    private void RdataLoad(){
        Rdata = GameObject.Find("ResultData");
    }

    //乱数(イベントのナンバー)を生成
    int rand(){
        RdataLoad();
        int flag = MenuPanel.Send();
        int a = 0; //indexに入る値
        int kaburiCheck = 2;

        if(flag == 0)
        {
            
            a = Random.Range(3,41);//3~40
            //a = Random.Range(21,23);//指定範囲の動作確認用
            Debug.Log(a);

            kaburiCheck = int.Parse(Rdata.GetComponent<Result_Data>().ResultData[a][1]);
            Debug.Log("kaburiCheck = " + kaburiCheck);
        
            for(int i = 0;i < 20;i++) //もし解放済みのイベントだった場合、再抽選 最大5回
                                    //実験にあたってなるべく被らせたくないので15回に
            {
                if(kaburiCheck == 1)
                {
                    a = Random.Range(3,41);//3~40
                    //a = Random.Range(21,23);//指定範囲の動作確認用
                    //a = 50;
                    kaburiCheck = int.Parse(Rdata.GetComponent<Result_Data>().ResultData[a][1]);
                    Debug.Log("a1 = " + a);
                }
            }
            
        }
        else if(flag == 1){
            //a = Random.Range(3,23);
            a = Random.Range(3,41);//3~29

            kaburiCheck = int.Parse(Rdata.GetComponent<Result_Data>().ResultData[a][1]);
            Debug.Log("kaburiCheck = " + kaburiCheck);
        
            for(int i = 0;i < 20;i++) //もし解放済みのイベントだった場合、再抽選 最大5回
                                        //今回は15回
            {
                if(kaburiCheck == 1)
                {
                    a = Random.Range(3,41);//3~40
                    kaburiCheck = int.Parse(Rdata.GetComponent<Result_Data>().ResultData[a][1]);
                    Debug.Log("a1 = " + a);
                }
            }
        }
        else if(flag == 3){
            a = 54;//配給用のイベント
        }
        return a;
    }

    //イベント画面のテキストや画像関係のロード
    void EventLoad(int a){
        Image eventbg;
        Image eventimg;
        Text titletext;
        Text eventtext;
        Text cleartext;
        Text failtext;

        GameObject EData = GameObject.Find("EventData");

        eventbg = EventBackground.GetComponent<Image>();
        eventimg = EventImage.GetComponent<Image>();
        titletext = TitleText.GetComponent<Text>();
        eventtext = EventText.GetComponent<Text>();
        cleartext = ClearText.GetComponent<Text>();
        failtext = FailText.GetComponent<Text>();


        //イベントセレクトシーンの背景の色(半透明)
        string str = spritesDirectory+a.ToString();
        eventbg.sprite = Resources.Load<Sprite>(str);
        var c = eventbg.color;
        eventbg.color = new Color(c.r, c.g, c.b, 100.0f/255.0f);//透明度を上げる

        //ランダムに選択されたイベントNo.のイラストを読み込む
        eventimg.sprite = Resources.Load<Sprite>(str);

        //ランダムに選択されたイベントNo.のタイトルを読み込む
        titletext.text = EData.GetComponent<Event_Data>().EventData[a][1];

        //ランダムに選択されたイベントNo.の概要を読み込む
        eventtext.text = EData.GetComponent<Event_Data>().EventData[a][2];

        //ランダムに選択されたイベントNo.の正解選択肢を読み込む
        cleartext.text = EData.GetComponent<Event_Data>().EventData[a][7];
        //ランダムに選択されたイベントNo.の不正解選択肢を読み込む
        failtext.text = EData.GetComponent<Event_Data>().EventData[a][8];

        int Change = 0;
        Change = Transform;

        if(Change == 1) //正解
        {
            //ランダムに選択されたイベントNo.の正解選択肢を読み込む
            cleartext.text = EData.GetComponent<Event_Data>().EventData[a][7];
            //ランダムに選択されたイベントNo.の不正解選択肢を読み込む
            failtext.text = EData.GetComponent<Event_Data>().EventData[a][8];
            Debug.Log("Change = 1");
        }
        else if(Change == 2) //不正解
        {
            //ランダムに選択されたイベントNo.の不正解選択肢を読み込む
            cleartext.text = EData.GetComponent<Event_Data>().EventData[a][8];
            //ランダムに選択されたイベントNo.の正解選択肢を読み込む
            failtext.text = EData.GetComponent<Event_Data>().EventData[a][7];
            Debug.Log("Change = 2");
        }
    }

    public void ClearButtonDown()//イベント画面に遷移
    {
        int Check = 0;
        Check = Transform;
        if(Check == 1) //正位置　正解
        {
            RdataLoad();
            Rdata.GetComponent<Result_Data>().ResultData[index][1] = "1";
            Rdata.GetComponent<Result_Data>().CsvSave();
            Answer = 0;
            Debug.Log("正解");
            
            SEplayer.PlayOneShot(SeikaiSE);
            StartCoroutine("Seikai");
        }
        else if(Check == 2) //逆位置　不正解
        {
            RdataLoad();
            Rdata.GetComponent<Result_Data>().ResultData[index][1] = "1";
            Rdata.GetComponent<Result_Data>().CsvSave();
            Answer = 1;
            Debug.Log("不正解"); 

            SEplayer.PlayOneShot(FuseikaiSE);
            StartCoroutine("Huseikai");
        }
    }

    public void FailButtonDown()//イベント画面に遷移
    {
        int Check = 0;
        Check = Transform;
        if(Check == 1) //正位置　不正解
        {
            RdataLoad();
            Rdata.GetComponent<Result_Data>().ResultData[index][1] = "1";
            Rdata.GetComponent<Result_Data>().CsvSave();
            Answer = 1;
            Debug.Log("不正解"); 

            SEplayer.PlayOneShot(FuseikaiSE);                     
            StartCoroutine("Huseikai");
        }
        else if(Check == 2) //逆位置 正解
        {
            RdataLoad();
            Rdata.GetComponent<Result_Data>().ResultData[index][1] = "1";
            Rdata.GetComponent<Result_Data>().CsvSave();
            Answer = 0;
            Debug.Log("正解");

            SEplayer.PlayOneShot(SeikaiSE);
            StartCoroutine("Seikai");
        }
    }

    IEnumerator Seikai(){//正解の時のアニメーション
        Maru.gameObject.SetActive(true);//MaruをActiveにする
        Color c = Maru.color;
        c.a = 1.3f; 
        Maru.color = c; // 画像の不透明度を1にする
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            yield return null; // 1フレーム待つ
            c.a -= 0.02f;
            Maru.color = c; // 画像の不透明度を下げる
    
            if (c.a <= 0f) // 不透明度が0以下のとき
            {
                c.a = 0f;
                Maru.color = c; // 不透明度を0
                break; // 繰り返し終了
            }
        }        
        Maru.gameObject.SetActive(false); // 画像を非アクティブにする
        SceneManager.LoadScene("EventScene"); 
    }

    IEnumerator Huseikai(){//不正解の時のアニメーション
        Batsu.gameObject.SetActive(true);//BatsuをActiveにする
        Color c = Batsu.color;
        c.a = 1.3f; 
        Batsu.color = c; // 画像の不透明度を1にする
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            yield return null; // 1フレーム待つ
            c.a -= 0.02f;
            Batsu.color = c; // 画像の不透明度を下げる
    
            if (c.a <= 0f) // 不透明度が0以下のとき
            {
                c.a = 0f;
                Batsu.color = c; // 不透明度を0
                break; // 繰り返し終了
            }
        }        
        Batsu.gameObject.SetActive(false); // 画像を非アクティブにする
        SceneManager.LoadScene("EventScene"); 
    }

    public static int Send()//イベントナンバーを返す
    {
        return index;
    }

    public static int SendAnswer()//回答を返す
    {
        return Answer;
    }
}
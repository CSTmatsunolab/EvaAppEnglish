using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;//乱数生成のために追加
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class QuizManagement : MonoBehaviour
{
    public GameObject Content;
    public Text QuizNo;
    public Text QuizSentence;
    public Text cleartext;
    public Text failtext;
    public Image Maru;
    public Image Batsu;
    public GameObject Result;
    public GameObject Event;
    private GameObject Pdata;
    private GameObject Edata;
    GameObject[] ListButton;
    Image btnimg;
    Text btntext;
    static public int Seikaisu = 0;

    [SerializeField] private AudioSource SEplayer;
    [SerializeField] private AudioClip SeikaiSE;
    [SerializeField] private AudioClip FuseikaiSE;
    

    int[] a = new int[10];//選出されたクイズのNo.
    int[] result = new int[10];//
    int no = 0;//クイズが何題めか
    int Change = 0;
    int Change2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        Syokika();
        Shuffle();
        EdataLoad();
        NoSet();
        QuizLoad();
        Result.SetActive(false);
        Event.SetActive(false);
        Maru.gameObject.SetActive(false);
        Batsu.gameObject.SetActive(false);
    }

    private void PdataLoad(){
        Pdata = GameObject.Find("Player_Data");
    }

    private void NoSet(){
        QuizNo.text="Question "+(no+1).ToString();
    } 

    private void EdataLoad(){
        Edata = GameObject.Find("EventData");
    }
    private int Rand(){
        int a = Random.Range(1,3);//1~2
        return a; 
    }
    
    void QuizLoad(){
        //ランダムに選択されたイベントNo.の問題文を読み込む
        QuizSentence.text = Edata.GetComponent<Event_Data>().EventData[a[no]][2];
        Change = Rand();
        if(Change == 1) //正解
        {
            //ランダムに選択されたイベントNo.の正解選択肢を読み込む
            cleartext.text = Edata.GetComponent<Event_Data>().EventData[a[no]][7];
            //ランダムに選択されたイベントNo.の不正解選択肢を読み込む
            failtext.text = Edata.GetComponent<Event_Data>().EventData[a[no]][8];
            Debug.Log("Change = 1");
        }
        else if(Change == 2) //不正解
        {
            //ランダムに選択されたイベントNo.の不正解選択肢を読み込む
            cleartext.text = Edata.GetComponent<Event_Data>().EventData[a[no]][8];
            //ランダムに選択されたイベントNo.の正解選択肢を読み込む
            failtext.text = Edata.GetComponent<Event_Data>().EventData[a[no]][7];
            Debug.Log("Change = 2");
        }
    }

    public void Buttondown1(){
        Change2 = 1;
    }

    public void Buttondown2(){
        Change2 = 2;
    }

    void Answer(){
        if(Change==Change2){
            Debug.Log("正解");
            Debug.Log(result[no]);
            Seikaisu++;
            StartCoroutine("Seikai");

            SEplayer.PlayOneShot(SeikaiSE);
            StartCoroutine("Seikai");

        }else{
            Debug.Log("不正解");
            result[no]=1;
            Debug.Log(result[no]);
            StartCoroutine("Huseikai");

            SEplayer.PlayOneShot(FuseikaiSE);                     
            StartCoroutine("Huseikai");

        }
    }

    public void AnswerButtonDown(){
        Answer();
        no++;
        if(no > 9){
            ListButtonMake();
            Result.SetActive(true);
        }
        else{
        QuizLoad();
        NoSet();
        }
       
    }

    void Shuffle(){
        int x = 37;//イベントの数−3
        int[] b = new int[x];
        for(int i=0;i<x;i++){
            b[i]=i+3;
        }
        b = b.OrderBy(y => System.Guid.NewGuid()).ToArray();
        for(int i=0;i<10;i++){
            a[i]=b[i];
            Debug.Log(a[i]);
        }
        
    }
    private void ListButtonMake(){
        EdataLoad();
        GameObject btn = (GameObject)Resources.Load("Prefabs/ResultButton");
        ListButton = new GameObject[10];
        for (int i = 0 ;i < 10;i++){
            
            ListButton[i] = Instantiate(btn);
            ListButton[i].transform.SetParent(Content.transform,false);
            btnimg = ListButton[i].transform.Find("Image").gameObject.GetComponent<Image>();
            btntext = ListButton[i].transform.Find("Text").gameObject.GetComponent<Text>();
            ListButton[i].name = (a[i]).ToString();  //各ボタンにイベントのNoに応じた名前がつく
            if(result[i]==0){
                btnimg.sprite = Resources.Load<Sprite>("Sprites/Maru");
            }else{
                btnimg.sprite = Resources.Load<Sprite>("Sprites/Batsu");
            }
            
            btntext.text = "Question"+(i+1).ToString()+"："+Edata.GetComponent<Event_Data>().EventData[a[i]][2];
        }
    }

    IEnumerator Seikai(){
        Maru.gameObject.SetActive(true);
        Color c = Maru.color;
        c.a = 1f; 
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
    }

    IEnumerator Huseikai(){
        Batsu.gameObject.SetActive(true);
        Color c = Batsu.color;
        c.a = 1f; 
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
    }

    public void GotoEvent(){
        PdataLoad();
        if(Seikaisu==10){
            Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "51";
        }
        else if(Seikaisu >=6){
            Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "52";
        }
        else{
            Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "53";
        }
        
        SceneManager.LoadScene("EventScene"); //ボタンが押されたら遷移
    }

    private void Syokika(){
        Seikaisu=0;
        for(int i=0;i<10;i++){
            result[i]=0;
        }
    }

    public void ListButtonClose(){
        Event.SetActive(false);//Eventパネルを非表示
    }

    static public int Send(){
        return Seikaisu;
    }
}


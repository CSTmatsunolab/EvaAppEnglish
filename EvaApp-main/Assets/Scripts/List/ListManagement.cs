using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ListManagement : MonoBehaviour
{
    int EResultLine = 55;//EventResultの行数(1行目(項目名)を含む)
    public Text Kanpan;
    public Text Mizu;
    public Text OpenedEvent;
    public GameObject Content;
    [SerializeField] GameObject Event;
    GameObject[] ListButton;
    GameObject btn;
    GameObject Edata;
    GameObject Pdata;
    GameObject Rdata;
    Image btnimg;
    Text btntext;
    

    // Start is called before the first frame update
    void Start(){
        TextLoad();
        ListButtonMake();
        ListButtonClose();  //Eventパネルを非表示
    }

    private void PdataLoad(){
        Pdata = GameObject.Find("Player_Data");
    }

    private void EdataLoad(){
        Edata = GameObject.Find("EventData");
    }
    
    private void RdataLoad(){
        Rdata = GameObject.Find("ResultData");
    }
    

    private void TextLoad(){
        PdataLoad();
        RdataLoad();
        Kanpan.text = Pdata.GetComponent<Player_Data>().PlayerData[1][4];
        Mizu.text = Pdata.GetComponent<Player_Data>().PlayerData[1][3];
        int OpenedEventCount = 0;
        for (int i = 0 ;i < EResultLine-1; i++){ //EventResult.csvの行数分、開放フラグを足すのを繰り返す 0行目は飛ばす       
            OpenedEventCount += int.Parse(Rdata.GetComponent<Result_Data>().ResultData[i+1][1]);
        }
        OpenedEvent.text = OpenedEventCount.ToString();
    }

    private void ListButtonMake(){
        EdataLoad();
        RdataLoad();
        int a = Edata.GetComponent<Event_Data>().EventData.Count-1;
        int b = 0;
        btn = (GameObject)Resources.Load("Prefabs/Button");
        ListButton = new GameObject[a];
        for (int i = 0 ;i < a;i++){
            
            ListButton[i] = Instantiate(btn);
            ListButton[i].transform.SetParent(Content.transform,false);
            btnimg = ListButton[i].transform.Find("Image").gameObject.GetComponent<Image>();
            btntext = ListButton[i].transform.Find("Text").gameObject.GetComponent<Text>();
            ListButton[i].name = (i+1).ToString();  //各ボタンにイベントのNoに応じた名前がつく
            b = int.Parse(Rdata.GetComponent<Result_Data>().ResultData[i+1][1]);
            if(b == 0){
                ListButton[i].GetComponent<Button>().interactable = false;
                btntext.text = "???";
                btnimg.sprite = Resources.Load<Sprite>("Sprites/NoImage");
            }
            else{
                btnimg.sprite = Resources.Load<Sprite>("Sprites/Event"+(i+1).ToString());
                btntext.text = Edata.GetComponent<Event_Data>().EventData[i+1][1];
            }
        }
    }

    public void ListButtonClose(){
        Event.SetActive(false);//Eventパネルを非表示
    }

}

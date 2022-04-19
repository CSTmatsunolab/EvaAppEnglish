using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EventDetail : MonoBehaviour{
    [SerializeField] private string spritesDirectory = "Sprites/Event";
    GameObject Event;
    GameObject Edata;
    GameObject TitleText;
    GameObject GaiyouText;
    GameObject EventImage;

    private void EdataLoad(){
        Edata = GameObject.Find("EventData");
    }
        
    public void ListButtonDatail(){
        EdataLoad();
        Event = GameObject.Find("Canvas").transform.Find("EventPanel").gameObject;
        Event.SetActive(true);//Eventパネルを表示
        TitleText = GameObject.Find("Canvas").transform.Find("EventPanel").transform.Find("BackgroundImage").transform.Find("TitleText").gameObject;
        GaiyouText = GameObject.Find("Canvas").transform.Find("EventPanel").transform.Find("BackgroundImage").transform.Find("GaiyouText").gameObject;
        EventImage = GameObject.Find("Canvas").transform.Find("EventPanel").transform.Find("BackgroundImage").transform.Find("EventImage").gameObject;
        int a = int.Parse(this.name);   //リストボタンを押したときに名前を取得
        string str = spritesDirectory+a.ToString();     //イベント画像までのパスを作ってる
        TitleText.GetComponent<Text>().text = Edata.GetComponent<Event_Data>().EventData[a][1];
        GaiyouText.GetComponent<Text>().text = Edata.GetComponent<Event_Data>().EventData[a][9];
        EventImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(str);
    }
}

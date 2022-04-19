using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class InputText : MonoBehaviour
{
    //オブジェクトと結びつける
    public InputField inputField;
    public Text titletext;
    public Text input;
    public Image background;
    public GameObject Hand;
    public Sprite smartphone;
    public Sprite syomei;
    public GameObject submit;
    public Sprite black;
    public Sprite brown;
    GameObject Pdata;
    string name = "";
    int introcheck;

    void Start () {
    //Componentを扱えるようにする
        Hand.SetActive(false);
        inputField = inputField.GetComponent<InputField> ();
        PdataLoad();
        introcheck = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][8]);
        Titletext();
    }

    private void Titletext(){
        if (introcheck == 0){   //パネル表示１回目
            Hand.SetActive(true);
            background.sprite = smartphone;
            submit.GetComponent<Image>().sprite = black;
            titletext.text = "<color=#FF0000>避難所名</color>を\n入力してください(任意)";
            input.text = "避難所名";
        }
        if(introcheck == 1){    //パネル表示２回目
            background.sprite = syomei;
            submit.GetComponent<Image>().sprite = brown;
            titletext.text = "<color=#FF0000>名前</color>を\n入力してください(任意)";
            input.text = "名前";
        }
    }

    public void Input(){
        if(inputField.text!=""){
            name = inputField.text;
        }

        Debug.Log(name);
        if (introcheck == 0){
            if(name != ""){
                //入力した避難所名を配列に保存
                Pdata.GetComponent<Player_Data>().PlayerData[1][9] = name;
            }
            // 現在のScene名を取得する
            Scene loadScene = SceneManager.GetActiveScene();
            // Sceneの読み直し
            SceneManager.LoadScene(loadScene.name);
        }
        if (introcheck == 1){
            if(name != ""){
                //入力した名前を配列に保存
                Pdata.GetComponent<Player_Data>().PlayerData[1][10] = name;
            }
            SceneManager.LoadScene("SelectScene");
        }
    }

    private void PdataLoad(){
        Pdata = GameObject.Find("Player_Data");
    }

    public void OnValueChanged()
    {
        string value = inputField.text;
        if (value.IndexOf("\n") != -1)
        {
        	value = value.Replace("\r", "").Replace("\n", "");
         	inputField.text = value;
        }
    }
}

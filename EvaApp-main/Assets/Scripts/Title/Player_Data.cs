using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Player_Data : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    public List<string[]> PlayerData = new List<string[]>(); // CSVの中身を入れるリスト;
    private Button ContinueButton;
    void Start(){
        string path = Application.persistentDataPath + "/PlayerData.csv";
        if (!File.Exists(path)){
            ContinueButton = GameObject.Find("Canvas/ContinueButton").GetComponent<Button>();
            ContinueButton.interactable=false;
        }
    }

    public void StartButtonDown()
    {
        Debug.Log(Application.persistentDataPath);
        //string csv_path = "Player_Data";  // CSVファイルのパス
        csvFile = Resources.Load("PlayerData") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            PlayerData.Add(line.Split(',')); // , 区切りでリストに追加
        }
        // データ確認用
        
        for (int i = 0; i < PlayerData.Count; i++)
        {
            for (int j = 0; j < PlayerData[0].Length; j++)
            {
                Debug.Log("PlayerData[" + i + "][" + j + "]=" + PlayerData[i][j]);
            }
        }
        DontDestroyOnLoad(this);
    }

    public void ContinueButtonDown()
    {
        string path = Application.persistentDataPath + "/PlayerData.csv";
        using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            while (fs.Peek() != -1)
            {
                string line = fs.ReadLine(); // 一行ずつ読み込み
                PlayerData.Add(line.Split(',')); // , 区切りでリストに追加
            }

        }
        
        for (int i = 0; i < PlayerData.Count; i++)
        {
            for (int j = 0; j < PlayerData[0].Length; j++)
            {
                Debug.Log("PlayerData[" + i + "][" + j + "]=" + PlayerData[i][j]);
            }
        }
        DontDestroyOnLoad(this);
    }

    public void CsvSave()
    {
        string path = Application.persistentDataPath + "/PlayerData.csv";
        using (var fs = new StreamWriter(path, false, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            for (var y=0; y < 2; y++)
            {
                for(var x=0; x < 13; x++)
                {
                    fs.Write(PlayerData[y][x]+",");
                    fs.Flush();
                }
                fs.WriteLine();
            }
        }
    }
}
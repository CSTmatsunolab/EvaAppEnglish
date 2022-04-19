using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Event_Data : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    public List<string[]> EventData = new List<string[]>(); // CSVの中身を入れるリスト;

void Start()
    {
        csvFile = Resources.Load("EventData") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            EventData.Add(line.Split(',')); // , 区切りでリストに追加
        }
        // データ確認用
        for (int i = 0; i < EventData.Count; i++)
        {
            for (int j = 0; j < EventData[0].Length; j++)
            {
                Debug.Log("EventData[" + i + "][" + j + "]=" + EventData[i][j]);
            }
        }
        DontDestroyOnLoad(this);
    }
}
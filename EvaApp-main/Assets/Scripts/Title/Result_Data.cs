using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Result_Data : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    
    public List<string[]> ResultData = new List<string[]>(); // CSVの中身を入れるリスト;

    public void StartButtonDown()
    {
        Debug.Log(Application.persistentDataPath);
        //string csv_path = "Player_Data";  // CSVファイルのパス
        csvFile = Resources.Load("EventResult") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            ResultData.Add(line.Split(',')); // , 区切りでリストに追加
        }
        // データ確認用
        
        for (int i = 0; i < ResultData.Count; i++)
        {
            for (int j = 0; j < ResultData[0].Length; j++)
            {
                Debug.Log("ResultData[" + i + "][" + j + "]=" + ResultData[i][j]);
            }
        }
        CsvSave();
        DontDestroyOnLoad(this);
    }

    public void ContinueButtonDown()
    {
        string path = Application.persistentDataPath + "/EventResult.csv";
        using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            while (fs.Peek() != -1)
            {
                string line = fs.ReadLine(); // 一行ずつ読み込み
                ResultData.Add(line.Split(',')); // , 区切りでリストに追加
            }

        }
        
        for (int i = 0; i < ResultData.Count; i++)
        {
            for (int j = 0; j < ResultData[0].Length; j++)
            {
                Debug.Log("ResultData[" + i + "][" + j + "]=" + ResultData[i][j]);
            }
        }
        DontDestroyOnLoad(this);
    }

    public void CsvSave()
    {
        string path = Application.persistentDataPath + "/EventResult.csv";
        using (var fs = new StreamWriter(path, false, System.Text.Encoding.GetEncoding("UTF-8")))
        {
            for (var y=0; y < ResultData.Count; y++)
            {
                for(var x=0;x < 2;x++)
                {
                    fs.Write(ResultData[y][x]+",");
                    fs.Flush();
                }
                fs.WriteLine();
            }
        }
    }
        
}
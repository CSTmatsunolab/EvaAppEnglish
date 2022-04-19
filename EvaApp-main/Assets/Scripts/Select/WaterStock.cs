using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;  // 追加しましょう

public class WaterStock: MonoBehaviour
{
    public GameObject name_object = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Pdata = GameObject.Find("Player_Data");
        // オブジェクトからTextコンポーネントを取得
        Text stock_text = name_object.GetComponent<Text>();
        // テキストの表示を入れ替える
        stock_text.text = "×"+Pdata.GetComponent<Player_Data>().PlayerData[1][3];
    }
}
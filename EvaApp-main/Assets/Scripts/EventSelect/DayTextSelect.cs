using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using UnityEngine.UI;  // 追加しましょう
public class DayTextSelect: MonoBehaviour
{
    public GameObject name_object = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Pdata = GameObject.Find("Player_Data");
        int i = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][6]);
        //昼、夜の判定
        string time = "昼";
        if (i == 0)
            {
                time = "昼";
            }
        else if(i == 1)
            {
                time = "夜";
            }
        // オブジェクトからTextコンポーネントを取得
        Text sta_text = name_object.GetComponent<Text>();
        // テキストの表示を入れ替える
        sta_text.text = Pdata.GetComponent<Player_Data>().PlayerData[1][5] + "日目" + time;
    }
}
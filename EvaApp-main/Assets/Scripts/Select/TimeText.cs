using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    public GameObject name_object = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Pdata = GameObject.Find("Player_Data");
        int i = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][6]);
        string time = "昼";
        //昼、夜の判定
        if (i == 0)
            {
                time = "昼";
            }
        else if(i == 1)
            {
                time = "夜";
            }
        Text TimeText = name_object.GetComponent<Text>();
        TimeText.text = time;
    }
}

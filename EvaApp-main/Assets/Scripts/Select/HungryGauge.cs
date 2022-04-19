using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;  // 追加しましょう
public class HungryGauge : MonoBehaviour
{
    public GameObject name_object = null;

    Slider _slider;
    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("HungryGauge").GetComponent<Slider>();
        //　配列の取得 
        GameObject Pdata = GameObject.Find("Player_Data");
        int i = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][0]);

        int Hungry = 0;
        Hungry = i;
        _slider.value = Hungry;
    }

    void Update()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoll : MonoBehaviour
{
    private Vector3 Staffrollposition;
    public RectTransform rectTransform;
    public float Endpos;
    // Start is called before the first frame update
    void Start()
    {
        Staffrollposition = rectTransform.anchoredPosition;
    }
    // Update is called once per frame
    void Update()//スタッフロールを上昇させる処理
    {
        if (rectTransform.anchoredPosition.y < Endpos) {

            Staffrollposition.y += 3f;
            rectTransform.anchoredPosition = Staffrollposition;
        }
    }
}
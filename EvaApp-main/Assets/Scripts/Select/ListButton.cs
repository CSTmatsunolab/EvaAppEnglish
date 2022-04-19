using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ListButton : MonoBehaviour
{
    // リストシーンに遷移
    public void ListButtonDown()
    {
        SceneManager.LoadScene("ListScene");
    }
}
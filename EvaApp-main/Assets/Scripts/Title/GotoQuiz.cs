using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoQuiz : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonDown(){
        SceneManager.LoadScene("QuizScene");
    }
}

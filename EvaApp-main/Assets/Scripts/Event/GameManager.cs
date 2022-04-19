using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;


// MonoBehaviourを継承することでオブジェクトにコンポーネントとして
// アタッチすることができるようになる
public class GameManager : MonoBehaviour
{
    private GameObject Pdata;
    private string _text = "";
    private const string COMMAND_WAIT_TIME = "wait";
    private float _waitTime = 0;
    private string index = "";
    private string answer = "";

    private const char SEPARATE_MAIN_START = '「';
    private const char SEPARATE_MAIN_END = '」';
    private const char SEPARATE_PAGE = '&';
    private const char SEPARATE_COMMAND = '!';
    private const char COMMAND_SEPARATE_PARAM = '=';
    private const string COMMAND_BACKGROUND = "background";
    private const string COMMAND_SPRITE = "_sprite";
    private const string COMMAND_COLOR = "_color";
    private const string COMMAND_CHARACTER_IMAGE = "charaimg";
    private const string COMMAND_SIZE = "_size";
    private const string COMMAND_POSITION = "_pos";
    private const string COMMAND_ROTATION = "_rotate";
    private const string CHARACTER_IMAGE_PREFAB = "CharacterImage";
    private const string COMMAND_ACTIVE = "_active";
    private const string COMMAND_DELETE = "_delete";
    private const char COMMAND_SEPARATE_ANIM = '%';
    private const string COMMAND_ANIM = "_anim";
    private const string COMMAND_FOREGROUND = "foreground";

    // SerializeFieldと書くとprivateなパラメーターでも
    // インスペクター上で値を変更できる
    [SerializeField]
    private Text mainText;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private float captionSpeed = 0.2f;
    [SerializeField]
    private GameObject nextPageIcon;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private string spritesDirectory = "Sprites/";
    [SerializeField]
    private GameObject characterImages;
    [SerializeField]
    private string prefabsDirectory = "Prefabs/";
    private List<Image> _charaImageList = new List<Image>();
    [SerializeField]
    private string textFile = "";
    [SerializeField]
    private string animationsDirectory = "Animations/";
    [SerializeField]
    private string overrideAnimationClipName = "Clip";
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    GameObject Result;
    [SerializeField]
    private Text titleText;
     [SerializeField]
    GameObject InputPanel;

    private Queue<string> _pageQueue;
    private Queue<char> _charQueue;

    private Queue<char> SeparateString(string str)
    {
        // 文字列をchar型の配列にする = 1文字ごとに区切る
        char[] chars = str.ToCharArray();
        Queue<char> charQueue = new Queue<char>();
        // foreach文で配列charsに格納された文字を全て取り出し
        // キューに加える
        foreach (char c in chars) charQueue.Enqueue(c);
        return charQueue;
    }

    private void ReadLine(string text)
    {
        if (text[0].Equals(SEPARATE_COMMAND))
        {
            ReadCommand(text);
            //if (_selectButtonList.Count > 0) return;
            if (_waitTime > 0)
            {
                StartCoroutine(WaitForCommand());
                return;
            }
            ShowNextPage();
            return;
        }
        string[] ts = text.Split(SEPARATE_MAIN_START);
        string name = ts[0];
        string main = ts[1].Remove(ts[1].LastIndexOf(SEPARATE_MAIN_END));
        if (name.Equals("")) nameText.transform.parent.gameObject.SetActive(false);
        else
        {
            if(name == "あなた"){
                name = Pdata.GetComponent<Player_Data>().PlayerData[1][10];
            }
            nameText.text = name;
            nameText.transform.parent.gameObject.SetActive(true);
        }
        mainText.text = "";
        _charQueue = SeparateString(main);
        StartCoroutine(ShowChars(captionSpeed));
    }


    private bool OutputChar()
    {
        if (_charQueue.Count <= 0)
        {
            nextPageIcon.SetActive(true);
            return false;
        }
        mainText.text += _charQueue.Dequeue();
        return true;
    }

    private void OutputAllChar()
    {
        StopCoroutine(ShowChars(captionSpeed));
        while (OutputChar()) ;
        _waitTime = 0;
        nextPageIcon.SetActive(true);
    }

    private IEnumerator ShowChars(float wait)
    {
        // OutputCharメソッドがfalseを返す(=キューが空になる)までループする
        while (OutputChar())
        // wait秒だけ待機
        yield return new WaitForSeconds(wait);
        // コルーチンを抜け出す
        yield break;
    }

    private Queue<string> SeparateString(string str, char sep)
    {
        string[] strs = str.Split(sep);
        Queue<string> queue = new Queue<string>();
        foreach (string l in strs) queue.Enqueue(l);
        return queue;
    }

    private void Init()
    {
        Result.SetActive(false);
        InputPanel.SetActive(false);
        PdataLoad();
        TextIndex();
        _text = LoadTextFile(textFile);
        _pageQueue = SeparateString(_text, SEPARATE_PAGE);
        ShowNextPage();
    }

    private bool ShowNextPage()
    {
        if (_pageQueue.Count <= 0) return false;
        // オブジェクトの表示/非表示を設定する
        nextPageIcon.SetActive(false);
        ReadLine(_pageQueue.Dequeue());
        return true;
    }

    private void OnClick()
    {
        if (_charQueue.Count > 0) OutputAllChar();
        else
        {
            if (!ShowNextPage()){
                GoToResult();
            }
            
           // UnityエディタのPlayモードを終了する
           //SceneManager.LoadScene("EatScene"); 
        }
    }

    private void SetBackgroundImage(string cmd, string parameter)
    {
        cmd = cmd.Replace(COMMAND_BACKGROUND, "");
        SetImage(cmd, parameter, backgroundImage);
    }

    private void ReadCommand(string cmdLine)
    {
        cmdLine = cmdLine.Remove(0, 1);
        Queue<string> cmdQueue = SeparateString(cmdLine, SEPARATE_COMMAND);
        foreach (string cmd in cmdQueue)
        {
            string[] cmds = cmd.Split(COMMAND_SEPARATE_PARAM);
            if (cmds[0].Contains(COMMAND_BACKGROUND))
                SetBackgroundImage(cmds[0], cmds[1]);
            if (cmds[0].Contains(COMMAND_FOREGROUND))
                SetForegroundImage(cmds[0], cmds[1]);
            if (cmds[0].Contains(COMMAND_CHARACTER_IMAGE))
                SetCharacterImage(cmds[1], cmds[0], cmds[2]);
            /*if (cmds[0].Contains(COMMAND_JUMP))
                JumpTo(cmds[1]);
            if (cmds[0].Contains(COMMAND_SELECT))
                SetSelectButton(cmds[1], cmds[0], cmds[2]);*/
            if (cmds[0].Contains(COMMAND_WAIT_TIME))
                SetWaitTime(cmds[1]);
            /*if (cmds[0].Contains(COMMAND_BGM))
                SetBackgroundMusic(cmds[0], cmds[1]);
            if (cmds[0].Contains(COMMAND_SE))
                SetSoundEffect(cmds[1], cmds[0], cmds[2]);*/
        }
    }

    private Sprite LoadSprite(string name)
    {
    return Instantiate(Resources.Load<Sprite>(spritesDirectory + name));
    }

    private Color ParameterToColor(string parameter)
    {
        string[] ps = parameter.Replace(" ", "").Split(',');
        if (ps.Length > 3)
            return new Color32(byte.Parse(ps[0]), byte.Parse(ps[1]),
                                            byte.Parse(ps[2]), byte.Parse(ps[3]));
        else
            return new Color32(byte.Parse(ps[0]), byte.Parse(ps[1]),
                                            byte.Parse(ps[2]), 255);
    }

    private void SetCharacterImage(string name, string cmd, string parameter)
    {
        cmd = cmd.Replace(COMMAND_CHARACTER_IMAGE, "");
        name = name.Substring(name.IndexOf('"') + 1, name.LastIndexOf('"') - name.IndexOf('"') - 1);
        Image image = _charaImageList.Find(n => n.name == name);
        if (image == null)
        {
            image = Instantiate(Resources.Load<Image>(prefabsDirectory + CHARACTER_IMAGE_PREFAB), characterImages.transform);
            image.name = name;
            _charaImageList.Add(image);
        }
        SetImage(cmd, parameter, image);
    }

    private Vector3 ParameterToVector3(string parameter)
    {
        string[] ps = parameter.Replace(" ", "").Split(',');
        return new Vector3(float.Parse(ps[0]), float.Parse(ps[1]), float.Parse(ps[2]));
    }

    private void SetImage(string cmd, string parameter, Image image)
    {
        cmd = cmd.Replace(" ", "");
        parameter = parameter.Substring(parameter.IndexOf('"') + 1, parameter.LastIndexOf('"') - parameter.IndexOf('"') - 1);
        switch (cmd)
        {
            case COMMAND_SPRITE:
                image.sprite = LoadSprite(parameter);
                break;
            case COMMAND_COLOR:
                image.color = ParameterToColor(parameter);
                break;
            case COMMAND_SIZE:
                image.GetComponent<RectTransform>().sizeDelta = ParameterToVector3(parameter);
                break;
            case COMMAND_POSITION:
                image.GetComponent<RectTransform>().anchoredPosition = ParameterToVector3(parameter);
                break;
            case COMMAND_ROTATION:
                image.GetComponent<RectTransform>().eulerAngles = ParameterToVector3(parameter);
                break;
            case COMMAND_ACTIVE:
                image.gameObject.SetActive(ParameterToBool(parameter));
                break;
            case COMMAND_DELETE:
                _charaImageList.Remove(image);
                Destroy(image.gameObject);
                break;
            case COMMAND_ANIM:
                ImageSetAnimation(image, parameter);
                break;
        }
    }

    private string LoadTextFile(string fname)
    {
        TextAsset textasset = Resources.Load<TextAsset>(fname);
        return textasset.text.Replace("\n", "").Replace("\r", "");
    }

    private bool ParameterToBool(string parameter)
    {
        string p = parameter.Replace(" ", "");
        return p.Equals("true") || p.Equals("TRUE");
    }

    private AnimationClip ParameterToAnimationClip(Image image, string[] parameters)
    {
        string[] ps = parameters[0].Replace(" ", "").Split(',');
        string path = animationsDirectory + SceneManager.GetActiveScene().name + "/" + image.name;
        AnimationClip prevAnimation = Resources.Load<AnimationClip>(path + "/" + ps[0]);
        Debug.Log(path+ "/" + ps[0]);
        AnimationClip animation = new AnimationClip();
        #if UNITY_EDITOR
            if (ps[3].Equals("Replay") && prevAnimation != null)
                return Instantiate(prevAnimation);
            Color startcolor = image.color;
            Vector3[] start = new Vector3[3];
            start[0] = image.GetComponent<RectTransform>().sizeDelta;
            start[1] = image.GetComponent<RectTransform>().anchoredPosition;
            Color endcolor = startcolor;
            if (parameters[1] != "") endcolor = ParameterToColor(parameters[1]);
            Vector3[] end = new Vector3[3];
            for (int i = 0; i < 2; i++)
            {
                if (parameters[i + 2] != "")
                    end[i] = ParameterToVector3(parameters[i + 2]);
                else end[i] = start[i];
            }
            AnimationCurve[,] curves = new AnimationCurve[4, 4];
            if (ps[3].Equals("EaseInOut"))
            {
                
                curves[0, 0] = AnimationCurve.EaseInOut(float.Parse(ps[1]), startcolor.r, float.Parse(ps[2]), endcolor.r);
                curves[0, 1] = AnimationCurve.EaseInOut(float.Parse(ps[1]), startcolor.g, float.Parse(ps[2]), endcolor.g);
                curves[0, 2] = AnimationCurve.EaseInOut(float.Parse(ps[1]), startcolor.b, float.Parse(ps[2]), endcolor.b);
                curves[0, 3] = AnimationCurve.EaseInOut(float.Parse(ps[1]), startcolor.a, float.Parse(ps[2]), endcolor.a);
                for (int i = 0; i < 2; i++)
                {
                    curves[i + 1, 0] = AnimationCurve.EaseInOut(float.Parse(ps[1]), start[i].x, float.Parse(ps[2]), end[i].x);
                    curves[i + 1, 1] = AnimationCurve.EaseInOut(float.Parse(ps[1]), start[i].y, float.Parse(ps[2]), end[i].y);
                    curves[i + 1, 2] = AnimationCurve.EaseInOut(float.Parse(ps[1]), start[i].z, float.Parse(ps[2]), end[i].z);
                }
                
            }
            else
            {
                curves[0, 0] = AnimationCurve.Linear(float.Parse(ps[1]), startcolor.r, float.Parse(ps[2]), endcolor.r);
                curves[0, 1] = AnimationCurve.Linear(float.Parse(ps[1]), startcolor.g, float.Parse(ps[2]), endcolor.g);
                curves[0, 2] = AnimationCurve.Linear(float.Parse(ps[1]), startcolor.b, float.Parse(ps[2]), endcolor.b);
                curves[0, 3] = AnimationCurve.Linear(float.Parse(ps[1]), startcolor.a, float.Parse(ps[2]), endcolor.a);
                for (int i = 0; i < 2; i++)
                {
                    curves[i + 1, 0] = AnimationCurve.Linear(float.Parse(ps[1]), start[i].x, float.Parse(ps[2]), end[i].x);
                    curves[i + 1, 1] = AnimationCurve.Linear(float.Parse(ps[1]), start[i].y, float.Parse(ps[2]), end[i].y);
                    curves[i + 1, 2] = AnimationCurve.Linear(float.Parse(ps[1]), start[i].z, float.Parse(ps[2]), end[i].z);
                }
            }
            string[] b1 = { "r", "g", "b", "a" };
            for (int i = 0; i < 4; i++)
            {
                AnimationUtility.SetEditorCurve(
                    animation,
                    EditorCurveBinding.FloatCurve("", typeof(Image), "m_Color." + b1[i]),
                    curves[0, i]
                );
            }
            string[] a = { "m_SizeDelta", "m_AnchoredPosition" };
            string[] b2 = { "x", "y", "z" };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    AnimationUtility.SetEditorCurve(
                        animation,
                        EditorCurveBinding.FloatCurve("", typeof(RectTransform), a[i] + "." + b2[j]),
                        curves[i + 1, j]
                    );
                }   
            }
            if (!Directory.Exists("Assets/Resources/" + path))
                Directory.CreateDirectory("Assets/Resources/" + path);
            AssetDatabase.CreateAsset(animation, "Assets/Resources/" + path + "/" + ps[0] + ".anim");
            AssetDatabase.ImportAsset("Assets/Resources/" + path + "/" + ps[0] + ".anim");
        #elif UNITY_STANDALONE
            animation = prevAnimation;
        #endif
        return Instantiate(animation);
    }

    private void ImageSetAnimation(Image image, string parameter)
    {
        Animator animator = image.GetComponent<Animator>();
        AnimationClip clip = ParameterToAnimationClip(image, parameter.Split(COMMAND_SEPARATE_ANIM));
        AnimatorOverrideController overrideController;
        if (animator.runtimeAnimatorController is AnimatorOverrideController)
            overrideController = (AnimatorOverrideController)animator.runtimeAnimatorController;
        else
        {
            overrideController = new AnimatorOverrideController();
            overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;
            animator.runtimeAnimatorController = overrideController;
        }
        overrideController[overrideAnimationClipName] = clip;
        animator.Update(0.0f);
        animator.Play(overrideAnimationClipName, 0);
    }

    private void SetWaitTime(string parameter)
    {
        parameter = parameter.Substring(parameter.IndexOf('"') + 1, parameter.LastIndexOf('"') - parameter.IndexOf('"') - 1);
        _waitTime = float.Parse(parameter);
    }

    /**
    * 次の読み込みを待機するコルーチン
    */
    private IEnumerator WaitForCommand()
    {
        yield return new WaitForSeconds(_waitTime);
        _waitTime = 0;
        ShowNextPage();
        yield break;
    }

    private void SetForegroundImage(string cmd, string parameter)
    {
        cmd = cmd.Replace(COMMAND_FOREGROUND, "");
        SetImage(cmd, parameter, foregroundImage);
    }

    private void PdataLoad(){
        Pdata = GameObject.Find("Player_Data");
        Debug.Log(Pdata.GetComponent<Player_Data>().PlayerData[1][8]);
    }

    private void TextIndex()    //イントロのシナリオを始めるかチェックする
    {
        int introcheck = int.Parse(Pdata.GetComponent<Player_Data>().PlayerData[1][8]);
        Debug.Log(introcheck);
        if(introcheck == 0){        //イントロシナリオにいく
            index = "1";
            TitleSet();
        }
        else if(introcheck == 1){        //イントロシナリオにいく
            index = "2";
            TitleSet();
            Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "2";
        }
        else if(introcheck == 49){
            index = "49";
            TitleSet();
        }
        else if(introcheck == 51){
            int a = Random.Range(1,3);//1~2
            Debug.Log(a);
            if(a==1){
                index = "50";
                Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "50";
            }
            else{
                index = "51";
            }
            TitleSet();
        }
        else if(introcheck == 52){
            index = "52";
            TitleSet();
        }
        else if(introcheck == 53){
            index = "53";
            TitleSet();
        }
        else if(introcheck == 54){
            index = "54";
            Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "2";
            TitleSet();
        }
        else{
            index = (ESManagement.Send()).ToString();
            TitleSet();
            answer = (ESManagement.SendAnswer()).ToString();
            index = index + "-" + answer;
        }
        textFile = ("Texts/Scenario" + index);
    }

    private void Start()
    {
        Init();
    }

    private void TitleSet(){
        GameObject EData = GameObject.Find("EventData");
        titleText.text = EData.GetComponent<Event_Data>().EventData[int.Parse(index)][1];
    }
 
    private void Update()
    {
        // 左(=0)クリックされたらOnClickメソッドを呼び出し
        if (Input.GetMouseButtonDown(0)) OnClick();
    }

    public void GoToResult()
    {
        if(index=="1"){     //イントロのイベント１が再生され終わったとき
            Pdata.GetComponent<Player_Data>().PlayerData[1][8] = "1";
            InputPanel.SetActive(true);//避難所名入力のインプットパネルを開く
        }
        else if(index == "2"){      //イベント２が終わったら選択画面に行く
            InputPanel.SetActive(true);//名前入力のインプットパネルを開く
        }
        else if((index == "49")||(index == "50")||(index=="51")||(index=="52")||(index=="53"))
        {
            SceneManager.LoadScene("EndingScene");
        }
        else{
            Result.SetActive(true);
        }
        //Main.SetActive(false);//Mainパネルを非表示
    }

    
}
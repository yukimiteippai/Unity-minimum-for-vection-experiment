using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Textを扱うために要追加
using UnityEngine.SceneManagement;//LoadSceneを使うために追加
using System.IO;//ファイルの書き出し関係


public class NewBehaviourScript : MonoBehaviour
{
    GameObject[] cubes;
    GameObject canvas;  //Ganvasオブジェクト取得用
    Text text1;         //Text1コンポーネント取得用
    TextIntensity ti;   //TextIntensityコンポーネント取得用

    string str1 = "動きを知覚している間\nスペースを押し続けてください\n（escで実験を強制終了）";
    string str2 = "上下の矢印で強度を入力しEnterキーを押す";

    static LogSave csv = null;

    float timeFirstPush = 0f;//最初の説明の後、ユーザがspaceキーを押しはじめた時間
    float timeFirstMove = 0f;//物体が移動を始める時間
    float timeAllPush = 0f;//spaceキーを押した総時間

    void Start()
    {
        if (csv == null) csv = new LogSave();//newされるとファイルが初期化されてしまうので避ける
        timeFirstPush = 0f;//時間を初期化しておく
        timeFirstMove = 0f;
        timeAllPush = 0f;

        cubes = new GameObject[500];
        float radi = 2.0f;
        float csize = 0.1f;

        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubes[i].name = "Cube" + i.ToString();
            float xx = Random.Range(-1f * radi, 1f * radi);
            float yy = Random.Range(-1f * radi, 1f * radi);
            //float zz = Random.Range(Camera.main.transform.position.z, 0f);
            //↑z=カメラのZ位置(-30にUnity上で設定)～0
            //簡易的に重なりを防ぐ：Z値の位置をずらす
            float dz = Camera.main.transform.position.z / cubes.Length;
            float zz = Random.Range(dz * i, dz * (i + 1) - csize);
            cubes[i].transform.position = new Vector3(xx, yy, zz);
            cubes[i].transform.localScale = new Vector3(csize, csize, csize);
            cubes[i].SetActive(false);
        }
        //Canvasを取得してcanvasに代入
        canvas = GameObject.Find("Canvas");
        //componentの取得↓子階層のComponentを取得する
        text1 = canvas.GetComponentInChildren<Text>();
        ti = canvas.GetComponentInChildren<TextIntensity>();
        //テキストの設定
        text1.text = str1;// "動きを知覚している間\nスペースを押し続けてください\n（escで実験を強制終了）";
        ti.setVisible(false);

        StartCoroutine(WaitProcess());
    }

    void Update()
    {
        //Enterキーが押されたら
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //textがstr2になったら(数値入力が終了したら)Enterを受け付ける
            if (text1.text == str2)
            {
                SceneManager.LoadScene(0);

                //時間の保存                
                float timeToRecognize = timeFirstPush - timeFirstMove;
                if (timeFirstPush == 0f) timeToRecognize = -1;//スペースキーを押さなかった場合
                csv.logSave("," + timeToRecognize + "," + timeAllPush + "," + ti.getScore());
                //csv.logSave("Enter Key");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeFirstPush = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            timeAllPush = Time.time - timeFirstPush;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //参考：https://web-dev.hatenablog.com/entry/unity/quit-game
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
                UnityEngine.Application.Quit();
            #endif
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            if (!cubes[i].activeSelf) continue;

            cubes[i].transform.Translate(0f, 0f, -0.1f);
            //cubeがカメラより後ろに行ったら、初期位置z=0に戻す
            Vector3 cube = cubes[i].transform.position;
            if (cube.z < Camera.main.transform.position.z)
            {
                cubes[i].transform.position = new Vector3(cube.x, cube.y, 0f);
            }
        }
    }


    IEnumerator WaitProcess()
    {
        //Debug.Log("指定した秒数だけ処理を待ちます");        
        yield return new WaitForSeconds(2f);

        Debug.Log("待ち終わり");
        for (int i = 0; i < cubes.Length; i++)
        {
            //cubesをActiveにする
            cubes[i].SetActive(true);
        }     
        //Canvasを非Activeにする
        canvas.SetActive(false);

        //動き始めの時間
        timeFirstMove = Time.time;

        //        
        float t = 2f;//cubesの移動時間
        yield return new WaitForSeconds(t);

        Debug.Log("cubesが移動した後の処理");
        for (int i = 0; i < cubes.Length; i++)
        {
            //cubesを非Activeにする
            cubes[i].SetActive(false);
        }
        //CanvasをActiveにする
        canvas.SetActive(true);
        //表示テキストを変える:
        text1.text = str2;// "上下の矢印で強度を入力しEnterキーを押す";
        //text1.fontSize = 30;

        //↑すでに非アクティブだとアクセスするだけでエラー？https://marunouchi-tech.i-studio.co.jp/2290/
        ti.setVisible(true);
    }
}



public class LogSave
{
    string filename;
    FileInfo fi;

    public LogSave()
    {
        System.DateTime thisDay = System.DateTime.Now;
        string day = //thisDay.Year.ToString() +
            thisDay.Month.ToString() +
            thisDay.Day.ToString() +
            "_" +
            thisDay.Hour.ToString() +
            thisDay.Minute.ToString();
        //filename = @"C:/Users/yuki/Documents/FileName" + day + ".csv";
        //↑このように絶対パスでフォルダ位置を指定することもできる
        filename = "FileName" + day + ".csv";
        //↑Assetsと同じフォルダに保存される。
        //ProjectウィンドウからAssetsを右クリックしShowInExplorerを選ぶとフォルダが開ける

        fi = new FileInfo(filename);
        //fi = new FileInfo(Application.dataPath + "../FileName.csv");

        using (var sw = new StreamWriter(
                fi.Create(),
                System.Text.Encoding.UTF8))
        {
            sw.WriteLine("," + "動き始めてからボタンを押すまでの時間" +
                            "," + "ボタンを押した総時間" +
                            "," + "主観的強度");
        }
    }

    public void logSave(string txt)
    {
        //作ったファイルに追加で書き込む場合はAppendText
        using (StreamWriter sw = fi.AppendText())
        {
            sw.WriteLine(txt);
        }
    }

}
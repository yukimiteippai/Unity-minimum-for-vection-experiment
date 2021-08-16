# 11. 実験結果のファイル出力
csvファイルで以下のような情報を書き出します（3回実験を繰り返した場合）。
|動き始めてからボタンを押すまでの時間	|ボタンを押した総時間	|主観的強度|
| ----: | ---: | ----: |
|0.2704341	|2.002032	|10|
|0.2536511	|2.00467	|20|
|0.2730904	|1.857029	|40|

以下は出来上がりのNewBehaiviourScript.csですが、前回と変化のない部分は`/*変化なし*/`と記載して省略しています。

```c#
/*変化なし*/
using System.IO;//ファイルの書き出し関係

public class NewBehaviourScript : MonoBehaviour
{
    /*変化なし*/

    static LogSave csv = null;//staticにすることは重要

    float timeFirstPush;//最初の説明の後、ユーザがspaceキーを押しはじめた時間
    float timeFirstMove;//物体が移動を始める時間
    float timeAllPush;//spaceキーを押した総時間

    void Start()
    {
        if (csv == null) csv = new LogSave();//毎回newされるとファイルが実験のたびに初期化されてしまうので避ける
        timeFirstPush = 0f;//時間を初期化しておく
        timeFirstMove = 0f;
        timeAllPush = 0f;

        cubes = new GameObject[500];
        /*変化なし*/
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
        /*変化なし*/
    }

    IEnumerator WaitProcess()
    {
        /*変化なし*/
        canvas.SetActive(false);

        //動き始めの時間
        timeFirstMove = Time.time;

        float t = 2f;//cubesの移動時間
        /*変化なし*/
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
```

### 解説1：csvの書き出し

まず、LogSaveクラスを新たに定義します。上記プログラムの下部。ここではまず、クラスと同じ名前のコンストラクタ（クラスをnewしたときに一度だけ行われる処理）であるLogSaveにて以下を行っています。
1. ファイル名に日時をいれるため、日時を取得
2. ファイル名（ファイルの場所を含む）を指定
3. ファイルの作成と書き込み。

また、実験が終わるたびに記録するため、logSave関数の処理を行います。  
コンストラクタと名前が紛らわしくてすみませんが、コンストラクタではありません（大文字と小文字は区別されます）。  
ここでは、作ったファイルに追加で文字列を書き込む処理を行っています。

次に、LogSaveクラスをNewBehaiviourScriptクラス内で以下のように定義します。
```
static LogSave csv = null;
```
**static**キーワードは静的メンバーを作成するときに使用します。静的メンバーとは、変数や、メソッド等を、インスタンス単位で生成するのではなく、アプリケーションにただ１つだけ生成したいときに使用します。  
参考：https://anderson02.com/cs/cskiso/cskisoniwari-25/  
つまり、実験を繰り返してもLogSaveのコンストラクタが毎回呼ばれないようにします。

しかし、newはここで定義とともに行うことができないので、Start関数内に書きます。
すると、実験を繰り返すときにStart関数が呼ばれるため、newされてしまいます。

そこで、**null**を代入しておき、Start内ではcsv変数がnullのときだけnewをするようにします。
newした後はnullではなくなるので、最初の実験の時だけnewされるようになります。

logSaveは一度の実験が終わりEnterを押されたときに呼ばれるようにします。
この時に保存したい情報をlogSaveに渡すことでファイルに記述されます。
```
csv.logSave("," + timeToRecognize + "," + timeAllPush + "," + ti.getScore());
```
time＊＊は全てfloatですが、そのまま文字列型のstringとして、ここでは渡すことができます。
**getScore**関数↓は新たにTextIntensity.csに加えましょう。
```
    public string getScore() { 
        return score.ToString();
    }
```
文字列は+で繋ぐことができます。  
ここではデータの間に”,”を入れることで、csvファイル内でのセルの区切りを表します。


### 解説2：その他
他に、time＊＊でベクションの実験で取得したいデータを取得しています。

これに伴い、スペースキーを押したかどうかなどをUpdate内に追記しています。  
時間の取得にはTime.timeというシーン開始から経過した時間を使っています。  
ついでにエスケープキーを押したら実験を終了するようにしています。  　

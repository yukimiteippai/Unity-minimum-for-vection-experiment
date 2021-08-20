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
        canvas.SetActive(true);

        //動き始めの時間
        timeFirstMove = Time.time;
        
        text1.text = str2;
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
```c#
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
```c#
csv.logSave("," + timeToRecognize + "," + timeAllPush + "," + ti.getScore());
```
time＊＊は全てfloatですが、そのまま文字列型のstringとして、ここでは渡すことができます。
**getScore**関数↓は新たにTextIntensity.csに加えましょう。
```c#
    public string getScore() { 
        return score.ToString();
    }
```
文字列は+で繋ぐことができます。  
ここではデータの間に”,”を入れることで、csvファイル内でのセルの区切りを表します。

#### できたCSVファイルはどこにある？
この例で言う「New Unity Project」フォルダ内にあります。
AssetsProjectウィンドウからAssetsを右クリックしShowInExplorerを選ぶとそのフォルダが開ける（Assetsフォルダと同じところにあるので）。


### 解説2：その他
他に、time＊＊でベクションの実験で取得したいデータを取得しています。

これに伴い、スペースキーを押したかどうかなどをUpdate内に追記しています。  
時間の取得にはTime.timeというシーン開始から経過した時間を使っています。  
ついでにエスケープキーを押したら実験を終了するようにしています。  　

### 最後に
最後に課題を課すなら、cubeではなくsphere（またはその他の形状など）にしてみる、複数の素材を使う、などして面白いシーンを作るとよいと思います。
この入門では扱いませんでしたが、テクスチャや法線を与えるnormal mapなどを使うのも面白いですし、TrailなどのGameObjectを使うのもベクション的に面白そうです。
お疲れさまでした。

最後におまけで、GameObjectの3D ObjectのQuadなどのMeshの頂点にアクセスして、スクリプトから以下のような形状を作成する関数を載せます。
※2個目の図は更につぶつぶのNormal mapを適用したものです。

![image](https://user-images.githubusercontent.com/5643842/130216816-b7bbf7d7-e6b6-4000-a55f-3ac0b6eb1f86.png)　![image](https://user-images.githubusercontent.com/5643842/130216925-a1af946f-4639-4f8c-8f4a-80e17e1e0f54.png)



#### 形状作成スクリプト
```c#
Mesh createMeshDynamic()
{
    int siz = 120;//一辺の頂点数
    float intv = 0.5f;//頂点の間隔
    Mesh mesh0 = new Mesh();
    Vector3[] newVertices = new Vector3[siz * siz];
    Vector2[] newUV = new Vector2[siz * siz];
    int[] newTriangles = new int[(siz ) * (siz ) * 2 * 3];//(siz - 1)*(siz - 1)=num of rect

    float r = 1f;//円の基本的な半径
    float psc = 15f;// gw.GetComponent<Gwave>().meshPSC;//perlin noiseのscaling
    float ph = 2f;// gw.GetComponent<Gwave>().meshBump;//凹凸具合
    for (int j = 0; j < siz; j++){
        for (int i = 0; i < siz; i++){
            {   
                int id = i + j * siz;
                float ang = (float)i / (siz-1);
                float pi = (float)i / (siz-1) * psc;
                float pj = (float)j / (siz-1) * psc;
                float rr = Mathf.PerlinNoise(pi, pj);
                    
                if (i >= siz-1 - 10 || i <= 10)
                {
                    pi = (float)Mathf.Abs(siz - 1 - i) / (siz - 1) * psc;                                                
                    rr = rr + Mathf.PerlinNoise(pi, pj) ;
                    rr /= 2f;                        
                }

                float x = Mathf.Cos(ang * Mathf.PI * 2f) * (r + rr * ph);
                float y = Mathf.Sin(ang * Mathf.PI * 2f) * (r + rr * ph);
                newVertices[id] = new Vector3(x, y, j * intv);
                newUV[id] = new Vector2((float)i / (siz-1), (float)j / (siz - 1));//normalize
            }        
            {
                int ip = i + 1;
                int jp = j + 1;
                if (ip >= siz ) continue;
                if (jp >= siz ) continue;
                int id = i + j * (siz );//id of rect
                newTriangles[id * 2 * 3 + 0] = i + j * siz;
                newTriangles[id * 2 * 3 + 1] = i + jp * siz;
                newTriangles[id * 2 * 3 + 2] = ip + jp * siz;
                newTriangles[id * 2 * 3 + 3] = ip + jp * siz;
                newTriangles[id * 2 * 3 + 4] = ip + j * siz;
                newTriangles[id * 2 * 3 + 5] = i + j * siz;
            }
        }
    }

    mesh0.vertices = newVertices;
    mesh0.uv = newUV;
    mesh0.triangles = newTriangles;

    mesh0.RecalculateNormals();
    mesh0.RecalculateBounds();

    GetComponent<MeshFilter>().sharedMesh = mesh0;
    GetComponent<MeshFilter>().sharedMesh.name = "myMesh";

    return mesh0;
}
```

#### ガラスっぽいマテリアル
おまけでガラスっぽいマテリアル[glass_002.mat](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/glass_002.mat)を共有しておきます。



### 目次とリンク
- [unity1:Unityのダウンロード、ウィンドウ](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity1.md)
- [unity2:GameObjectの作成と移動](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity2.md)
- [unity3:材質の指定、スカイボックス](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity3.md)
- [unity4:UI](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity4.md)
- [unity5:実験の繰り返し](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity5.md)
- [unity6:ファイル出力](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity6.md)(★here)

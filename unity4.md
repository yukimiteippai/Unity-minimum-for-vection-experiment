# 9. UI：テキストやボタン

## 9.1 テキストを表示する

Hierarchyの＋ボタンから、　
> UI > Text

を選びます。

Canvasの子としてTextが作られ、EventSystemも同時に作成されます。  
（親子関係について：https://dkrevel.com/makegame-beginner/parent/）  
図の例ではText1にリネームしています↓

![image](https://user-images.githubusercontent.com/5643842/128685175-b275b8e3-e28e-4cbd-a5a5-c78024db61c2.png)


InspectorのRect TransformやTextで、テキストボックスのサイズやテキストの編集ができます  
（詳しくはこちらを参照：https://www.sejuku.net/blog/55029）。

![image](https://user-images.githubusercontent.com/5643842/128685190-723932c4-64f8-4829-98a5-912314094465.png) ![image](https://user-images.githubusercontent.com/5643842/128685206-722fe746-a078-4e93-a65a-6232ddcffd03.png)




調整したテキストの例です↓

![image](https://user-images.githubusercontent.com/5643842/128685225-06c22a84-10e5-469c-8bbf-b7fd5b3127ad.png)




## 9.2 テキストを表示と実験のタイミング制御

背景のグラフィックのせいで文字が見づらいので
> UI>Image

を文字の背景として作成し、HierarchyウィンドウのCanvas内にText1と同じ階層に配置します（drag & dropで移動できます）。  
このとき、Hierarchyウィンドウで見てImageをTextより上に置くことで、画面上の前後関係でImageを奥にします。  
※ImageとTextは2Dのオブジェクトなので、InspectorのRect TransformでPos Zを変更しても奥行に反映されないようです。

Canvas内にImageとTextを両方配置すると、CanvasのInspectorのチェック（↓の右図）を外したときに、ImageもTextも同時に非表示になります（親子関係による）。


![image](https://user-images.githubusercontent.com/5643842/128685294-2fa89ec8-0a2b-416d-84c9-c54eda7ef08e.png) ![image](https://user-images.githubusercontent.com/5643842/128685315-c3f2cfda-30fe-42a7-a52b-7bd5b977cde0.png)



### 9.2.1 開始のタイミング
背景とテキストを、実験開始後数秒だけ表示したあと非表示にし、その後、cubesの移動を開始するようにプログラムしていきましょう。

待ち時間の参考：https://ekulabo.com/coroutine-wait

上記のページを参考に作成したプログラムが以下になります。
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject[] cubes;

    void Start()
    {
        cubes = new GameObject[500];
        float radi = 2.0f;
        float csize = 0.1f;

        for (int i = 0; i < cubes.Length; i++)//cubes.Lengthはcubesの個数
        {
            cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubes[i].name = "Cube" + i.ToString();
            float xx = Random.Range(-1f * radi, 1f * radi);
            float yy = Random.Range(-1f * radi, 1f * radi);
            //float zz = Random.Range(Camera.main.transform.position.z, 0f);
            //↑z=カメラのZ位置(-30にUnity上で設定)～0
            //↓簡易的に重なりを防ぐ：Z値の位置をずらす
            float dz = Camera.main.transform.position.z / cubes.Length;
            float zz = Random.Range(dz * i, dz * (i + 1) - csize);
            cubes[i].transform.position = new Vector3(xx, yy, zz);
            cubes[i].transform.localScale = new Vector3(csize, csize, csize);
            cubes[i].SetActive(false);//追加3
        }
        StartCoroutine(WaitProcess());//追加1
    }

    void Update() {}

    void FixedUpdate()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            if (!cubes[i].activeSelf) continue; //追加4

            cubes[i].transform.Translate(0f, 0f, -0.1f);
            //cubeがカメラより後ろに行ったら、初期位置z=0に戻す
            Vector3 cube = cubes[i].transform.position;
            if (cube.z < Camera.main.transform.position.z)
            {
                cubes[i].transform.position = new Vector3(cube.x, cube.y, 0f);
            }
        }
    }

    IEnumerator WaitProcess()//追加2
    {        
        Debug.Log("指定した秒数だけ処理を待ちます");        
        yield return new WaitForSeconds(3f);    
        
        Debug.Log("待ち終わり");
        for (int i = 0; i < cubes.Length; i++)
        {
            //cubesをActiveにする
            cubes[i].SetActive(true);           
        }
        //Canvasを非Activeにする
        GameObject canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);

        //Update系は待っている途中も呼び出される
        //Start関数の処理が終わった後WaitProcessが呼ばれるぽいので
        //SetActive(true)はWaitProcess内に記述する
    }
}
```

**追加1.**  
開始直後に停止したいので、以下をStart関数内に加えています。
```
StartCoroutine(WaitProcess());      
```
ちなみに最初からHierarchyウィンドウにあるオブジェクトはこのスクリプトが処理される時点で既に生成されていますので、説明テキストなどはこの時点で存在しています。

**追加2.**  
更に、WaitProcess関数を作成します。
待った後に、cubesの移動を開始するため、SetActiveでアクティブにしています。

また、逆にテキストは非表示にするため、同じくSetActiveで非アクティブにしています。  
スクリプト内で別のGameObjectを取得する方法の参考：https://marunouchi-tech.i-studio.co.jp/2266/


**追加3.**  
追加2に関連してcubesを最初に作成した際には非アクティブにしておきます。
これでプログラム開始時にはcubesは非アクティブ、テキスト表示数秒後にはアクティブになります。

**追加4.**  
追加2,3に関連して、アクティブなcubeのみ移動を適用するため、アクティブかどうか判定し、もしアクティブでなければcontinueで移動の処理をスキップしています。
もしcubeが非アクティブでも移動処理をすると、非表示のまま位置は更新されていまいますので、このように処理を避ける必要があります。


### 9.2.2 スクリプトから文字を変更する
今回の実験では、一定時間cubesを移動させた後、ベクションの強度を矢印キーで入力します。このとき、説明のためのテキストも表示します。最初に表示したテキストを変更し、再度表示してみましょう。
まず、cubesの移動時間をt秒として、先ほど同様WaitForSecondsでcubesの移動時間だけ待ちます。

```
IEnumerator WaitProcess()
{        
    yield return new WaitForSeconds(2f);    
        
    Debug.Log("待ち終わり");
    for (int i = 0; i < cubes.Length; i++)
    {
        //cubesをActiveにする
        cubes[i].SetActive(true);           
    }
    //Canvasを非Activeにする
    canvas.SetActive(false);

    //以下が追加部分
    float t = 2f;//cubesの移動時間
    yield return new WaitForSeconds(t);

    Debug.Log("cubesが移動した後の処理");        
    for (int i = 0; i < cubes.Length; i++)
    {
        //cubesを非Activeにする
        cubes[i].SetActive(false);
    }
    //CanvasをActiveにする　※以前はここでcanvasを定義していたがclassの最初にメンバとして定義に変更。findはStartで。
    canvas.SetActive(true);
    //表示テキストを変える　※
    //text1.text = "上下の矢印で強度を入力しEnterキーを押す";
    //ti.setVisible(true);
}
```

WaitProcessを上記のように、プログラムの冒頭を下記のように変えると、t秒後に最初と同じテキストが表示されます。WaitProcessの最後のコメントアウトを取ると最後に表示されるテキストが変わります。

```
public class NewBehaviourScript : MonoBehaviour
{
    GameObject[] cubes;
    GameObject canvas;  //Ganvasオブジェクト取得用
    Text text1;         //Text1コンポーネント取得用
    TextIntensity ti;   //TextIntensityコンポーネント取得用

    void Start()
    {
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
        text1.text = "動きを知覚している間スペースを押し続けてください\n（escで実験を強制終了）";
        ti.setVisible(false);

        StartCoroutine(WaitProcess());      
    }
```





### 9.2.3 数値入力
Hierarchyウィンドウから再び
> UI＞Text
を選びTextIntensityにリネームして追加します。  
Inspectorからスクリプトコンポーネントを追加します。スクリプトのファイル名はTextIntensityにしました。  
内容は以下になります。

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIntensity : MonoBehaviour
{
    private int score;//点数を格納する整数の変数

    void Start()
    {
        score = 0;//点数を0で初期化
        setVisible(false);//文字の色を透明色で初期化
    }

    void Update()
    {
        //矢印キーによる操作
        if (Input.GetKeyDown(KeyCode.UpArrow))
            score += 10;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            score -= 10;
        if (score < 0) score = 0;
        if (score > 100) score = 100;
        GetComponent<Text>().text = score.ToString();
        //↑点数を文字列にしてオブジェクトのTextに指定
    }

    public void setVisible(bool bb) {
        if (bb)
            gameObject.GetComponent<Text>().color = new Color(0, 0, 0, 1);
        else 
            gameObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);
    }
}
```

setVisibleではbbがtrueの場合テキストを黒、falseの場合透明にします。
非表示化は2つ方法があり、SetActiveを用いる方法のほかに、今回のように色を透明にする方法があります。
それぞれ長所短所があるので詳しい解説は以下を見てください。

参考：https://techno-monkey.hateblo.jp/entry/2018/05/09/120653

setActiveを使った場合、一度非アクティブにしてしまうと以降のコンポーネントへのアクセスがエラーになってしまうので、ここでは透明色を使いました。
最後はこうなるはずです↓



https://user-images.githubusercontent.com/5643842/128694242-70c18e4c-e314-4296-84ca-46a131642e00.mp4








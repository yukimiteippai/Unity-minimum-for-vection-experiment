# 10. 実験を繰り返す
## 10.1 SceneManager.LoadScene

Enterキー（Returnキー）を押したら実験を繰り返すように変更しましょう。
NewBehaiviourScript.cs (Emptyのスクリプトコンポーネント)を変更します。

1. 冒頭に以下を追加します。

> using UnityEngine.SceneManagement;

2. クラス冒頭に説明文のstring変数（str1, str2）を定義します。
StartおよびWaitProcess関数で代入していた説明文を上記の変数で置き換えます。

3. UpdateにてEnterキーが押され、かつtextがstr2になっていたら、LoadSceneでシーンをロードします。

参考：http://negi-lab.blog.jp/archives/12908675.html

※詳細は他のページがよいかもしれません。照明の再設定は次の節で述べます。

変更したコードが以下です。

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Textを扱うために要追加
using UnityEngine.SceneManagement;//LoadSceneを使うために追加

public class NewBehaviourScript : MonoBehaviour
{
    GameObject[] cubes;
    GameObject canvas;  //Ganvasオブジェクト取得用
    Text text1;         //Text1コンポーネント取得用
    TextIntensity ti;   //TextIntensityコンポーネント取得用

    string str1 = "動きを知覚している間\nスペースを押し続けてください\n（escで実験を強制終了）";
    string str2 = "上下の矢印で強度を入力しEnterキーを押す";

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

    void Update() {
        //Enterキーが押されたら
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //textがstr2になったら(数値入力が終了したら)Enterを受け付ける
            if (text1.text == str2)
            {
                Debug.Log("Enter key");
                SceneManager.LoadScene(0);
            }
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
        ti.setVisible(true);
    }
}
```

## 10.2 Lightingの修正
SceneManager.LoadSceneでシーンをロードした場合、lightingがおかしくなります。
Lightingウィンドウを開き、設定を変更しましょう。

**Lighitngウィンドウ**：https://docs.unity3d.com/ja/2018.4/Manual/GlobalIllumination.html
**ライトマッピング**：https://docs.unity3d.com/ja/2018.4/Manual/Lightmapping.html

照明の計算を毎フレーム行うのは計算コストが高いです。
ライトマッピングは、シーンのサーフェスの明るさを事前計算し、あとで使用することで計算コストを節約しているようですが、LoadSceneを使うとき、ライトマップを再計算しないとおかしな照明になってしまいます。

> メニュー＞Window＞Rendering＞Lighting　を選びます。

Lightingタブを見てください。
このままだとsettingは変えられないので、Sceneタブ上部にある*New Lighting Settings*を選ぶと、新しい設定ができるようになります。
Lightingタブの下部にあるWorkflow Settingsの*Auto Generate*をチェックすると、ライトマップを更新することができます。




https://user-images.githubusercontent.com/5643842/129184559-8aa14f90-0c2b-4027-ab05-6070e8307ae3.mp4




# ６．GameObjectの作成と移動

## 6.1 Hierarchyウィンドウ・GameObjectメニューから作る方法
> Hierarchyウィンドウの左上の＋ボタンから3Dobject→cubeと選ぶ

と1x1x1（単位はm）のサイズの立方体が作成されます。  
Effectのparticleなども宇宙的なビジュアルを作るのに良いかもしれません。  
参考：https://ekulabo.com/game-object-effects  
UIのテキストやボタンも実験で使えそうですね（後述）。  
(GameObjectメニューからの作成方法も同様です)



## プログラムで作成する方法
### 1. 空のGameObjectの生成
GameObjectを大量に生成したり、動的に（状況によって数や位置やサイズを変えるなど）作成したい場合は、プログラムで作成する方法が便利です。
Cubeを大量に作成するスクリプト（プログラムのコードのこと）を作成しましょう。
スクリプトはGameObjectのComponentとして付与するので、既に存在するcubeに関するスクリプトならば、それにComponentをaddするのですが何もない状態からプログラムで作成する場合はどうするのでしょうか？
> Hierarchyの＋ボタンから、「Create Empty」を選ぶ

と空のGameObjectが作成されます。
これを使っていきます。  
参考：https://biotech-lab.org/articles/4241

Create EmptyするとデフォルトでGameObjectという名前でgame objectが作成されます。  
ここでは名前をEmptyとしておきます。  
Emptyを選択したときのInspectorは図のようになります。

![image](https://user-images.githubusercontent.com/5643842/127962105-537f0c67-db8e-4101-806a-86cadd604a76.png)

### 2. 空のGameObjectにスクリプトコンポーネントを追加する
> Inspectorの下部にある「Add Component」ボタンを押して、
> new script→create and add　と選択してください。

Create and addのとき、スクリプト名を編集できます。  
デフォルトではNewBehaviorScriptになります。  
ここではデフォルトの名前のまま進みます。


### 3. スクリプトを開く
作られたスクリプトは右図のようにInspectorに加えられます。  
> スクリプト名の右端にある「…」をクリックし、edit scriptを選ぶとスクリプトが開かれます。

![image](https://user-images.githubusercontent.com/5643842/127962153-38641c92-4778-471c-9b6f-1bf45933b593.png)

このとき、想定していたエディタと異なるものが開く場合は
> Edit>Preferences>External Tool>External Script Editor

より好みのエディタを選びます。
開くエディタはVisual Studio Community 2019(VSC19)がデフォルトみたいです。
Unityのインストールの時に、デフォルトでダウンロードされるようになっていたと思います。
C＃言語がUnityのデフォルト言語として想定されています。
他の言語でも可能みたいですが、本資料はC＃で行います。
C＃が編集でき、付随して必要となるライブラリなどの導入に問題がなければ、エディタも必ずしも上記のVisual Studioを使用する必要はないと思いますが、デフォルトのものが便利そうです。　　
※ここでedit scriptでスクリプトを開いた場合、追加で必要となるライブラリがVSC19では右上のところにお知らせされ、「インストール」というリンクをクリックするだけで導入することができます。


### 4. スクリプトの編集「cubeの作成」
以下のスクリプトからcubeを作成します。

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
``` 

Emptyオブジェクトに適用するプログラム（class）の中身を記述していく形となります。  
**Start関数**にはこのオブジェクトが生成されたときに一度だけ行う処理  
（例えばこのオブジェクトの設定等）、  
**Update関数**にはその後、1秒間に何十回も繰り返す処理  
（例えばこのオブジェクトの移動等）  
を記述します。

Emptyオブジェクトは空なので、まずはStart関数でGameObjectを生成してみます。  
Start関数に以下を記述し、プログラムを保存してください（short cut key = Ctrl+s）。  
```c#
GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
``` 

次に、Unity画面の上部中央にある再生ボタンを押しましょう。  
(short cut key = Ctrl+p)

![image](https://user-images.githubusercontent.com/5643842/127962384-78a8a36e-d63c-4912-851d-b8b12c8bbf10.png)

Sceneウィンドウにあるxyzの軸だけで何もないのがEmptyです。  
Gameウィンドウでは最初にHierarchyから追加したcubeのほかにもう一つ、スクリプトで作成されたcubeが表示されます(図)。  
※Sceneウィンドウには、動的に生成されるオブジェクトは表示されないので注意。  

右図の左は非実行時、右は実行時のHierarchyを示しています。実行時にCubeがもう一つ作成されています。

### 5. スクリプトの編集「cubeの設定」
作成したcubeをプログラムから設定します。
なにがプログラムできるかは、Unity上でEmptyのInspectorにあるもの、と考えるとわかりやすいです（が、色々な設定の仕方があります）。
```c#
void Start()
{
    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    cube.name = "Cube2";
    cube.transform.position = new Vector3(0,2,0);
    cube.transform.localScale = new Vector3(1, 2, 1);
} 
```
上記を実行すると、図のようになります。  
※Vector3はxyzの3つのfloatを持つクラス（データ型のようなもの）です。

![image](https://user-images.githubusercontent.com/5643842/127962647-6ce350b0-ff27-4c20-b10b-8029c54f7f19.png)


### 6. スクリプトの編集「cubeの移動」
![image](https://user-images.githubusercontent.com/5643842/127962705-b930bd7f-4dfc-49c1-9a0e-8b5ce08af3a7.png)![image](https://user-images.githubusercontent.com/5643842/127962709-9ab81feb-8cad-4816-ac81-7f150e8958a5.png)![image](https://user-images.githubusercontent.com/5643842/127962717-e3463797-6661-4e0a-95a2-2bbefc4f2315.png)![image](https://user-images.githubusercontent.com/5643842/127962721-0c5be028-ac16-4705-a7e7-0d7df7a29be5.png)




プログラムを以下のように変更して、オブジェクトがこちらに近づいて来るようにします。
```c#
public class NewBehaviourScript : MonoBehaviour
{
    GameObject cube;
//↑cubeの定義。Start内からこちらに移動してUpdateなどでも使えるようにする
   
    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "Cube2";
        cube.transform.position = new Vector3(0,2,0);
        cube.transform.localScale = new Vector3(1, 2, 1);
    }

    // Update is called once per frame
    void Update(){}

    void FixedUpdate()//少しずつ移動するときはFixedUpdateの方を使う[参考]
    {
            cube.transform.Translate(0f, 0f, -0.1f);//cubeの平行移動
     //       Debug.Log("test");//コンソールへの文字の出力
    }
}
```
参考：下記のページでは矢印キーでオブジェクトを動かすプログラムが記載されています。
https://squmarigames.com/2018/11/08/unity-beginner-translate/#toc2
 



### 7. スクリプトの編集「オブジェクトを増やす」
![image](https://user-images.githubusercontent.com/5643842/127962774-1d08afe0-428d-4797-a6dc-e4d7c1d9ee8a.png)![image](https://user-images.githubusercontent.com/5643842/127962780-5ed88671-5eb4-48ae-9691-00addf482b28.png)![image](https://user-images.githubusercontent.com/5643842/127962787-471dd94d-5686-41ed-bfa7-6dbc369c1814.png)![image](https://user-images.githubusercontent.com/5643842/127962791-62f0c7fb-fa7f-402f-8c88-03c744b72e84.png)![image](https://user-images.githubusercontent.com/5643842/127962797-a88d8492-57a6-48e9-aa8c-d891acc17b70.png)





配列を使ってたくさんオブジェクトを作ってみましょう。
配列はデータを一度に大量に作成して、格納しておくことができます。
ここでは配列やfor分の基礎は知っているという前提で進めます。
先ほどまでのプログラムをベースに変更していきます。

```c#
GameObject[] cubes;
//↑カーソルをcubeに合わせてCtrl+rでcubeをcubesに変更。これは単純にわかりやすくするため。右クリックで名前の変更を選んでも一緒。そして配列に変更しています。

void Start()
{
    cubes = new GameObject[100];
    float radi = 2.0f;//範囲の最大値

    for (int i = 0; i < cubes.Length; i++) {
    //↑cubes.Lengthには配列のサイズ、100が入っている
        cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubes[i].name = "Cube"+i.ToString();
        //↑下の図みたいな名前にする。ToString()は数字を文字に変換。
        float xx = Random.Range(-1f * radi, 1f * radi);
        float yy = Random.Range(-1f * radi, 1f * radi);
        //↑最大値と最小値の範囲で乱数を作成
        cubes[i].transform.position = new Vector3(xx, yy, 0f);
        cubes[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
void Update(){}

void FixedUpdate()
{
    for (int i = 0; i < cubes.Length; i++)
    {
        cubes[i].transform.Translate(0f, 0f, -0.1f);
    }//100個のcubeを視点の方向に動かす
}
```

だいぶベクション実験っぽくなってきました。
図は実行後のHierarchyウィンドウです。

![image](https://user-images.githubusercontent.com/5643842/127962892-9a39b6e0-cb53-4ad1-aee7-80a8549f113f.png)


### 8. スクリプトの編集「絶え間なくオブジェクトを流す」
![image](https://user-images.githubusercontent.com/5643842/127971676-f126f7bf-050e-45f2-800d-59ce826445b1.png)![image](https://user-images.githubusercontent.com/5643842/127971683-c1dd3f40-62f5-49e7-add4-ad9384f7192d.png)![image](https://user-images.githubusercontent.com/5643842/127971687-f81a67ef-28b1-462a-a661-22b734e348f3.png)

視界から消えたオブジェクトを繰り返し初期位置に戻して動かすようにしましょう。
プログラム全体は6.5の最後に記載しています。

8.1. カメラの位置を調整する。図と同じに設定しましょう。  

![image](https://user-images.githubusercontent.com/5643842/127971711-adac0c91-a879-4081-bc8d-1bcd680cb9ae.png)

 
8.2. cubesのZ値をばらつかせる（カメラのZ位置から0までの間でランダムに）  
一つ一つのcubeの位置を乱数で以下のように決めるとz値をばらつかせることができます。
```c#
float zz = Random.Range(Camera.main.transform.position.z, 0f);
```
ただ、これだとcube同士が重なることがあります。
簡易的にそれを回避するには、x,y,zのどれかだけでも重ならないように以下のように区切った範囲内で乱数を生成するとよいです。
```c#
float dz = Camera.main.transform.position.z / cubes.Length;
float zz = Random.Range(dz * i, dz * (i + 1) - csize);
```
Camera.main.transform.position.zはMain Cameraのz位置です。
スクリプトから外部のGameObjectへのアクセスは通常は少し面倒ですが、Main Cameraはこのように簡単にアクセスできるようになっています。
この辺の少し詳しいことは以下のリンクを参照するとよいです。他のGameObjectへのアクセス方法がわかります。  
参考：https://tech.pjin.jp/blog/2018/01/31/unity_get-main-camera/


8.3. cubesがカメラより後ろに行ったら、位置をz=0に戻す  
これは単純にFixedUpdate()内でそのように処理するだけです。  
注意としては、x,yの値は変えないようにすることです。

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject[] cubes;

    // Start is called before the first frame update
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
        }
    }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].transform.Translate(0f, 0f, -0.1f);
            //cubeがカメラより後ろに行ったら、初期位置z=0に戻す
            Vector3 cube = cubes[i].transform.position;
            if (cube.z < Camera.main.transform.position.z)
            {
                cubes[i].transform.position = new Vector3(cube.x, cube.y, 0f);
            }
        }
    }
}
```



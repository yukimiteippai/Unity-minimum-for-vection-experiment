# ６．GameObjectの作成と移動

## 6.1 Hierarchyウィンドウ・GameObjectメニューから作る方法
Hierarchyウィンドウの左上の＋ボタンから3Dobject→cubeと選ぶと1x1x1（単位はm）のサイズの立方体が作成されます。
Effectのparticleなども宇宙的なビジュアルを作るのに良いかもしれません。
参考：https://ekulabo.com/game-object-effects
UIのテキストやボタンも実験で使えそうですね（後述）。
(GameObjectメニューからの作成方法も同様です)



## 6.2 プログラムで作成する方法
GameObjectを大量に生成したり、動的に（状況によって数や位置やサイズを変えるなど）作成したい場合は、プログラムで作成する方法が便利です。
Cubeを大量に作成するスクリプト（プログラムのコードのこと）を作成しましょう。
スクリプトはGameObjectのComponentとして付与するので、既に存在するcubeに関するスクリプトならば、それにComponentをaddするのですが何もない状態からプログラムで作成する場合はどうするのでしょうか？
Hierarchyの＋ボタンから、「Create Empty」を選ぶと空のGameObjectが作成されます。
これを使っていきます。
参考：https://biotech-lab.org/articles/4241

### 6.2.1. Create EmptyするとデフォルトでGameObjectという名前でgame objectが作成されます。ここでは名前をEmptyとしておきます。
Emptyを選択したときのInspectorは図のようになります。

![image](https://user-images.githubusercontent.com/5643842/127962105-537f0c67-db8e-4101-806a-86cadd604a76.png)

### 6.2.2. Inspectorの下部にある「Add Component」ボタンを押して、
new script→create and add　と選択してください。
Create and addのとき、スクリプト名を編集できます。デフォルトではNewBehaviorScriptになります。ここではデフォルトの名前のまま進みます。


### 6.2.3. 作られたスクリプトは右図のようにInspectorに加えられます。スクリプト名の右端にある「…」をクリックし、edit scriptを選ぶとスクリプトが開かれます。

![image](https://user-images.githubusercontent.com/5643842/127962153-38641c92-4778-471c-9b6f-1bf45933b593.png)

このとき、想定していたエディタと異なるものが開く場合は
Edit>Preferences>External Tool>External Script Editor
より好みのエディタを選びます。
開くエディタはVisual Studio Community 2019(VSC19)がデフォルトみたいです。
Unityのインストールの時に、デフォルトでダウンロードされるようになっていたと思います。
C＃言語がUnityのデフォルト言語として想定されています。
他の言語でも可能みたいですが、本資料はC＃で行います。
C＃が編集でき、付随して必要となるライブラリなどの導入に問題がなければ、エディタも必ずしも上記のVisual Studioを使用する必要はないと思いますが、デフォルトのものが便利そうです。
※ここでedit scriptでスクリプトを開いた場合、追加で必要となるライブラリがVSC19では右上のところにお知らせされ、「インストール」というリンクをクリックするだけで導入することができます。


### 6.2.4. 以下のスクリプトからcubeを作成します。

``` 
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
Start関数にはこのオブジェクトが生成されたときに一度だけ行う処理  
（例えばこのオブジェクトの設定等）、  
Update関数にはその後、1秒間に何十回も繰り返す処理  
（例えばこのオブジェクトの移動等）  
を記述します。

Emptyオブジェクトは空なので、まずはStart関数でGameObjectを生成してみます。  
Start関数に以下を記述し、プログラムを保存してください（short cut key = Ctrl+s）。  
``` GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);``` 

次に、Unity画面の上部中央にある再生ボタンを押しましょう。
(short cut key = Ctrl+p)

![image](https://user-images.githubusercontent.com/5643842/127962384-78a8a36e-d63c-4912-851d-b8b12c8bbf10.png)

Sceneウィンドウにあるxyzの軸だけで何もないのがEmptyです。  
Gameウィンドウでは最初にHierarchyから追加したcubeのほかにもう一つ、スクリプトで作成されたcubeが表示されます(図)。  
※Sceneウィンドウには、動的に生成されるオブジェクトは表示されないので注意。  

右図の左は非実行時、右は実行時のHierarchyを示しています。実行時にCubeがもう一つ作成されています。

### 6.2.5. 作成したcubeをプログラムから設定します。
なにがプログラムできるかは、Unity上でEmptyのInspectorにあるもの、と考えるとわかりやすいです（が、色々な設定の仕方があります）。
```
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


## 6.3 移動
![image](https://user-images.githubusercontent.com/5643842/127962705-b930bd7f-4dfc-49c1-9a0e-8b5ce08af3a7.png)![image](https://user-images.githubusercontent.com/5643842/127962709-9ab81feb-8cad-4816-ac81-7f150e8958a5.png)![image](https://user-images.githubusercontent.com/5643842/127962717-e3463797-6661-4e0a-95a2-2bbefc4f2315.png)![image](https://user-images.githubusercontent.com/5643842/127962721-0c5be028-ac16-4705-a7e7-0d7df7a29be5.png)




プログラムを以下のように変更して、オブジェクトがこちらに近づいて来るようにします。
```
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
 



## 6.4 オブジェクトを増やす
![image](https://user-images.githubusercontent.com/5643842/127962774-1d08afe0-428d-4797-a6dc-e4d7c1d9ee8a.png)![image](https://user-images.githubusercontent.com/5643842/127962780-5ed88671-5eb4-48ae-9691-00addf482b28.png)![image](https://user-images.githubusercontent.com/5643842/127962787-471dd94d-5686-41ed-bfa7-6dbc369c1814.png)![image](https://user-images.githubusercontent.com/5643842/127962791-62f0c7fb-fa7f-402f-8c88-03c744b72e84.png)![image](https://user-images.githubusercontent.com/5643842/127962797-a88d8492-57a6-48e9-aa8c-d891acc17b70.png)





配列を使ってたくさんオブジェクトを作ってみましょう。
配列はデータを一度に大量に作成して、格納しておくことができます。
ここでは配列やfor分の基礎は知っているという前提で進めます。
先ほどまでのプログラムをベースに変更していきます。

```
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

![image](https://user-images.githubusercontent.com/5643842/127962892-9a39b6e0-cb53-4ad1-aee7-80a8549f113f.png)



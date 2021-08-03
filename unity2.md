# ６．GameObjectの作成と移動

6.1 Hierarchyウィンドウ・GameObjectメニューから作る方法
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

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

![d](https://user-images.githubusercontent.com/5643842/127961569-0d97cea2-cb62-4eed-9d8b-dbac92143062.png)


### 6.2.2. Inspectorの下部にある「Add Component」ボタンを押して、
new script→create and add　と選択してください。
Create and addのとき、スクリプト名を編集できます。デフォルトではNewBehaviorScriptになります。ここではデフォルトの名前のまま進みます。


### 6.2.3. 作られたスクリプトは右図のようにInspectorに加えられます。スクリプト名の右端にある「…」をクリックし、edit scriptを選ぶとスクリプトが開かれます。

![e](https://user-images.githubusercontent.com/5643842/127961585-927edb80-3933-4d0d-a898-f16ead4f28a5.png)


このとき、想定していたエディタと異なるものが開く場合は
Edit>Preferences>External Tool>External Script Editor
より好みのエディタを選びます。
開くエディタはVisual Studio Community 2019(VSC19)がデフォルトみたいです。
Unityのインストールの時に、デフォルトでダウンロードされるようになっていたと思います。
C＃言語がUnityのデフォルト言語として想定されています。
他の言語でも可能みたいですが、本資料はC＃で行います。
C＃が編集でき、付随して必要となるライブラリなどの導入に問題がなければ、エディタも必ずしも上記のVisual Studioを使用する必要はないと思いますが、デフォルトのものが便利そうです。
※ここでedit scriptでスクリプトを開いた場合、追加で必要となるライブラリがVSC19では右上のところにお知らせされ、「インストール」というリンクをクリックするだけで導入することができます。

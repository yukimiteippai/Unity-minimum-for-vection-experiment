# １．Unity hubのダウンロード
Unity Hubをインストールしてください。
https://unity3d.com/get-unity/download

※Unity hubのインストールがうまくいかない場合、旧バージョンをインストール後、更新する方法が有効でした。以下では非公式に旧バージョンを入手できます。
https://detail.chiebukuro.yahoo.co.jp/qa/question_detail/q13228982998

Unity hubのダウンロードページの右上に以下のシステム要件が書いてあります。

> System requirements
> OS: Windows 7 SP1+, 8, 10, 64-bit versions only; Mac OS X 10.12+; Ubuntu 16.04, 18.04, and CentOS 7.
> GPU: Graphics card with DX10 (shader model 4.0) capabilities.

Unityだけで3.5GB、Visual Studioだけで最低5GB、SSDやHDDの空き領域が必要なので、ご注意ください。

本資料ではWindowsを前提に進めているので、多少Macの場合は自分で調べて補う必要があるかもしれません。

# ２．Unityのダウンロード
Unity Hubを起動し、インストールの項目からUnityをインストールしてください。  
この資料では2020.3.14f1のバージョンを使います(2021のバージョンがこの資料を書いている間にリリースされました…多分こちらでもそんなに違いはないと思います)。

※いくつかダウンロードの選択肢がありますが、デフォルトのままでインストールするとvisual studio community 2019がインストールされます。

![image](https://user-images.githubusercontent.com/5643842/127955424-6472320f-b784-413d-a971-517bc5593c22.png)

 

# ３．プロジェクトの新規作成
Unity Hubの右上のボタン「新規作成」を押す

テンプレートやプロジェクト名や保存先が選べる。
デフォルトの状態（3D, New Unity Project, C:\Users\yuki）で右下の「作成」を押してよい。
プロジェクト名や保存先は適宜変えてよい。
以下は初期の画面。

![a](https://user-images.githubusercontent.com/5643842/127955755-e0abb4f2-a629-4eb5-8c96-068f05497018.png)

画面下部に以下のような警告が出る場合は、

 ![image](https://user-images.githubusercontent.com/5643842/127955464-73c546e2-059d-4d11-91c4-2bc8c103f45d.png)

下記URLに従って更新後、Unityを再起動をすると警告が消えるみたいです。

https://baba-s.hatenablog.com/entry/2021/07/14/180000




# ４．HierarchyウィンドウとGameObject

![image](https://user-images.githubusercontent.com/5643842/127955498-21211fe9-67e4-4c74-ba97-03256bf384a6.png)

HierarchyにはSampleSceneの中にMain CameraとDirectiona Lightがあり、シーンの中に二つのオブジェクトが含まれている階層構造がわかります。


これらのカメラやライト、その他これからシーン内に作成するオブジェクトはGameObjectと呼ばれ、上部メニューやHierarchyの左上の＋ボタンなどから追加することができます。

図はGameObjectの3D Objectの例です。
他のも見てみてください。

![image](https://user-images.githubusercontent.com/5643842/127955539-bc192d95-9d3d-413a-b31d-7baa28e4a984.png)





# ５．GameObjectのCameraとScene・Game・Inspectorウィンドウ
今あるMainCameraを選択すると、Sceneウィンドウ、Inspectorウィンドウではこのオブジェクトの情報の表示に切り替わります。
オブジェクトの選択はSceneウィンドウ上でもクリックすることで行うことができます。

![image](https://user-images.githubusercontent.com/5643842/127955545-0bdcffd3-27b8-4175-8f41-d7b2eac12c8e.png) ![image](https://user-images.githubusercontent.com/5643842/127955551-dfea6ad0-84cd-4601-ad3f-bfc17ce8835c.png)

↑何も選択されていない状態(左)と選択された状態(右)

Gameウィンドウにはカメラから見えるプログラム実行時の画面が表示されます。

Sceneウィンドウは開発者のためのオブジェクトの俯瞰図が表示されます。
右ドラッグで見る方向、
スクロールドラッグで上下左右の平行移動、
右上のxyzの円錐の選択でxyz軸に平行な視線を選べます。
また、この中心のcubeをクリックすると、並行投影と透視投影の切り替えができます。
スクロールで拡大縮小ができます。

Inspectorウィンドウではカメラの場合は図のような感じです。

![b](https://user-images.githubusercontent.com/5643842/127955966-7e64390c-6764-4a75-abf5-b1da7eb86adf.png)

中身の詳細な説明はウェブにUnityのマニュアルがあるので、そちらを参照してください。

[Transformのマニュアル](https://docs.unity3d.com/Manual/class-Transform.html)  
[Cameraのマニュアル](https://docs.unity3d.com/ja/2020.3/Manual/class-Camera.html)

マニュアルのページではunityのversionや表示言語が選べます。

Transformはオブジェクトの位置や方向、大きさを設定するものなので、Inspectorで数字を変えれば連動してSceneの内容も変わります。
逆に、Sceneの方で選択したオブジェクトから出ているxyzの矢印をドラッグしても移動でき、その際はInspectorの数値が変更されます。
w/e/rでposition/rotate/scaleのUIを切り替えることができます（図）。


![image](https://docs.unity3d.com/uploads/Main/TransformGizmo35.png)

 

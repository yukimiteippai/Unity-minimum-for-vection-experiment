# 3.1 材質の指定

材質はマテリアルによって指定できます。
マテリアルとは マテリアルとは色や光沢、テクスチャの情報によって、金属やプラスチックなどの物質の材質を表現する概念です。
Unityにはオブジェクトの材質表現をできるようにするためにマテリアルコンポーネントが用意されています。

最初にHierarchyウィンドウで作成したCubeのInspectorを見てみましょう。以下のようなDefault-Materialというコンポーネントがあります。

![image](https://user-images.githubusercontent.com/5643842/128003563-8656b57f-a861-495e-9591-083cb2e649db.png)

自分で指定したMaterialを使用する場合は、
> ProjectウィンドウのAssetsを選択＞左上の＋ボタンを押す＞Material

を選んでください。するとProjectウィンドウのAssets内にNew Material（これはデフォルト名で、指定することもできます）が作成されます。

できたNew Materialを選択するとInspectorから様々な設定ができます（図）。
とりあえずAlbedo（ベースとなる色のこと）を赤っぽく変更しました。

![image](https://user-images.githubusercontent.com/5643842/128003594-41c0dcdc-51eb-4124-b80b-cb0e6d796ed8.png) ![image](https://user-images.githubusercontent.com/5643842/128003601-d77ef944-c33a-4992-838c-e579c41bf406.png) ![image](https://user-images.githubusercontent.com/5643842/128003612-9de8b44d-9dab-428b-8831-2862322b9c66.png)


ProjectウィンドウのNewMaterialをドラッグ＆ドロップでHierarchyウィンドウのCubeにドロップすると、このMaterialがCubeに適用されます（図右）。

Albedo以外にも様々な項目があり、金属ぽくしたり、テクスチャを張り付けたり、凹凸を付けたりできます。
詳しくは以下のURLなどを見て、好みの材質を指定してみてください。
参考：https://unity-guide.moon-bear.com/material/#toc5

### スクリプトでの材質の指定
今回の場合の例を二つ以下に載せておきます。
```c#
//例１：cubesの色やMetallicを指定する＠Start関数のfor文最後など
cubes[i].GetComponent<Renderer>().material.color = Color.green;
cubes[i].GetComponent<Renderer>().material.SetFloat("_Metallic", 1.0f);//cubeだとメタリックはわかりにくいですが…
```
```c#   
//例２:他で使っている材質をコピーする＠Start関数内
//↓これはforの前に書きましょう
GameObject cube = GameObject.Find("Cube");
//↓これはforの最後などに書きましょう
cubes[i].GetComponent<Renderer>().material = cube.GetComponent<Renderer>().material;
```


# 3.2 背景を指定する：スカイボックス

背景を指定するスカイボックスを設定します。

> MainCamera＞Inspector＞Add Component>Rendering>skybox

を選びます。
Inspectorに追加されたSkyboxは最初はNoneになっています。Noneの右の◎を押すといくつか選択肢が出てきます。SpatialMappingWireframeを選ぶと以下のような感じ。  
**※Mac環境でSpatialMappingWireframeではうまくいかないという報告がありましたので注意してください**
   
![image](https://user-images.githubusercontent.com/5643842/128003730-67508bf5-78eb-4dc0-ad8f-f206e5b03cc0.png) ![image](https://user-images.githubusercontent.com/5643842/128003736-5b08ecd4-b31a-49ad-bff2-748673b14a29.png)


しかし選べるアセットの数があまりないので、UnityのAsset store(以下のURL)から無料のものをDLしましょう。  
> Unity Asset Store https://assetstore.unity.com/?locale=ja-JP  

サイト上部の検索ボックスにskyboxといれて、表示順を価格の安い順にすると、FREEのアセットがたくさん出てきます。好きなものを選んで「マイアセットに追加する」「Unityで開く」と進んでください。


![image](https://user-images.githubusercontent.com/5643842/128003776-bff843ed-41bd-4264-b980-04c1b85ff085.png) → ![image](https://user-images.githubusercontent.com/5643842/128003783-38dd4d42-6ec7-46f7-b6a2-22e6d2c6a28d.png)


Unityで開くと以下のPackage Managerの左側に追加したアセットが表示されているので、選びます。右側に出てくるDownloadボタンを押し、その後、Importボタンが出てくるので押します。


![image](https://user-images.githubusercontent.com/5643842/128003799-ded9eb14-0f69-4c1c-a82c-72a62c1ee3bf.png)→![image](https://user-images.githubusercontent.com/5643842/128003820-afa557c3-8ff3-481f-b173-c94cfe687585.png)


更に以下のようなウィンドウとボタンが出ますので、Importを押します。すると、MainCameraのSkyboxの◎ボタンから追加したアセットが選べるようになっています。アセットを選ぶと新たなskyboxが適用され、その結果は以下のようになります。
 
 
 ![image](https://user-images.githubusercontent.com/5643842/128004848-42c7f7b5-1fb8-4b42-a2a7-92ad0d4655a0.png)→![image](https://user-images.githubusercontent.com/5643842/128004861-af1d05ee-f5b1-4ed2-9be7-2a5914533bd5.png) ![image](https://user-images.githubusercontent.com/5643842/128004868-2237229a-7043-48f1-b8fc-c7e0e4be7de9.png)





Package managerやAsset storeはUnityのメニューのWindowからもアクセスできるようです（後者は私の環境では結局ウェブに飛ばされました）。

この章で参考にしたウェブ：https://styly.cc/ja/tips/asset-store/ 



### 目次とリンク
- [unity1:Unityのダウンロード、ウィンドウ](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity1.md)
- [unity2:GameObjectの作成と移動](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity2.md)
- [unity3:材質の指定、スカイボックス](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity3.md)(here)
- [unity4:UI](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity4.md)
- [unity5:実験の繰り返し](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity5.md)
- [unity6:ファイル出力](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity6.md)


# 視覚心理実験のためのUnity入門

視覚心理実験で使えそうなUnityのミニマム講座です。  
講義ドキュメントunity1-6と最終的にできるプログラムを配布しています。
具体的な実験サンプルとして、下記のような（物体の移動、ベクション強度の入力、csvによる実験データの書き出し、など）プログラムを行います。

https://user-images.githubusercontent.com/5643842/129184559-8aa14f90-0c2b-4027-ab05-6070e8307ae3.mp4

最終的にできるプロジェクトファイル（プログラムを含む）はNew Unity Projectフォルダにて配布しています。  
Unity-minimum-for-vection-experimentのメインページ右上の緑のCodeボタンより、Download zipを選ぶなどすると自分のPCにダウンロードされるので、これを実行することもできます。  
講義の内容は、Unityをダウンロードし、1からプログラムしていくところから始まりますが、途中で困った場合は正解（配布プロジェクトファイル）を参照することもできます。

結構急いで作ったので、テキストに不足もあるかもしれません。  
遠慮なく質問・ご指摘などしていただければありがたいです。

## プログラミング環境
Windowsで動作確認済みです。その他の環境でも実装可能だと思います。  
UnityもVisualStudioもインストールしていない人は、PCの空き領域が10GBあるのが望ましいです。  

## 予備知識
C#プログラミングの基礎知識があった方がよいです。
一応Unity初心者向けですが、具体例ありきなので基礎の解説はリンク先に任せている部分も多いです。  
しかしながら、これを土台としてプログラミングをしながら調べて知識や技能を追加していくことはできると思います。

プログラミング経験のない人は講義ドキュメントの最初の「Unityのダウンロード」まで行った上で、下記のようなC#の基礎をしておくことが望ましいです（下記ページでいうと応用編の静的メンバくらいまで）。
https://csharp.sevendays-study.com/


## 講義ドキュメント
講義ドキュメントにしたがって、1からプログラムを作成していくことができます。
講義ドキュメントの内訳は以下の通りです。


### [unity1:](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity1.md)
1.1 Unity hubのダウンロード
1.2 Unityのダウンロード
1.3 プロジェクトの新規作成
1.4 HierarchyウィンドウとGameObject
1.5 GameObjectのCameraとScene・Game・Inspectorウィンドウ

### [unity2:](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity2.md)
2. GameObjectの作成と移動  

### [unity3:](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity3.md)
3.1 材質の指定
3.2 背景を指定する：スカイボックス

### [unity4:](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity4.md)
4. UI:テキストやボタン

### [unity5:](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity5.md)
5. 実験の繰り返し

### [unity6:](https://github.com/yukimiteippai/Unity-minimum-for-vection-experiment/blob/main/unity6.md)
6. ファイル出力 

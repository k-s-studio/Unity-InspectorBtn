方法資訊不能被序列化
能序列化的不能攜帶方法資訊

就算可以傳參數也只能是靜態的
=> 連參數也用Reflection抓就好了
=> 用自訂類或多一個bool值紀錄是實例欄位或值

* 就算有辦法傳CallerMemberName並在初始化時查找，也要先以字串抓組件的Type，再查詢MethodInfo，效能不會比直接使用序列化給的fieldInfo好；至少要初始化直接取得Type才夠效率
* 未定義text時默認是欄位名還是方法名?
* 實作IEnumrator用add加入參數?
* 當方法和參數都是靜態時，直接跳過Reflection查找?
* 有複數需要查找的參數時，先整理成List再一起查找?
* 自定義代表需要查找的類，然後以附加方法o.ByRef()去傳遞? => nameof(參數)是唯一例外，就算呼叫擴充方法、或是類別的靜態方法，參考到實例的參數就紅線。=>那我不就不可能分辨出字串值vs變數名稱了? => 可以啦，只不過nameof(HelloAction).Param()這樣。
* InspectorBtnManager cache所有InspectorBtnDrawer

ToDo:

- [x] 整合InspectorBtn和InspectorBtnStatic
- [x] button style
- [ ] 可傳入參數
- [ ] ButtonCacheManager
- [ ] InspectorButtonGroup: 一次性宣告多個按鈕(並一次進行所有反射查詢)、可以簡單排版
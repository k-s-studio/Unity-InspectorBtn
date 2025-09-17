using UnityEngine;
using Assets.KsCode.InspectorBtn;
using System;
public class InspectorBtnExample : MonoBehaviour {
    public string message;
    void Hello(string msg) => Debug.Log(msg);
    public void HelloAction() => Hello(message);
    public InspectorBtn myButton2 = nameof(HelloAction);
    public InspectorBtn myButton3 = ("Hello from Example", (Method)nameof(HelloAction));
    public InspectorBtn myButton4 = new("Hello from Example", (Method)nameof(HelloAction));
    public InspectorBtn myButton5 = (Action)(() => {
        Method m1 = default;
        //Method m2 = ((string)null).Method();
        //Method m3 = (Method)null;
        Method m4 = (Method)"m3";
        Debug.Log($"{m1 == null}, {m4 == null}");
    });
}
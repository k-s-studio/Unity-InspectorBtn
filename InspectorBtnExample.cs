using UnityEngine;
using Assets.KsCode.InspectorBtn;
public class InspectorBtnExample : MonoBehaviour {
    public string message;
    void Hello(string msg) => Debug.Log(msg);
    public void HelloAction() => Hello(message);
    public InspectorBtn myButton2 = nameof(HelloAction);
    public InspectorBtn myButton3 = ("Hello from Example", nameof(HelloAction));
    public InspectorBtn myButton4 = new("Hello from Example", nameof(HelloAction));
}
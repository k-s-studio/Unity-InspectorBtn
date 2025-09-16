using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.KsCode.InspectorBtn.Editor {
    [CustomPropertyDrawer(typeof(InspectorBtn))]
    public class InspectorBtnDrawer : PropertyDrawer {
        // static InspectorBtnDrawer(){
        //     var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/KsCode/InspectorBtn/InspectorBtnStyle.uss");
        // }
        readonly StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/KsCode/InspectorBtn/InspectorBtnStyle.uss");
        private const BindingFlags Filter = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var declaringInstance = property.serializedObject.targetObject;
            var target = (InspectorBtn)fieldInfo.GetValue(declaringInstance);
            // Debug.Log(declaringInstance.GetType());
            // Debug.Log(target.GetType());
            Button btn = new(GetAction2(target)) { text = target.Text ?? preferredLabel };
            btn.styleSheets.Add(styleSheet);
            btn.AddToClassList("ks-inspector-btn");
            //styling...
            return btn;

            // Action GetAction(InspectorBtn btn) => btn.Type switch {
            //     ButtonType.NonStatic => () => fieldInfo.DeclaringType.GetMethod(((NonStaticButton)btn).Data.method, Filter).Invoke(declaringInstance, null),
            //     ButtonType.Static => ((StaticButton)btn).Data.method,
            //     _ => () => Debug.LogWarning($"不對喔: {btn.Type}")
            // };

            Action GetAction2(InspectorBtn btn) => btn switch {
                NonStaticButton btn_r => () => fieldInfo.DeclaringType.GetMethod(btn_r.Data.method, Filter).Invoke(declaringInstance, null),
                StaticButton btn_s => btn_s.Data.method,
                _ => () => Debug.LogWarning($"不對喔: {btn.GetType()}")
            };
        }
    }
}
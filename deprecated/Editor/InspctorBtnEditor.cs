using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.KsCode.InspectorBtn.deprecated.Editor {
    [CustomPropertyDrawer(typeof(InspctorBtn))]
    public class InspectorBtnDrawer : PropertyDrawer {
        readonly StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/KsCode/InspectorBtn/InspectorBtnStyle.uss");
        private const BindingFlags Filter = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var declaringInstance = property.serializedObject.targetObject;
            var target = (InspctorBtn)fieldInfo.GetValue(declaringInstance);
            Button btn = new(GetAction2(target)) { text = target.Text ?? preferredLabel };
            btn.styleSheets.Add(styleSheet);
            btn.AddToClassList("ks-inspector-btn");
            return btn;

            Action GetAction2(InspctorBtn btn) => btn switch {
                NonStaticButton btn_r => () => fieldInfo.DeclaringType.GetMethod(btn_r.Data.method, Filter).Invoke(declaringInstance, null),
                StaticButton btn_s => btn_s.Data.method,
                _ => () => Debug.LogWarning($"不對喔: {btn.GetType()}")
            };
        }
    }
}
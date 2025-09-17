using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.KsCode.InspectorBtn.Editor {
    [CustomPropertyDrawer(typeof(InspectorBtn))]
    public class InspectorBtnDrawer : PropertyDrawer {
        readonly StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/KsCode/InspectorBtn/InspectorBtnStyle.uss");
        private const BindingFlags Filter = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var declaringInstance = property.serializedObject.targetObject;
            var target = (InspectorBtn)fieldInfo.GetValue(declaringInstance);

            Button btn = new(GetAction(target)) { text = target.Text ?? preferredLabel };
            btn.styleSheets.Add(styleSheet);
            btn.AddToClassList("ks-inspector-btn");
            return btn;

            Action GetAction(InspectorBtn btn) => btn.Type switch {
                ButtonType.NonStatic => CreateAction(btn.Method),
                ButtonType.Static => btn.Method,
                _ => () => Debug.LogWarning($"Type: {btn.Type}")
            };
            Action CreateAction(Method m) {
                Debug.Log("info!");
                var methodInfo = fieldInfo.DeclaringType.GetMethod(m.ToString(), Filter);
                return Delegate.CreateDelegate(typeof(Action), declaringInstance, methodInfo) as Action;
            }
        }
    }
}
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.KsCode.InspectorBtn.Editor {
    [CustomPropertyDrawer(typeof(InspectorBtn_r))]
    public class InspectorBtnDrawer : PropertyDrawer {
        private const BindingFlags Filter = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var declaringInstance = property.serializedObject.targetObject;
            var target = (InspectorBtn_r)fieldInfo.GetValue(declaringInstance);
            var (m, t) = target.Data;
            var methodInfo = fieldInfo.DeclaringType.GetMethod(m, Filter);
            Button btn = new(() => methodInfo.Invoke(declaringInstance, null)) { text = t };
            //styling...
            return btn;
        }
    }

}
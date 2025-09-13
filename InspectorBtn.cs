using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.KsCode.InspectorBtn {

    [Serializable]
    public readonly struct InspectorBtn {
        readonly string m_MethodName;
        readonly string m_ButtonText;
        //[NonSerialized] readonly object[] m_Parameters;
        public InspectorBtn(string butonText, string methodName) {
            m_MethodName = methodName ?? throw new ArgumentNullException("Create InspectorBtn with null methodName.");
            m_ButtonText = butonText ?? methodName;
        }
        public readonly (string method, string text) Data => (m_MethodName, m_ButtonText);
        public static implicit operator InspectorBtn((string buttonText, string methodName) data) => new(data.buttonText, data.methodName);
        public static implicit operator InspectorBtn(string methodName) => new(methodName, methodName);
    }

    [Serializable]
    public readonly struct InspectorBtnStatic {
        readonly string m_ButtonText;
        readonly Action m_Action;
        public InspectorBtnStatic(string butonText, Action action) {
            m_ButtonText = butonText;
            m_Action = action ?? throw new ArgumentNullException("Create InspectorBtnStatic with null action.");;
        }
        public InspectorBtnStatic(Action action) {
            m_ButtonText = null; //use property.label in PropertyDrawer
            m_Action = action ?? throw new ArgumentNullException("Create InspectorBtnStatic with null action.");;
        }
        public readonly (string text, Action method) Data => (m_ButtonText, m_Action);
        public static implicit operator InspectorBtnStatic(Action action) => new(null, action);
        public static implicit operator InspectorBtnStatic((string butonText, Action action) data) => new(data.butonText, data.action);
    }
    public readonly struct Param {
        public readonly string name;
        private Param(string name) {
            this.name = name;
        }
        public static implicit operator Param(string name) => new(name);
    }
    public static partial class Extension {
        public static Param Param(this string str) => (Param)str;
    }
}

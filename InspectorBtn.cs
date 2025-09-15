using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.KsCode.InspectorBtn {
    [Serializable]
    public class InspectorBtn {
        private readonly ButtonType m_Type;
        protected readonly string m_ButtonText;
        private InspectorBtn(ButtonType type, string buttonText) {
            m_Type = type;
            m_ButtonText = buttonText;
        }
        internal ButtonType Type => m_Type;

        #region Public .ctors
        // NonStaticButton .ctors
        /// <summary>
        /// Create InspectorBtn for non-static method.
        /// </summary>
        public InspectorBtn(string buttonText, string methodName) => new NonStaticButton(buttonText, (Method)methodName);
        /// <summary>
        /// Create InspectorBtn for non-static method.
        /// </summary>
        public InspectorBtn(Method method) => new NonStaticButton(method.name, method);

        public static implicit operator InspectorBtn((string buttonText, string methodName) data) => new(data.buttonText, data.methodName);
        public static implicit operator InspectorBtn(string methodName) => new((Method)methodName);

        // StaticButton .ctors
        /// <summary>
        /// Create InspectorBtn for static method.
        /// </summary>
        public InspectorBtn(string buttonText, Action method) => new StaticButton(buttonText, method);
        /// <summary>
        /// Create InspectorBtn for static method.
        /// </summary>
        public InspectorBtn(Action method) => new StaticButton(null, method);

        public static implicit operator InspectorBtn((string butonText, Action action) data) => new(data.butonText, data.action);
        public static implicit operator InspectorBtn(Action action) => new(null, action);
        #endregion Public .ctors

        #region Subclasses
        [Serializable]
        internal class NonStaticButton : InspectorBtn {
            readonly Method m_Method;
            //[NonSerialized] readonly object[] m_Parameters;
            internal NonStaticButton(string buttonText, Method method) : base(ButtonType.NonStatic, buttonText) {
                if (method.name == null) throw new ArgumentNullException("Create InspectorBtn with null methodName.");
                m_Method = method;
            }
            internal (Method method, string text) Data => (m_Method, m_ButtonText);
        }

        [Serializable]
        internal class StaticButton : InspectorBtn {
            readonly Action m_Action;
            internal StaticButton(string butonText, Action action) : base(ButtonType.Static, butonText) {
                m_Action = action ?? throw new ArgumentNullException("Create InspectorBtnStatic with null action."); ;
            }
            internal (Action method, string text) Data => (m_Action, m_ButtonText);
        }
        #endregion Subclasses
    }

    public readonly struct Param {
        public readonly string name;
        private Param(string name) {
            this.name = name ?? throw new ArgumentNullException("Param name cannot be null.");
        }
        public static explicit operator Param(string name) => new(name);
    }
    public readonly struct Method {
        public readonly string name;
        private Method(string name) {
            this.name = name ?? throw new ArgumentNullException("Method name cannot be null.");
        }
        public static explicit operator Method(string name) => new(name);
    }
    public static partial class Extension {
        public static Param Param(this string str) => (Param)str;
        public static Method Method(this string str) => (Method)str;
    }
    internal enum ButtonType {
        NonStatic,
        Static,
        NonStaticParam,
        StaticParam
    }
}
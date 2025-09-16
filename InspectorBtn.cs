using System;
using UnityEngine;

namespace Assets.KsCode.InspectorBtn {
    [Serializable]
    public class InspectorBtn {
        //private readonly ButtonType m_Type;
        protected readonly string m_ButtonText;
        private protected InspectorBtn(string buttonText) {
            //m_Type = type;
            m_ButtonText = buttonText;
        }
        //public ButtonType Type => m_Type;
        public string Text => m_ButtonText;

        #region Factory
        // NonStaticButton
        /// <summary>
        /// Create InspectorBtn for non-static method with specified Button.text.
        /// </summary>
        public static InspectorBtn Create(string buttonText, Method method) => new NonStaticButton(buttonText, method);
        /// <summary>
        /// Create InspectorBtn for non-static method with specified Button.text.
        /// </summary>
        public static InspectorBtn Create(string buttonText, string methodName) => Create(buttonText, (Method)methodName);
        /// <summary>
        /// Create InspectorBtn for non-static method.
        /// </summary>
        public static InspectorBtn Create(string methodName) => Create(methodName, (Method)methodName);
        /// <summary>
        /// Create InspectorBtn for non-static method.
        /// </summary>
        public static InspectorBtn Create(Method method) => Create(method, method);

        public static implicit operator InspectorBtn((string buttonText, Method method) data) => Create(data.buttonText, data.method);
        public static implicit operator InspectorBtn((string buttonText, string methodName) data) => Create(data.buttonText, data.methodName);
        public static implicit operator InspectorBtn(string methodName) => Create(methodName);
        public static implicit operator InspectorBtn(Method method) => Create(method);

        // StaticButton
        /// <summary>
        /// Create InspectorBtn for static method with specified Button.text.
        /// </summary>
        public static InspectorBtn Create(string buttonText, Action method) => new StaticButton(buttonText, method);
        /// <summary>
        /// Create InspectorBtn for static method.
        /// </summary>
        public static InspectorBtn Create(Action method) => Create(null, method);

        public static implicit operator InspectorBtn((string butonText, Action action) data) => Create(data.butonText, data.action);
        public static implicit operator InspectorBtn(Action action) => Create(action);
        #endregion Factory
    }

    #region Subclasses
    [Serializable]
    public sealed class NonStaticButton : InspectorBtn {
        readonly Method m_Method;
        //[NonSerialized] readonly object[] m_Parameters;
        public NonStaticButton(string buttonText, Method method) : base(buttonText) {
            if (method == null) throw new ArgumentNullException("Create InspectorBtn with null methodName.");
            m_Method = method;
        }
        public (Method method, string text) Data => (m_Method, m_ButtonText);
    }

    [Serializable]
    public sealed class StaticButton : InspectorBtn {
        readonly Action m_Action;
        public StaticButton(string butonText, Action action) : base(butonText) {
            m_Action = action ?? throw new ArgumentNullException("Create InspectorBtnStatic with null action."); ;
        }
        public (Action method, string text) Data => (m_Action, m_ButtonText);
    }
    #endregion Subclasses

    public readonly struct Param {
        public readonly string name;
        private Param(string name) {
            this.name = name ?? throw new ArgumentNullException("Param name cannot be null.");
        }
        public static explicit operator Param(string name) => new(name);
        public static implicit operator string(Param param) => param.name;
    }
    public readonly struct Method {
        private readonly string name;
        private Method(string name) {
            this.name = name ?? throw new ArgumentNullException("Method name cannot be null.");
        }
        public static explicit operator Method(string name) => new(name);
        public static implicit operator string(Method method) => method.name;
        public override bool Equals(object obj) => obj == null ? name == null : base.Equals(obj);
        public override int GetHashCode() => name.GetHashCode();
    }
    public static partial class Extension {
        public static Param Param(this string str) => (Param)str;
        public static Method Method(this string str) => (Method)str;
    }
    public enum ButtonType {
        Undefined,
        NonStatic,
        Static,
        NonStaticParam,
        StaticParam
    }
}
using System;

namespace Assets.KsCode.InspectorBtn {
    public class InspectorBtn {
        readonly ButtonType m_Type;
        readonly string m_ButtonText;
        readonly dynamic m_Method;
        // readonly Method m_Method;
        // readonly Action m_Action;
        public ButtonType Type => m_Type;
        public string Text => m_ButtonText;
        public dynamic Method => m_Method;
        public InspectorBtn(ButtonType type, string text) {
            m_Type = type;
            m_ButtonText = text;
        }
        public InspectorBtn(string buttonText, Method method)
            : this(ButtonType.NonStatic, buttonText) => m_Method = method;
        public InspectorBtn(string buttonText, string methodName) : this(buttonText, (Method)methodName) { }
        public InspectorBtn(Method method) : this(null, method) { }
        public InspectorBtn(string methodName) : this(null, (Method)methodName) { }
        public static implicit operator InspectorBtn((string buttonText, Method method) data) => new(data.buttonText, data.method);
        public static implicit operator InspectorBtn((string buttonText, string methodName) data) => new(data.buttonText, data.methodName);
        public static implicit operator InspectorBtn(Method method) => new(method);
        public static implicit operator InspectorBtn(string methodName) => new(methodName);

        public InspectorBtn(string buttonText, Action method)
             : this(ButtonType.Static, buttonText)
                => m_Method = method;
        public InspectorBtn(Action method) : this(null, method) { }
        public static implicit operator InspectorBtn((string butonText, Action action) data) => new(data.butonText, data.action);
        public static implicit operator InspectorBtn(Action action) => new(action);
    }
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
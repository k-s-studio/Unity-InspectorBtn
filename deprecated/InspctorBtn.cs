using System;

namespace Assets.KsCode.InspectorBtn.deprecated {
    //[Obsolete("Using InspectorBtn can be smoothier.",false)]
    [Serializable]
    public class InspctorBtn {
        protected readonly string m_ButtonText;
        private protected InspctorBtn(string buttonText) {
            m_ButtonText = buttonText;
        }
        public string Text => m_ButtonText;

        #region Factory
        // NonStaticButton
        /// <summary>
        /// Create InspectorBtn for non-static method with specified Button.text.
        /// </summary>
        public static InspctorBtn Create(string buttonText, Method method) => new NonStaticButton(buttonText, method);
        /// <summary>
        /// Create InspectorBtn for non-static method with specified Button.text.
        /// </summary>
        public static InspctorBtn Create(string buttonText, string methodName) => Create(buttonText, (Method)methodName);
        /// <summary>
        /// Create InspectorBtn for non-static method.
        /// </summary>
        public static InspctorBtn Create(string methodName) => Create(methodName, (Method)methodName);
        /// <summary>
        /// Create InspectorBtn for non-static method.
        /// </summary>
        public static InspctorBtn Create(Method method) => Create(method, method);

        public static implicit operator InspctorBtn((string buttonText, Method method) data) => Create(data.buttonText, data.method);
        public static implicit operator InspctorBtn((string buttonText, string methodName) data) => Create(data.buttonText, data.methodName);
        public static implicit operator InspctorBtn(string methodName) => Create(methodName);
        public static implicit operator InspctorBtn(Method method) => Create(method);

        // StaticButton
        /// <summary>
        /// Create InspectorBtn for static method with specified Button.text.
        /// </summary>
        public static InspctorBtn Create(string buttonText, Action method) => new StaticButton(buttonText, method);
        /// <summary>
        /// Create InspectorBtn for static method.
        /// </summary>
        public static InspctorBtn Create(Action method) => Create(null, method);

        public static implicit operator InspctorBtn((string butonText, Action action) data) => Create(data.butonText, data.action);
        public static implicit operator InspctorBtn(Action action) => Create(action);
        #endregion Factory
    }

    #region Subclasses
    [Serializable]
    public sealed class NonStaticButton : InspctorBtn {
        readonly Method m_Method;
        public NonStaticButton(string buttonText, Method method) : base(buttonText) {
            if (method == null) throw new ArgumentNullException("Create InspectorBtn with null methodName.");
            m_Method = method;
        }
        public (Method method, string text) Data => (m_Method, m_ButtonText);
    }

    [Serializable]
    public sealed class StaticButton : InspctorBtn {
        readonly Action m_Action;
        public StaticButton(string butonText, Action action) : base(butonText) {
            m_Action = action ?? throw new ArgumentNullException("Create InspectorBtnStatic with null action."); ;
        }
        public (Action method, string text) Data => (m_Action, m_ButtonText);
    }
    #endregion Subclasses

}
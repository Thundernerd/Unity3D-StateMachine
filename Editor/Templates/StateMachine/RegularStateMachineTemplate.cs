﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace TNRD.StateManagement.Templates.StateMachine
{
    using System.Text;
    using System.Collections.Generic;
    using TNRD.StateManagement;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Repositories\Unity\StateMachine\Packages\Unity3D-StateMachine\Editor\Templates\StateMachine\RegularStateMachineTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class RegularStateMachineTemplate : RegularStateMachineTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\nusing ");
            
            #line 3 "D:\Repositories\Unity\StateMachine\Packages\Unity3D-StateMachine\Editor\Templates\StateMachine\RegularStateMachineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write(".Contracts;\r\n\r\nnamespace ");
            
            #line 5 "D:\Repositories\Unity\StateMachine\Packages\Unity3D-StateMachine\Editor\Templates\StateMachine\RegularStateMachineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    public sealed partial class ");
            
            #line 7 "D:\Repositories\Unity\StateMachine\Packages\Unity3D-StateMachine\Editor\Templates\StateMachine\RegularStateMachineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FullStateMachineName));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        public ");
            
            #line 9 "D:\Repositories\Unity\StateMachine\Packages\Unity3D-StateMachine\Editor\Templates\StateMachine\RegularStateMachineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(FullStateMachineName));
            
            #line default
            #line hidden
            this.Write("(\r\n            ");
            
            #line 10 "D:\Repositories\Unity\StateMachine\Packages\Unity3D-StateMachine\Editor\Templates\StateMachine\RegularStateMachineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(StateFactoryInterfaceName));
            
            #line default
            #line hidden
            this.Write(" stateFactory,\r\n            ");
            
            #line 11 "D:\Repositories\Unity\StateMachine\Packages\Unity3D-StateMachine\Editor\Templates\StateMachine\RegularStateMachineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TransitionFactoryInterfaceName));
            
            #line default
            #line hidden
            this.Write(" transitionFactory,\r\n            ");
            
            #line 12 "D:\Repositories\Unity\StateMachine\Packages\Unity3D-StateMachine\Editor\Templates\StateMachine\RegularStateMachineTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(UpdateProviderInterfaceName));
            
            #line default
            #line hidden
            this.Write(" updateProvider\r\n        )\r\n            : base(stateFactory, transitionFactory, updateProvider)\r\n        {\r\n        }\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "D:\Repositories\Unity\StateMachine\Packages\Unity3D-StateMachine\Editor\Templates\StateMachine\RegularStateMachineTemplate.tt"

private string _NamespaceField;

/// <summary>
/// Access the Namespace parameter of the template.
/// </summary>
private string Namespace
{
    get
    {
        return this._NamespaceField;
    }
}

private string _StateMachineNameField;

/// <summary>
/// Access the StateMachineName parameter of the template.
/// </summary>
private string StateMachineName
{
    get
    {
        return this._StateMachineNameField;
    }
}

private string _FullStateMachineNameField;

/// <summary>
/// Access the FullStateMachineName parameter of the template.
/// </summary>
private string FullStateMachineName
{
    get
    {
        return this._FullStateMachineNameField;
    }
}

private string _StateMachineInterfaceNameField;

/// <summary>
/// Access the StateMachineInterfaceName parameter of the template.
/// </summary>
private string StateMachineInterfaceName
{
    get
    {
        return this._StateMachineInterfaceNameField;
    }
}

private string _StateMachineNameLowercaseField;

/// <summary>
/// Access the StateMachineNameLowercase parameter of the template.
/// </summary>
private string StateMachineNameLowercase
{
    get
    {
        return this._StateMachineNameLowercaseField;
    }
}

private string _FullStateMachineNameLowercaseField;

/// <summary>
/// Access the FullStateMachineNameLowercase parameter of the template.
/// </summary>
private string FullStateMachineNameLowercase
{
    get
    {
        return this._FullStateMachineNameLowercaseField;
    }
}

private string _SubContainerManagerNameField;

/// <summary>
/// Access the SubContainerManagerName parameter of the template.
/// </summary>
private string SubContainerManagerName
{
    get
    {
        return this._SubContainerManagerNameField;
    }
}

private string _StateIdNameField;

/// <summary>
/// Access the StateIdName parameter of the template.
/// </summary>
private string StateIdName
{
    get
    {
        return this._StateIdNameField;
    }
}

private string _TransitionIdNameField;

/// <summary>
/// Access the TransitionIdName parameter of the template.
/// </summary>
private string TransitionIdName
{
    get
    {
        return this._TransitionIdNameField;
    }
}

private string _InitialStateNameField;

/// <summary>
/// Access the InitialStateName parameter of the template.
/// </summary>
private string InitialStateName
{
    get
    {
        return this._InitialStateNameField;
    }
}

private string _BaseStateNameField;

/// <summary>
/// Access the BaseStateName parameter of the template.
/// </summary>
private string BaseStateName
{
    get
    {
        return this._BaseStateNameField;
    }
}

private string _BaseTransitionNameField;

/// <summary>
/// Access the BaseTransitionName parameter of the template.
/// </summary>
private string BaseTransitionName
{
    get
    {
        return this._BaseTransitionNameField;
    }
}

private string _StateFactoryNameField;

/// <summary>
/// Access the StateFactoryName parameter of the template.
/// </summary>
private string StateFactoryName
{
    get
    {
        return this._StateFactoryNameField;
    }
}

private string _StateFactoryInterfaceNameField;

/// <summary>
/// Access the StateFactoryInterfaceName parameter of the template.
/// </summary>
private string StateFactoryInterfaceName
{
    get
    {
        return this._StateFactoryInterfaceNameField;
    }
}

private string _TransitionFactoryNameField;

/// <summary>
/// Access the TransitionFactoryName parameter of the template.
/// </summary>
private string TransitionFactoryName
{
    get
    {
        return this._TransitionFactoryNameField;
    }
}

private string _TransitionFactoryInterfaceNameField;

/// <summary>
/// Access the TransitionFactoryInterfaceName parameter of the template.
/// </summary>
private string TransitionFactoryInterfaceName
{
    get
    {
        return this._TransitionFactoryInterfaceNameField;
    }
}

private string _UpdateProviderNameField;

/// <summary>
/// Access the UpdateProviderName parameter of the template.
/// </summary>
private string UpdateProviderName
{
    get
    {
        return this._UpdateProviderNameField;
    }
}

private string _UpdateProviderInterfaceNameField;

/// <summary>
/// Access the UpdateProviderInterfaceName parameter of the template.
/// </summary>
private string UpdateProviderInterfaceName
{
    get
    {
        return this._UpdateProviderInterfaceNameField;
    }
}

private global::System.Collections.Generic.List<TNRD.StateManagement.StateMachineGraph.StateData> _StatesField;

/// <summary>
/// Access the States parameter of the template.
/// </summary>
private global::System.Collections.Generic.List<TNRD.StateManagement.StateMachineGraph.StateData> States
{
    get
    {
        return this._StatesField;
    }
}

private global::System.Collections.Generic.List<TNRD.StateManagement.StateMachineGraph.TransitionData> _TransitionsField;

/// <summary>
/// Access the Transitions parameter of the template.
/// </summary>
private global::System.Collections.Generic.List<TNRD.StateManagement.StateMachineGraph.TransitionData> Transitions
{
    get
    {
        return this._TransitionsField;
    }
}

private string _CustomField;

/// <summary>
/// Access the Custom parameter of the template.
/// </summary>
private string Custom
{
    get
    {
        return this._CustomField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool NamespaceValueAcquired = false;
if (this.Session.ContainsKey("Namespace"))
{
    this._NamespaceField = ((string)(this.Session["Namespace"]));
    NamespaceValueAcquired = true;
}
if ((NamespaceValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Namespace");
    if ((data != null))
    {
        this._NamespaceField = ((string)(data));
    }
}
bool StateMachineNameValueAcquired = false;
if (this.Session.ContainsKey("StateMachineName"))
{
    this._StateMachineNameField = ((string)(this.Session["StateMachineName"]));
    StateMachineNameValueAcquired = true;
}
if ((StateMachineNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("StateMachineName");
    if ((data != null))
    {
        this._StateMachineNameField = ((string)(data));
    }
}
bool FullStateMachineNameValueAcquired = false;
if (this.Session.ContainsKey("FullStateMachineName"))
{
    this._FullStateMachineNameField = ((string)(this.Session["FullStateMachineName"]));
    FullStateMachineNameValueAcquired = true;
}
if ((FullStateMachineNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("FullStateMachineName");
    if ((data != null))
    {
        this._FullStateMachineNameField = ((string)(data));
    }
}
bool StateMachineInterfaceNameValueAcquired = false;
if (this.Session.ContainsKey("StateMachineInterfaceName"))
{
    this._StateMachineInterfaceNameField = ((string)(this.Session["StateMachineInterfaceName"]));
    StateMachineInterfaceNameValueAcquired = true;
}
if ((StateMachineInterfaceNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("StateMachineInterfaceName");
    if ((data != null))
    {
        this._StateMachineInterfaceNameField = ((string)(data));
    }
}
bool StateMachineNameLowercaseValueAcquired = false;
if (this.Session.ContainsKey("StateMachineNameLowercase"))
{
    this._StateMachineNameLowercaseField = ((string)(this.Session["StateMachineNameLowercase"]));
    StateMachineNameLowercaseValueAcquired = true;
}
if ((StateMachineNameLowercaseValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("StateMachineNameLowercase");
    if ((data != null))
    {
        this._StateMachineNameLowercaseField = ((string)(data));
    }
}
bool FullStateMachineNameLowercaseValueAcquired = false;
if (this.Session.ContainsKey("FullStateMachineNameLowercase"))
{
    this._FullStateMachineNameLowercaseField = ((string)(this.Session["FullStateMachineNameLowercase"]));
    FullStateMachineNameLowercaseValueAcquired = true;
}
if ((FullStateMachineNameLowercaseValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("FullStateMachineNameLowercase");
    if ((data != null))
    {
        this._FullStateMachineNameLowercaseField = ((string)(data));
    }
}
bool SubContainerManagerNameValueAcquired = false;
if (this.Session.ContainsKey("SubContainerManagerName"))
{
    this._SubContainerManagerNameField = ((string)(this.Session["SubContainerManagerName"]));
    SubContainerManagerNameValueAcquired = true;
}
if ((SubContainerManagerNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("SubContainerManagerName");
    if ((data != null))
    {
        this._SubContainerManagerNameField = ((string)(data));
    }
}
bool StateIdNameValueAcquired = false;
if (this.Session.ContainsKey("StateIdName"))
{
    this._StateIdNameField = ((string)(this.Session["StateIdName"]));
    StateIdNameValueAcquired = true;
}
if ((StateIdNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("StateIdName");
    if ((data != null))
    {
        this._StateIdNameField = ((string)(data));
    }
}
bool TransitionIdNameValueAcquired = false;
if (this.Session.ContainsKey("TransitionIdName"))
{
    this._TransitionIdNameField = ((string)(this.Session["TransitionIdName"]));
    TransitionIdNameValueAcquired = true;
}
if ((TransitionIdNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("TransitionIdName");
    if ((data != null))
    {
        this._TransitionIdNameField = ((string)(data));
    }
}
bool InitialStateNameValueAcquired = false;
if (this.Session.ContainsKey("InitialStateName"))
{
    this._InitialStateNameField = ((string)(this.Session["InitialStateName"]));
    InitialStateNameValueAcquired = true;
}
if ((InitialStateNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("InitialStateName");
    if ((data != null))
    {
        this._InitialStateNameField = ((string)(data));
    }
}
bool BaseStateNameValueAcquired = false;
if (this.Session.ContainsKey("BaseStateName"))
{
    this._BaseStateNameField = ((string)(this.Session["BaseStateName"]));
    BaseStateNameValueAcquired = true;
}
if ((BaseStateNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("BaseStateName");
    if ((data != null))
    {
        this._BaseStateNameField = ((string)(data));
    }
}
bool BaseTransitionNameValueAcquired = false;
if (this.Session.ContainsKey("BaseTransitionName"))
{
    this._BaseTransitionNameField = ((string)(this.Session["BaseTransitionName"]));
    BaseTransitionNameValueAcquired = true;
}
if ((BaseTransitionNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("BaseTransitionName");
    if ((data != null))
    {
        this._BaseTransitionNameField = ((string)(data));
    }
}
bool StateFactoryNameValueAcquired = false;
if (this.Session.ContainsKey("StateFactoryName"))
{
    this._StateFactoryNameField = ((string)(this.Session["StateFactoryName"]));
    StateFactoryNameValueAcquired = true;
}
if ((StateFactoryNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("StateFactoryName");
    if ((data != null))
    {
        this._StateFactoryNameField = ((string)(data));
    }
}
bool StateFactoryInterfaceNameValueAcquired = false;
if (this.Session.ContainsKey("StateFactoryInterfaceName"))
{
    this._StateFactoryInterfaceNameField = ((string)(this.Session["StateFactoryInterfaceName"]));
    StateFactoryInterfaceNameValueAcquired = true;
}
if ((StateFactoryInterfaceNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("StateFactoryInterfaceName");
    if ((data != null))
    {
        this._StateFactoryInterfaceNameField = ((string)(data));
    }
}
bool TransitionFactoryNameValueAcquired = false;
if (this.Session.ContainsKey("TransitionFactoryName"))
{
    this._TransitionFactoryNameField = ((string)(this.Session["TransitionFactoryName"]));
    TransitionFactoryNameValueAcquired = true;
}
if ((TransitionFactoryNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("TransitionFactoryName");
    if ((data != null))
    {
        this._TransitionFactoryNameField = ((string)(data));
    }
}
bool TransitionFactoryInterfaceNameValueAcquired = false;
if (this.Session.ContainsKey("TransitionFactoryInterfaceName"))
{
    this._TransitionFactoryInterfaceNameField = ((string)(this.Session["TransitionFactoryInterfaceName"]));
    TransitionFactoryInterfaceNameValueAcquired = true;
}
if ((TransitionFactoryInterfaceNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("TransitionFactoryInterfaceName");
    if ((data != null))
    {
        this._TransitionFactoryInterfaceNameField = ((string)(data));
    }
}
bool UpdateProviderNameValueAcquired = false;
if (this.Session.ContainsKey("UpdateProviderName"))
{
    this._UpdateProviderNameField = ((string)(this.Session["UpdateProviderName"]));
    UpdateProviderNameValueAcquired = true;
}
if ((UpdateProviderNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("UpdateProviderName");
    if ((data != null))
    {
        this._UpdateProviderNameField = ((string)(data));
    }
}
bool UpdateProviderInterfaceNameValueAcquired = false;
if (this.Session.ContainsKey("UpdateProviderInterfaceName"))
{
    this._UpdateProviderInterfaceNameField = ((string)(this.Session["UpdateProviderInterfaceName"]));
    UpdateProviderInterfaceNameValueAcquired = true;
}
if ((UpdateProviderInterfaceNameValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("UpdateProviderInterfaceName");
    if ((data != null))
    {
        this._UpdateProviderInterfaceNameField = ((string)(data));
    }
}
bool StatesValueAcquired = false;
if (this.Session.ContainsKey("States"))
{
    this._StatesField = ((global::System.Collections.Generic.List<TNRD.StateManagement.StateMachineGraph.StateData>)(this.Session["States"]));
    StatesValueAcquired = true;
}
if ((StatesValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("States");
    if ((data != null))
    {
        this._StatesField = ((global::System.Collections.Generic.List<TNRD.StateManagement.StateMachineGraph.StateData>)(data));
    }
}
bool TransitionsValueAcquired = false;
if (this.Session.ContainsKey("Transitions"))
{
    this._TransitionsField = ((global::System.Collections.Generic.List<TNRD.StateManagement.StateMachineGraph.TransitionData>)(this.Session["Transitions"]));
    TransitionsValueAcquired = true;
}
if ((TransitionsValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Transitions");
    if ((data != null))
    {
        this._TransitionsField = ((global::System.Collections.Generic.List<TNRD.StateManagement.StateMachineGraph.TransitionData>)(data));
    }
}
bool CustomValueAcquired = false;
if (this.Session.ContainsKey("Custom"))
{
    this._CustomField = ((string)(this.Session["Custom"]));
    CustomValueAcquired = true;
}
if ((CustomValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Custom");
    if ((data != null))
    {
        this._CustomField = ((string)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class RegularStateMachineTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}

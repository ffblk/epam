﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Errors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Errors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Cinema.Resources.Errors", typeof(Errors).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле должно быть установлено.
        /// </summary>
        internal static string NoField {
            get {
                return ResourceManager.GetString("NoField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка заказа.
        /// </summary>
        internal static string Order {
            get {
                return ResourceManager.GetString("Order", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Некорректный адрес.
        /// </summary>
        internal static string SetMail {
            get {
                return ResourceManager.GetString("SetMail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Длина строки должна быть от 5 до 15 символов.
        /// </summary>
        internal static string SetName {
            get {
                return ResourceManager.GetString("SetName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пароли не совпадают.
        /// </summary>
        internal static string SetPass {
            get {
                return ResourceManager.GetString("SetPass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Некорректный телефон.
        /// </summary>
        internal static string SetPhone {
            get {
                return ResourceManager.GetString("SetPhone", resourceCulture);
            }
        }
    }
}

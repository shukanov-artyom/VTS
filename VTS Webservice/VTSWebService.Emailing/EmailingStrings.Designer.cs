﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VTSWebService.Emailing {
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
    internal class EmailingStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EmailingStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VTSWebService.Emailing.EmailingStrings", typeof(EmailingStrings).Assembly);
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
        ///   Looks up a localized string similar to &lt;html&gt;
        ///&lt;head&gt;
        ///&lt;/head&gt;
        ///&lt;body&gt;
        ///	&lt;div&gt;
        ///		&lt;div style=&quot;background: yellowgreen&quot;&gt;
        ///			&lt;h3 style=&quot;margin-left: 60px; padding-top: 10px; padding-bottom: 10px&quot;&gt;Для вас создан аккаунт в системе VTS Автомониторинг.&lt;/h3&gt;
        ///		&lt;/div&gt;
        ///		&lt;p&gt;Вы можете &lt;a href=&quot;http://vtsapp.azurewebsites.net&quot;&gt;войти в систему&lt;/a&gt; со своими новыми логином и паролем, и просмотреть диагностические данные и историю обслуживания вашего автомобиля.&lt;/p&gt;
        ///		&lt;p&gt;Для входа в систему вам может понадобиться установить Silverlight Plugin для вашего б [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ClientRegistrationNotification {
            get {
                return ResourceManager.GetString("ClientRegistrationNotification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;html&gt;
        ///&lt;head&gt;
        ///&lt;/head&gt;
        ///&lt;body&gt;
        ///	&lt;div&gt;
        ///		&lt;div style=&quot;background: yellowgreen&quot;&gt;
        ///			&lt;h3 style=&quot;margin-left: 60px; padding-top: 10px; padding-bottom: 10px&quot;&gt;Вам предоставлены данные по машине в системе VTS Автомониторинг.&lt;/h3&gt;
        ///		&lt;/div&gt;
        ///		&lt;p&gt;Машина {0} {1}, VIN: {2}&lt;/p&gt;
        ///		&lt;p&gt;Вы можете &lt;a href=&quot;http://vtsapp.azurewebsites.net&quot;&gt;войти в систему&lt;/a&gt; со своими логином и паролем, и просмотреть диагностические данные и историю обслуживания этого автомобиля.&lt;/p&gt;
        ///	&lt;/div&gt;
        ///	&lt;div style=&quot;background: silver&quot;&gt;
        ///		&lt;p st [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ClientVehicleAssociationNotification {
            get {
                return ResourceManager.GetString("ClientVehicleAssociationNotification", resourceCulture);
            }
        }
    }
}

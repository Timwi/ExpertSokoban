﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExpertSokoban.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ExpertSokoban.Properties.Resources", typeof(Resources).Assembly);
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
        
        internal static System.Drawing.Bitmap ArrowDown {
            get {
                object obj = ResourceManager.GetObject("ArrowDown", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ArrowLeft {
            get {
                object obj = ResourceManager.GetObject("ArrowLeft", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ArrowRight {
            get {
                object obj = ResourceManager.GetObject("ArrowRight", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ArrowUp {
            get {
                object obj = ResourceManager.GetObject("ArrowUp", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Icon ExpertSokoban {
            get {
                object obj = ResourceManager.GetObject("ExpertSokoban", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ImgLevelSolved {
            get {
                object obj = ResourceManager.GetObject("ImgLevelSolved", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ImgPiece {
            get {
                object obj = ResourceManager.GetObject("ImgPiece", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ImgPieceSelected {
            get {
                object obj = ResourceManager.GetObject("ImgPieceSelected", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ImgPieceTarget {
            get {
                object obj = ResourceManager.GetObject("ImgPieceTarget", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ImgSokoban {
            get {
                object obj = ResourceManager.GetObject("ImgSokoban", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ImgTarget {
            get {
                object obj = ResourceManager.GetObject("ImgTarget", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ImgTargetUnder {
            get {
                object obj = ResourceManager.GetObject("ImgTargetUnder", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ImgWall {
            get {
                object obj = ResourceManager.GetObject("ImgWall", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.IO.UnmanagedMemoryStream SndEditorClick {
            get {
                return ResourceManager.GetStream("SndEditorClick", resourceCulture);
            }
        }
        
        internal static System.IO.UnmanagedMemoryStream SndLevelDone {
            get {
                return ResourceManager.GetStream("SndLevelDone", resourceCulture);
            }
        }
        
        internal static System.IO.UnmanagedMemoryStream SndMeep {
            get {
                return ResourceManager.GetStream("SndMeep", resourceCulture);
            }
        }
        
        internal static System.IO.UnmanagedMemoryStream SndPiecePlaced {
            get {
                return ResourceManager.GetStream("SndPiecePlaced", resourceCulture);
            }
        }
    }
}

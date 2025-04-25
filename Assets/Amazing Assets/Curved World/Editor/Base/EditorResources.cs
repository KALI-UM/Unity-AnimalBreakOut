// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>

using UnityEngine;
using UnityEditor;


namespace AmazingAssets.CurvedWorld.Editor
{
    static public class EditorResources
    {
        static Texture2D iconForum;
        static public Texture2D IconForum
        {
            get
            {
                if (iconForum == null)
                    iconForum = EditorUtilities.LoadTexture("IconForum", TextureWrapMode.Clamp, false);

                return iconForum;
            }
        }
        static Texture2D iconManual;
        static public Texture2D IconManual
        {
            get
            {
                if (iconManual == null)
                    iconManual = EditorUtilities.LoadTexture("IconManual", TextureWrapMode.Clamp, false);

                return iconManual;
            }
        }
        static Texture2D iconSupport;
        static public Texture2D IconSupport
        {
            get
            {
                if (iconSupport == null)
                    iconSupport = EditorUtilities.LoadTexture("IconSupport", TextureWrapMode.Clamp, false);

                return iconSupport;
            }
        }

        static Texture2D iconRate;
        static public Texture2D IconRate
        {
            get
            {
                if (iconRate == null)
                    iconRate = EditorUtilities.LoadTexture("IconRate", TextureWrapMode.Clamp, false);

                return iconRate;
            }
        }
        static Texture2D iconMore;
        static public Texture2D IconMore
        {
            get
            {
                if (iconMore == null)
                    iconMore = EditorUtilities.LoadTexture("IconMore", TextureWrapMode.Clamp, false);

                return iconMore;
            }
        }
        static Texture2D iconMaterial;
        static public Texture2D IconMaterial
        {
            get
            {
                if (iconMaterial == null)
                    iconMaterial = EditorUtilities.LoadTexture("IconMaterial", TextureWrapMode.Clamp, false);

                return iconMaterial;
            }
        }
        static Texture2D iconShader;
        static public Texture2D IconShader
        {
            get
            {
                if (iconShader == null)
                    iconShader = EditorUtilities.LoadTexture("IconShader", TextureWrapMode.Clamp, false);

                return iconShader;
            }
        }
        static Texture2D iconSelection;
        static public Texture2D IconSelection
        {
            get
            {
                if (iconSelection == null)
                    iconSelection = EditorUtilities.LoadTexture("IconSelection", TextureWrapMode.Clamp, false);

                return iconSelection;
            }
        }




        static GUIStyle guiStyleOptionsHeader;
        static public GUIStyle GUIStyleOptionsHeader
        {
            get
            {
                if(guiStyleOptionsHeader == null)
                    guiStyleOptionsHeader = new GUIStyle((GUIStyle)"SettingsHeader");

                return guiStyleOptionsHeader;
            }
        }
        static public int GUIStyleOptionsHeaderHeight = Mathf.CeilToInt(GUIStyleOptionsHeader.CalcSize(new GUIContent("Manage")).y);

        static GUIStyle guiStyleControllersButton;
        static public GUIStyle GUIStyleControllersButton
        {
            get
            {
                if(guiStyleControllersButton == null)
                {
                    guiStyleControllersButton = new GUIStyle(EditorStyles.miniButtonRight);
                    guiStyleControllersButton.alignment = TextAnchor.MiddleLeft;
                }

                return guiStyleControllersButton;
            }
        }

        static GUIStyle guiStyleAnalyzeSaveButton;
        static public GUIStyle GUIStyleAnalyzeSaveButton
        {
            get
            {
                if (guiStyleAnalyzeSaveButton == null)
                {
                    guiStyleAnalyzeSaveButton = new GUIStyle(EditorStyles.miniButtonLeft);
                    guiStyleAnalyzeSaveButton.richText = true;
                }

                return guiStyleAnalyzeSaveButton;
            }
        }

        static GUIStyle guiStyleButtonTab;
        static public GUIStyle GUIStyleButtonTab
        {
            get
            {
                if (guiStyleButtonTab == null)
                {
                    guiStyleButtonTab = new GUIStyle(GUIStyle.none);

                    if (UnityEditor.EditorGUIUtility.isProSkin)
                        guiStyleButtonTab.normal.textColor = Color.white * 0.95f;

                    guiStyleButtonTab.alignment = TextAnchor.MiddleLeft;
                }

                return guiStyleButtonTab;
            }
        }

        static GUIStyle guityleBoldFoldout;
        public static GUIStyle GUIStyleBoldFoldout
        {
            get
            {
                if(guityleBoldFoldout == null)
                {
                    guityleBoldFoldout = new GUIStyle(EditorStyles.foldout);
                    guityleBoldFoldout.fontStyle = FontStyle.Bold;
                }

                return guityleBoldFoldout;
            }
        }
    }
}
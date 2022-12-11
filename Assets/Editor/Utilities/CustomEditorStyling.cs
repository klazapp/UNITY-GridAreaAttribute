using UnityEditor;
using UnityEngine;

public static class CustomEditorStyling
{
    public const int squareSize = 20;

    public static GUIStyle headerStyle = new()
    {
        fontSize = 18,
        fontStyle = FontStyle.Bold,
        alignment = TextAnchor.UpperCenter,
        normal =
        {
            textColor = Color.white
        }
    };

    public static GUIStyle preTitleStyle = new()
    {
        fontSize = 18,
        fontStyle = FontStyle.Bold,
        normal =
        {
            textColor = Color.white
        },
        alignment = TextAnchor.UpperCenter,
    };

    public static GUIStyle titleStyle = new()
    {
        fontSize = 15,
        fontStyle = FontStyle.Bold,
        alignment = TextAnchor.UpperCenter,
        normal =
        {
            textColor = Color.white
        }
    };

    public static GUIStyle titleStyleV2 = new()
    {
        fontSize = 15,
        fontStyle = FontStyle.Bold,
        alignment = TextAnchor.MiddleCenter,
        normal =
        {
            textColor = Color.white
        }
    };

    public static GUIStyle subTitleStyle = new()
    {
        fontSize = 14,
        fontStyle = FontStyle.Bold,
        normal =
        {
            textColor = Color.white
        },
        alignment = TextAnchor.UpperCenter,
    };

    public static GUIStyle labelStyle = new(GUI.skin.label)
    {
        fontSize = 14,
        alignment = TextAnchor.UpperCenter,
        fontStyle = FontStyle.Bold,
    };

    public static GUIStyle subLabelStyle = new(GUI.skin.label)
    {
        fontSize = 12,
        alignment = TextAnchor.MiddleLeft,
        fontStyle = FontStyle.Bold,
    };
    
    public static GUIStyle subLabelStyleV2 = new(GUI.skin.label)
    {
        fontSize = 12,
        alignment = TextAnchor.MiddleCenter,
        fontStyle = FontStyle.Bold,
    };


    public static GUIStyle foldOutStyle = new(EditorStyles.foldout)
    {
        fontStyle = FontStyle.Bold,
        fontSize = 14,

        onNormal =
        {
            textColor = myStyleColor,
        },
        normal =
        {
            textColor = myStyleColor,
        },
        hover =
        {
            textColor = myStyleColor,
        },
        onHover =
        {
            textColor = myStyleColor,
        },
        focused =
        {
            textColor = myStyleColor,
        },
        onFocused =
        {
            textColor = myStyleColor,
        },
        active =
        {
            textColor = myStyleColor,
        },  
        onActive = 
        {
            textColor = myStyleColor,
        },  
    };
    public static Color myStyleColor = Color.red;
}

using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InotaColorChanger))]
public class InotaColorChangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        InotaColorChanger colorControl = (InotaColorChanger)target;
        if(GUILayout.Button("Change Inota's Color"))
        {
            UnityEngine.Debug.Log("Inota's Color Changed!");
            colorControl.ColorChange();
        }
    }
}


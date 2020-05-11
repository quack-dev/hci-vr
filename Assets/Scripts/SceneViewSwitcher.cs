using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
public class SceneViewSwitcher : MonoBehaviour
{
    public bool useSceneView = false;
    private void Awake()
    {
        if (useSceneView)
            UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
    }
}
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EndMenu : MonoBehaviour
{
    public void Quit()
    {
        #if UNITY_EDITOR
        // Detener la simulación en el editor de Unity
        EditorApplication.isPlaying = false;
        #else
        // Cerrar la aplicación cuando esté en una compilación
        Application.Quit();
        #endif
    }
}

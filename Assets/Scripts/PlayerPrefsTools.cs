using UnityEditor;
using UnityEngine;

public class PlayerPrefsTools
{
    [MenuItem("Tools/Clear PlayerPrefs Keys")]
    private static void ClearPrefs()
    {
        // Example: clear only certain keys
        PlayerPrefs.DeleteKey("HighScore");

        Debug.Log("Selected PlayerPrefs keys cleared!");
    }
}

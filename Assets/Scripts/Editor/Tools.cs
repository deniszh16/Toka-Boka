using UnityEditor;
using UnityEngine;

namespace DZGames.TokaBoka.Editor
{
    public static class Tools
    {
        [MenuItem("Tools/Clear PlayerPrefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace UnityStories 
{
	public static class StoriesUtils
	{
		// Returns the currently selected folder path
		public static string GetSelectedPathOrFallback()
		{
			string path = "Assets";
			
			foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
			{
				path = AssetDatabase.GetAssetPath(obj);
				if (!string.IsNullOrEmpty(path) && File.Exists(path)) 
				{
					path = Path.GetDirectoryName(path);
					break;
				}
			}
			return path;
		}
	}
}
#endif
using UnityEditor;

[InitializeOnLoadAttribute]
public static class PlayRefreshEditor {
 
	static PlayRefreshEditor() {
		EditorApplication.playModeStateChanged += PlayRefresh;
	}
 
	private static void PlayRefresh(PlayModeStateChange state) {
		if(state == PlayModeStateChange.ExitingEditMode) {
			AssetDatabase.Refresh();
		}
	}
	
	[MenuItem("Edit/Run _F5")] // shortcut key F5 to Play (and exit playmode also)
	private static void PlayGame() {
		EditorApplication.ExecuteMenuItem("Edit/Play");
	}
}
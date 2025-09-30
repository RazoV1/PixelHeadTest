using UnityEngine;

public class ComputerGUI : MonoBehaviour
{
	[SerializeField] private Transform fileGrid;
	[SerializeField] private FileSystem fileSystem;


	public Transform GetFileGrid() => fileGrid;

	public void TryCreateNewFile()
	{
		fileSystem.CreateNewFile();
	}

	public void TryDeleteFile()
	{
		fileSystem.DeleteLastFile();
	}

	public void ShowError(string errorText)
	{
		Debug.Log($"FileSystem error: <color=red>{errorText}");
	}
}

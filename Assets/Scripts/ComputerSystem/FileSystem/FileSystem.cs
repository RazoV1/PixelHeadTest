using System.Collections.Generic;
using UnityEngine;

public class FileSystem : MonoBehaviour
{
	private List<GameObject> files = new List<GameObject>();
	[SerializeField] private int maxCapacity = 117;

	[SerializeField] private GameObject filePrefab;
	[SerializeField] private ComputerGUI computerGUI;

	public void CreateNewFile()
	{
		if (files.Count < maxCapacity)
		{
		    GameObject newFile = Instantiate(filePrefab, computerGUI.GetFileGrid());
			files.Add(newFile);
		}
		else
		{
			computerGUI.ShowError("якхьйнл лмнцн тюикнб");
		}
	}

	public void DeleteLastFile()
	{
		if (files.Count > 0)
		{
			Destroy(files[files.Count - 1]);
			files.RemoveAt(files.Count - 1);
		}
		else
		{
			computerGUI.ShowError("мер тюикнб дкъ сдюкемхъ");
		}
	}
}

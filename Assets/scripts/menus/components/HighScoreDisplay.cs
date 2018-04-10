using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreDisplay : MonoBehaviour {

	public string boardName;
	public string format;
	public Text[] fields;

	void Start () 
	{
		for(int index = 0; index < fields.Length; index++)
		{
			if(PlayerPrefs.HasKey(boardName + "_" + index))
			{
				fields[index].text = "" + (index + 1) + ". " + PlayerPrefs.GetInt(boardName + "_" + index).ToString(format);
			}else{
				fields[index].text = "" + (index + 1) + ". " + format;
			}
		}
	}
}

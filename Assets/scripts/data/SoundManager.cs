using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public SoundData[] sounds;

	private Dictionary<string, int> _associations = new Dictionary<string, int>();

	public AudioClip GetSound(string name)
	{
		if(!_associations.ContainsKey(name))
		{
			for(int index = 0; index < sounds.Length; index++)
			{
				if(sounds[index].soundName == name)
				{
					_associations.Add(name, index);
					return sounds[index].clip;
				}
			}

		}else{
			return sounds[_associations[name]].clip;
		}
		throw new KeyNotFoundException("COULDN'T FIND SOUND NAMED: " + name);
	}

}

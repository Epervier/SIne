using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TechTree : MonoBehaviour {
	
	public bool m_bSaveNow = false;
	
	
	public void Initialize()
	{
	}
	
	public void Save()
	{
		//FILE IO
		Debug.Log("Saved Tree!");
	}
	
	public void Update()
	{
		if( m_bSaveNow == true )
		{
			Save ();
			m_bSaveNow = false;
		}
	}
	
}

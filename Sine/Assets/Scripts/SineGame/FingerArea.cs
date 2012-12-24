using UnityEngine;
using System.Collections;

public class FingerArea : MonoBehaviour {
	
	public FingerDisplay m_pDisplay;
	private int m_nIndex;
	
	public void Initialize(int nIndex)
	{
		m_nIndex = nIndex;
	}
	
	public void MoveDisplay(Vector3 pos)
	{
		m_pDisplay.transform.position = pos;
	}
	
	public int GetValue()
	{
		float fBase = this.transform.localPosition.y;
		if( fBase < 0)
			fBase *= -1;
		float fValue = (fBase * 0.5f + m_pDisplay.transform.localPosition.y);
		//Debug.Log(fValue.ToString());
		return (int)( fValue * 0.02f) + 1;
	}
	
}

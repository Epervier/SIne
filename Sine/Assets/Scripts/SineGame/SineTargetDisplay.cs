using UnityEngine;
using System.Collections;

public class SineTargetDisplay : MonoBehaviour {
	
	public Color32 m_pColor;
	public SineTargetDisplayItem m_pTargetItemPrefab;
	public UIGrid m_pGrid;
	public int m_nItemCount = 10;
	
	private SineTargetDisplayItem[] m_pItems;
	
	public void Initialize()
	{
		m_pItems = new SineTargetDisplayItem[m_nItemCount];
		for (int i = 0; i < m_nItemCount; i++) {
			
			GameObject go = NGUITools.AddChild(m_pGrid.gameObject, m_pTargetItemPrefab.gameObject);
			go.name = string.Format("{0:00}. Item", i);
			SineTargetDisplayItem pItem = go.GetComponent<SineTargetDisplayItem>();
			m_pItems[i] = pItem;
			UISprite sprite = pItem.GetComponentInChildren<UISprite>();
			sprite.color = m_pColor;
		}
		m_pGrid.Reposition();
	}
	
	public void SetDisplay(int nCount)
	{
		for (int i = 0; i < m_nItemCount; i++) {
			NGUITools.SetActive(m_pItems[i].gameObject, i < nCount);
		}
		m_pGrid.Reposition();
	}
}

using UnityEngine;
using System.Collections;

public class ColorButton : MonoBehaviour {

    public Color ButtonColor;
    public GameData.ColorNames m_eColor;
	public delegate void OnColorClick(GameData.ColorNames name);
	private OnColorClick m_pHandler;
	
	public void Initialize(OnColorClick click)
	{
		m_pHandler = click;
	}

    public void OnClick()
    {
		if( m_pHandler != null)
		{
			m_pHandler(m_eColor);
		}
    }

    public void UpdateColor()
    {
        //IndexColor = Player.Instance.GetIndex();
        ButtonColor = GameData.GetColorData(m_eColor);

        UISlicedSprite sprite = gameObject.GetComponentInChildren<UISlicedSprite>();
        sprite.color = new Color(ButtonColor.r, ButtonColor.g, ButtonColor.b);
    }

}

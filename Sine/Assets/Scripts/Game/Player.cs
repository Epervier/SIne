using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float 		m_fSize;
    public GameData.ColorNames m_eColor;
    public float 		m_fAbsorptionMultiplier;
    public float 		m_fDamageMultiplier;

    private Transform 	m_fTransform;
    private float 		m_fScale;
	private Game		m_pParent;

    public ColorButton[] Buttons = null;

    public float GetScale()
    {
        return m_fSize;
    }

	// Use this for initialization
	public void Initialize (Game parent) {
		
		m_pParent = parent;
        m_fTransform = this.transform;
        if (m_fSize <= 0)
            m_fSize = 3;

        m_fTransform.localScale = new Vector3(m_fSize, m_fSize, m_fSize);

        ChangeColor( (GameData.ColorNames)(Random.Range(0, (int)GameData.ColorNames.NumColors)) );
		
		for (int i = 0; i < Buttons.Length; i++) {
			if( Buttons[i] != null)
				Buttons[i].Initialize(ChangeColor);
		}

        m_fAbsorptionMultiplier *= GameData.AbsorbtionRate;
	}

    public void UpdateButtons()
    {
        if (Buttons != null)
        {
            foreach (ColorButton button in Buttons)
            {
                if (button != null)
                    button.UpdateColor();
            }
        }
    }

    public void ChangeColor(GameData.ColorNames newColor)
    {
        if (m_pParent.m_bIsPaused)
            return;

        m_eColor = newColor;

        this.gameObject.renderer.material.color = GameData.GetColorData(m_eColor);
        UpdateButtons();
    }

	// Update is called once per frame
	public void UpdatePlayer (float dt) {
		
		m_fTransform.localScale = new Vector3(m_fSize, m_fSize, m_fSize);

        if ( Input.GetKeyDown("q") == true )
        {
            ChangeColor(GameData.ColorNames.Color1);
        }
        if (Input.GetKeyDown("w") == true)
        {
            ChangeColor(GameData.ColorNames.Color2);
        }
        if ( Input.GetKeyDown("e") == true )
        {
            ChangeColor(GameData.ColorNames.Color3);
        }
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            
        }
    }

    public void AbsorbEnemy(GameData.ColorNames targetColor, float scale)
    {
        if (targetColor == this.m_eColor)
        {
            m_fSize += scale * m_fAbsorptionMultiplier;

            if (m_fSize > 20)
            {
                //  WIN
            }
        }
        else
        {
            m_fSize -= scale * m_fDamageMultiplier;

            if (m_fSize <= 0)
            {
                //  GAMEOVER
            }
        }

        m_fTransform.localScale = new Vector3(m_fSize, m_fSize, m_fSize);
    }
}

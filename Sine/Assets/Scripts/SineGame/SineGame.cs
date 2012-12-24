using UnityEngine;
using System.Collections;

public class SineGame : MonoBehaviour {
	
	public SineTargetDisplay m_pPlayerDisplay;
	public SineTargetDisplay m_pTargetDisplay;
	public FingerArea[] m_pAreas;
	
	public float m_fTimerChangeInterval = 3.0f;
	
	private float m_fTargetChangeTimer = 0.0f;
	private int m_nTargetValue = 5;
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < m_pAreas.Length; i++) {
			m_pAreas[i].Initialize(i);
		}
		m_pTargetDisplay.Initialize();
		m_pPlayerDisplay.Initialize();
		
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		m_fTargetChangeTimer -= dt;
		if( m_fTargetChangeTimer <= 0.0f)
		{
			m_nTargetValue = Random.Range(2,10);
			m_pTargetDisplay.SetDisplay(m_nTargetValue);
			m_fTargetChangeTimer = m_fTimerChangeInterval;
		}
		
		int nValue = 0;
		for (int i = 0; i < m_pAreas.Length; i++) {
			nValue += m_pAreas[i].GetValue();
		}
		
		m_pPlayerDisplay.SetDisplay(nValue);
		
	}
	
	void OnEnable()
    {
        FingerGestures.OnFingerMove += FingerGestures_OnFingerMove;	
    }

    void OnDisable()
    {
        FingerGestures.OnFingerMove -= FingerGestures_OnFingerMove;
    }

    #region Per-Finger Event Callbacks
    void FingerGestures_OnFingerMove( int fingerIndex, Vector2 fingerPos )
    {
		GameObject selectedObject = PickObject(fingerPos);
		for (int i = 0; i < m_pAreas.Length; i++) {
			if( selectedObject == m_pAreas[i].gameObject )
			{
				m_pAreas[i].MoveDisplay( new Vector3(0, UICamera.currentCamera.ScreenToWorldPoint(fingerPos).y, 0) );
			}
		}
    }
    #endregion

	// Return the GameObject at the given screen position, or null if no valid object was found
    public static GameObject PickObject( Vector2 screenPos )
    {
        Ray ray = Camera.main.ScreenPointToRay( screenPos );
        RaycastHit hit;

        if( Physics.Raycast( ray, out hit ) )
            return hit.collider.gameObject;

        return null;
    }
}

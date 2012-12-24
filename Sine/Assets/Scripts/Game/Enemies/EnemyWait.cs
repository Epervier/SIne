using UnityEngine;
using System.Collections;

public class EnemyWait : Enemy {
    public float WaitTimeMin;
    public float WaitTimeMax;
    public float WaitDistanceMin;
    public float WaitDistanceMax;
    public float SpeedBoost = 2.5f;
	public float m_fSpeed;
	
	public Vector2 m_vDirection = new Vector2(0,0);
	
    public float m_fWaitTime;
    public float m_fWaitTimeCounter;
    public float m_fWaitDistance;
    public bool m_bHasWaited;
    
    private float m_fActualSpeed;
	private float m_fSpeedScale;
	
//    private Quaternion m_BaseRotation;
//    private Quaternion m_AlteredRotation;
	
	private Vector3 m_vInternalRotation = new Vector3(0,0,0);
	
    public void Start()
    {
//        m_BaseRotation = localRenderer.transform.rotation;
    }
	
	public override void ReInitialize( Vector2 vPosition, float fScale )
    {
        base.ReInitialize(vPosition, fScale);
        
		m_vDirection = m_vTarget - m_vPosition;
		m_vDirection.Normalize();
		
		m_pDisplay.SetRotation( Quaternion.FromToRotation( new Vector3(0, -1, 0), m_vDirection) );
		//m_vInternalRotation = new Vector3(m_vDirection.x, m_vDirection.y, 0);
		
        m_fWaitTime = Random.Range(WaitTimeMin, WaitTimeMax);
        m_fWaitTimeCounter = m_fWaitTime;
        m_fWaitDistance = Random.Range(WaitDistanceMin, WaitDistanceMax);
        m_bHasWaited = false;
		m_fSpeedScale = 1f;
    }

    // Update is called once per frame
    public override void UpdateEnemy(float dt)
    {
     	 base.UpdateEnemy(dt); 
	}
	
	protected override void UpdateNormal (float dt)
	{
		base.UpdateNormal (dt);
		
		Vector2 target = m_vTarget - m_vPosition;
        target = target.normalized;
		
		if ( m_bHasWaited == false )
        {
            if ( m_pTransform.position.magnitude < m_fWaitDistance )
            {
                if (m_fWaitTimeCounter > 0.0F)
                {
                    m_fActualSpeed -= m_fActualSpeed * m_fActualSpeed * dt;
                    m_fWaitTimeCounter -= dt;
                    
					target *= m_fActualSpeed;
                    
					Vector3 vect = m_pTransform.localEulerAngles;
					vect.z = (  180 - 180 * (m_fWaitTimeCounter / m_fWaitTime) );
					m_pTransform.localEulerAngles = vect;
					//m_pDisplay.SetRotation( Quaternion.FromToRotation(new Vector3(0, -1, 0), m_vInternalRotation) );
                }
                else
				{
                    m_bHasWaited = true;
                  	m_fSpeedScale *= SpeedBoost;
                }
            }
            else
            {
      			m_fActualSpeed = m_fSpeed * m_fSpeedScale;
				target *= m_fActualSpeed;
                   
            }
        }
        else
        {
            m_fActualSpeed = m_fSpeed * m_fSpeedScale;
			target *= m_fActualSpeed;
            
        }

//        m_pTransform.Translate(0, dt * m_fActualSpeed, 0);

		m_vPosition += target * dt;
		m_pTransform.localPosition = m_vPosition;
	}

}

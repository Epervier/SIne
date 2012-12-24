using UnityEngine;
using System.Collections;

public class EnemySpiral : Enemy {
	
	public float m_fAngleMin;
	public float m_fAngleMax;
	public float m_fAngle;
	public float m_fAngleRate = Mathf.PI * 0.01f;
	
    protected float m_fSin;
    protected float m_fCos;
	
	private enum SubState { Increase, Decrease, Total};
	private SubState m_eSubState;
	
	override public void Initialize(EnemyManager pManager)
    {
        m_fSin = Mathf.Sin(Mathf.PI);
        m_fCos = Mathf.Cos(Mathf.PI * 2);

        base.Initialize(pManager);
    }

    override public void ReInitialize( Vector2 vPosition, float fScale )
    {
        base.ReInitialize(vPosition, fScale);
		m_fAngle = Mathf.PI;
		m_eSubState = (SubState)( Random.Range(0, (int)SubState.Total) );

    }
	
    // Update is called once per frame
    public override void UpdateEnemy(float dt)
    {
        //float dt = Time.deltaTime;
        base.UpdateEnemy(dt);
		
//        Vector3 target = m_pTransform.position;
//        float fX = target.x * m_fCos - target.y * m_fSin;
//        float fY = target.x * m_fSin + target.y * m_fCos;
//        target.x = fX;
//        target.y = fY;
//        target = target.normalized;
//
//        target.z = 0;
//        m_pTransform.Translate(target * dt * -m_fSpeed);
	}
	
	protected override void UpdateInitialized (float dt)
	{
		base.UpdateInitialized (dt);
	}
	
	protected override void UpdateNormal (float dt)
	{
		base.UpdateNormal (dt);
		
		if( m_eSubState == SubState.Increase )
		{
			m_fAngle += m_fAngleRate * dt;
			if( m_fAngle >= m_fAngleMax )
			{
				m_fAngle = m_fAngleMax;
				m_eSubState = SubState.Decrease;
			}
		}
		else
		{
			m_fAngle -= m_fAngleRate * dt;
			if( m_fAngle <= m_fAngleMin )
			{
				m_fAngle = m_fAngleMin;
				m_eSubState = SubState.Increase;
			}
		}
		
		m_fSin = Mathf.Sin(m_fAngle);
        m_fCos = Mathf.Cos(m_fAngle);
		
        Vector2 target = m_vTarget - m_vPosition;
		
        float fX = target.x * m_fCos - target.y * m_fSin;
        float fY = target.x * m_fSin + target.y * m_fCos;
		
        target.x = target.x * 0.5f + fX * 0.5f;
        target.y = target.y * 0.5f + fY * 0.5f;
        target = target.normalized;
		target *= 10;
		
		m_vPosition += target * dt;
		
		m_pTransform.localPosition = m_vPosition;
	}

    
}
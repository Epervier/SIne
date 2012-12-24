using UnityEngine;
using System.Collections;

public class EnemyZigZag : Enemy {
	
	public float m_fZigMaxTime;
	
	private enum SubState { Left, Right };
	private SubState m_eSubState;
	
	protected float m_fSin;
    protected float m_fCos;
	
	private float m_fZigTimer;
	private Vector2 m_vZigStart = new Vector2(0,0);
	
	public override void ReInitialize( Vector2 vPosition, float fScale )
    {
        base.ReInitialize(vPosition, fScale);

        Vector2 vDirection = m_vTarget - m_vPosition;
		vDirection.Normalize();
        
        m_pTransform.localRotation = Quaternion.FromToRotation(new Vector3(0,1,0), -1 * vDirection);
		m_fZigTimer = m_fZigMaxTime;
    }
	
	// Update is called once per frame
    public override void UpdateEnemy(float dt)
    {
        base.UpdateEnemy(dt);
	}
	
	protected override void UpdateNormal (float dt)
	{
		base.UpdateNormal (dt);
		
		m_fZigTimer -= dt;
		if( m_fZigTimer < 0 )
		{
			m_eSubState = m_eSubState == SubState.Left ? SubState.Right : SubState.Left;
			m_fZigTimer = m_fZigMaxTime;
			m_vZigStart.x = m_vPosition.x;
			m_vZigStart.y = m_vPosition.y;
		}
		
		float fAngle = m_eSubState == SubState.Left ? -Mathf.PI * 0.25f : Mathf.PI * 0.25f;
		
		m_fSin = Mathf.Sin( fAngle );
        m_fCos = Mathf.Cos( fAngle );
		
        Vector2 target = m_vTarget - m_vZigStart;
		
        float fX = target.x * m_fCos - target.y * m_fSin;
        float fY = target.x * m_fSin + target.y * m_fCos;
		
        target.x = target.x * 0.5f + fX * 0.5f;
        target.y = target.y * 0.5f + fY * 0.5f;
		
        target = target.normalized;
		target *= 10;
		
		m_vPosition += target * dt;//(m_fZigMaxTime - m_fZigTimer);
		
		m_pTransform.localPosition = m_vPosition;
	}
	
}

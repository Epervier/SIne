using UnityEngine;
using System.Collections;

public class EnemyRush : Enemy {
	
	public Vector2 m_vDirection = new Vector2(0,0);
	public override void ReInitialize( Vector2 vPosition, float fScale )
    {
        base.ReInitialize(vPosition, fScale);

        m_vDirection = m_vTarget - m_vPosition;
		m_vDirection.Normalize();
        
        m_pTransform.localRotation = Quaternion.FromToRotation(new Vector3(0,1,0), -1 * m_vDirection);
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
		target *= 10;
		
		m_vPosition += target * dt;
		
		m_pTransform.localPosition = m_vPosition;
	}
	
}

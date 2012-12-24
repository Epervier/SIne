using UnityEngine;
using System.Collections;

public class EnemyDisplay : MonoBehaviour {
	public MeshRenderer m_pMesh;
	
	public delegate void OnCollision(Collider other);
	private OnCollision m_pOnCollision;

	public void Initialize(OnCollision pCollision)
	{
		if( m_pMesh == null)
			m_pMesh = GetComponentInChildren<MeshRenderer>();
		m_pOnCollision = pCollision;
	}
	
	public void SetColor( Color pColor)
	{
		if( m_pMesh == null)
			return;
		
		m_pMesh.material.color = pColor;
	}
	
	public void SetSize(float fSize)
	{
		this.transform.localScale = new Vector3(fSize, fSize, fSize);
	}
	
	public void SetRotation(Quaternion rotation)
	{
		//this.transform.localRotation.eulerAngles = rotation;
		this.transform.localRotation = rotation;
	}
	
	protected virtual void OnTriggerEnter(Collider other) 
    {
		if( m_pOnCollision != null)
			m_pOnCollision(other);
	}
}

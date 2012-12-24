using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float Speed;
    public float Lifetime;
    public static float fCost = 0.0f;

    private Vector3 m_vDirection;

//    private Transform m_Transform;
    private float m_Size;
    private float m_fTimeAlive;

	// Use this for initialization
	void Start () {
//        m_Transform = this.transform;

        m_vDirection = new Vector3(0, 0, 1);
        m_vDirection.Normalize();
        m_fTimeAlive = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //m_Transform.Translate(m_vDirection * Time.deltaTime * Speed);
        m_fTimeAlive += Time.deltaTime;
        
        if (m_fTimeAlive > Lifetime)
            Destroy(this.gameObject);
	}

}

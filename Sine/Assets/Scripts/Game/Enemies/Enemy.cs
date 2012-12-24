using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float m_fSpawnDelay = 3f;
	public float m_fSpawnCounter = -1f;
	public float m_fTouchTime = 1.5f;

	public enum State {Initialized, TransitionIn, Normal, Touched, Dead, TransitionOut};

	protected Transform m_pTransform;
	protected float m_fSize = 1.0f;
	protected Vector2 m_vPosition = new Vector2(0,0);
	protected Vector2 m_vVelocity = new Vector2(0,0);
	protected Vector2 m_vAcceleration = new Vector2(0,0);
	protected Vector2 m_vTarget = new Vector2(0,0);
	
	public State m_eState = State.Initialized;
	protected GameData.ColorNames m_eColor = GameData.ColorNames.Color1;
	
	protected EnemyDisplay m_pDisplay = null;
    protected EnemyManager m_pManager = null;

	public virtual void Initialize(EnemyManager pManager)
    {
		m_pManager = pManager;
        m_pTransform = gameObject.transform;
		m_pDisplay = gameObject.GetComponentInChildren<EnemyDisplay>();
		m_pDisplay.Initialize(OnCollision);
		
		m_fSpawnDelay *= GameData.SpawnDelay;
		
		m_pManager.ReinitEnemy(this);
    }
	
	public virtual void ReInitialize( Vector2 vPosition, float fScale )
    { 
        SetColors();
		
		m_vPosition = vPosition;
		m_pTransform.localPosition = m_vPosition;
		m_fSize = fScale;
		
		m_vTarget = new Vector2(0,0);
		m_vVelocity = new Vector2(0,0);
		m_vAcceleration = new Vector2(0,0);
		
        m_fSpawnCounter = Random.Range(1, m_fSpawnDelay);
		SetState(State.Initialized);
    }
		
	// Update is called once per frame
	public virtual void UpdateEnemy(float dt)
    {
        //float dt = Time.deltaTime;
		switch (m_eState) {
		case State.Initialized:
			UpdateInitialized(dt);
			break;
		case State.TransitionIn:
			UpdateTransitionIn(dt);
			break;
		case State.Normal:
			UpdateNormal(dt);
			break;
		case State.Touched:
			UpdateTouched(dt);
			break;
		case State.TransitionOut:
			UpdateTransitionOut(dt);
			break;
		case State.Dead:
			UpdateDead(dt);
			break;
		default:
		break;
		}
		
		if( m_pDisplay != null)
			m_pDisplay.SetSize(m_fSize);

	}
	
	protected virtual void UpdateInitialized(float dt)
	{
		if (m_fSpawnCounter > 0)
        {
            m_fSpawnCounter -= dt;
        }
        else
        {
            SetState(State.Normal);
        }
	}
	
	protected virtual void UpdateTransitionIn(float dt)
	{
	}
	
	protected virtual void UpdateTransitionOut(float dt)
	{
	}
	
	protected virtual void UpdateNormal(float dt)
	{
	}
	
	protected virtual void UpdateTouched(float dt)
	{
	}
	
	protected virtual void UpdateDead(float dt)
	{
	}
	
    protected virtual void OnCollision(Collider other) 
    {
        if (other.tag == "player")
        {
            Player player = (Player)other.GetComponent<Player>();
            player.AbsorbEnemy(m_eColor, m_fSize);
            OnDeath();
        }
        if (other.tag == "bullet")
        {
//            OnDeath();
//            Destroy(other.gameObject); 
        }
		if( other.tag == "enemy" )
		{
			
		}
    }
	
	public virtual void OnTouch()
	{
		SetState(State.Touched);
	}

    public virtual void OnDeath()
    {
        m_pManager.ReinitEnemy(this);
    }
	
    protected virtual void SetColors()
    {
        m_eColor = m_pManager.GetColor();
        Color color = m_pManager.GetColor(m_eColor);
        
		if( m_pDisplay != null)
        	m_pDisplay.SetColor(color);
    }
	
	protected virtual void SetState(State eState)
	{
		m_eState = eState;
		
		if( m_eState == State.Initialized || m_eState == State.Dead)
		{
			this.gameObject.layer = LayerMask.NameToLayer("Disabled");
		}
		if( m_eState == State.Normal )
		{	
			this.gameObject.layer = LayerMask.NameToLayer("Game");
		}
	}
	
	protected virtual void SetScale(float fScale)
	{
		m_fSize = fScale;
		m_pDisplay.SetSize(fScale);
		
	}
	
}

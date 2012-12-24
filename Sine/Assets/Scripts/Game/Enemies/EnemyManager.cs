using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public Enemy[] EnemyPrefab;

    public float Interval;
    public float IntervalInterval;
    public float IntervalMinimum;
    
	public float EnemySpeedMultiplier = 1.0f;
    public float SpawnMin = 20;
    public float SpawnMax = 40;
	
	private float m_fPlayerSize = 1.0f;
    private float m_fTime;
    private Enemy[] enemyList = null;
	private Game m_pParent;

	// Use this for initialization
	public void Initialize (Game pParent) {
		m_pParent = pParent;
        m_fTime = 0.0f;

        SpawnMax *= GameData.SpawnDistance;
        SpawnMin *= GameData.SpawnDistance;
        Interval *= GameData.SpawnRate;
        EnemySpeedMultiplier *= GameData.EnemySpeed;

        enemyList = new Enemy[10];

        Enemy[] initialEnemies = this.GetComponentsInChildren<Enemy>();
        foreach (Enemy enemy in initialEnemies)
        {
            if (enemy != null)
            {
                enemy.Initialize(this);
                AddEnemy(enemy);
            }
        }
	}

    private void AddEnemy(Enemy enemy)
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
       		if (enemyList[i] == null)
            {
                enemyList[i] = enemy;
                break;
            }
        }
    }
	
	public void SetPlayerSize(float fSize)
	{
		m_fPlayerSize = fSize;
	}
	
	public void UpdateEnemies(float dt) {

        m_fTime += dt;
        if (m_fTime > Interval)
        {
            Interval += IntervalInterval;
            if (IntervalMinimum > Interval)
                Interval = IntervalMinimum;

            SpawnEnemy();
			
            m_fTime = 0.0f;
        }
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i] == null)
                continue;
            enemyList[i].UpdateEnemy(dt);
        }
       
	}
	
	public Enemy SpawnEnemy()
	{
		Debug.Log("Spawn!");
        int nType = Random.Range(0, EnemyPrefab.Length);
        
		GameObject go = NGUITools.AddChild(this.gameObject, EnemyPrefab[nType].gameObject);
        Enemy enemy = go.GetComponent<Enemy>();
        enemy.Initialize(this);
        AddEnemy(enemy);
		
		return enemy;
	}
	
	public void ReinitEnemy(Enemy pEnemy)
	{
		pEnemy.ReInitialize(GetSpawnPosition(), Random.Range(2f, 5f) );
	}

    public Vector3 GetSpawnPosition()
    {
        float fRadius = Random.Range(SpawnMin, SpawnMax) + m_fPlayerSize;
        float fAngle = Random.Range(0.0f, 360.0f);

        Vector2 vect = new Vector3(Mathf.Sin(fAngle), Mathf.Cos(fAngle), 0);
        vect.Normalize();

        return vect * fRadius;
    }

    public Color GetColor(GameData.ColorNames name)
    {
		//nIndex = Mathf.Clamp(nIndex, 0, (int)GameData.ColorNames.NumColors - 1);
        return GameData.GetColorData( name );
    }

    public GameData.ColorNames GetColor()
    {
        return (GameData.ColorNames)(Random.Range(0, (int)GameData.ColorNames.NumColors));
    }
	
    public float GetSpeedScale()
    {
        return EnemySpeedMultiplier;
    }
}

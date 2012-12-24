using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public float 			ScaleToWin = 20;
    public float 			ScaleToLose = 1;
	public bool 			m_bIsPaused = false;
	
    public GameObject 		SideBar = null;
   	public Camera 			m_pCamera;
    public UILabel 			EndGameLabel = null;
	
	public Color[]			m_pColors;
	
    private Player 			m_pPlayer = null;
	private EnemyManager 	m_pEnemyManager = null;

    private float 			m_fScale = 3;

    private float 			m_fTransitionTimeLeft = 3.0f;
    private float 			m_fTransitionTime = 3.0f;
    private bool 			m_bTransition = false;

    public GameData.GameMode m_nCurrentGameMode = GameData.GameMode.Normal;

    void Start()
    {
		for (int i = 0; i < (int)GameData.ColorNames.NumColors; i++) {
			GameData.SetColorData( (GameData.ColorNames)i, m_pColors[i]);
		}
		
        EndGameLabel.gameObject.SetActiveRecursively(false);

        ScaleToWin *= GameData.WinSize;
        m_nCurrentGameMode = GameData.nextGameMode;
		
		m_pEnemyManager = GetComponentInChildren<EnemyManager>();
		m_pEnemyManager.Initialize(this);
		
		m_pPlayer = GetComponentInChildren<Player>();
		m_pPlayer.Initialize(this);
		
        switch (m_nCurrentGameMode)
        {
            case GameData.GameMode.Normal:
                break;
            case GameData.GameMode.Endless:
                ScaleToWin = 100;
                ScaleToLose = -100;
                break;
            case GameData.GameMode.Progression:
                break;
            case GameData.GameMode.Reverse:
                break;
            default:
                break;
        }
    }

    public void PauseGame()
    {
        m_bIsPaused = true;
    }

    public void UnPauseGame()
    {
        m_bIsPaused = false;
    }

	// Update is called once per frame
	void Update () 
	{
        if ( m_bIsPaused )
        {
            return;
        }
		
		float dt = Time.deltaTime;

        if (m_bTransition)
        {
            m_fTransitionTimeLeft -= dt;
            if (m_fTransitionTimeLeft < 0)
            {
                Application.LoadLevel("MainMenu");
            }
        }
        else
        {
			UpdateScale();
			m_pEnemyManager.SetPlayerSize(m_fScale);
			m_pEnemyManager.UpdateEnemies(dt);
			
			m_pPlayer.UpdatePlayer(dt);
			
            switch (m_nCurrentGameMode)
            {
                case GameData.GameMode.Normal:
                    UpdateNormal(dt);
                    break;
                case GameData.GameMode.Endless:
                    UpdateEndless(dt);
                    break;
                case GameData.GameMode.Progression:
                    UpdateProgression(dt);
                    break;
                case GameData.GameMode.Reverse:
                    UpdateReverse(dt);
                    break;
                default:
                    break;
            }
        }
	}

    private void UpdateNormal(float dt)
    {
		
    }

    private void UpdateEndless(float dt)
    {
        if (m_fScale < 1)
        	m_pPlayer.m_fSize = 1;
    }

    private void UpdateProgression(float dt)
    {
	}

    private void UpdateReverse(float dt)
    {
    }

    private void StartGameOver(bool Won)
    {
        m_fTransitionTimeLeft = m_fTransitionTime;
        m_bTransition = true;
		
        EndGameLabel.gameObject.SetActiveRecursively(true);
        if (Won)
        {
			EndGameLabel.text = "You Win!  Good Job!";
        }
        else
        {
            EndGameLabel.text = "You Lose, thats a shame.";
        }
    }

    private void UpdateScale()
    {
        m_fScale = m_pPlayer.GetScale();

        if (m_fScale > ScaleToWin)
        {
            StartGameOver(true);
        }
        else if (m_fScale < ScaleToLose)
        {
            StartGameOver(false);
        }
    }

}

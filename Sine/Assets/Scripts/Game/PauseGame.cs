using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    public Game game = null;
    public UIPanel pausePanel = null;
    public bool bPauseGame = true;

    public void OnClick()
    {
        if (bPauseGame)
        {
            game.PauseGame();
            pausePanel.gameObject.SetActiveRecursively(true);
        }
        else
        {
            game.UnPauseGame();
            pausePanel.gameObject.SetActiveRecursively(false);
        }
    }
}

using UnityEngine;
using System.Collections;

public class OpenScene : MonoBehaviour {

    public string SceneName = "default";
    public string FunctionToCall = "default";

    public void OnClick()
    {
        switch (FunctionToCall)
        {
            case "GameModeEndless":
                GameModeEndless();
                break;
            case "GameModeQuick":
                GameModeQuick();
                break;
            case "GameModeProgression":
                GameModeProgression();
                break;
            default:
                break;
        }

        Application.LoadLevel(SceneName);
    }

    public void GameModeEndless()
    {
        GameData.nextGameMode = GameData.GameMode.Endless;
    }

    public void GameModeQuick()
    {
        GameData.nextGameMode = GameData.GameMode.Normal;
    }

    public void GameModeProgression()
    {
        GameData.nextGameMode = GameData.GameMode.Progression;
    }
}

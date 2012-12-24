using UnityEngine;
using System.Collections;

public class GameDataObserver : MonoBehaviour {

    public enum Difficulties { Easy = 0, Normal, Hard, CrazyHard }
    public string[] Selections = { "Easy", "Normal", "Hard", "Crazy Hard" };

    public float[] DataEasy = new float[6];
    public float[] DataNormal = new float[6];
    public float[] DataHard = new float[6];
    public float[] DataCrazyHard = new float[6];

    public UISlider[] Sliders;

    public void OnSelectionChange(string selection)
    {
        Debug.Log("Changin selection");
        if (selection == Selections[0])
        {
            DifficultyEasy();
        }
        else if (selection == Selections[1])
        {
            DifficultyNormal();
        }
        else if (selection == Selections[2])
        {
            DifficultyHard();
        }
        else if (selection == Selections[3])
        {
            DifficultyCrazyHard();
        }

    }

    public void UpdateSliders(float[] data)
    {
        for (int i = 0; i < Sliders.Length; i++)
        {
            Sliders[i].sliderValue = data[i];
            Sliders[i].ForceUpdate();
        }
    }

    public void DifficultyEasy()
    {
        GameData.SetData(DataEasy);
        UpdateSliders(DataEasy);
    }

    public void DifficultyNormal()
    {
        GameData.SetData(DataNormal);
        UpdateSliders(DataNormal);
    }

    public void DifficultyHard()
    {
        GameData.SetData(DataHard);
        UpdateSliders(DataHard);
    }

    public void DifficultyCrazyHard()
    {

        GameData.SetData(DataCrazyHard);
        UpdateSliders(DataCrazyHard);
    }

    public void SliderChangeEnemySpeed(float fValue)
    {
        GameData.EnemySpeed = fValue;
    }

    public void SliderChangeAbsorptionRate(float fValue)
    {
        GameData.AbsorbtionRate = fValue;
    }

    public void SliderChangeWinSize(float fValue)
    {
        GameData.WinSize = fValue;
    }

    public void SliderChangeSpawnDelay(float fValue)
    {
        GameData.SpawnDelay = fValue;
    }

    public void SliderChangeSpawnRate(float fValue)
    {
        GameData.SpawnRate = fValue;
    }

    public void SliderChangeSpawnDistance(float fValue)
    {
        GameData.SpawnDistance = fValue;
    }


}

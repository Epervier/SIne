using UnityEngine;
using System.Collections;

public static class GameData : object {
    public enum GameSettings {EnemySpeed = 0, AbsorptionRate, WinSize, SpawnDelay, SpawnRate, SpawnDistance }
	public enum GameMode { Normal = 0, Endless, Reverse, Progression };
    public enum ColorNames { Color1, Color2, Color3, NumColors };
	
    public static GameMode nextGameMode = GameMode.Normal;
	
	private static Color[] ColorValue = { new Color(0,0,0,1), new Color(1,1,1,1), new Color(0.5f,0.5f,0.5f,1) };
	
	private static float fEnemySpeed = 1;
    private static float fAbsorbtionRate = 1;
    private static float fWinSize = 1;
    private static float fSpawnDelay = 1;
    private static float fSpawnRate = 1;
    private static float fSpawnDistance = 1;

    private static float fEnemySpeedBase = 0.5f;
    private static float fAbsorbtionRateBase = 0.5f;
    private static float fWinSizeBase = 0.5f;
    private static float fSpawnDelayBase = 0.5f;
    private static float fSpawnRateBase = 0.5f;
    private static float fSpawnDistanceBase = 1.0f;

    #region Properties
	public static void SetColorData(ColorNames name, Color color)
	{
		if( name != ColorNames.NumColors) 
			ColorValue[(int)name] = color;
	}
	
	public static Color GetColorData(ColorNames name)
	{
		Color result = new Color(1,1,1,1);
		if( name != ColorNames.NumColors )
		{
			result.r = ColorValue[(int)name].r;
			result.g = ColorValue[(int)name].g;
			result.b = ColorValue[(int)name].b;
			result.a = ColorValue[(int)name].a;
		}
		return result;
	}
	
    public static float EnemySpeed 
    {
        get { return fEnemySpeed; }
        set { fEnemySpeed = fEnemySpeedBase + value; }
    }

    public static float AbsorbtionRate
    {
        get { return fAbsorbtionRate; }
        set { fAbsorbtionRate = fAbsorbtionRateBase + value; }
    }

    public static float WinSize
    {
        get { return fWinSize; }
        set { fWinSize = fWinSizeBase + value; }
    }

    public static float SpawnDelay
    {
        get { return fSpawnDelay; }
        set { fSpawnDelay = fSpawnDelayBase + value; }
    }

    public static float SpawnRate
    {
        get { return fSpawnRate; }
        set { fSpawnRate = fSpawnRateBase + value; }
    }

    public static float SpawnDistance
    {
        get { return fSpawnDistance; }
        set { fSpawnDistance = fSpawnDistanceBase + value; }
    }

    #endregion

    public static void SetData(float[] data)
    {
        if (data.Length != 6)
            return;

        EnemySpeed = data[0];
        AbsorbtionRate = data[1];
        WinSize = data[2];
        SpawnDelay = data[3];
        SpawnRate = data[4];
        SpawnDistance = data[5];
    }
}

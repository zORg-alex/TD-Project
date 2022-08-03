using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TDCreepWaves")]
public class TDCreepWaves : ScriptableObject
{
    static TDCreepWaves instance;
    public static TDCreepWaves Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<TDCreepWaves>(typeof(TDCreepWaves).ToString());
            }
            return instance;
        }
    }

    public List<TDCreepWave> Waves;
    public float timeBetween = 15f;
}

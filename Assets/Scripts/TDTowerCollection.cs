using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TDTowerCollection")]
public class TDTowerCollection : ScriptableObject
{
    static TDTowerCollection instance;
    public static TDTowerCollection Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<TDTowerCollection>(typeof(TDTowerCollection).ToString());
            }
            return instance;
        }
    }
    public List<TDTowerSO> TowerCollection;
}

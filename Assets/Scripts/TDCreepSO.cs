using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TD Creep")]
public class TDCreepSO : ScriptableObject
{
	public float HP = 100;
	public float Damage = 1f;
	public float Speed = 1f;
	public float Diameter = 1f;
	public TDCreep prefab;
}

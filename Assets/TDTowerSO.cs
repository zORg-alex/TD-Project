using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TD Tower")]
public class TDTowerSO : ScriptableObject
{
	public GameObject prefab;
	
	[Space]
	public float MaxDistance;
	public float MinDistance;
	public float PauseBetweenShots;
	public float BurstRechargeTime = 2f;
	public int BurstShotCount = 1;
	public bool IsSplash;
	public float SplashRadius;
	public bool IsRectSplash;
	public Vector2 RectSplash;

	[Space]
	public float Damage;
}
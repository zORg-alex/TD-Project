using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatUEvent : UnityEvent<float> { }
public class CastleScript : MonoBehaviour
{
    static CastleScript _instance;
    public static CastleScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CastleScript>();
            }
            return _instance;
        }
    }


    public Action OnDeath;

	public float MaxHealth = 100;
	public float Health = -1;
    public FloatUEvent OnHealthChanged = new FloatUEvent();

	void Start()
	{
		if (Health == -1)
			Health = MaxHealth;
        OnHealthChanged?.Invoke(Health);
	}


    public static void ApplyDamage(float damage) => Instance.ApplyDamage_(damage);
	public void ApplyDamage_(float damage)
	{
        Health -= damage;
        Health = Mathf.Max(0, Instance.Health);
        OnHealthChanged?.Invoke(Health);
        if (Health <= 0) OnDeath?.Invoke();
	}

	void OnTriggerEnter(Collider other)
	{
        if (other.GetComponent<TDCreep>() is TDCreep creep)
            creep.DamageCastle();
	}
}

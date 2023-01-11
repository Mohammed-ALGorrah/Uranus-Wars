using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/TowerSO")]

public class TowerSO : ScriptableObject
{
	public string Name;
	public Sprite Image;
	public TowerType Type;
	public int price;
	public float power;
	public float maxHealth;
	public float fireRate;
	public int maxCapacity;
	public int currentCapacity;
	public float maxRange;
	public Bullet bulletPrefab;
	
}

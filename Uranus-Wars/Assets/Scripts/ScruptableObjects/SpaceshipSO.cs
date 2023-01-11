using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/SpaceshipSO")]

public class SpaceshipSO : ScriptableObject
{
	public string Name;
	public Sprite Image;
	public SpaceshipType Type;
	public float speed;
	public int price;
	public float power;
	public float maxHealth;
	public float fireRate;
	public int maxCapacity;
	public int currentCapacity;
	public float maxRange;
	public Bullet bulletPrefab;
		
}

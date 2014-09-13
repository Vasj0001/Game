using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {
	public int _damage;
	public int _vitality;
	public int _agility;
	public int _intelligence;
	private HeroControllerScript hcs;
	private Transform Player;
	public Texture2D Icon;
	public GameObject ItemObject;
	private Inventory inv;
	
	void Start(){
		gameObject.name = gameObject.name.Replace("(Clone)", "");
		Player = GameObject.FindWithTag("Player").transform;
		hcs = Player.GetComponent<HeroControllerScript>();
		inv = Player.GetComponent<Inventory>();
		ItemObject = Resources.Load(gameObject.name) as GameObject;
		inv.all_items[0] = ItemObject;
	}

	public enum rarity{
		Common,
		Rare,
		Mystical,
		Legendary,
		Set
	}
	public enum type{
		Weapon,
		Left_Hand,
		Armor,
		Ring,
		Amulet,
		Potion,
		Quest,
		Other
	}
}

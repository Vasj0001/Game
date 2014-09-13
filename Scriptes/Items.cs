using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {
	public int _damage;
	public int _protection;
	public int _vitality;
	public int _agility;
	public int _intelligence;
	public float _attackSpeed;
	private HeroControllerScript hcs;
	private Transform Player;
	public Texture2D Icon;
	public GameObject ItemObject;
	private Inventory inv;
	public string Sname;
	public type TypeItem;
	public rarity RarityItem;
	private bool state = false;
	
	void Start(){
		gameObject.name = gameObject.name.Replace("(Clone)", "");
		Player = GameObject.FindWithTag("Player").transform;
		hcs = Player.GetComponent<HeroControllerScript>();
		inv = Player.GetComponent<Inventory>();
		ItemObject = Resources.Load(gameObject.name) as GameObject;
		for (int i=0; i<=3; i++) {
			if ((inv.all_items[i] == null) && (state == false)) {
				inv.all_items[i] = ItemObject;
				state = true;
			}
		}
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
		Body,
		Head,
		Boots,
		Hands,
		Ring,
		Amulet,
		Potion,
		Quest,
		Other
	}
}

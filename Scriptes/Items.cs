using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {
	public int _damage;
	public int _protection;
	public int _vitality;
	public int _agility;
	public int _intelligence;
	public int _gold;
	public float _attackSpeed;
	private HeroControllerScript hcs;
	private Transform Player;
	public Texture2D Icon;
	public GameObject ItemObject;
	private Inventory inv;
	public string Sname;
	public type TypeItem;
	public rarity RarityItem;
	public bool state = false;
	public int _price;
	
	void Awake(){
		gameObject.name = gameObject.name.Replace("(Clone)", "");
		Player = GameObject.FindWithTag("Player").transform;
		hcs = Player.GetComponent<HeroControllerScript>();
		inv = Player.GetComponent<Inventory>();
		ItemObject = Resources.Load(gameObject.name) as GameObject;
	}
	void Start(){
		
	}
	void Update(){
		if (Vector2.Distance(Player.position, transform.position)<=1.0f && TypeItem!=type.Gold){
			for (int i=0; i<=3; i++) {
				if ((inv.all_items[i] == null) && (state == false)) {
					inv.all_items[i] = ItemObject;
					state = true;
				}
			}
			if (state){
				Destroy(gameObject);
			}else{
				Debug.Log("Max inv");
			}
		}
		if (Vector2.Distance(Player.position, transform.position)<=1.0f && TypeItem==type.Gold){
			hcs.gold+=_gold;
			Player.GetComponent<Info>().timer = 3.0f;
			Player.GetComponent<Info>().gold = _gold;
			Destroy(gameObject);
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
		Gold,
		Other
	}
}

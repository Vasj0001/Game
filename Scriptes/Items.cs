using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {
	private int _damage;
	private int _vitality;
	private int _agility;
	private int _intelligence;
	private HeroControllerScript hcs;
	private Transform Player;
	public Texture2D Icon;
	
	void Start(){
		hcs = Player.GetComponent<HeroControllerScript>();
	}

	public enum rarity{
		Common,
		Rare,
		Mystical;
		Legendary;
		Set
	}
	public enum type{
		Weapon,
		Left Hand,
		Armor,
		Ring,
		Amulet,
		Potion,
		Quest,
		Other
	}
}

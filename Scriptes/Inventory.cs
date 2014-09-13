using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
	public GameObject[] all_items = new GameObject[41];
	private bool stats20 = false; // Надето оружие?
	public bool visibleInv=false;
	
	void Start(){
	
	}
	void Update(){
		if((all_items[20] != null) && (!stats20)) {
			gameObject.GetComponent<HeroControllerScript>()._damageW += all_items[20].GetComponent<Items>()._damage;		
			stats20 = true;
		}
		if((all_items[20] == null) && (stats20)) {
			gameObject.GetComponent<HeroControllerScript>()._damageW = 0;
			stats20 = false;
		}
	}
}
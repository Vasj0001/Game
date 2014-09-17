using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
	public GameObject[] all_items = new GameObject[41];
	public bool stats18 = false;
	public bool stats20 = false; // Надето оружие?
	public bool visibleInv=false;
	
	void Start(){
	
	}
	void Update(){
		if(((gameObject.GetComponent<GUI_Inv>().start==0 && all_items[GetComponent<GUI_Inv>().mouseAtSlot].GetComponent<Items>().TypeItem == Items.type.Weapon) || (all_items[20] != null && (!stats20)))) {
			gameObject.GetComponent<HeroControllerScript>()._damageW += all_items[20].GetComponent<Items>()._damage;
			gameObject.GetComponent<HeroControllerScript>()._vitalityI += all_items[20].GetComponent<Items>()._vitality;
			gameObject.GetComponent<HeroControllerScript>()._agilityI += all_items[20].GetComponent<Items>()._agility;
			gameObject.GetComponent<HeroControllerScript>()._intelligenceI += all_items[20].GetComponent<Items>()._intelligence;
			stats20 = true;
			if (gameObject.GetComponent<GUI_Inv>().start==0)
				gameObject.GetComponent<GUI_Inv>().start=2;
		}
		if(((gameObject.GetComponent<GUI_Inv>().start==1 && all_items[GetComponent<GUI_Inv>().mouseAtSlot].GetComponent<Items>().TypeItem == Items.type.Weapon) || (all_items[20] == null && (stats20)))) {
			gameObject.GetComponent<HeroControllerScript>()._damageW -= all_items[28].GetComponent<Items>()._damage;
			gameObject.GetComponent<HeroControllerScript>()._vitalityI -= all_items[28].GetComponent<Items>()._vitality;
			gameObject.GetComponent<HeroControllerScript>()._agilityI -= all_items[28].GetComponent<Items>()._agility;
			gameObject.GetComponent<HeroControllerScript>()._intelligenceI -= all_items[28].GetComponent<Items>()._intelligence;
			stats20 = false;
			if (gameObject.GetComponent<GUI_Inv>().start==1)
				gameObject.GetComponent<GUI_Inv>().start=0;
		}
		//18
		if(((gameObject.GetComponent<GUI_Inv>().start==0 && all_items[GetComponent<GUI_Inv>().mouseAtSlot].GetComponent<Items>().TypeItem == Items.type.Body) || (all_items[18] != null && (!stats18)))) {
			gameObject.GetComponent<HeroControllerScript>()._damageW += all_items[18].GetComponent<Items>()._damage;
			gameObject.GetComponent<HeroControllerScript>()._vitalityI += all_items[18].GetComponent<Items>()._vitality;
			gameObject.GetComponent<HeroControllerScript>()._agilityI += all_items[18].GetComponent<Items>()._agility;
			gameObject.GetComponent<HeroControllerScript>()._intelligenceI += all_items[18].GetComponent<Items>()._intelligence;	
			stats18 = true;
			if (gameObject.GetComponent<GUI_Inv>().start==0)
				gameObject.GetComponent<GUI_Inv>().start=2;
		}
		if(((gameObject.GetComponent<GUI_Inv>().start==1 && all_items[GetComponent<GUI_Inv>().mouseAtSlot].GetComponent<Items>().TypeItem == Items.type.Body) || (all_items[18] == null && (stats18)))) {
			gameObject.GetComponent<HeroControllerScript>()._damageW -= all_items[26].GetComponent<Items>()._damage;
			gameObject.GetComponent<HeroControllerScript>()._vitalityI -= all_items[26].GetComponent<Items>()._vitality;
			gameObject.GetComponent<HeroControllerScript>()._agilityI -= all_items[26].GetComponent<Items>()._agility;
			gameObject.GetComponent<HeroControllerScript>()._intelligenceI -= all_items[26].GetComponent<Items>()._intelligence;
			stats18 = false;
			if (gameObject.GetComponent<GUI_Inv>().start==1)
				gameObject.GetComponent<GUI_Inv>().start=0;
		}
	}
}
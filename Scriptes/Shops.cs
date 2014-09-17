using UnityEngine;
using System.Collections;

public class Shops : MonoBehaviour {
	public GameObject shop;
	private Rect[] rect = new Rect[4];
	private Inventory inv;
	public GameObject swordSale;
	public UILabel price1;
	public int mouseAtSlot=40;
	
	void Start(){
		inv = gameObject.GetComponent<Inventory>();
	}
	void Update(){
		rect[0]=new Rect(Screen.width/1.143f, Screen.height/3.802f,Screen.width-(Screen.width/1.143f+Screen.width/10.052f),Screen.height-(Screen.height/3.802f+Screen.height/1.447f));
		price1.text=swordSale.GetComponent<Items>()._price.ToString();
		if (mouseAtSlot==0 && Input.GetMouseButtonDown(1)){
			if(gameObject.GetComponent<HeroControllerScript>().gold>=swordSale.GetComponent<Items>()._price){
				for (int i=0; i<=3; i++) {
					if ((inv.all_items[i] == null) && (swordSale.GetComponent<Items>().state == false)) {
						inv.all_items[i] = swordSale.GetComponent<Items>().ItemObject;
						swordSale.GetComponent<Items>().state = true;
					}
				}
				if (swordSale.GetComponent<Items>().state) {
					gameObject.GetComponent<HeroControllerScript>().gold-=swordSale.GetComponent<Items>()._price;
				} else {
					Debug.Log("Max inv");
				}
			}else{
				Debug.Log("Not enough money");
			}
		}
	}
	void OnGUI(){
		if (NGUITools.GetActive(shop))
			GUI.DrawTexture(rect[0], swordSale.GetComponent<Items>().Icon);
	}
	void atShop1(){
		mouseAtSlot=0;
	}
	void outShop(){
		mouseAtSlot=40;
	}
}
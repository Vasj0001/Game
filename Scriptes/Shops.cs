using UnityEngine;
using System.Collections;

public class Shops : MonoBehaviour {
	public GameObject shop;
	private Rect[] rect = new Rect[4];
	private Inventory inv;
	public GameObject swordSale;
	public UILabel price1;
	public GameObject sellMenu;
	private int sellItem;
	
	void Start(){
		inv = gameObject.GetComponent<Inventory>();
		inv.all_items[30]=swordSale.GetComponent<Items>().ItemObject;
	}
	void Update(){
		rect[0]=new Rect(Screen.width/1.143f, Screen.height/3.802f,Screen.width-(Screen.width/1.143f+Screen.width/10.052f),Screen.height-(Screen.height/3.802f+Screen.height/1.447f));
		price1.text=swordSale.GetComponent<Items>()._price.ToString();
		if (gameObject.GetComponent<GUI_Inv>().mouseAtSlot==30 && Input.GetMouseButtonDown(1)){
			if(gameObject.GetComponent<HeroControllerScript>().gold>=swordSale.GetComponent<Items>()._price){
				for (int i=0; i<=3; i++) {
					if ((inv.all_items[i] == null) && (swordSale.GetComponent<Items>().state == false)) {
						inv.all_items[i] = swordSale.GetComponent<Items>().ItemObject;
						swordSale.GetComponent<Items>().state = true;
					}
				}
				if (swordSale.GetComponent<Items>().state) {
					gameObject.GetComponent<HeroControllerScript>().gold-=swordSale.GetComponent<Items>()._price;
					audio.PlayOneShot(gameObject.GetComponent<Sounds>().pickGold,1.0f);
				} else {
					Debug.Log("Max inv");
				}
			}else{
				Debug.Log("Not enough money");
			}
		}
		if(NGUITools.GetActive(shop) && gameObject.GetComponent<GUI_Inv>().mouseAtSlot>=0 && gameObject.GetComponent<GUI_Inv>().mouseAtSlot<=24 && inv.all_items[gameObject.GetComponent<GUI_Inv>().mouseAtSlot]!=null && Input.GetMouseButtonDown(1)){
			sellItem=gameObject.GetComponent<GUI_Inv>().mouseAtSlot;
			NGUITools.SetActive(sellMenu,true);
		}
		if(NGUITools.GetActive(sellMenu) && Input.GetKeyDown(KeyCode.Return))
			sell();
	}
	void OnGUI(){
		if (NGUITools.GetActive(shop))
			GUI.DrawTexture(rect[0], swordSale.GetComponent<Items>().Icon);
	}
	void sell(){
		audio.PlayOneShot(gameObject.GetComponent<Sounds>().pickGold,1.0f);
		gameObject.GetComponent<HeroControllerScript>().gold+=inv.all_items[sellItem].GetComponent<Items>()._price/2;
		inv.all_items[sellItem]=null;
		sellItem=-1;
	}
	void sellYes(){
		sell();
		NGUITools.SetActive(sellMenu,false);
		sellItem=-1;
	}
	void sellNo(){
		NGUITools.SetActive(sellMenu,false);
		sellItem=-1;
	}
}
using UnityEngine;
using System.Collections;

public class GUI_Inv : MonoBehaviour {
	public GUISkin mySkin;
	
	private Rect[] rect = new Rect[24];
	private int mouseAtSlot;
	public int stats;
	private Inventory inv;
	private Rect curs;
	
	void Start(){
		stats = 40;
		mouseAtSlot = 40;
		inv = gameObject.GetComponent<Inventory>();
	}
	void Update(){
		//Debug.Log(mouseAtSlot);
		curs = new Rect(Input.mousePosition.x,Screen.height - Input.mousePosition.y,51,51);
		rect[0] = new Rect(Screen.width/68.0f, Screen.height/2.27f,Screen.width-(Screen.width/68.0f+Screen.width/1.045f),Screen.height-(Screen.height/2.27f+Screen.height/1.97f));
		//17 18 19 20 21 22 23 24
		rect[20] = new Rect(Screen.width/21.0f,Screen.height/3.121f,Screen.width-(Screen.width/21.0f+Screen.width/1.082f),Screen.height-(Screen.height/3.121f+Screen.height/1.584f));
		
		if(NGUITools.GetActive(gameObject.GetComponent<HeroControllerScript>().inventory)) {
			//0
			if (mouseAtSlot==0 && Input.GetMouseButton(0)){
				if(stats == 40)
					stats = 0;
			}
			if (mouseAtSlot==0 && Input.GetMouseButtonUp(0)){
				if(stats != 40 && stats!=0 && inv.all_items[0] == null){
					inv.all_items[0] = inv.all_items[stats];
					inv.all_items[stats] = null;
					stats = 40;
				}else stats = 40; 
			}
			//20
			if (mouseAtSlot==20 && Input.GetMouseButton(0)){
				if(stats == 40)
					stats = 20;
			}
			if (mouseAtSlot==20 && Input.GetMouseButtonUp(0)){
				if(stats != 40 && stats!=20 && inv.all_items[20] == null){
					inv.all_items[20] = inv.all_items[stats];
					inv.all_items[stats] = null;
					stats = 40;
				}else stats = 40; 
			}
		}
	}
	void OnGUI(){
		GUI.skin = mySkin;

		if(NGUITools.GetActive(gameObject.GetComponent<HeroControllerScript>().inventory)) {
		
			if (inv.all_items[0] != null && stats !=0) GUI.DrawTexture(rect[0], inv.all_items[0].GetComponent<Items>().Icon);
			if (inv.all_items[20] != null && stats !=20) GUI.DrawTexture(rect[20], inv.all_items[20].GetComponent<Items>().Icon); //Оружие
			if (stats != 40 && inv.all_items[stats] != null){
				GUI.DrawTexture(curs,inv.all_items[stats].GetComponent<Items>().Icon);
			}
			
			//Подсказки итемов
			if (inv.all_items[mouseAtSlot]!=null && stats == 40){
				GUI.Box(new Rect(Input.mousePosition.x+25,Screen.height - Input.mousePosition.y+10,200,300), "");
				GUI.DrawTexture(new Rect(Input.mousePosition.x+90,Screen.height - Input.mousePosition.y+25,50,50), inv.all_items[mouseAtSlot].GetComponent<Items>().Icon);
				GUI.Label(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+95,180,30), "Name: " + inv.all_items[mouseAtSlot].GetComponent<Items>().Sname);
				//Редкость Предмета
				switch (inv.all_items[mouseAtSlot].GetComponent<Items>().RarityItem) {
					case Items.rarity.Common:
						GUI.contentColor = Color.white;
					break;
					case Items.rarity.Rare:
						GUI.contentColor = Color.blue;
					break;
					case Items.rarity.Mystical:
						GUI.contentColor = Color.magenta;
					break;
					case Items.rarity.Legendary:
						GUI.contentColor = Color.yellow;
					break;	
					case Items.rarity.Set:
						GUI.contentColor = Color.green;
					break;						
				}
				GUI.Label(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+135,180,30), "Rarity: " + inv.all_items[mouseAtSlot].GetComponent<Items>().RarityItem);
				GUI.contentColor = Color.white;
				GUI.Label(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+175,180,30), "Type: " + inv.all_items[mouseAtSlot].GetComponent<Items>().TypeItem);
				//GUI.contentColor = Color.white;
				//Выбор типа предмета
				switch (inv.all_items[mouseAtSlot].GetComponent<Items>().TypeItem) {
					case Items.type.Armor:
						GUI.Label(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215,180,30), "Protection: " + inv.all_items[mouseAtSlot].GetComponent<Items>()._protection);
					break;
				
					case Items.type.Weapon:
						GUI.Label(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215,180,30), "Damage: " + inv.all_items[mouseAtSlot].GetComponent<Items>()._damage);
						GUI.Label(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+255,180,30), "Attack Speed: " + inv.all_items[mouseAtSlot].GetComponent<Items>()._attackSpeed + "aps",GUI.skin.GetStyle("newtext"));
					break;
				}
			}
		}
	}
	void at0(){
		gameObject.GetComponent<GUI_Inv>().mouseAtSlot=0;
	}
	void out0(){
		gameObject.GetComponent<GUI_Inv>().mouseAtSlot=40;
	}
	void at20(){
		gameObject.GetComponent<GUI_Inv>().mouseAtSlot=20;
	}
	void out20(){
		mouseAtSlot=40;
	}
}
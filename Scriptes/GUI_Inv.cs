using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Inv : MonoBehaviour {
	public GUISkin mySkin;
	
	private Rect[] rect = new Rect[24];
	private int mouseAtSlot;
	public int stats;
	private Inventory inv;
	private Rect curs;
	private bool isChecked=false;
	private int isVit=0;
	private	int isAg=0;
	private int isInt=0;
	public List<Rect> eqStats;
	private int countV;
	private int countAg;
	private int countInt;
	
	void Start(){
		stats = 40;
		mouseAtSlot = 40;
		inv = gameObject.GetComponent<Inventory>();
		eqStats = new List<Rect>();
	}
	void Update(){
		//Debug.Log(mouseAtSlot);
		curs = new Rect(Input.mousePosition.x,Screen.height - Input.mousePosition.y,51,51);
		rect[0] = new Rect(Screen.width/68.0f, Screen.height/2.27f,Screen.width-(Screen.width/68.0f+Screen.width/1.045f),Screen.height-(Screen.height/2.27f+Screen.height/1.97f));
		rect[1] = new Rect(Screen.width/21.210f, Screen.height/2.27f,Screen.width-(Screen.width/21.210f+Screen.width/1.082f),Screen.height-(Screen.height/2.27f+Screen.height/1.97f));
		rect[2] = new Rect(Screen.width/12.229f, Screen.height/2.27f,Screen.width-(Screen.width/12.229f+Screen.width/1.122f),Screen.height-(Screen.height/2.27f+Screen.height/1.97f));
		rect[3] = new Rect(Screen.width/8.727f, Screen.height/2.27f,Screen.width-(Screen.width/8.727f+Screen.width/1.165f),Screen.height-(Screen.height/2.27f+Screen.height/1.97f));
		//17 18 19 20 21 22 23 24 +8reserv
		rect[18] = new Rect(Screen.width/64.0f, Screen.height/3.763f,Screen.width-(Screen.width/64.0f+Screen.width/1.044f),Screen.height-(Screen.height/3.763f+Screen.height/1.457f));
		rect[20] = new Rect(Screen.width/21.0f,Screen.height/3.121f,Screen.width-(Screen.width/21.0f+Screen.width/1.082f),Screen.height-(Screen.height/3.121f+Screen.height/1.584f));
		
		if(NGUITools.GetActive(gameObject.GetComponent<HeroControllerScript>().inventory)) {
			//Proverka!
			if (Input.GetMouseButtonUp(0) && stats>=17 && stats<=24){
				if (inv.all_items[mouseAtSlot]==null && (inv.stats18==false || inv.stats20==false))
					inv.all_items[stats+8]=null;
			}
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
			//1
			if (mouseAtSlot==1 && Input.GetMouseButton(0)){
				if(stats == 40)
					stats = 1;
			}
			if (mouseAtSlot==1 && Input.GetMouseButtonUp(0)){
				if(stats != 40 && stats!=1 && inv.all_items[1] == null){
					inv.all_items[1] = inv.all_items[stats];
					inv.all_items[stats] = null;
					stats = 40;
				}else stats = 40; 
			}
			//2
			if (mouseAtSlot==2 && Input.GetMouseButton(0)){
				if(stats == 40)
					stats = 2;
			}
			if (mouseAtSlot==2 && Input.GetMouseButtonUp(0)){
				if(stats != 40 && stats!=2 && inv.all_items[2] == null){
					inv.all_items[2] = inv.all_items[stats];
					inv.all_items[stats] = null;
					stats = 40;
				}else stats = 40; 
			}
			//3
			if (mouseAtSlot==3 && Input.GetMouseButton(0)){
				if(stats == 40)
					stats = 3;
			}
			if (mouseAtSlot==3 && Input.GetMouseButtonUp(0)){
				if(stats != 40 && stats!=3 && inv.all_items[3] == null){
					inv.all_items[3] = inv.all_items[stats];
					inv.all_items[stats] = null;
					stats = 40;
				}else stats = 40; 
			}
			//18
			if (mouseAtSlot==18 && Input.GetMouseButton(0)){
				if(stats == 40)
					stats = 18;
			}
			if (mouseAtSlot==18 && Input.GetMouseButtonUp(0)){
				if(stats != 40 && stats!=18 && inv.all_items[18] == null && inv.all_items[stats].GetComponent<Items>().TypeItem == Items.type.Body){
					inv.all_items[18] = inv.all_items[stats];
					inv.all_items[26] = inv.all_items[stats];
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
				if(stats != 40 && stats!=20 && inv.all_items[20] == null && inv.all_items[stats].GetComponent<Items>().TypeItem == Items.type.Weapon){
					inv.all_items[20] = inv.all_items[stats];
					inv.all_items[28] = inv.all_items[stats];
					inv.all_items[stats] = null;
					stats = 40;
				}else stats = 40; 
			}
		}
		//
		if (mouseAtSlot!=40 && inv.all_items[mouseAtSlot]!=null && stats == 40){
			if(eqStats.Count!=0){
				eqStats[0]=new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+95,180,30);
			}else{
				eqStats.Add(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+95,180,30));
			}
			if(eqStats.Count!=1){
				eqStats[1]=new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+135,180,30);
			}else{
				eqStats.Add(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+135,180,30));
			}
			if(eqStats.Count!=2){
				eqStats[2]=new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+175,180,30);
			}else{
				eqStats.Add(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+175,180,30));
			}
			if(inv.all_items[mouseAtSlot].GetComponent<Items>().TypeItem == Items.type.Body){
				if(eqStats.Count!=3){
					eqStats[3] = new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215,180,30);
				}else{
					eqStats.Add(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215,180,30));
				}
			}
			if(inv.all_items[mouseAtSlot].GetComponent<Items>().TypeItem == Items.type.Weapon){
				if (eqStats.Count!=3){
					eqStats[3]=new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215,180,30);
				}else{
					eqStats.Add(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215,180,30));
				}
				if (eqStats.Count!=4){
					eqStats[4] = new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+255,180,30);
				}else{
					eqStats.Add(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+255,180,30));
				}
			}
			//Vitality
			if(inv.all_items[mouseAtSlot].GetComponent<Items>()._vitality>0 && isVit==0){
				countV = eqStats.Count;
				eqStats.Add(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215+40*(countV-3),180,30));
				isVit=eqStats.Count-1;
			}
			if(isVit!=0){
				eqStats[isVit] = new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215+40*(countV-3),180,30);
			}
			//Agility
			if(inv.all_items[mouseAtSlot].GetComponent<Items>()._agility>0 && isAg==0){
				countAg = eqStats.Count;
				eqStats.Add(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215+40*(countAg-3),180,30));
				isAg=eqStats.Count-1;
			}
			if(isAg!=0){
				eqStats[isAg] = new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y+215+40*(countAg-3),180,30);
			}
			
			//
		}
		if(mouseAtSlot==40){
			eqStats.Clear();
			countV=0;
			countAg=0;
			countInt=0;
			isVit=0;
			isAg=0;
			isInt=0;
		}

	}
	void OnGUI(){
		GUI.skin = mySkin;

		if(NGUITools.GetActive(gameObject.GetComponent<HeroControllerScript>().inventory)) {
		
			if (inv.all_items[0] != null && stats !=0) GUI.DrawTexture(rect[0], inv.all_items[0].GetComponent<Items>().Icon);
			if (inv.all_items[1] != null && stats !=1) GUI.DrawTexture(rect[1], inv.all_items[1].GetComponent<Items>().Icon);
			if (inv.all_items[2] != null && stats !=2) GUI.DrawTexture(rect[2], inv.all_items[2].GetComponent<Items>().Icon);
			if (inv.all_items[3] != null && stats !=3) GUI.DrawTexture(rect[3], inv.all_items[3].GetComponent<Items>().Icon);
			if (inv.all_items[18] != null && stats !=18) GUI.DrawTexture(rect[18], inv.all_items[18].GetComponent<Items>().Icon);
			if (inv.all_items[20] != null && stats !=20) GUI.DrawTexture(rect[20], inv.all_items[20].GetComponent<Items>().Icon); //Оружие
			if (stats != 40 && inv.all_items[stats] != null){
				GUI.DrawTexture(curs,inv.all_items[stats].GetComponent<Items>().Icon);
			}
			
			//Подсказки итемов
			if (inv.all_items[mouseAtSlot]!=null && stats == 40){
				if(eqStats.Count<=5){
					GUI.Box(new Rect(Input.mousePosition.x+25,Screen.height - Input.mousePosition.y+10,200,300), "");
				}else{
					GUI.Box(new Rect(Input.mousePosition.x+25,Screen.height - Input.mousePosition.y+10,200,400), "");
				}
				GUI.DrawTexture(new Rect(Input.mousePosition.x+90,Screen.height - Input.mousePosition.y+25,50,50), inv.all_items[mouseAtSlot].GetComponent<Items>().Icon);
				GUI.Label(eqStats[0], "Name: " + inv.all_items[mouseAtSlot].GetComponent<Items>().Sname);
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
				GUI.Label(eqStats[1], "Rarity: " + inv.all_items[mouseAtSlot].GetComponent<Items>().RarityItem);
				GUI.contentColor = Color.white;
				GUI.Label(eqStats[2], "Type: " + inv.all_items[mouseAtSlot].GetComponent<Items>().TypeItem);
				//GUI.contentColor = Color.white;
				//Выбор типа предмета
				switch (inv.all_items[mouseAtSlot].GetComponent<Items>().TypeItem) {
					case Items.type.Body:
						GUI.Label(eqStats[3], "Protection: " + inv.all_items[mouseAtSlot].GetComponent<Items>()._protection);
					break;
				
					case Items.type.Weapon:
						GUI.Label(eqStats[3], "Damage: " + inv.all_items[mouseAtSlot].GetComponent<Items>()._damage);
						GUI.Label(eqStats[4], "Attack Speed: " + inv.all_items[mouseAtSlot].GetComponent<Items>()._attackSpeed + "aps",GUI.skin.GetStyle("newtext"));
					break;
				}
				if (isVit!=0)
					GUI.Label(eqStats[isVit], "Vitality: " + inv.all_items[mouseAtSlot].GetComponent<Items>()._vitality);
				if (isAg!=0)
					GUI.Label(eqStats[isAg], "Agility: " + inv.all_items[mouseAtSlot].GetComponent<Items>()._agility);
			}
		}
	}
	void at0(){
		gameObject.GetComponent<GUI_Inv>().mouseAtSlot=0;
	}
	void at1(){
		gameObject.GetComponent<GUI_Inv>().mouseAtSlot=1;
	}
	void at2(){
		gameObject.GetComponent<GUI_Inv>().mouseAtSlot=2;
	}
	void at3(){
		gameObject.GetComponent<GUI_Inv>().mouseAtSlot=3;
	}
	void at18(){
		gameObject.GetComponent<GUI_Inv>().mouseAtSlot=18;
	}
	void at20(){
		gameObject.GetComponent<GUI_Inv>().mouseAtSlot=20;
	}
	void out0(){
		mouseAtSlot=40;
	}
}
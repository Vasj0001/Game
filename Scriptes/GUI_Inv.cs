using UnityEngine;
using System.Collections;

public class GUI_Inv : MonoBehaviour {
	
	private Rect[] rect = new Rect[10];
	private int mouseAtSlot=40;
	private int stats;
	private Inventory inv;
	void Start(){
		stats = 40;
		inv = gameObject.GetComponent<Inventory>();
	}
	void Update(){
		rect[0] = new Rect(Screen.width/68.0f, Screen.height/2.27f,Screen.width-(Screen.width/68.0f+Screen.width/1.045f),Screen.height-(Screen.height/2.27f+Screen.height/1.97f));
		//17 18 19 20 21 22 23 24
		rect[20] = new Rect(Screen.width/21.0f,Screen.height/3.121f,Screen.width-(Screen.width/21.0f+Screen.width/1.082f),Screen.height-(Screen.height/3.121f+Screen.height/1.584f));
		
		if (mouseAtSlot==0 && Input.GetMouseButton(0)){
			if(stats == 40)
				stats = 0;
		}
		if (mouseAtSlot==0 && Input.GetMouseButtonUp(0)){
			if(stats != 40 && stats!=0 && inv.all_items[0] == null){
				inv.all_items[0] = inv.all_items[stats];
				inv.all_items[stats] = null;
0				stats = 40;
			}else stats = 40; 
		}
		if (mouseAtSlot==20 && Input.GetMouseButton(0)){
			if(stats == 40)
				stats = 20;
		}
		if (mouseAtSlot==20 && Input.GetMouseButtonUp(0)){
			if(stats != 40 && stats!=20 && inv.all_items[20] == null){
				inv.all_items[20] = inv.all_items[stats];
				inv.all_items[stats] = null;
0				stats = 40;
			}else stats = 40; 
		}
	}
	void OnGUI(){
		if (inv.all_items[0] != null && stats !=0) GUI.DrawTexture(rect[0], inv.all_items[0].GetComponent<Item>().Icon);
		if (inv.all_items[20] != null && stats !=20) GUI.DrawTexture(rect[20], inv.all_items[20].GetComponent<Item>().Icon); //Оружие
		if (stats != 40 && inv.all_items[stats] != null){
			GUI.DrawTexture(Input.mousePosition,inv.all_items[stats].GetComponent<Item>().Icon);
		}
	}
		
	
	void at0(){
		mouseAtSlot=0;
	}
	void out0(){
		mouseAtSlot=40;
	}
	void at20(){
		mouseAtSlot=20;
	}
	void out20(){
		mouseAtSlot=40;
	}
}
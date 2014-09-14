using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {
	private bool checkExp;
	private HeroControllerScript hcs;
	public GUISkin mySkin;
	
	void Start(){
		hcs = gameObject.GetComponent<HeroControllerScript>();
	}
	void Update(){
	
	}
	void OnGUI(){
		GUI.skin = mySkin;
	
		if(checkExp){
			GUI.Box(new Rect(Input.mousePosition.x+10,Screen.height - Input.mousePosition.y-80,250,100), "", GUI.skin.GetStyle("oblako"));
			GUI.contentColor = Color.magenta;
			GUI.Label(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y-55,240,70), "To above level "+ (hcs.LVL+1).ToString() + " left "+ (hcs.MaxXP-hcs.CurXP).ToString() + "XP");
			GUI.contentColor = Color.white;
		}
	}
	void atExp(){
		checkExp=true;
	}
	void outExp(){
		checkExp=false;
	}
}
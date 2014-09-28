using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {
	private bool checkExp;
	private HeroControllerScript hcs;
	public GUISkin mySkin;
	public GUISkin infoSkin;
	public int gold=0;
	public float timer=0;
	private bool audioCheck=false;
	
	void Start(){
		hcs = gameObject.GetComponent<HeroControllerScript>();
	}
	void Update(){
		if (timer>0)timer-=Time.deltaTime;
		if (timer<0)timer = 0;
		if (timer==0){
			audioCheck=false;
			gold=0;
		}
	}
	void OnGUI(){
		GUI.skin = mySkin;
	
		if(checkExp){
			GUI.Box(new Rect(Input.mousePosition.x+10,Screen.height - Input.mousePosition.y-80,250,100), "", GUI.skin.GetStyle("oblako"));
			GUI.contentColor = Color.magenta;
			GUI.Label(new Rect(Input.mousePosition.x+35,Screen.height - Input.mousePosition.y-55,240,70), "To above level "+ (hcs.LVL+1).ToString() + " left "+ (hcs.MaxXP-hcs.CurXP).ToString() + "XP");
			GUI.contentColor = Color.white;
		}
		GUI.skin = infoSkin;
		if(gold!=0){
			GUI.contentColor = Color.yellow;
			GUI.Label(new Rect(Screen.width/2.4f,Screen.height/7.2f,500,150), "You pick up " + gold.ToString() + " gold!");
			GUI.contentColor = Color.white;
		}
		if(gold!=0 && !audioCheck){
			audio.PlayOneShot(gameObject.GetComponent<Sounds>().pickGold,1.0f);
			audioCheck=true;
		}
	}
	void atExp(){
		checkExp=true;
	}
	void outExp(){
		checkExp=false;
	}
}
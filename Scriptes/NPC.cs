using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
	public GameObject Npc1;
	private GameObject Player;
	public GUISkin mySkin;
	
	void Start(){
		Player=GameObject.FindWithTag("Player");
	}
	void Update(){
		if (Npc1.transform.position.y>Player.transform.position.y){
			Npc1.transform.position = new Vector3(Npc1.transform.position.x,Npc1.transform.position.y, 1);
		}else{
			Npc1.transform.position = new Vector3(Npc1.transform.position.x,Npc1.transform.position.y, -1);
		}
		if(Vector2.Distance(Player.transform.position,Npc1.transform.position)<=1.2f && Input.GetKeyDown(KeyCode.E)){
			NGUITools.SetActive(gameObject.GetComponent<Shops>().shop,true);
		}
		if(Vector2.Distance(Player.transform.position,Npc1.transform.position)>1.2f){
			NGUITools.SetActive(gameObject.GetComponent<Shops>().shop,false);
		}
	}
	void OnGUI(){
		GUI.skin=mySkin;
		
		if(Vector2.Distance(Player.transform.position,Npc1.transform.position)<=1.2f){
			Vector3 screenPosition = Camera.main.WorldToScreenPoint(Npc1.transform.position);
			GUI.Label(new Rect(screenPosition.x - 15, Screen.height - screenPosition.y - 65, 100, 20),"Talk E", GUI.skin.GetStyle("Talk"));
		}
	}
}
using UnityEngine;
using System.Collections;

public class Drop : MonoBehaviour {
	public GameObject mob1;
	public GameObject drop1;
	public GameObject mob2;
	public GameObject drop2;
	private AI Aiscript;
	
	void Start(){
	
	}
	void Update(){
		if(mob1.GetComponent<AI>().CurHp==0 && mob1.GetComponent<AI>().life){
			drop1.transform.position=mob1.transform.position + new Vector3(0,-1,0);
		}
		if(mob2.GetComponent<AI>().CurHp==0 && mob2.GetComponent<AI>().life){
			drop2.GetComponent<Items>()._gold=10;
			drop2.transform.position=mob2.transform.position + new Vector3(0,-1,0);
		}
	}	
}
using UnityEngine;
using System.Collections;

public class Drop : MonoBehaviour {
	public GameObject mob1;
	public GameObject drop1;
	private AI Aiscript;
	
	void Start(){
	
	}
	void Update(){
		if(mob1.GetComponent<AI>().CurHp==0 && mob1.GetComponent<AI>().life){
			drop1.transform.position=mob1.transform.position + new Vector3(0,-1,0);
		}
	}	
}
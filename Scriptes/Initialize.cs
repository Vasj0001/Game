using UnityEngine;
using System.Collections;

public class Initialize : MonoBehaviour {
	private Inventory inv;
	public GameObject sword1;
	public GameObject armor1;
	public GameObject armor2;


	void Start(){
		inv = gameObject.GetComponent<Inventory>();
		for (int i=0; i<=3; i++) {
			if ((inv.all_items[i] == null) && (sword1.GetComponent<Items>().state == false)) {
				inv.all_items[i] = sword1.GetComponent<Items>().ItemObject;
				sword1.GetComponent<Items>().state = true;
			}
		}
		if (sword1.GetComponent<Items>().state) {
			Destroy(sword1);
		} else {
			Debug.Log("Max inv");
		}
		for (int i=0; i<=3; i++) {
			if ((inv.all_items[i] == null) && (armor1.GetComponent<Items>().state == false)) {
				inv.all_items[i] = armor1.GetComponent<Items>().ItemObject;
				armor1.GetComponent<Items>().state = true;
			}
		}
		if (armor1.GetComponent<Items>().state) {
			Destroy(armor1);
		} else {
			Debug.Log("Max inv");
		}
		/*
		for (int i=0; i<=3; i++) {
			if ((inv.all_items[i] == null) && (armor2.GetComponent<Items>().state == false)) {
				inv.all_items[i] = armor2.GetComponent<Items>().ItemObject;
				armor2.GetComponent<Items>().state = true;
			}
		}
		if (armor2.GetComponent<Items>().state) {
			Destroy(armor2);
		} else {
			Debug.Log("Max inv");
		}*/
	}
}
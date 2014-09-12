using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour {
	public GameObject itemInfoPanel;
	public int weaponDMG;
	public UILabel itemInfoLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (NGUITools.GetActive(itemInfoPanel)){
			itemInfoLabel.text="Basic Sword\nDamage 2\nAttack Speed 0.5";
			itemInfoPanel.transform.localPosition=new Vector3(Input.mousePosition.x+20,Input.mousePosition.y-720,0);
		}
		if(Input.GetKeyDown(KeyCode.Mouse0))
			NGUITools.SetActive(itemInfoPanel,false);
	}
	void item_InfoF(){
		NGUITools.SetActive(itemInfoPanel,false);
	}
	void item_InfoT(){
		NGUITools.SetActive(itemInfoPanel,true);
	}
}

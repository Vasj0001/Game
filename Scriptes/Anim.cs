using UnityEngine;
using System.Collections;

public class Anim : MonoBehaviour {
	private Animator anim;
	private GameObject player;
	private HeroControllerScript hcs;
	private bool z=true;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		hcs = player.GetComponent<HeroControllerScript>();
		
	}
	
	// Update is called once per frame
	void Update () {

		if(z && !NGUITools.GetActive(hcs.statsMenu)){
			anim.SetTrigger("Stats");
			z=false;
		}
		if(hcs.menuCheck)
			anim.SetTrigger("StatsReverse");
	}
}

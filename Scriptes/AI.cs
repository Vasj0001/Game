//(float)((int)(Vector2.Distance(myTransform.position,target.position)*100.0f))/100.0f
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {
	public Transform target;
	public int moveSpeed;
	private Transform myTransform;
	private bool isFacingRight = false;
	public float range;
	private Animator anim;
	private bool isBlocked = false;
	public LayerMask whatIsBlock;
	public Transform blockCheck;
	private Vector2 pointA;
    private Vector2 pointB;
	public int LVLEnemy;
	public int MaxHp;
	public int CurHp;
	public int Dmg;
	private float cd;
	private float attackTimer;
	int attackHash = Animator.StringToHash("Attack_Pump");
	public GameObject HPEnemyBar;
	private float HPBarLeng;
	public	GUISkin myBar;
	public int height;
	public int width;

	private HeroControllerScript HS;
	public List<Transform> moblist;
	
	// Use this for initialization
	void Awake(){
		myTransform = transform;
	}
	
	void Start () {
		HS = myTransform.GetComponent<HeroControllerScript>;
		attackTimer = 0;
		cd = 2.0f;
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		anim = GetComponent<Animator>();
	}
	
	private void FixedUpdate(){
		pointA = blockCheck.position;
		pointB.x = blockCheck.position.x + 0.9f;
		pointB.y = blockCheck.position.y + 0.2f;
		isBlocked = Physics2D.OverlapArea(pointA, pointB, whatIsBlock);
		anim.SetBool ("Block", isBlocked);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(target.position, myTransform.position);
		if (attackTimer>0){attackTimer-=Time.deltaTime;}
		if (attackTimer<0){attackTimer=0;}
		if (CurHp<=0){
			myTransform.position = new Vector3(8,-3,0);
		}
		
		if (myTransform.position.y>target.position.y){
			myTransform.position = new Vector3(myTransform.position.x,myTransform.position.y, 1);
		}else{
			myTransform.position = new Vector3(myTransform.position.x,myTransform.position.y, -1);
		}
		
		if (Vector2.Distance(myTransform.position,target.position) < range && ((float)((int)(Vector2.Distance(myTransform.position,target.position)*10.0f))/10.0f > 1.2f)){
			anim.SetBool("Walk", true);
			//Debug.Log(Vector3.Distance(myTransform.position,target.position));
			if (myTransform.position.x>=target.position.x){
				if (isFacingRight){
					Flip();
				}
				myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime;
			}else{
				if (!isFacingRight){
					Flip();
				}
				myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;		
			}
			if (myTransform.position.y-target.position.y>0.66f){
				myTransform.position -= myTransform.up * moveSpeed * Time.deltaTime;
			}else if (target.position.y - myTransform.position.y>0.66f){
				myTransform.position += myTransform.up * moveSpeed * Time.deltaTime;		
			}			
		}else{
			anim.SetBool("Walk", false);
		}
		//if (isBlocked){
		//	anim.SetBool("Block", false);
		//	isBlocked=false;
		//	rigidbody2D.AddForce(new Vector2(0, 150));
		//}
		if (Vector2.Distance(myTransform.position,target.position) <= 1.3f){
			anim.SetBool("Walk", false);
			if (attackTimer==0){
				Damage();
				attackTimer=cd;
			}
		}
		HPBarLeng = (float)CurHp/(float)MaxHp;
	}
	
	void OnGUI(){
		GUI.skin = myBar;
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(myTransform.position);
		Rect position = new Rect(screenPosition.x - width, Screen.height - screenPosition.y - height, 60f, 15f);
		GUI.Box(new Rect(screenPosition.x - width, Screen.height - screenPosition.y - height, 60f*HPBarLeng, 15f), " ", GUI.skin.GetStyle("fon")); 
		GUI.TextField(position, CurHp + "/" + MaxHp, 20);
		GUI.Box(position, " ", GUI.skin.GetStyle("Bar"));
	}
	
	void Damage(){
		anim.SetTrigger(attackHash);
		HeroControllerScript hs = (HeroControllerScript)target.GetComponent("HeroControllerScript");
		hs.AddJustCurrHealth(-Dmg);
	}
	
	public void AddJustCurrHealth(int add){
		CurHp+=add;
		if (CurHp<0){CurHp=0;}
		if (CurHp>MaxHp){CurHp=MaxHp;}
	}
	
	private void Flip()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }


    public void Rogonov()
    {
    	private HeroControllerScript ;
    	moblist=HS.targets;
    	moblist.Remove(gameObject.name);

    	float x1=myTransform.x;
    	float y1=myTransform.y;
 
     	float x2=target.x;
     	float y2=target.y;
 
 		foreach(Transform mob in moblist)
 		{
     		float x3=mob.x;
    		float y3=mob.y;

			if (x1*y2*1+x2*y3*1+x3*y1*1-x3*y2*1-x2*y1*1-x1*y3*1==0) and (Vector2.Distance(myTransform.position, target.position)<=2) and (x1<x3)
			{ 
				if y1==y2 
				{
					myTransform.position -= myTransform.up * moveSpeed * Time.deltaTime;
				}

				if x1>=x2 
				{
					myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime;
				}
				else
				{				
					myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;
				}
			}

		}
	}


}
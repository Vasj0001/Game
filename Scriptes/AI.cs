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
	public bool life=true;
	private float timerLife=0;
	private float timerDeath=0;
	private bool check=true;
	//private Rect testRect;

	private HeroControllerScript HS;
	public List<Transform> moblist;
	
	// Use this for initialization
	void Awake(){
		myTransform = transform;
	}
	
	void Start () {
		attackTimer = 0;
		cd = 2.0f;
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		anim = GetComponent<Animator>();
		HS = target.GetComponent<HeroControllerScript>();
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
		//testRect = new Rect(Screen.width/64.0f, Screen.height/3.763f,Screen.width-(Screen.width/64.0f+Screen.width/1.044f),Screen.height-(Screen.height/3.763f+Screen.height/1.457f));
		Debug.DrawLine(target.position, myTransform.position);
		if (attackTimer>0){attackTimer-=Time.deltaTime;}
		if (attackTimer<0){attackTimer=0;}
		if (timerLife>0)
			timerLife-=Time.deltaTime;
		if (timerLife<0){timerLife=0;}
		if (timerDeath>0){timerDeath-=Time.deltaTime;}
		if (timerDeath<0){timerDeath=0;}
		
		if (myTransform.position.y>target.position.y){
			myTransform.position = new Vector3(myTransform.position.x,myTransform.position.y, 1);
		}else{
			myTransform.position = new Vector3(myTransform.position.x,myTransform.position.y, -1);
		}
		if (CurHp>0){
			if (Vector2.Distance(myTransform.position,target.position) < range && ((float)((int)(Vector2.Distance(myTransform.position,target.position)*10.0f))/10.0f > 1.2f))
			{
				anim.SetBool("Walk", true);
				//Debug.Log(Vector3.Distance(myTransform.position,target.position));


				AIWalk(Stena());

			
			}
			else
			{
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
		}else{
			anim.SetBool("Walk", false);
			if (check){
				timerDeath=1.0f;
				timerLife=0.2f;
				check=false;
			}
			if(timerLife==0)
				life=false;
			if (timerDeath==0)
				gameObject.SetActive(false);			
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
		//GUI.Box(testRect, "");
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



	public bool Stena()
    {	
    	bool stena=false;
    	moblist=HS.targets;
    	foreach(Transform mob in moblist)
 		{
 			if (Vector2.Distance(myTransform.position, mob.position)!=0 && Vector2.Distance(myTransform.position, mob.position)<=2)
 			{
 				stena=true;
 			}
 		}
 		return stena;
    }

    public void Rogonov()
    {

    	
    	//moblist.Remove(myTransform);

    	float x1=myTransform.position.x;
    	float y1=myTransform.position.y;
 
     	float x2=target.position.x;
     	float y2=target.position.y;

			if (x1<x2)
			{
				myTransform.position += myTransform.up * moveSpeed * Time.deltaTime;
				myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime;
			}

			if (x1>x2)
			{
				myTransform.position -= myTransform.up * moveSpeed * Time.deltaTime;
				myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;
			}
			if (y1>y2)
			{
				myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;
			}
			if (y1<y2)
			{
				myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime;
			}
	}
	public void Dviz()
	{	
		if (myTransform.position.x>=target.position.x)
		{
			if (isFacingRight)
			{
				Flip();
			}
			myTransform.position -= myTransform.right * moveSpeed * Time.deltaTime;
		}
		else
		{
			if (!isFacingRight)
			{
				Flip();
			}
			myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;		
		}
		if (myTransform.position.y-target.position.y>0.66f)
		{				
			myTransform.position -= myTransform.up * moveSpeed * Time.deltaTime;
		}
		else if (target.position.y - myTransform.position.y>0.66f)
		{
			myTransform.position += myTransform.up * moveSpeed * Time.deltaTime;		
		}			
	}

	public void AIWalk(bool stenaf)
    {
    	bool stena=stenaf;
    	if(stena)
    	{
    		Rogonov();
    	}
    	else
    	{
    		Dviz();
    	}
    }

}
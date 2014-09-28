//(float)((int)(Vector2.Distance(myTransform.position,target.position)*100.0f))/100.0f
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

 
public class AI : MonoBehaviour 
{
	public Transform target;
	private Transform myTransform;

	public int moveSpeed=1;
	public int MaxHp;
	public int CurHp;
<<<<<<< HEAD

    public int Wherep=1;
    public bool Scanp;
    public RaycastHit2D hit;

	private bool isFacingRight=false;

 
    // Use this for initialization
    void Start () 
    {
   		myTransform=transform;
   		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
    }
    private void FixedUpdate()
    {
        Scan();
    }

    void Update () 
    {
        Where();
=======
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
		//testRect = new Rect(Screen.width/1.143f, Screen.height/3.802f,Screen.width-(Screen.width/1.143f+Screen.width/10.052f),Screen.height-(Screen.height/3.802f+Screen.height/1.447f));
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
>>>>>>> origin/master

       // Debug.Log(Wherep);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x+3,transform.position.y+3));
        Debug.DrawLine(transform.position, new Vector2(transform.position.x-3,transform.position.y-3));
        Debug.DrawLine(transform.position, new Vector2(transform.position.x+3,transform.position.y-3));
        Debug.DrawLine(transform.position, new Vector2(transform.position.x-3,transform.position.y+3));


<<<<<<< HEAD
    }
 
    /*Now you will need to override the methods you want to use for the enemy, these can be
     * found in the bottom of the EnemyBehaviour-class*/
 
    public void AMove()
    {

		if ( Math.Round(myTransform.position.y, 1) == Math.Round(target.position.y, 1))
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
		}
		else
		{
			if (myTransform.position.y > target.position.y)
			{				
				myTransform.position -= myTransform.up * moveSpeed * Time.deltaTime;
			}
			else if (myTransform.position.y < target.position.y)
			{
				myTransform.position += myTransform.up * moveSpeed * Time.deltaTime;		
			}
		}		
    }

    public void Move()
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

    public void AddJustCurrHealth(int add)
    {
=======
			
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
>>>>>>> origin/master
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


    private void Where()
    {
        if((target.position.y+target.position.x>myTransform.position.y+myTransform.position.x) && (target.position.y-target.position.x>myTransform.position.y-myTransform.position.x))
        {
            Wherep=4;
        }

        if((target.position.y+target.position.x<myTransform.position.y+myTransform.position.x) && (target.position.y-target.position.x<myTransform.position.y-myTransform.position.x))
        {
            Wherep=2;
        }
        if((target.position.y+target.position.x>myTransform.position.y+myTransform.position.x) && (target.position.y-target.position.x<myTransform.position.y-myTransform.position.x))
        {
            Wherep=1;
        }
        if((target.position.y+target.position.x<myTransform.position.y+myTransform.position.x) && (target.position.y-target.position.x>myTransform.position.y-myTransform.position.x))
        {
            Wherep=3;
        }
    }


    private bool Scan()
    {
        
        switch (Wherep)
        {
            case 1:
            {
                hit=Physics2D.Raycast(transform.position+new Vector3(0.6f,0,0), target.position,3f);
                Debug.DrawLine(transform.position+new Vector3(0.6f,0,0), new Vector2(target.position.x,target.position.y));
            }
            break;
            case 2:
            {
                hit=Physics2D.Raycast(transform.position-new Vector3(0,0.6f,0), target.position,3f);
                Debug.DrawLine(transform.position-new Vector3(0,0.6f,0), new Vector2(target.position.x,target.position.y));
            }
                break;
            case 3:
            {
                hit=Physics2D.Raycast(transform.position-new Vector3(0.6f,0,0), target.position,3f);
                Debug.DrawLine(transform.position-new Vector3(0.6f,0,0), new Vector2(target.position.x,target.position.y));
            }
                break;
            case 4:
            {
                hit=Physics2D.Raycast(transform.position+new Vector3(0,0.6f,0), target.position,3f);
                Debug.DrawLine(transform.position+new Vector3(0,0.6f,0), new Vector2(target.position.x,target.position.y));
            }
                break;
        }

        
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            return true;
        }
        else
        {
            return false;
        }
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
            }

            if (x1>x2)
            {
                myTransform.position -= myTransform.up * moveSpeed * Time.deltaTime;
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



}
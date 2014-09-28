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

       // Debug.Log(Wherep);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x+3,transform.position.y+3));
        Debug.DrawLine(transform.position, new Vector2(transform.position.x-3,transform.position.y-3));
        Debug.DrawLine(transform.position, new Vector2(transform.position.x+3,transform.position.y-3));
        Debug.DrawLine(transform.position, new Vector2(transform.position.x-3,transform.position.y+3));


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
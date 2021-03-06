﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class HeroControllerScript : MonoBehaviour
{
    //переменная для установки макс. скорости персонажа
    private float maxSpeed; 
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;
    //ссылка на компонент анимаций
    private Animator anim;
    //находится ли персонаж на земле или в прыжке?
    private bool isGrounded = false;
	private bool isSky = false;
	private bool isRighted = false;
	private bool isLefted = false;
    //ссылка на компонент Transform объекта
    //для определения соприкосновения с землей
    public Transform groundCheck;
	public Transform skyCheck;
	public Transform rightCheck;
	public Transform leftCheck;
    //радиус определения соприкосновения с землей
    private float groundRadius = 0.2f;
    //ссылка на слой, представляющий землю
    public LayerMask whatIsGround;
	private Transform myPlayer;
	
	public LayerMask whatIsGroundOfXp;
		
	int attackHash = Animator.StringToHash("Attack_War");
	
	public GameObject inventory;
	public GameObject LevelUp;
	public GameObject LevelUpSe;
	public GameObject statsMenu;
	public GameObject openMenu;
	
	public int LVL;
	private string Class;
	public int MaxXP;
	public int CurXP;
	private int MaxHealth; // Максимальное количество здоровья
	private int CurHealth; // Текущее количество здоровья
	private int MaxMana; // Максимально количество маны
	private int CurMana; // Текущее количество маны
	private float HealthReg;
	private float ManaReg;
	private float x;
	private float y;
	private float z;
	
	private float TimerLevel=3.0f;
	private float TimerRegen=1.0f;
	private bool done=false;
	private string PlayerName;
	
	private int Damage;
	private int Vitality; //0.2 регена ХП
	private int Agility;
	private int Intelligence; // 0.2 регена МП
	private int Points;
	private int DamageWW;
	private int VitalityTotal;
	private int AgilityTotal;
	private int IntelligenceTotal;
	public int gold;
	
	public UISlider XP;
	public UILabel XpText;
	public UISlider HP;
	public UILabel HpText;
	public UISlider MP;
	public UILabel MPText;
	public UILabel Level;
	public UILabel goldL;
	
	public UILabel PointsL;
	public UILabel DamageL;
	public UILabel VitalityL;
	public UILabel _VitL;
	public UILabel AgilityL;
	public UILabel _AgL;
	public UILabel IntelligenceL;
	public UILabel _IntL;
	public UILabel DamageWWL;
	public UILabel HPReg;
	public UILabel MPReg;
	public UILabel MS;
	public UILabel Nick;
	public UILabel NickInv;
	private Vector3 mouseOver;
	private float cd=2.0f;
	private float attackTimer=0;
	public List<Transform> targets;
	public List<Transform> targetsInRange;
	//Items
	public int _damageW;
	public int _vitalityI;
	public int _agilityI;
	public int _intelligenceI;
	
	//First Skill
	public float cdFirstSkill;
	private float firstSkillTimer;
	public List<Transform> targetsInFirstSkill;
	public List<Transform> targetsDamagedFirstSkill;
	public GameObject wave;
	private float ttlFirstSkill=0.0f;
	private bool isFacingRightFirstSkill=true;
	public GameObject fonFirstSkill;
	public UILabel cdFirstSkillText;
	//Second Skill
	public float cdSecondSkill;
	private float SecondSkillTimer;
	public List<Transform> targetsInSecondSkill;
	public List<Transform> targetsDamagedSecondSkill;
	private float ttlSecondSkill=0.0f;
	private bool isFacingRightSecondSkill;
	public GameObject fonSecondSkill;
	public UILabel cdSecondSkillText;
	//
	public Transform selectedTarget;
	private GameObject[] enemies;
	private AI AIscript;
	private Items ItemsScript;
	private bool block=true;
	private float menuTimer=0;
	public bool menuCheck=false;
	public GameObject rightPartSys;
	public GameObject leftPartSys;
	//Sounds
	private bool checkAudio=false;
	
	void Awake(){
		myPlayer = transform;
		targets = new List<Transform>();
		AddAllEnemies();
	}
    /// <summary>
    /// Начальная инициализация
    /// </summary>
    private void Start()
    {
		loading();
		transform.position = new Vector3(x,y,z);
		Nick.text=PlayerName;
		NickInv.text=PlayerName;
		
        anim = GetComponent<Animator>();
		
    }

	public void AddAllEnemies(){
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject enemy in enemies)
			AddTarget(enemy.transform);
	}
	
	public void AddTarget(Transform enemy){
			targets.Add(enemy);
	}
	
	private void SortTargetsByDistance(){
		targets.Sort(delegate(Transform t1, Transform t2){
			return Vector3.Distance(t1.position, myPlayer.position).CompareTo(Vector3.Distance(t2.position, myPlayer.position));
		});
	}
	private void TargetEnemy(){
		SortTargetsByDistance();
/*		AI aibar = (AI)targets[0].GetComponent("AI");
		if (Vector3.Distance(targets[0].position, myPlayer.position) <= 12.0f){
			selectedTarget = targets[0];
			aibar.enemyHealthBar();
			NGUITools.SetActive(aibar.HPEnemyBar, true);
		}else{
			NGUITools.SetActive(aibar.HPEnemyBar, false);
		}
*/
	}
	
    /// <summary>
    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    /// </summary>
    private void FixedUpdate()
    {
			//определяем, на земле ли персонаж
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		isSky = Physics2D.OverlapCircle(skyCheck.position, groundRadius, whatIsGround);
		isRighted = Physics2D.OverlapCircle(rightCheck.position, groundRadius, whatIsGround);
		isLefted = Physics2D.OverlapCircle(leftCheck.position, groundRadius, whatIsGround);
			//устанавливаем соответствующую переменную в аниматоре
        //anim.SetBool ("Ground", isGrounded);
			//устанавливаем в аниматоре значение скорости взлета/падения
        anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
			//если персонаж в прыжке - выход из метода, чтобы не выполнялись действия, связанные с бегом
			//используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
			//при стандартных настройках проекта 
			//-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
			//1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
        float move = Input.GetAxis("Horizontal");

        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        
		

        //обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
        //равную значению оси Х умноженное на значение макс. скорости
		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_War")){			
				anim.SetFloat("Speed", Mathf.Abs(move));
				rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		}else{
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		}
		if (isRighted && move>0){
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		}
		
		
        //если нажали клавишу для перемещения вправо, а персонаж направлен влево
        if(move > 0 && !isFacingRight && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_War"))
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if (move < 0 && isFacingRight && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_War"))
            Flip();
			
		if (Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGroundOfXp)){
			CurXP+=1;
		}
    }

    private void Update()
    {	
		targetsInRange = targets.FindAll(bot =>((isFacingRight && Mathf.Abs(myPlayer.position.y-bot.position.y)<=1.2f && myPlayer.transform.position.x<=bot.transform.position.x && bot.transform.position.x<=myPlayer.transform.position.x+2.0f) || (!isFacingRight && myPlayer.transform.position.x-2.0f<=bot.transform.position.x && bot.transform.position.x<=myPlayer.transform.position.x)));
		//Debug.Log(_damageW);
		if (Input.GetKey(KeyCode.F))
			CurXP+=10;
		TargetEnemy();
		if (CurHealth>MaxHealth)
			CurHealth=MaxHealth;
		if (CurMana>MaxMana)
			CurMana=MaxMana;
		if (attackTimer>0){attackTimer-=Time.deltaTime;}
		if (attackTimer<0){attackTimer=0;}
		if (menuTimer>0){menuTimer-=Time.deltaTime;}
		if (menuTimer<0){menuTimer=0;}
		if (menuTimer==0 && menuCheck){
			NGUITools.SetActive (statsMenu, false);
			checkAudio=false;
		}
			
		if (firstSkillTimer>1){
			firstSkillTimer-=Time.deltaTime;
			cdFirstSkillText.text=(Mathf.Floor(firstSkillTimer)).ToString();
		}
		if (firstSkillTimer<1){
			firstSkillTimer=1;
			NGUITools.SetActive(fonFirstSkill,false);
		}
		//
		if (SecondSkillTimer>1){
			SecondSkillTimer-=Time.deltaTime;
			//cdSecondSkillText.text=(Mathf.Floor(SecondSkillTimer)).ToString();
		}
		if (SecondSkillTimer<1){
			SecondSkillTimer=1;
			//NGUITools.SetActive(fonSecondSkill,false);
		}
	
		if (NGUITools.GetActive(openMenu)){
			Time.timeScale=0;
		}
		else{
			Time.timeScale=1;
		}
	
	
        //если персонаж на земле и нажат пробел...
        if (!isSky && Input.GetKey(KeyCode.W) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_War")) 
        {
            //устанавливаем в аниматоре переменную в false
            //anim.SetBool("Ground", false);
            //прикладываем силу вверх, чтобы персонаж подпрыгнул
			myPlayer.position += myPlayer.up * maxSpeed * Time.deltaTime;
			//Debug.Log(maxSpeed);
        }
		if (!isGrounded && Input.GetKey(KeyCode.S) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_War")) 
        {
            //устанавливаем в аниматоре переменную в false
            //anim.SetBool("Ground", false);
            //прикладываем силу вверх, чтобы персонаж подпрыгнул
            myPlayer.position -= myPlayer.up * maxSpeed * Time.deltaTime;				
        }
		if (!block && Input.GetKeyDown(KeyCode.Mouse0)){
			//mouseOver = Input.mousePosition;
			//Debug.Log(mouseOver);
			//if (mouseOver.x >= 880.0f && !isFacingRight && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_War")){Flip();}
			//if (mouseOver.x < 880.0f && isFacingRight && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_War")){Flip();}
			if (attackTimer==0){
				anim.SetTrigger(attackHash);
				DamageAI();
				attackTimer=cd;
			}
		}
		//First Skill
		if (Input.GetKeyDown(KeyCode.Alpha1) && firstSkillTimer==1){
			firstSkillTimer=cdFirstSkill;
			NGUITools.SetActive(fonFirstSkill,true);
			wave.SetActive(true);
			ttlFirstSkill=1.0f;
			if(!isFacingRightFirstSkill){
				Vector3 theScale = wave.transform.localScale;
				theScale.x *= -1;
				wave.transform.localScale = theScale;
			}
			isFacingRightFirstSkill=isFacingRight;
			if(isFacingRightFirstSkill){
				wave.transform.position=new Vector3(myPlayer.position.x+1,myPlayer.position.y,0);
				rightPartSys.SetActive(true);
				leftPartSys.SetActive(false);
			}else{
				rightPartSys.SetActive(false);
				leftPartSys.SetActive(true);
				Vector3 theScale = wave.transform.localScale;
				theScale.x *= -1;
				wave.transform.localScale = theScale;
				isFacingRightFirstSkill=false;
				wave.transform.position=new Vector3(myPlayer.position.x-1,myPlayer.position.y,0);
			}
		}
		if (ttlFirstSkill>=0){
			if(isFacingRightFirstSkill){
				wave.transform.position += wave.transform.right * 7 * Time.deltaTime;
			}else{
				wave.transform.position -= wave.transform.right * 7 * Time.deltaTime;
			}
			targetsInFirstSkill= targets.FindAll(bot => Mathf.Abs(bot.position.x-wave.transform.position.x)<=1.0f && Mathf.Abs(bot.position.y-wave.transform.position.y)<=0.6f);
			foreach(Transform x in targetsInFirstSkill){
				if(!targetsDamagedFirstSkill.Contains(x)){
					AIscript = (AI)x.GetComponent("AI");
					AIscript.AddJustCurrHealth(-15);
					targetsDamagedFirstSkill.Add(x);
				}
			}
			ttlFirstSkill-=Time.deltaTime;
		}
		if (ttlFirstSkill<=0){
			wave.SetActive(false);
			targetsDamagedFirstSkill.Clear();
		}
		//
		//Second Skill
		if (Input.GetKeyDown(KeyCode.Alpha2) && SecondSkillTimer==1)
		{
			SecondSkillTimer=cdSecondSkill;
			//NGUITools.SetActive(fonSecondSkill,true);
			ttlSecondSkill=0.2f;
			isFacingRightSecondSkill=isFacingRight;
		}
		if (ttlSecondSkill>=0)
		{
			if(isFacingRightSecondSkill)
			{
				myPlayer.transform.position += myPlayer.transform.right * maxSpeed * 8 * Time.deltaTime;
			}
			else
			{
				myPlayer.transform.position -= myPlayer.transform.right * maxSpeed * 8 * Time.deltaTime;
			}
			targetsInSecondSkill= targets.FindAll(bot => Mathf.Abs(bot.position.x-myPlayer.transform.position.x)<=1.0f && Mathf.Abs(bot.position.y-myPlayer.transform.position.y)<=0.6f);
			foreach(Transform x in targetsInSecondSkill)
			{
				if(!targetsDamagedSecondSkill.Contains(x))
				{
					AIscript = (AI)x.GetComponent("AI");
					AIscript.AddJustCurrHealth(-15);
					targetsDamagedSecondSkill.Add(x);
				}
			}
			ttlSecondSkill-=Time.deltaTime;
		}
		if (ttlSecondSkill<=0)
		{
			targetsDamagedSecondSkill.Clear();
		}
		//
		//
		if (Input.GetKeyDown(KeyCode.C) && NGUITools.GetActive(inventory) && myPlayer.GetComponent<GUI_Inv>().stats == 40 && !NGUITools.GetActive(gameObject.GetComponent<Shops>().shop)){
			NGUITools.SetActive (inventory, false);
			checkAudio=false;
		}
		else if(Input.GetKeyDown(KeyCode.C) && !NGUITools.GetActive(inventory)){
			NGUITools.SetActive (inventory, true);
		}
		//Stats
		//Debug.Log(_VitL.color);
		if(_vitalityI>0){
			_VitL.color = new Color(0,1,0,1);
		}else{
			_VitL.color = new Color(1,1,1,1);
		}
		if(_agilityI>0){
			_AgL.color = new Color(0,1,0,1);
		}else{
			_AgL.color = new Color(1,1,1,1);
		}
		if(_intelligenceI>0){
			_IntL.color =new Color(0,1,0,1);
		}else{
			_IntL.color =new Color(1,1,1,1);
		}
		VitalityTotal=Vitality+_vitalityI;
		AgilityTotal=Agility+_agilityI;
		IntelligenceTotal=Intelligence+_intelligenceI;
		VitalityL.text = Vitality.ToString();
		AgilityL.text = Agility.ToString();
		_VitL.text = _vitalityI.ToString();
		_IntL.text = _intelligenceI.ToString();
		_AgL.text = _agilityI.ToString();
		MaxHealth = VitalityTotal*18;
		MaxMana = IntelligenceTotal*16;
		maxSpeed = 4.0f+AgilityTotal*0.05f;
		maxSpeed = ((float)((int)(maxSpeed*100.0f))/100.0f);
		HealthReg = Mathf.Floor(VitalityTotal/5.0f);
		ManaReg = Mathf.Floor(IntelligenceTotal/5.0f);
		IntelligenceL.text = Intelligence.ToString();
		XP.sliderValue = (float)CurXP/(float)MaxXP;
		XpText.text = CurXP.ToString()+"/"+MaxXP.ToString();		
		HP.sliderValue = (float)CurHealth/(float)MaxHealth;
		HpText.text = CurHealth.ToString()+"/"+MaxHealth.ToString();
		MP.sliderValue = (float)CurMana/(float)MaxMana;
		MPText.text = CurMana.ToString()+"/"+MaxMana.ToString();
		Level.text = LVL.ToString();
		PointsL.text = Points.ToString();
		MS.text = maxSpeed.ToString();
		HPReg.text = ((int)HealthReg).ToString() + " hps";
		MPReg.text = ((int)ManaReg).ToString() + " mps";
		DamageWW = Damage + _damageW;
		DamageWWL.text = DamageWW.ToString();
		goldL.text = gold.ToString();
		
		
		if (CurXP>=MaxXP){
			CurXP=CurXP-MaxXP;
			MaxXP*=2;
			LVL+=1;
			Points+=4;
			NGUITools.SetActive(LevelUp,true);
			NGUITools.SetActive(LevelUpSe,true);
			done=true;
		}
		if (done && TimerLevel>0){
			TimerLevel-=Time.deltaTime;
		}
		if(done && TimerLevel<=0){
			NGUITools.SetActive(LevelUp,false);
			NGUITools.SetActive(LevelUpSe,false);
			TimerLevel=3.0f;
			done=false;
		}
		if (TimerRegen>0){
			TimerRegen-=Time.deltaTime;
		}
		if (TimerRegen<=0){
			if (CurHealth<MaxHealth){
				CurHealth+=(int)HealthReg;
				if(CurHealth>MaxHealth){
					CurHealth=MaxHealth;
				}
			}
			if (CurMana<MaxMana){
				CurMana+=(int)ManaReg;
				if(CurMana>MaxMana){
					CurMana=MaxMana;
				}
			}
			TimerRegen=1.0f;
		}
		if (NGUITools.GetActive(statsMenu) && !checkAudio){
			audio.PlayOneShot(gameObject.GetComponent<Sounds>().openPanel);
			checkAudio=true;
		}
		//		
    }

    /// <summary>
    /// Метод для смены направления движения персонажа и его зеркального отражения
    /// </summary>
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
	void exit () {
		saving();
		Application.Quit();
	}
	void options(){
		NGUITools.SetActive (openMenu,true);
	}
	void mainmenu(){
		saving();
		Application.LoadLevel(0);
	}
	void back(){
		NGUITools.SetActive (openMenu, false);
	}
	void stats(){
		if (NGUITools.GetActive (statsMenu)){
			menuTimer=0.3f;
			menuCheck=true;
			audio.PlayOneShot(gameObject.GetComponent<Sounds>().closePanel);
		}else{
			menuCheck=false;
			NGUITools.SetActive (statsMenu, true);
		}
	}
	void plusVit(){
		if (Points>0){
			Vitality+=1;
			//MaxHealth+=18;
			HealthReg+=0.2f;
			Points-=1;
		}
	}
	void plusInt(){
		if (Points>0){
			Intelligence+=1;
			//MaxMana+=16;
			ManaReg+=0.2f;
			Points-=1;
		}
	}
	void plusAgil(){
		if (Points>0){
			Agility+=1;
			//maxSpeed+=0.05f;
			//maxSpeed = ((float)((int)(maxSpeed*100.0f))/100.0f);
			Points-=1;
		}	
	}
	public void AddJustCurrHealth(int add){
		CurHealth+=add;
		if (CurHealth<0){CurHealth=0;}
		if (CurHealth>MaxHealth){CurHealth=MaxHealth;}
	}
	
	void DamageAI(){
		foreach(Transform x in targetsInRange){
				AIscript = (AI)x.GetComponent("AI");
				AIscript.AddJustCurrHealth(-DamageWW);
		}
	}
	
	void blockClick(){
		block=true;
	}
	void unblockClick(){
		block=false;
	}
	
	void saving(){
		StreamWriter save = new StreamWriter("Save/Char.save");		
		save.WriteLine(PlayerName);
		save.WriteLine(Class);
		save.WriteLine(LVL);
		save.WriteLine(transform.position.x);
		save.WriteLine(transform.position.y);
		save.WriteLine(transform.position.z);
		save.WriteLine(gold);
		save.WriteLine(CurXP);
		save.WriteLine(MaxXP);
		save.WriteLine(CurHealth);
		save.WriteLine(CurMana);
		save.WriteLine(MaxHealth);
		save.WriteLine(MaxMana);
		save.WriteLine(maxSpeed);
		save.WriteLine(Damage);
		save.WriteLine(Vitality);
		save.WriteLine(Agility);
		save.WriteLine(Intelligence);
		save.WriteLine(HealthReg);
		save.WriteLine(ManaReg);
		save.WriteLine(Points);
		
		save.Close();	
	}	
	void loading(){
		StreamReader load = new StreamReader("Save/Char.save");
		while (!load.EndOfStream){		
			PlayerName = load.ReadLine();
			Class = load.ReadLine();
			LVL = System.Convert.ToInt32(load.ReadLine());
			x = System.Convert.ToSingle(load.ReadLine());
			y = System.Convert.ToSingle(load.ReadLine());
			z = System.Convert.ToSingle(load.ReadLine());
			gold = System.Convert.ToInt32(load.ReadLine());
			CurXP = System.Convert.ToInt32(load.ReadLine());
			MaxXP = System.Convert.ToInt32(load.ReadLine());
			CurHealth = System.Convert.ToInt32(load.ReadLine());
			CurMana = System.Convert.ToInt32(load.ReadLine());
			MaxHealth = System.Convert.ToInt32(load.ReadLine());
			MaxMana = System.Convert.ToInt32(load.ReadLine());
			maxSpeed = System.Convert.ToSingle(load.ReadLine());
			Damage = System.Convert.ToInt32(load.ReadLine());
			Vitality = System.Convert.ToInt32(load.ReadLine());
			Agility = System.Convert.ToInt32(load.ReadLine());
			Intelligence = System.Convert.ToInt32(load.ReadLine());
			HealthReg = System.Convert.ToSingle(load.ReadLine());
			ManaReg = System.Convert.ToSingle(load.ReadLine());
			Points = System.Convert.ToInt32(load.ReadLine());		
		}	
	}
}
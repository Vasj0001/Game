using UnityEngine;
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
	
	private int countInv=1;
	public GameObject inventory;
	public GameObject LevelUp;
	public GameObject LevelUpSe;
	public GameObject statsMenu;
	public GameObject openMenu;
	
	private int LVL;
	private string Class;
	private int MaxXP;
	private int CurXP;
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
	private int count=0;
	private string PlayerName;
	
	private int Damage;
	private int Vitality; //0.2 регена ХП
	private int Agility;
	private int Intelligence; // 0.2 регена МП
	private int Points;
	
	public UISlider XP;
	public UILabel XpText;
	public UISlider HP;
	public UILabel HpText;
	public UISlider MP;
	public UILabel MPText;
	public UILabel Level;
	
	public UILabel PointsL;
	public UILabel DamageL;
	public UILabel VitalityL;
	public UILabel AgilityL;
	public UILabel IntelligenceL;
	public UILabel DamageWW;
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
	
	//First Skill
	public float cdFirstSkill;
	private float firstSkillTimer;
	public List<Transform> targetsInFirstSkill;
	public List<Transform> targetsDamagedFirstSkill;
	public GameObject wave;
	private float ttlFirstSkill=0.0f;
	private bool isFacingRightFirstSkill;
	public GameObject fonFirstSkill;
	public UILabel cdFirstSkillText;
	//
	public Transform selectedTarget;
	public GameObject expInfoPanel;
	public UILabel expInfoLabel;
	private GameObject[] enemies;
	private AI AIscript;
	private Items ItemsScript;
	private bool block=true;
	
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
		
		PointsL.text = Points.ToString();
		DamageL.text = Damage.ToString();
		VitalityL.text = Vitality.ToString();
		AgilityL.text = Agility.ToString();
		IntelligenceL.text = Intelligence.ToString();
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
		//Debug.Log(weapon);
		if (Input.GetKey(KeyCode.F))
			CurXP+=10;
		TargetEnemy();
		if (attackTimer>0){attackTimer-=Time.deltaTime;}
		if (attackTimer<0){attackTimer=0;}
		if (firstSkillTimer>1){
			firstSkillTimer-=Time.deltaTime;
			cdFirstSkillText.text=(Mathf.Floor(firstSkillTimer)).ToString();
		}
		if (firstSkillTimer<1){
			firstSkillTimer=1;
			NGUITools.SetActive(fonFirstSkill,false);
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
			isFacingRightFirstSkill=isFacingRight;
			if(isFacingRightFirstSkill){
				wave.transform.position=new Vector3(myPlayer.position.x+1,myPlayer.position.y,0);
			}else{
				wave.transform.position=new Vector3(myPlayer.position.x-1,myPlayer.position.y,0);
			}
		}
		if (ttlFirstSkill>=0){
			if(isFacingRightFirstSkill){
				wave.transform.position += wave.transform.right * 8 * Time.deltaTime;
			}else{
				wave.transform.position -= wave.transform.right * 8 * Time.deltaTime;
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
		
		if (Input.GetKeyDown(KeyCode.C) && countInv==0){
			NGUITools.SetActive (inventory, true);
			countInv=1;
		}
		else if (Input.GetKeyDown(KeyCode.C) && countInv==1){
			NGUITools.SetActive (inventory, false);
			countInv=0;
		}

		XP.sliderValue = (float)CurXP/(float)MaxXP;
		XpText.text = CurXP.ToString()+"/"+MaxXP.ToString();		
		HP.sliderValue = (float)CurHealth/(float)MaxHealth;
		HpText.text = CurHealth.ToString()+"/"+MaxHealth.ToString();
		MP.sliderValue = (float)CurMana/(float)MaxMana;
		MPText.text = CurMana.ToString()+"/"+MaxMana.ToString();
		Level.text = LVL.ToString();
		PointsL.text = Points.ToString();
		MS.text = maxSpeed.ToString();
		HPReg.text = ((int)HealthReg).ToString();
		MPReg.text = ((int)ManaReg).ToString();
		
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
		if (NGUITools.GetActive(expInfoPanel)){
			expInfoLabel.text="To obtain level "+(LVL+1).ToString()+ " left "+ (MaxXP-CurXP).ToString() + "XP";
			expInfoPanel.transform.localPosition=new Vector3(Input.mousePosition.x+80,Input.mousePosition.y-900,0);
		}
		targetsInRange = targets.FindAll(bot =>((isFacingRight && Mathf.Abs(myPlayer.position.y-bot.position.y)<=1.2f && myPlayer.transform.position.x<=bot.transform.position.x && bot.transform.position.x<=myPlayer.transform.position.x+2.0f) || (!isFacingRight && myPlayer.transform.position.x-2.0f<=bot.transform.position.x && bot.transform.position.x<=myPlayer.transform.position.x)));
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
		if (count==0){
			NGUITools.SetActive (statsMenu, true);
			count=1;
		}
		else
		{
			NGUITools.SetActive (statsMenu, false);
			count=0;
		}
	}
	void plusVit(){
		if (Points>0){
			Vitality+=1;
			VitalityL.text = Vitality.ToString();
			MaxHealth+=18;
			HealthReg+=0.2f;
			Points-=1;
		}
	}
	void plusInt(){
		if (Points>0){
			Intelligence+=1;
			IntelligenceL.text = Intelligence.ToString();
			MaxMana+=16;
			ManaReg+=0.2f;
			Points-=1;
		}
	}
	void plusAgil(){
		if (Points>0){
			Agility+=1;
			AgilityL.text = Agility.ToString();
			maxSpeed+=0.05f;
			maxSpeed = ((float)((int)(maxSpeed*100.0f))/100.0f);
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
				AIscript.AddJustCurrHealth(-Damage);
		}
	}
	void expInfoT(){
//		Debug.Log(Input.mousePosition);
//		Debug.Log(expInfoPanel.transform.localPosition);
		NGUITools.SetActive(expInfoPanel,true);
	}
	void expInfoF(){
		NGUITools.SetActive(expInfoPanel,false);
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
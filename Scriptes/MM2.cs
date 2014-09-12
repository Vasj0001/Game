using UnityEngine;
using System.Collections;
using System.IO;

public class MM2 : MonoBehaviour {
	public GameObject openMenu;
	public UIToggle sound;
	public GameObject slider;
	public GameObject percent;
	public GameObject chooseMenu;
	public GameObject createMenu;
	private string PlayerName;
	public UIInput Nick;
	public UILabel slotName;
	public UILabel classL;
	private string LVL;
	private string Class;
	private bool buttonSlot1 = false;
	public UIScrollBar volumeLevel;
	public AudioClip myClip2;
	
	void Start(){
		DirectoryInfo di = Directory.CreateDirectory("Save");	
		if (File.Exists("Save/Char.save")){
			buttonSlot1 = true;
			StreamReader load = new StreamReader("Save/Char.save");
			if (load.Peek() != -1){
				PlayerName = load.ReadLine();
				Class = load.ReadLine();
				LVL = load.ReadLine();
				slotName.fontSize=20;
				slotName.text = PlayerName+"\n"+"Class - "+Class+" Level - "+LVL;
			}
		}
	}
	
	void singleplayer () {
		NGUITools.SetActive(chooseMenu,true);
	}
	void back2(){
		NGUITools.SetActive(createMenu,false);
		NGUITools.SetActive(chooseMenu,true);
	}
	void create(){
		NGUITools.SetActive(chooseMenu,false);
		NGUITools.SetActive(createMenu,true);
	}
	void mainmenuGL(){
		NGUITools.SetActive(chooseMenu,false);
	}
	
	void createPers(){
		PlayerName = Nick.value;
		LVL="1";
		NGUITools.SetActive(createMenu,false);
		NGUITools.SetActive(chooseMenu,true);
		FileStream fs = File.Create("Save/Char.save");
		fs.Close();
		buttonSlot1 = true;
		saving();
		slotName.fontSize=20;
		slotName.text = PlayerName+"\n"+"Class - "+classL.text+"  Level - "+LVL;
	}
	
	void exit () {
		Application.Quit();
	}
	void multiplayer () {
	
	}
	void options(){
		NGUITools.SetActive (openMenu,true);
	}
	void back1(){
		NGUITools.SetActive (openMenu, false);
	}
	void stats(){

	}
	void strelka(){
		
	}
	void Update(){
		if (!audio.isPlaying){
			audio.clip=myClip2;
			audio.Play();
		}
		if (NGUITools.GetActive(openMenu)){
			Time.timeScale=0;
		}
		else{
			Time.timeScale=1;
		}
		if (!sound.isChecked){
			NGUITools.SetActive(slider,false);
			NGUITools.SetActive(percent,false);
			audio.volume=0;
		}
		if (sound.isChecked){
			NGUITools.SetActive(slider,true);
			NGUITools.SetActive(percent,true);
			audio.volume=volumeLevel.scrollValue;
		}
	}
	void slot1(){
		if (buttonSlot1){Application.LoadLevel(1);}
	}
	
	void saving(){
		StreamWriter save = new StreamWriter("Save/Char.save");		
		save.WriteLine(PlayerName);
		save.WriteLine(classL.text);
		save.WriteLine(1);
		save.WriteLine(-4);
		save.WriteLine(-2);
		save.WriteLine(0);
		save.WriteLine(0);
		save.WriteLine(100);
		save.WriteLine(180);
		save.WriteLine(64);
		save.WriteLine(180);
		save.WriteLine(64);
		save.WriteLine(4.15f);
		save.WriteLine(12);
		save.WriteLine(10);
		save.WriteLine(6);
		save.WriteLine(4);
		save.WriteLine(3.0f);
		save.WriteLine(1.8f);
		save.WriteLine(0);
		
		save.Close();	
	}
}
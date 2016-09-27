//Script written by Andrei Shulgach
//If you use this script, please send me a link to a game or video you made using it.
//Enjoy!

public var GUI_Skin : GUISkin;
public var FirstReponseWait : float = 5.0;
//Time to wait after start until the first reponse is requested
public var SecondResponseWait : float = 5.0;
//Time to wait after the first reponse until the second response is requested
private var CanAction1 = false;
private var CanAction2 = false;
private var CanAction3 = false;

var audioVolume : int = 1.0;

function Start()
{
	//audio.volume = audioVolume;
	yield WaitForSeconds(FirstReponseWait);
	CanAction1 = true;
}

function Update () 
{
	//If player press Action key, play the next camera animation
	if(CanAction1)
	{
		FirstReponse();
	}
	
	//If player press Action key, play the next camera animation
	if(CanAction2  && Input.GetKeyDown("e"))
	{
		SecondReponse();
		CanAction2 = false;
	}
}


function FirstReponse()
{
	if(Input.GetKeyDown("e"))
	{
		ActionOne();
		this.gameObject.animation.CrossFadeQueued("cameraanim", 1, QueueMode.PlayNow);
	}
}

function OnGUI()
{
	//Set the text style for screen prompt
	GUI.skin = GUI_Skin;
	
	//If game is waiting for response 1, show screen prompt
	if(CanAction1)
	{
		GUI.Label(Rect((Screen.width / 2) - (19 / 2), Screen.height / 2, 500 , 100), "Press 'E' for Action 1");
	}
	
	//If game is waiting for response 2, show screen prompt
	if(CanAction2)
	{
		GUI.Label(Rect((Screen.width / 2) - (19 / 2), Screen.height / 2, 500 , 100), "Press 'E' for Action 2");
	}
}

function ActionOne()
{
	CanAction1 = false;
	yield WaitForSeconds(SecondResponseWait);
	CanAction2 = true;
}

function SecondReponse()
{
	this.gameObject.animation.CrossFadeQueued("Animation3", 1, QueueMode.PlayNow);
}
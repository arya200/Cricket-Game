using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controls : MonoBehaviour
{
    public static controls instance { get; private set; }
    public GameObject Ball_Speed_Slider; // object for the ball speed slider.
    public GameObject Bat_force_slider; // object for the bat force slider.
    public GameObject outPanel; //the panel to that contains the out text.
    public GameObject goodshotpanel;// the panel that coontains the shot! text.
    public Text shot_text;
    public Text out_text;
    public Text bat_force_text;
    public Text ball_speed_text;
    public Text ball_type_button; // ball type is straight, off spin or leg spin.
    public float min_realworld_speed; // of ball
    public float max_realworld_speed; // of ball
    public float min_ingame_speed; // of ball
    public float max_ingame_speed; // of ball

    public float min_realworld_force; // of bat
    public float max_realworld_force;// of bat
    public float min_bat_ingame_force;// of bat
    public float max_bat_ingame_force; // of bat
    private float slider_val; // value obtained from the speed slider.
    private float bat_slider_val; // force value obtained from the bat slider.
    private float game_ball_speed; // in game speed of the ball
    private float real_ball_speed; // real world ball speed.
     private float game_bat_force; // in game force of the bat.
    private float real_bat_force; // real world force of the bat.
    private int ball_type=0; // int to keep track of which type of ball it is.




    void Awake()//default both panels to false, in order to hide the screen text.
    {
        instance=this;
        outPanel.SetActive(false);
        goodshotpanel.SetActive(false);
        
    }

    void start()
    {
        changeballspeed();
        changeballtype();
        changebatforce();
        
    }

    // this is the main scalling function for the values. 
    //A friend I connected with on a unity forum when I asked a question helped me with this.
    private float ScaleSpeedToIngame (float speed, float scaleMinTo, float scaleMaxTo, float scaleMinFrom, float scaleMaxFrom){
		return (scaleMaxTo - scaleMinTo) * (speed - scaleMinFrom) / (scaleMaxFrom - scaleMinFrom) + scaleMinTo;
	} 
    
    public void changeballspeed()
    {
        slider_val = Ball_Speed_Slider.GetComponent<Slider>().value; // obtain the value from the slider
        game_ball_speed = ScaleSpeedToIngame(slider_val, min_ingame_speed, max_ingame_speed, 0 ,1); // scale it to ingame
        real_ball_speed = ScaleSpeedToIngame(slider_val, min_realworld_speed, max_realworld_speed, 0, 1); // scale the realworld speed
        ball_speed_text.text = real_ball_speed.ToString("#.##") + " km/hr"; // display it as the text.
        Ballscript.instance.ball_speed = game_ball_speed; // set the speed of the ball to the ingame ball speed calculated.
    }
    public void changeballtype()
    {
        ball_type++; // everytime button is pressed change the ball type.
        if(ball_type>2) // make sure it doesn't exceed 2.
        {
            ball_type = 0;
        }
        switch(ball_type)
        {
            case 0:
            ball_type_button.text = "Straight";
            break;
            case 1:
            ball_type_button.text = "Off spin";
            break;
            case 2:
            ball_type_button.text = "Leg spin";
            break;
        }
        Ballscript.instance.ball_type = ball_type; // set the ball type in the bat instance in order to calculate respective spin.
    }

    public void changebatforce() // selecting and scaling bat force is identical to ball speed.
    {
        bat_slider_val = Bat_force_slider.GetComponent<Slider>().value;
        game_bat_force = ScaleSpeedToIngame(bat_slider_val, min_bat_ingame_force, max_bat_ingame_force, 0 ,1);
        real_bat_force = ScaleSpeedToIngame(bat_slider_val, min_realworld_force, max_realworld_force, 0, 1);
        bat_force_text.text =  real_bat_force.ToString("#.##") + " force";
        Ballscript.instance.batforce = game_bat_force; // setting the value of bat force to the calcualted in game bat force.
    
    }

    public void display_out() // display the "You're out"
    {
        outPanel.SetActive(true);
        out_text.text = "You're out!";
    }

    public void display_shot() // displat the shot! 
    {
        goodshotpanel.SetActive(true);
        shot_text.text = "Shot!";
    }
}

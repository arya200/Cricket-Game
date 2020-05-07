using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ballscript : MonoBehaviour
{
    public static Ballscript instance {get; private set; }
    public GameObject ball;
    public GameObject marker;
    public Vector3 default_pos; //default pos of ball
    private Vector3 target_pos; //target pos of ball
    private Vector3 start_pos;// start pos of ball
    private Vector3 direction; //direction vector of ball
    public float spin_val; //amount the ball should spin by can be adjusted in inspector
    public float bounce_val; //bounce value;
    public float speed; // in game speed of ball
    public float bat_force; // force of bat determenied by the slider.
    public float real_speed; // real world speed of ball
    private int balltype;// 0 for straight, 1 for off spin, 2 for leg spin.
    private Rigidbody rb;// rigid body of the ball
    private float spin_by; // in game spin amount
    private bool is_ball_thrown=false;
    private bool first_bounce=false;

    public float ball_speed
    { 
        set
        {
            speed=value;
        }
    }
    public int ball_type
    {
        set
        {
            balltype = value;
        }
    }
    public float batforce
    {
        set
        {
            bat_force = value;
        }
    }

    public bool isballthrown
    {
        get
        {
            return is_ball_thrown;
        }
        
    }  

    void Awake()
    {
    
        instance = this;
        
       
    }
    void Start()
    {
        default_pos = transform.position;
        start_pos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //continously checks to see if ball is in position to be hit, once ready it returns true value
        if(transform.position.z >= (float)-3.5)
        {
            batscript.instance.is_ball_inpos = true;
        }
    }

private void OnCollisionEnter(Collision collision) 
    {

        if(collision.gameObject.CompareTag("pitch"))
        {
            //When ball colides with pitch set the spin value by what was given.
            switch(balltype)
            {
                case 0:
                spin_by = direction.x;//for straight ball
                break;
                case 1:
                spin_by = -spin_val/speed;//for offspin in negative x direction.
                break;
                case 2:
                spin_by = spin_val/speed;//for leg spin
                break;
            }
            if(!first_bounce)
            {

                first_bounce = true;
                rb.useGravity = true;
                direction = new Vector3 (spin_by, -direction.y * (bounce_val * speed), direction.z); //create a new direction for the ball based on spin
				direction = Vector3.Normalize (direction);//normalize the direction
				rb.AddForce (direction * speed, ForceMode.Impulse); //add a force in that direction
                

            }

        }

        if(collision.gameObject.CompareTag("stump"))
        {
            //display respective message and audio for when clean bowled.
            audio_script.instance.play_hit_audio();
            controls.instance.display_out();

        }
    }

    public void Throw()
    {
    
        is_ball_thrown = true;
        target_pos = marker.transform.position; //recieves position of marker
        direction = Vector3.Normalize(target_pos - start_pos);
        rb.AddForce(direction*speed, ForceMode.Impulse);
        
    }
    public void HitTheBall(Vector3 hitDirection) {
		rb.velocity = Vector3.zero; // stop the ball from moving be setting velocity to zero
		direction = Vector3.Normalize(hitDirection);//normalise the direction ball must travel;
		float hitforce = (speed / 2) + bat_force; // calculates the ingame force
		rb.AddForce (-direction * hitforce, ForceMode.Impulse);  //adds a force in that direction to move the ball
		if(!first_bounce){
			rb.useGravity = true; // if the ball has not bounced before set it's gravity to true
		}
 	}

    public void Reset()
    {
        //sets all booleans to false.

        transform.position = default_pos; //brings ball back to original spot
        is_ball_thrown = false; 
        first_bounce = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        controls.instance.out_text.text = ""; // sets out text to empty.
    }

}

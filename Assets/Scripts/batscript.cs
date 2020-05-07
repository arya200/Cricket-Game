using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batscript : MonoBehaviour
{
    public static batscript instance {get; private set; }
    private float actual_distance; // z axis distance of bat from camera.
    private float z_min = (float)1.6; //min z value that bat can move to
    private float z_max = (float)4; //max z value that bat can move to 
    private float x_limit = (float)4.5; // max x value bat can move to, negative of this will be taken for other direction.
    private float y_min= (float) 13.81; // min y value bat can move to
    private float y_max= (float) 15.22; // max y value bat can move to
    private bool isbeingheld = false; // for if the bat is being held by the mouse.

    private bool isballinpos = false; // if ball is in position to be hit.

    private Vector3 default_pos; // default position of bat.

    public bool isbatbeingheld
    {
        get
        {
            return isbeingheld;
        }
        
    } //public functuon in order to access private variable isbatbeingheld

    public bool is_ball_inpos
    {
        set
        {
            isballinpos = value;
        }
    } //public setter for if the ball is in position. Called in ball script.
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        actual_distance = (transform.position - Camera.main.transform.position).magnitude; // sets the actual distance to the z-value distance from camera to bat
        default_pos = transform.position; // sets defualt position of bat as what it started with.
    }

    void Update()
    {
        
        if(isbeingheld && isballinpos)
        {
            Vector3 mousePositon = Input.mousePosition; //gets mouse position input.
            mousePositon.z=actual_distance; // sets the mouse z to the distance between bat and camera.
            transform.position=Camera.main.ScreenToWorldPoint(mousePositon); //converts the position from the screen to the in game pos
            gameObject.GetComponent<Rigidbody>().useGravity = true; // sets gravity of bat to true
            transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -x_limit, x_limit),
			Mathf.Clamp(transform.position.y, y_min, y_max),
			    Mathf.Clamp (transform.position.z, z_min, z_max)); // clamps the values thus preventing them from going out of bounds.

        }
    }

    private void OnMouseDown() // when bat is clicked throw the ball and make is being held true.
    {
        isbeingheld = true;
        Ballscript.instance.Throw();
        
    }

    private void OnMouseUp()// when bat is not being clicked make is being held false.
    {
        isbeingheld = false;
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.CompareTag("ball")) //checks if it is colliding with the ball.
        {
            
            Ballscript.instance.HitTheBall(transform.forward); //send the forward direction to to hit the ball.
            audio_script.instance.play_hit_audio(); // play the hit audio
            controls.instance.display_shot(); // display shot!
        }
        
    }
    public void reset() // when new ball is pressed, set all components to zero and false and bring bat back to default.
    {
        gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero; 
        gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody> ().useGravity = false;
        transform.position = default_pos;
        isballinpos = false;
        controls.instance.shot_text.text="";
    }
}

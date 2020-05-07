using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stumpscript : MonoBehaviour
{
    public Vector3 default_pos_left; // default pos of left stump
    public Vector3 default_pos_right; // default pos of right stump
    public Vector3 default_pos_middle; // default pos of middle stump
    public GameObject[] stumps; // storing the batter side stumps
    void Start() // setting the respective default positions as the start positions.
    {
        default_pos_left = stumps[0].transform.position; 
        default_pos_middle = stumps[1].transform.position;
        default_pos_right = stumps[2].transform.position;

    }

    public void Reset()
    {
    
       // loops through each object and sets its velocity to zero and gravity to false.
        foreach(GameObject stump in stumps)
        {
            stump.GetComponent<Rigidbody> ().velocity = Vector3.zero;
            stump.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
            

            
            

        }
        //reseting the positions as well as the rotation values.
        stumps[0].transform.position = default_pos_left;
        stumps[1].transform.position = default_pos_middle;
        stumps[2].transform.position = default_pos_right;
        stumps[0].transform.rotation = Quaternion.identity;
        stumps[1].transform.rotation = Quaternion.identity;
        stumps[2].transform.rotation = Quaternion.identity;

        
    
    }
}

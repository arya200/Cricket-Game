using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class markermove : MonoBehaviour, IBeginDragHandler, IDragHandler {

	public GameObject marker;
	
    private float z_min = -3;
    private float z_max = 6; 
    private float x_limit = (float)4.5;
    private float scale_down = (float)0.05; // scale to slow down marker.
    private Vector2 start_pos; //the start pos based on where it is clicked.
    private Vector2 new_marker_pos; // the new position obtained.
    private Vector3 marker_start_pos; // the position the marker starts with.
    

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        start_pos = eventData.position; // setting the start pos as where it is clicked.
        marker_start_pos = marker.transform.position;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!Ballscript.instance.isballthrown)// making sure it can't be moved if the ball is already thrown.
        {
            new_marker_pos = eventData.position;

            marker.transform.position = new Vector3(marker_start_pos.x + (new_marker_pos.x - start_pos.x) * scale_down, marker.transform.position.y, 
            marker_start_pos.z + (new_marker_pos.y - start_pos.y) * scale_down); // setting th new position allowing it be moved.
        
            marker.transform.position = new Vector3 (Mathf.Clamp (marker.transform.position.x, -x_limit, x_limit),
				marker.transform.position.y,
				Mathf.Clamp (marker.transform.position.z, z_min, z_max)); // clamping the values preventing it from going outside the zone.

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveChainsaw : Danger
{
   
    public Transform[] Waypoint;

    public int flatform, startpoint, endpoint;
    int direction=1;
     int Damage=10;
    public float Speed;

    private void move()
    {
        Vector2 target = CurrentMovementTarget();
        Waypoint[flatform].position = Vector2.Lerp(Waypoint[flatform].position, target, Speed * Time.deltaTime);
        float distance = (target - (Vector2)Waypoint[flatform].position).magnitude;  
        if (distance<=0.1f)
        {
            direction *= -1;
        }    
    }
    Vector2 CurrentMovementTarget()
    {
        if (direction==1)
        { return Waypoint[startpoint].position; }  
        else
        {
            return Waypoint[endpoint].position;
        }    
    }
    private void OnDrawGizmos()
    {
        if (Waypoint[flatform] !=null && Waypoint[startpoint]!=null && Waypoint[endpoint]!=null)
        {
            Gizmos.DrawLine(Waypoint[flatform].position, Waypoint[startpoint].position);
            Gizmos.DrawLine(Waypoint[flatform].position, Waypoint[endpoint].position);
        } 


   
    void Update()
    {
        move();
        
    }

}
}

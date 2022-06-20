using UnityEditor;
using UnityEngine;

/*
This is a custome Editor to visualize the field of view of our sniper enemy
*/
[CustomEditor(typeof(EnemySniper))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI() 
    {
     EnemySniper fov = (EnemySniper)target;
     Handles.color = Color.white;
     Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360f, fov.radius); 

     Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -(fov.angle / 2));
     Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y,  fov.angle / 2);

     Handles.color = Color.red;
     Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
     Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

     if(fov.canSeePlayer)
     {
        Handles.color = Color.green;
        Handles.DrawLine(fov.transform.position, fov.player.transform.position);
     }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0 , Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

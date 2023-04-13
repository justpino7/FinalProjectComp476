using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckObstaclesGizmos : MonoBehaviour
{
    public CheckObstacles checkObstacles;
   // private Transform _transform;

    //public CheckObstaclesGizmos(Transform transform)
    //{
    //    _transform = transform;
    //}
    public void OnDrawGizmos()
    {
        Debug.Log("Drawing your gizmos");
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5.0f);
    }

}

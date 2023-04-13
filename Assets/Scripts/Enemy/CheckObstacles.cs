using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System.Linq;

public class CheckObstacles : TreeNode
{
    private Transform _transform;
    private float checkRadius = 8.0f; // Set the desired check radius
    private LayerMask obstacleLayerMask;

    public Transform Transfrom
    {
        get { return _transform; }
    }

    public float CheckRadius
    {
        get { return checkRadius; }
    }
    public CheckObstacles(Transform transform)
    {
        _transform = transform;
        // Set up the layer mask to include layers for "Enemy", "Structure", and "Asteroid"
        obstacleLayerMask = LayerMask.GetMask("Enemy", "Structure", "Asteroid", "EnemyAI");
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Evaluate Check of Obstacles");
        Collider[] colliders = Physics.OverlapSphere(_transform.position, checkRadius, obstacleLayerMask).Where(c => (c.GetComponent(typeof(BoxCollider)) as BoxCollider).center != Vector3.zero).ToArray();

        if (colliders.Length > 0)
        {
            parent.SetData("obstacle", (colliders[0].GetComponent(typeof(BoxCollider)) as BoxCollider).center);

            state = NodeState.SUCCESS;
            //Debug.Log("Success");
            return state;
        }
        else
        {
            parent.ClearData("obstacle");
            state = NodeState.FAILURE;
            //Debug.Log("Failure");
            return state;
        }
    }
}

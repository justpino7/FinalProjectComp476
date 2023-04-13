using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskFleeObstacles : TreeNode
{
    private Transform _transform;
    private float fleeSpeed = 15.0f;
    private LayerMask obstacleLayerMask;

    public TaskFleeObstacles(Transform transform)
    {
        _transform = transform;
        // Set up the layer mask to include layers for "Enemy", "Structure", and "Asteroid"
        obstacleLayerMask = LayerMask.GetMask("Enemy", "Structure", "Asteroid");
    }

    public override NodeState Evaluate()
    {
        Vector3 obstacle = (Vector3)GetData("obstacle");
        Debug.Log("Evaluating Task Flee Obstacles! evading obstacle: " + obstacle);
        float z = _transform.position.z;
        _transform.position = Vector3.MoveTowards(
            _transform.position, obstacle, -fleeSpeed * Time.deltaTime);
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, z);
        state = NodeState.RUNNING;
        return state;
    }
}
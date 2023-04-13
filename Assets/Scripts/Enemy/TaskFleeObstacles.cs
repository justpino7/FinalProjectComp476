using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskFleeObstacles : TreeNode
{
    private Transform _transform;
    private float fleeSpeed = 45.0f;
    //private float maxZDistance = 5.0f; // Set the desired maximum Z distance

    public TaskFleeObstacles(Transform transform)
    {
        _transform = transform;
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
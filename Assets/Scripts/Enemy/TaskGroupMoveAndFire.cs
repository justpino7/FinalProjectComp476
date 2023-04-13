using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

// When grouped, the enemies should fire at the same time and move faster together.

public class TaskGroupMoveAndFire : TreeNode
{
    private Transform _transform;
    private float regroupSpeed = 45.0f;

    public TaskGroupMoveAndFire(Transform transform)
    {
        _transform = transform;

    }

    public override NodeState Evaluate()
    {
        Vector3 destination = (Vector3)GetData("destination");
        Debug.Log("Evaluating Task GroupMove and Fire " + destination);
        _transform.position = Vector3.MoveTowards(
            _transform.position, destination, regroupSpeed * Time.deltaTime);
        state = NodeState.RUNNING;
        return state;
    }
}

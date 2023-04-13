using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System.Linq;
using UnityEngine.UIElements;

// When called, it should check if the enemies are grouped

public class CheckIfGrouped : TreeNode
{
    private Transform _transform;
    private float tolerance = 0.1f;

    public Transform Transfrom
    {
        get { return _transform; }
    }


    public CheckIfGrouped(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        GameObject[] groupPositionObjects = GameObject.FindGameObjectsWithTag("GroupPosition");
        Vector3[] enemyPositions = GameObject.FindGameObjectsWithTag("Enemy").Select(g => g.transform.position).ToArray();      // save enemy positions
        Debug.Log("Evaluate Check if grouped");
        Vector3 destination = groupPositionObjects.Select(g => g.transform.position)        // Determine the closest group position that doesn't have any enemies within the specified tolerance
        .OrderBy(position => Vector3.Distance(position, _transform.position))
        .Where(p => enemyPositions.Where(e => Vector3.Distance(p, e) < tolerance).Count() == 0)
        .First();
        if (Vector3.Distance(destination, _transform.position) > tolerance)
        {
            parent.SetData("destination", destination);
            state = NodeState.SUCCESS;
            //Debug.Log("Success group");
            return state;
        }
        else
        {
            parent.ClearData("destination");
            state = NodeState.FAILURE;
            //Debug.Log("Failure group");
            return state;
        }
    }
}
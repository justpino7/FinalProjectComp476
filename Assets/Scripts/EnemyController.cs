using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private RailEnemy railEnemy;
    private EnemyBehaviorTree behaviorTree;

    private void Start()
    {
        railEnemy = GetComponent<RailEnemy>();
        behaviorTree = GetComponent<EnemyBehaviorTree>();

        if (behaviorTree != null)
        {
            behaviorTree.enabled = false;
        }
    }

    private void Update()
    {
        if (railEnemy != null && behaviorTree != null)
        {
            if (railEnemy.ReachedDestination && !behaviorTree.enabled)
            {
                behaviorTree.enabled = true;
            }
        }
    }
}

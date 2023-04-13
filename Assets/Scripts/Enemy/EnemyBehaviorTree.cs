using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree = BehaviorTree.Tree;
using TreeNode = BehaviorTree.TreeNode;
using Selector = BehaviorTree.Selector;
using Sequence = BehaviorTree.Sequence;

public class EnemyBehaviorTree : Tree
{
    public string treeIdentifier = "EnemyTree";


    //private void Start()
    //{
    //    // Disable the EnemyBehaviorTree script initially
    //    this.enabled = false;
    //}

    //private void Update()
    //{
    //    // Check if the enemy has reached its destination on the rail
    //    if (GetComponent<RailEnemy>().ReachedDestination)
    //    {
    //        // Enable the EnemyBehaviorTree script
    //        this.enabled = true;
    //    }
    //}

    protected override TreeNode SetupTree()
    {

        TreeNode rootSelector = new Selector(new List<TreeNode>
        {
            new Sequence(new List<TreeNode>
            {
                new CheckObstacles(transform),
                new TaskFleeObstacles(transform)
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIfGrouped(transform),
                new TaskGroupMoveAndFire(transform)
            }),
            //new Sequence(new List<TreeNode>
            //{
            //    new CheckEnemyDeathCount(),
            //    new TaskGroup()
            //}),
            //new Sequence(new List<TreeNode>
            //{
            //    new CheckEnemyHealth(),
            //    new Selector(new List<TreeNode>
            //    {
            //        new Sequence(new List<TreeNode>
            //        {
            //            new CheckLastEnemy(),
            //            new TaskSeekPlayer()
            //        }),
            //        new TaskFleeFromPlayer()
            //    }),
            //}),
            //new TaskMoveAndFire()
        });

        return rootSelector;
    }

    public TreeNode GetRootNode()
    {
        return _root;
    }
}

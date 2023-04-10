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

    protected override TreeNode SetupTree()
    {

        TreeNode rootSelector = new Selector(new List<TreeNode>
        {
            new Sequence(new List<TreeNode>
            {
                new CheckObstacles(),
                new TaskFleeObstacles()
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckIfGrouped(),
                new TaskGroupMoveAndFire()
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckEnemyDeathCount(),
                new TaskGroup()
            }),
            new Sequence(new List<TreeNode>
            {
                new CheckEnemyHealth(),
                new Selector(new List<TreeNode>
                {
                    new Sequence(new List<TreeNode>
                    {
                        new CheckLastEnemy(),
                        new TaskSeekPlayer()
                    }),
                    new TaskFleeFromPlayer()
                }),
            }),
            new TaskMoveAndFire()
        });

        return rootSelector;
    }

    public TreeNode GetRootNode()
    {
        return _root;
    }
}

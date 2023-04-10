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
            /*new Sequence(new List<TreeNode>
            {
                //new CheckSharedInfo(),
                //new CheckSeekerOrPlayerFOV(),
                new Selector(new List<TreeNode>
                {
                    *//*new Sequence(new List<TreeNode>
                    {
                        new CheckChaserChasing(),
                        new TaskFlank(chaserTransform, targetTransform, flankDistance, speed)
                    }),*//*
                    new Sequence(new List<TreeNode>
                    {
                        //new InvertDecorator(new CheckChaserChasing()),
                        new TaskChase(chaserTransform, targetTransform, chaseSpeed),
                        //new TaskShareInfo(chaserTransform, shareRadius, targetInfoKey)
                    }),
                    *//*new Sequence(new List<TreeNode>
                    {
                        new InvertDecorator(new CheckSeekerOrPlayerFOV()),
                        new TaskRotate(chaserTransform, rotationSpeed, angle)
                    })*//*
                })
            }),
            new TaskPatrol(chaserTransform, patrolSpeed, pointA, pointB)*/
        });

        return rootSelector;
    }

    public TreeNode GetRootNode()
    {
        return _root;
    }
}

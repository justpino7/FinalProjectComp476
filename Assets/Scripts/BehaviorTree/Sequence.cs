using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : TreeNode
    {
        public Sequence() : base() { }
        public Sequence(List<TreeNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            Debug.Log("Sequence.Evaluate() called");
            Debug.Log("Sequence.Evaluate() called, number of children: " + children.Count);

            bool anyChildIsRunning = false;

            for (int i = 0; i < children.Count; i++)
            {
                TreeNode node = children[i];
                Debug.Log("Evaluating child " + i + ": " + node.GetType().Name);

                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}

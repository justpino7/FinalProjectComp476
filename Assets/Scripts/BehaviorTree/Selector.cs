using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

namespace BehaviorTree
{
    public class Selector : TreeNode
    {
        public Selector() : base() { }
        public Selector(List<TreeNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            int childIndex = 0;
            foreach (TreeNode node in children)
            {
                childIndex++;
                NodeState childState = node.Evaluate();
                switch (childState)
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }

    }

}

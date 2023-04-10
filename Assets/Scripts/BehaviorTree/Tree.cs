using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        protected TreeNode _root = null;
        protected IEnumerator Start()
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second before initializing the tree
            _root = SetupTree();
        }

        protected virtual void Update()
        {
            
            if (_root != null && this is EnemyBehaviorTree && ((EnemyBehaviorTree)this).treeIdentifier == "EnemyTree")
            {
                Debug.Log("Root node being evaluated");
                _root.Evaluate();
            }
        }

        protected abstract TreeNode SetupTree();

    }

}

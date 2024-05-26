using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace DialogSystem
{
    [CreateAssetMenu(fileName = "New Dialog Graph", menuName = "Dialog/Dialog Graph")]
    public class DialogGraph : NodeGraph
    {

        public Node GetFirstNode()
        {
            Node FirstNode = null;
            float posX = 0;

            foreach (Node node in nodes)
            {
                float tempX = node.position.x;
                if (tempX < posX)
                {
                    posX = tempX;
                    FirstNode = node;
                }
            }

            return FirstNode;
        }




    }
}
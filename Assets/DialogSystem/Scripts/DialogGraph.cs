using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace DialogSystem
{
    /// <summary>
    /// This class is used to create a dialog graph
    /// </summary>
    [CreateAssetMenu(fileName = "New Dialog Graph", menuName = "Dialog/Dialog Graph")]
    public class DialogGraph : NodeGraph
    {
        /// <summary>
        /// The nodes of the graph
        /// </summary>
        /// <returns> The first node of the graph </returns>
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
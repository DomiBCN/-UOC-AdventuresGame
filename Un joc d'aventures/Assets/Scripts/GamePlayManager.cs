using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public class StoryNode
    {
        public string history;
        public string[] answers;
        public StoryNode[] nextNode;
        public bool isFinal = false;
        public delegate void NodeVisited();
        public NodeVisited nodeVisited;
    }

    private StoryNode current;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APathfinding : MonoBehaviour
{

    AGrid AGridReference;//For referencing the grid class
    public Transform StartPosition;//Starting position to pathfind from
    public Transform TargetPosition;//Starting position to pathfind to

    private void Awake()//When the program starts
    {
        AGridReference = GetComponent<AGrid>();//Get a reference to the game manager
    }

    private void Update()//Every frame
    {
        FindPath(StartPosition.position, TargetPosition.position);//Find a path to the goal
    }

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        ANode StartANode = AGridReference.ANodeFromWorldPoint(a_StartPos);//Gets the node closest to the starting position
        ANode TargetANode = AGridReference.ANodeFromWorldPoint(a_TargetPos);//Gets the node closest to the target position

        List<ANode> OpenList = new List<ANode>();//List of nodes for the open list
        HashSet<ANode> ClosedList = new HashSet<ANode>();//Hashset of nodes for the closed list

        OpenList.Add(StartANode);//Add the starting node to the open list to begin the program

        while (OpenList.Count > 0)//Whilst there is something in the open list
        {
            ANode CurrentANode = OpenList[0];//Create a node and set it to the first item in the open list
            for (int i = 1; i < OpenList.Count; i++)//Loop through the open list starting from the second object
            {
                if (OpenList[i].FCost < CurrentANode.FCost || OpenList[i].FCost == CurrentANode.FCost && OpenList[i].ihCost < CurrentANode.ihCost)//If the f cost of that object is less than or equal to the f cost of the current node
                {
                    CurrentANode = OpenList[i];//Set the current node to that object
                }
            }
            OpenList.Remove(CurrentANode);//Remove that from the open list
            ClosedList.Add(CurrentANode);//And add it to the closed list

            if (CurrentANode == TargetANode)//If the current node is the same as the target node
            {
                GetFinalPath(StartANode, TargetANode);//Calculate the final path
            }

            foreach (ANode NeighborANode in AGridReference.GetNeighboringANodes(CurrentANode))//Loop through each neighbor of the current node
            {
                if (!NeighborANode.bIsWall || ClosedList.Contains(NeighborANode))//If the neighbor is a wall or has already been checked
                {
                    continue;//Skip it
                }
                int MoveCost = CurrentANode.igCost + GetManhattanDistance(CurrentANode, NeighborANode);//Get the F cost of that neighbor

                if (MoveCost < NeighborANode.igCost || !OpenList.Contains(NeighborANode))//If the f cost is greater than the g cost or it is not in the open list
                {
                    NeighborANode.igCost = MoveCost;//Set the g cost to the f cost
                    NeighborANode.ihCost = GetManhattanDistance(NeighborANode, TargetANode);//Set the h cost
                    NeighborANode.ParentANode = CurrentANode;//Set the parent of the node for retracing steps

                    if (!OpenList.Contains(NeighborANode))//If the neighbor is not in the openlist
                    {
                        OpenList.Add(NeighborANode);//Add it to the list
                    }
                }
            }

        }
    }



    void GetFinalPath(ANode a_StartingANode, ANode a_EndANode)
    {
        List<ANode> FinalPath = new List<ANode>();//List to hold the path sequentially 
        ANode CurrentANode = a_EndANode;//ANode to store the current node being checked

        while (CurrentANode != a_StartingANode)//While loop to work through each node going through the parents to the beginning of the path
        {
            FinalPath.Add(CurrentANode);//Add that node to the final path
            CurrentANode = CurrentANode.ParentANode;//Move onto its parent node
        }

        FinalPath.Reverse();//Reverse the path to get the correct order

        AGridReference.FinalPath = FinalPath;//Set the final path

    }

    int GetManhattanDistance(ANode a_nodeA, ANode a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.iAGridX - a_nodeB.iAGridX);//x1-x2
        int iy = Mathf.Abs(a_nodeA.iAGridY - a_nodeB.iAGridY);//y1-y2

        return ix + iy;//Return the sum
    }
}
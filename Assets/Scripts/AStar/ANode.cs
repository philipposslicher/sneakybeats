using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANode
{

    public int iAGridX;//X Position in the Node Array
    public int iAGridY;//Y Position in the Node Array

    public bool bIsWall;//Tells the program if this node is being obstructed.
    public Vector3 vPosition;//The world position of the node.

    public ANode ParentANode;//For the AStar algoritm, will store what node it previously came from so it cn trace the shortest path.

    public int igCost;//The cost of moving to the next square.
    public int ihCost;//The distance to the goal from this node.

    public int FCost { get { return igCost + ihCost; } }//Quick get function to add G cost and H Cost, and since we'll never need to edit FCost, we dont need a set function.

    public ANode(bool a_bIsWall, Vector3 a_vPos, int a_iAgridX, int a_iAgridY)//Constructor
    {
        bIsWall = a_bIsWall;//Tells the program if this node is being obstructed.
        vPosition = a_vPos;//The world position of the node.
        iAGridX = a_iAgridX;//X Position in the Node Array
        iAGridY = a_iAgridY;//Y Position in the Node Array
    }

}
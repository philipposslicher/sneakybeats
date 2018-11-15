using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGrid : MonoBehaviour
{

    public Transform StartPosition;//This is where the program will start the pathfinding from.
    public LayerMask WallMask;//This is the mask that the program will look for when trying to find obstructions to the path.
    public Vector2 vAGridWorldSize;//A vector2 to store the width and height of the graph in world units.
    public float fANodeRadius;//This stores how big each square on the graph will be
    public float fDistanceBetweenANodes;//The distance that the squares will spawn from eachother.

    ANode[,] ANodeArray;//The array of ANodes that the A Star algorithm uses.
    public List<ANode> FinalPath;//The completed path that the red line will be drawn along


    float fANodeDiameter;//Twice the amount of the radius (Set in the start function)
    int iAGridSizeX, iAGridSizeY;//Size of the AGrid in Array units.


    private void Start()//Ran once the program starts
    {
        fANodeDiameter = fANodeRadius * 2;//Double the radius to get diameter
        iAGridSizeX = Mathf.RoundToInt(vAGridWorldSize.x / fANodeDiameter);//Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
        iAGridSizeY = Mathf.RoundToInt(vAGridWorldSize.y / fANodeDiameter);//Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
        CreateAGrid();//Draw the grid
    }

    void CreateAGrid()
    {
        ANodeArray = new ANode[iAGridSizeX, iAGridSizeY];//Declare the array of ANodes.
        Vector3 bottomLeft = transform.position - Vector3.right * vAGridWorldSize.x / 2 - Vector3.forward * vAGridWorldSize.y / 2;//Get the real world position of the bottom left of the grid.
        for (int x = 0; x < iAGridSizeX; x++)//Loop through the array of ANodes.
        {
            for (int y = 0; y < iAGridSizeY; y++)//Loop through the array of ANodes
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * fANodeDiameter + fANodeRadius) + Vector3.forward * (y * fANodeDiameter + fANodeRadius);//Get the world co ordinates of the bottom left of the graph
                bool Wall = true;//Make the ANode a wall

                //If the ANode is not being obstructed
                //Quick collision check against the current ANode and anything in the world at its position. If it is colliding with an object with a WallMask,
                //The if statement will return false.
                if (Physics.CheckSphere(worldPoint, fANodeRadius, WallMask))
                {
                    Wall = false;//Object is not a wall
                }

                ANodeArray[x, y] = new ANode(Wall, worldPoint, x, y);//Create a new ANode in the array.
            }
        }
    }

    //Function that gets the neighboring ANodes of the given ANode.
    public List<ANode> GetNeighboringANodes(ANode a_NeighborANode)
    {
        List<ANode> NeighborList = new List<ANode>();//Make a new list of all available neighbors.
        int icheckX;//Variable to check if the XPosition is within range of the ANode array to avoid out of range errors.
        int icheckY;//Variable to check if the YPosition is within range of the ANode array to avoid out of range errors.

        //Check the right side of the current ANode.
        icheckX = a_NeighborANode.iAGridX + 1;
        icheckY = a_NeighborANode.iAGridY;
        if (icheckX >= 0 && icheckX < iAGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < iAGridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(ANodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Left side of the current ANode.
        icheckX = a_NeighborANode.iAGridX - 1;
        icheckY = a_NeighborANode.iAGridY;
        if (icheckX >= 0 && icheckX < iAGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < iAGridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(ANodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Top side of the current ANode.
        icheckX = a_NeighborANode.iAGridX;
        icheckY = a_NeighborANode.iAGridY + 1;
        if (icheckX >= 0 && icheckX < iAGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < iAGridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(ANodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }
        //Check the Bottom side of the current ANode.
        icheckX = a_NeighborANode.iAGridX;
        icheckY = a_NeighborANode.iAGridY - 1;
        if (icheckX >= 0 && icheckX < iAGridSizeX)//If the XPosition is in range of the array
        {
            if (icheckY >= 0 && icheckY < iAGridSizeY)//If the YPosition is in range of the array
            {
                NeighborList.Add(ANodeArray[icheckX, icheckY]);//Add the grid to the available neighbors list
            }
        }

        return NeighborList;//Return the neighbors list.
    }

    //Gets the closest ANode to the given world position.
    public ANode ANodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        float ixPos = ((a_vWorldPos.x + vAGridWorldSize.x / 2) / vAGridWorldSize.x);
        float iyPos = ((a_vWorldPos.z + vAGridWorldSize.y / 2) / vAGridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((iAGridSizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((iAGridSizeY - 1) * iyPos);

        return ANodeArray[ix, iy];
    }


    //Function that draws the wireframe
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, new Vector3(vAGridWorldSize.x, vAGridWorldSize.y, 1));//Draw a wire cube with the given dimensions from the Unity inspector

        if (ANodeArray != null)//If the grid is not empty
        {
            foreach (ANode n in ANodeArray)//Loop through every ANode in the grid
            {
                if (n.bIsWall)//If the current ANode is a wall ANode
                {
                    Gizmos.color = Color.white;//Set the color of the ANode
                }
                else
                {
                    Gizmos.color = Color.yellow;//Set the color of the ANode
                }


                if (FinalPath != null)//If the final path is not empty
                {
                    if (FinalPath.Contains(n))//If the current ANode is in the final path
                    {
                        Gizmos.color = Color.red;//Set the color of that ANode
                    }

                }


                Gizmos.DrawCube(n.vPosition, Vector3.one * (fANodeDiameter - fDistanceBetweenANodes));//Draw the ANode at the position of the ANode.
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public static class AStarGridGraph
{
    public static void CreateGraph(Vector2 center)
    {
        Debug.Log("AStarGridGraph " + center);
        // This holds all graph data
        AstarData data = AstarPath.active.data;

        // This creates a Grid Graph
        GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;

        // Setup a grid graph with some values
        int width = 50;
        int depth = 50;
        float nodeSize = 1;

        gg.center = center;

        // Updates internal size from the above values
        gg.SetDimensions(width, depth, nodeSize);

        // Scans all graphs
        AstarPath.active.Scan();
    }
}

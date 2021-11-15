using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public static class AStarGridGraph
{
    public static void UpdateGraph(Vector2 centre)
    {
        //Debug.Log("AStarGridGraph " + centre);
        // This holds all graph data
        AstarData data = AstarPath.active.data;

        var gg = AstarPath.active.data.gridGraph;

        gg.center = centre;

        // Scans all graphs
        AstarPath.active.Scan();
    }
}

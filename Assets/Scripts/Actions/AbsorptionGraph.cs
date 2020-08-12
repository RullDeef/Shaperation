using System;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionGraph
{
    public class GraphNode
    {
        public Shape shape;
        public List<GraphNode> connectionsFrom = new List<GraphNode>();
        public List<GraphNode> connectionsTo = new List<GraphNode>();

        public bool IsRoot => connectionsFrom.Count == 0;
        public bool IsLeaf => connectionsTo.Count == 0;

        public bool IsExecuted = false;

        public GraphNode(Shape shape)
        {
            this.shape = shape;
        }
    }

    List<GraphNode> m_Nodes = new List<GraphNode>();
    public List<GraphNode> Nodes { get => m_Nodes; }

    private bool HasShapeAsNode(Shape shape)
    {
        foreach (GraphNode node in m_Nodes)
            if (node.shape == shape)
                return true;
        return false;
    }

    private GraphNode GetCorrespondingNode(Shape shape)
    {
        foreach (GraphNode node in m_Nodes)
            if (node.shape == shape)
                return node;

        Debug.LogError("Unreachable code block!!");
        return new GraphNode(shape);
    }

    private GraphNode addShapeAsNode(Shape shape)
    {
        if (!HasShapeAsNode(shape))
        {
            GraphNode node = new GraphNode(shape);
            m_Nodes.Add(node);
            // Debug.Log("Added shape as graph Node!");
            return node;
        }
        else return GetCorrespondingNode(shape);
    }

    private void ApplyAbsorption(GraphNode nodeFrom, GraphNode nodeTo)
    {
        nodeFrom.connectionsFrom.Add(nodeTo);
        nodeTo.connectionsTo.Add(nodeFrom);
    }

    public static AbsorptionGraph FromContext(TriggerableContext context)
    {
        AbsorptionGraph graph = new AbsorptionGraph();

        // wrap each context shape as graph node
        foreach (Absorption absorption in context.Absorptions)
        {
            GraphNode nodeFrom = graph.addShapeAsNode(absorption.Consumee);
            GraphNode nodeTo = graph.addShapeAsNode(absorption.Consumer);

            // setup proper connections between nodes
            graph.ApplyAbsorption(nodeFrom, nodeTo);
        }

        return graph;
    }

    private void ExecuteNode(GraphNode node)
    {
        foreach (GraphNode child in node.connectionsTo)
            ExecuteNode(child);

        node.shape.MakeTriggerAction(node, this);
        node.IsExecuted = true;

        // Debug.Log("Executed particular node!");
    }

    public void Execute()
    {
        // Debug.Log($"Executing graph with {m_Nodes.Count} nodes.");
        foreach (GraphNode node in m_Nodes)
        {
            if (!node.IsExecuted && node.IsRoot)
                ExecuteNode(node);
            // else
            // {
            //     Debug.Log($"Graph.Execute: node is executed({node.IsExecuted}) and Root({node.IsRoot})");
            // }
        }
    }
}
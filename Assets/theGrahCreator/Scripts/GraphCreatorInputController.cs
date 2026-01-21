using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles user input for graph point manipulation.
/// </summary>
public class GraphCreatorInputController : MonoBehaviour
{
    [SerializeField] private GraphCreator graph;
    [SerializeField] private InputField inputFieldX;
    [SerializeField] private InputField inputFieldY;

    public void AddPoint()
    {
        double x = CustomParser.ParseToDouble(inputFieldX.text);
        double y = CustomParser.ParseToDouble(inputFieldY.text);

        if (double.IsNaN(x) || double.IsNaN(y)) return;

        graph.InputX = (float)x;
        graph.InputY = (float)y;
        graph.AddPoint();
    }

    public void ClearGraph()
    {
        graph.DeleteAllPoints();
    }

    public void DeleteLastPoint()
    {
        graph.DeletePoint();
    }
}

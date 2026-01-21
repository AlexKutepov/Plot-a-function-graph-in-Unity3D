using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using TMPro;

/// <summary>
/// Creates and manages a function graph with dynamic point plotting.
/// </summary>
public class GraphCreator : MonoBehaviour
{
    [Header("Point Configuration")]
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private Transform pointContainer;

    [Header("Line Renderer")]
    [SerializeField] private UILineRenderer lineRenderer;

    [Header("Axis Labels")]
    [SerializeField] private Transform axisLabelContainer;

    [Header("Graph Dimensions")]
    [SerializeField] private float axisLengthX = 50f;
    [SerializeField] private float axisLengthY = 50f;
    [SerializeField] private int stepsX = 10;
    [SerializeField] private int stepsY = 10;

    [Header("Origin Offset")]
    [SerializeField] private float originX = 0f;
    [SerializeField] private float originY = 0f;

    public List<Vector2> DataPoints { get; private set; } = new List<Vector2>();
    public List<GameObject> PointInstances { get; private set; } = new List<GameObject>();

    public float InputX { private get; set; }
    public float InputY { private get; set; }

    private RectTransform rectTransform;
    private Transform pointContainerInitial;
    private Vector2 lastPlottedPoint;
    private float previousInputX;
    private float previousInputY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        pointContainerInitial = pointContainer;
    }

    private void Start()
    {
        InitializeGraph();
    }

    public void InitializeGraph()
    {
        CreateAxisLabels(Axis.X);
        CreateAxisLabels(Axis.Y);
    }

    private enum Axis { X, Y }

    private void CreateAxisLabels(Axis axis)
    {
        float axisLength = axis == Axis.X ? axisLengthX : axisLengthY;
        int steps = axis == Axis.X ? stepsX : stepsY;
        float origin = axis == Axis.X ? originX : originY;
        int templateIndex = axis == Axis.X ? 0 : 1;

        if (Mathf.Approximately(axisLength, 0f)) return;

        float stepSize = Mathf.Abs(axisLength / steps);
        bool isPositive = axisLength > 0;

        float current = origin;
        while (isPositive ? current <= axisLength : current >= axisLength)
        {
            CreateAxisLabel(axis, current, templateIndex);
            current += isPositive ? stepSize : -stepSize;
        }

        axisLabelContainer.GetChild(templateIndex).gameObject.SetActive(false);
    }

    private void CreateAxisLabel(Axis axis, float value, int templateIndex)
    {
        GameObject template = axisLabelContainer.GetChild(templateIndex).gameObject;
        GameObject label = Instantiate(template, axisLabelContainer);

        RectTransform labelRect = label.GetComponent<RectTransform>();
        TextMeshProUGUI labelText = label.GetComponent<TextMeshProUGUI>();
        RectTransform gridLineRect = label.transform.GetChild(0).GetComponent<RectTransform>();

        float axisLength = axis == Axis.X ? axisLengthX : axisLengthY;
        float graphSize = axis == Axis.X ? rectTransform.sizeDelta.x : rectTransform.sizeDelta.y;
        float normalizedPosition = (value * graphSize) / axisLength;

        if (axis == Axis.X)
        {
            labelRect.anchoredPosition = new Vector2(normalizedPosition, 0f);
            gridLineRect.sizeDelta = new Vector2(1f, rectTransform.sizeDelta.y);
        }
        else
        {
            labelRect.anchoredPosition = new Vector2(0f, normalizedPosition);
            gridLineRect.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 1f);

            if (value <= 0f)
            {
                labelText.text = string.Empty;
                return;
            }
        }

        labelText.text = Math.Round(value, 1).ToString();

        if (Mathf.Approximately(value, 0f))
        {
            gridLineRect.gameObject.SetActive(false);
        }
    }

    public void AddPoint()
    {
        if (!ValidatePoint()) return;
        if (lastPlottedPoint == new Vector2(InputX, InputY)) return;

        Vector2 newPoint = new Vector2(InputX, InputY);
        DataPoints.Insert(0, newPoint);

        GameObject pointInstance = Instantiate(pointPrefab, pointContainer.position, pointContainer.rotation);
        pointInstance.SetActive(true);
        pointInstance.transform.SetParent(pointPrefab.transform.parent);
        pointInstance.transform.localScale = Vector3.one;

        RectTransform pointRect = pointInstance.GetComponent<RectTransform>();
        Vector2 graphPosition = CalculateGraphPosition(InputX, InputY);
        pointRect.anchoredPosition = pointRect.anchoredPosition + graphPosition;

        PointInstances.Insert(0, pointInstance);
        UpdateLineRenderer();

        lastPlottedPoint = newPoint;
    }

    private Vector2 CalculateGraphPosition(float x, float y)
    {
        float posX = x * rectTransform.sizeDelta.x / axisLengthX;
        float posY = y * rectTransform.sizeDelta.y / axisLengthY;
        return new Vector2(posX, posY);
    }

    private void UpdateLineRenderer()
    {
        Vector2[] points = new Vector2[PointInstances.Count];
        for (int i = 0; i < PointInstances.Count; i++)
        {
            RectTransform rect = PointInstances[i].GetComponent<RectTransform>();
            points[i] = rect.anchoredPosition;
        }
        lineRenderer.Points = points;
    }

    private bool ValidatePoint()
    {
        if (Mathf.Approximately(previousInputX, InputX) && 
            Mathf.Approximately(previousInputY, InputY))
            return false;

        if (Mathf.Abs(InputX) > Mathf.Abs(axisLengthX) || 
            Mathf.Abs(InputY) > Mathf.Abs(axisLengthY))
            return false;

        return true;
    }

    public void DeletePoint()
    {
        if (lineRenderer.Points.Length == 0) return;

        Vector2[] newPoints = new Vector2[lineRenderer.Points.Length - 1];
        Array.Copy(lineRenderer.Points, 1, newPoints, 0, newPoints.Length);
        lineRenderer.Points = newPoints;

        Destroy(PointInstances[0]);
        PointInstances.RemoveAt(0);
        DataPoints.RemoveAt(0);

        lastPlottedPoint = Vector2.zero;
    }

    public void DeleteAllPoints()
    {
        if (PointInstances.Count == 0) return;

        foreach (var point in PointInstances)
        {
            Destroy(point);
        }

        PointInstances.Clear();
        DataPoints.Clear();
        lineRenderer.Points = Array.Empty<Vector2>();
        pointContainer = pointContainerInitial;
        lastPlottedPoint = Vector2.zero;
    }
}

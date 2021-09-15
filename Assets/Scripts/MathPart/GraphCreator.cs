using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System;

public class GraphCreator : MonoBehaviour {

    [Header("Точки для вставки в график")]
    [SerializeField] GameObject PointFor;
    [SerializeField] Transform PointForUTransform;
    Transform PointForUTransformTmp;

    [Header("UILineRenderer для создания графикa")]
    [SerializeField] UILineRenderer LineRenderForCreateGraph;

    [Header("Список вставленных точек в график")]
    public List<GameObject> InstancePoints;

    [Header("Сам график, где рисовать")]
    [SerializeField] GameObject Grafik;

    [Header("Объект, хранящий значения на осях")]
    [SerializeField]
    Transform axisTextHolder;

    [Header("Длина осей в графике по сторонам Х и У")]
    [SerializeField] float sizeStepX = 50;
    [SerializeField] float sizeStepY = 50;

    [Header("Количество шагов по оси Х")]
    [SerializeField]
    int Xsteps;
    [Header("Количество шагов по оси Y")]
    [SerializeField]
    int Ysteps;

    public List<Vector2> dataPoints = new List<Vector2>();
    RectTransform mainTransform;


    private void Awake() {
        mainTransform = GetComponent<RectTransform>();
        PointForUTransformTmp = PointForUTransform;
    }

    void Start() {
        CreateStartGraph();
    }

    public void CreateStartGraph()  {
        AddTextForX();
        AddTextForY();
    }

    [SerializeField]
    float StartCoordinateX=0;
    void AddTextForX() {
        if (sizeStepX>=0){
            float step = sizeStepX / Xsteps;
            for (float i = StartCoordinateX; i <= sizeStepX; i += step) {
                GameObject newText = Instantiate(axisTextHolder.GetChild(0).gameObject, axisTextHolder);
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector2((i * mainTransform.sizeDelta.x) / sizeStepX, 0f);
                newText.GetComponent<Text>().text = Math.Round(i, 1).ToString();
                newText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(1, mainTransform.sizeDelta.y);
                if (i == 0f) newText.transform.GetChild(0).gameObject.SetActive(false);
            }
            axisTextHolder.GetChild(0).gameObject.SetActive(false);
        } else {
            float step = Math.Abs(sizeStepX / Xsteps);
            for (float i = StartCoordinateX; i >= sizeStepX; i -= step)  {
                GameObject newText = Instantiate(axisTextHolder.GetChild(0).gameObject, axisTextHolder);
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector2((i * mainTransform.sizeDelta.x) / sizeStepX, 0f);
                newText.GetComponent<Text>().text = Math.Round(sizeStepX-i, 1).ToString();
                newText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(1, mainTransform.sizeDelta.y);
                if (i == 0f) newText.transform.GetChild(0).gameObject.SetActive(false);
            }
            axisTextHolder.GetChild(0).gameObject.SetActive(false);
        }
    }
    [SerializeField]
    float StartCoordinateY=0;
    void AddTextForY() {
        if (sizeStepY>=0){
            float step = sizeStepY / Ysteps;
            for (float i = StartCoordinateY; i <= sizeStepY; i += step) {
                GameObject newText = Instantiate(axisTextHolder.GetChild(1).gameObject, axisTextHolder);
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, (i * mainTransform.sizeDelta.y) / sizeStepY);
                if (i > 0f) newText.GetComponent<Text>().text = Math.Round(i, 1).ToString() + "  ";
                newText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(mainTransform.sizeDelta.x, 1);
                if (i == 0f) newText.transform.GetChild(0).gameObject.SetActive(false);
            }
            axisTextHolder.GetChild(1).gameObject.SetActive(false);
        } else {
            float step = sizeStepY / Ysteps;
            for (float i = StartCoordinateY; i >= sizeStepY; i -= step)  {
                GameObject newText = Instantiate(axisTextHolder.GetChild(1).gameObject, axisTextHolder);
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, (i * mainTransform.sizeDelta.y) / sizeStepY);
                if (i > 0f) newText.GetComponent<Text>().text = Math.Round(sizeStepY-1, 1).ToString() + "  ";
                newText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(mainTransform.sizeDelta.x, 1);
                if (i == 0f) newText.transform.GetChild(0).gameObject.SetActive(false);
            }
            axisTextHolder.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void DeleteAllPoints() {
        DeleteEveryGameObj(InstancePoints);
        LineRenderForCreateGraph.Points = new Vector2[0];
        PointForUTransform = PointForUTransformTmp;
        lastPoint = Vector2.zero;
        dataPoints.Clear();
    }

    void DeleteEveryGameObj(List<GameObject> DeleteGameObj) {
        if (DeleteGameObj.Count != 0) {
            for (int i = DeleteGameObj.Count - 1; i >= 0; i--)
                Destroy(DeleteGameObj[i]);
            DeleteGameObj.Clear();
        }
    }

    public float valueX { private get; set; }
    public float valueY { private get; set; }
    Vector2 lastPoint;
   // int pointIplus = 0;

    public void AddPoint() {
     
        if (lastPoint == new Vector2(valueX, valueY)) return;
        dataPoints.Insert(0, new Vector2(valueX, valueY));
        InstancePoints.Insert(0, Instantiate(PointFor, PointForUTransform.position,
        PointForUTransform.rotation));
        InstancePoints[0].SetActive(true);
        InstancePoints[0].transform.SetParent(PointFor.transform.parent);
        InstancePoints[0].transform.localScale = Vector3.one;
        InstancePoints[0].GetComponent<RectTransform>().anchoredPosition =
        new Vector3(InstancePoints[0].GetComponent<RectTransform>().anchoredPosition.x
            + valueX *
             Grafik.GetComponent<RectTransform>().sizeDelta.x / sizeStepX,
            InstancePoints[0].GetComponent<RectTransform>().anchoredPosition.y
            + valueY *
            Grafik.GetComponent<RectTransform>().sizeDelta.y / sizeStepY, 0);
        LineRenderForCreateGraph.Points = new Vector2[InstancePoints.Count];
        for (int i = 0; i < InstancePoints.Count; i++) {
            LineRenderForCreateGraph.Points[i] =  new Vector2(InstancePoints[i].GetComponent<RectTransform>().anchoredPosition.x,
            InstancePoints[i].GetComponent<RectTransform>().anchoredPosition.y);
               
        }
        //pointIplus++;
        lastPoint = new Vector2(valueX, valueY);
    }

    public void DeletePoint() {
        if (LineRenderForCreateGraph.Points.Length == 0) return;
        Vector2[] newPoints = new Vector2[LineRenderForCreateGraph.Points.Length - 1];
        for (int i = 0; i < newPoints.Length; i++) newPoints[i] = LineRenderForCreateGraph.Points[i + 1];
        LineRenderForCreateGraph.Points = newPoints;

        Destroy(InstancePoints[0]);
        InstancePoints.RemoveAt(0);
        lastPoint = Vector2.zero;
        dataPoints.RemoveAt(0);
    }
}
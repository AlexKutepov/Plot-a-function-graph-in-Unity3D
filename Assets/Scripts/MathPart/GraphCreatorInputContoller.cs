using System;
using UnityEngine;
using UnityEngine.UI;

public class GraphCreatorInputContoller : MonoBehaviour {

    public GraphCreator graph;

    public InputField inputX;
    public InputField inputY;

    public void SetPoint() {
        if (CustomParser.ParseToDouble(inputX.text)!=Double.NaN) graph.valueX = (float)CustomParser.ParseToDouble(inputX.text);
        else return;
        if (CustomParser.ParseToDouble(inputY.text) != Double.NaN) graph.valueY = (float)CustomParser.ParseToDouble(inputY.text);
        else return;
        graph.AddPoint();
    }

    public void ClearGrahp() {
        graph.DeleteAllPoints();
    }

    public void DeletePrewPoint() {
        graph.DeletePoint();
    }

}

using System;
using UnityEngine;
using UnityEngine.UI;

public class GraphCreatorInputContoller : MonoBehaviour {

    public GraphCreator graph;

    public InputField inputX;
    public InputField inputY;

    public void SetPoint() {
        if (CustomParser.ParseToDouble(inputX.text)==Double.NaN || 
        CustomParser.ParseToDouble(inputY.text) == Double.NaN) return;
        graph.valueX = (float)CustomParser.ParseToDouble(inputX.text);
        graph.valueY = (float)CustomParser.ParseToDouble(inputY.text);
        graph.AddPoint();
    }

    public void ClearGrahp() {
        graph.DeleteAllPoints();
    }

    public void DeletePrewPoint() {
        graph.DeletePoint();
    }

}

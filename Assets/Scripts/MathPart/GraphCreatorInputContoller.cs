using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System;

public class GraphCreatorInputContoller : MonoBehaviour {
    // Start is called before the first frame update
    public GraphCreator graph;

    public InputField inputX;
    public InputField inputY;

    public void SetPoint() {
        if (ParserHelp.ParseToDouble(inputX.text)!=Double.NaN) graph.valueX = (float)ParserHelp.ParseToDouble(inputX.text);
        else return;
        if (ParserHelp.ParseToDouble(inputY.text) != Double.NaN) graph.valueY = (float)ParserHelp.ParseToDouble(inputY.text);
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

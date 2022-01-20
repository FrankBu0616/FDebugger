using System.Collections.Generic;
using UnityEngine;

public struct dfloat
{
    public dfloat(float fvalue, float lower = 0.0f, float upper = 1.0f)
    {
        value = fvalue;
        fmin = lower;
        fmax = upper;
    }

    public float fmin { get; }
    public float fmax { get; }
    public float value { get; }

    public override string ToString() => $"({value}, {fmin}, {fmax})";
}

public class FDebugger : MonoBehaviour
{
    // Start is called before the first frame update
    private Dictionary<string, dfloat> floatDict = new Dictionary<string, dfloat>();
    public static FDebugger Singleton { get; private set; }
    private void SetSingleton() { Singleton = this; }
    private void Awake() {
        if (Singleton != null && Singleton != this) {
            Destroy(this);
            return;
        }

        SetSingleton();
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnGUI()
    {
        GUI.Box(new Rect(10, 500, 160, 20 + floatDict.Count * 40), "FDebugger");
        int count = 0;
        foreach(KeyValuePair<string, dfloat> entry in floatDict)
        {
            float v = entry.Value.value;
            GUI.Label(new Rect(20, 520 + count * 40, 60, 20), entry.Key);
            GUI.Label(new Rect(100, 520 + count * 40, 60, 20), v.ToString());
            GUI.HorizontalSlider(new Rect(60, 520 + count * 40 + 20, 100, 10), v, entry.Value.fmin, entry.Value.fmax);
            count++;
        }
    }

    public void inspect(string float_name, float value, float lower = 0.0f, float upper = 1.0f)
    {
        dfloat f = new dfloat(value, lower, upper);
        floatDict[float_name] = f;
    }
}

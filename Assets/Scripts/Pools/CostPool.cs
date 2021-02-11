using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[ExecuteInEditMode]
public class CostPool : MonoBehaviour
{
    public int maxResource = 10;
    public int currentResource = 10;
    public TMP_Text poolText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        poolText.text = string.Format("{0} / {1}", currentResource, maxResource);
    }
}

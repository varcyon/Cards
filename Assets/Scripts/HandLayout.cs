using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[ExecuteInEditMode]
public class HandLayout : MonoBehaviour {
    public int childCount;
    public float distanceBetween = 0.12f;
    public List<Transform> childrenList = new List<Transform>();
    void Start() {
        childCount = GetChildCount();
    }

    // Update is called once per frame
    void Update() {
        ChangePositions();
    }

    public int GetChildCount() {
        return this.transform.childCount;
    }

    public void ChangePositions() {
        childrenList = transform.GetChildrenTransforms();
        foreach (Transform item in childrenList) {
            item.localPosition = new Vector3(childrenList.IndexOf(item) * distanceBetween, 0, 0);
        }
        if(childrenList.Count > 0) {
            float xDiff = (childrenList[0].transform.localPosition.x - childrenList[childrenList.Count - 1].transform.localPosition.x) /2;
            foreach (Transform item in childrenList) {
                item.localPosition = new Vector3(item.localPosition.x + xDiff, 0, 0);
            }
        }
    }
}

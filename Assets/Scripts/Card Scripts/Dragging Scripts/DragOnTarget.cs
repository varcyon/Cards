using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnTarget : DraggingActions {
    public GameObject targetRoot;
    private GameObject arrow;
    private LineRenderer LR;
    private CardManager card;
    private Vector3 cardTargetStart;
    private Vector3 cardTargetEnd;
    private Vector3 mousePos;
    private RaycastHit hit;
    public override void OnDraggingInUpdate() {
        Ray ray = MouseInWorldCoords();
        if(Physics.Raycast(ray, out hit)) {
            cardTargetEnd = hit.point;
            
            targetRoot.transform.position = hit.point;
        }

        LR.positionCount = 2;
        LR.SetPositions(new Vector3[] { cardTargetStart, cardTargetEnd });
    }



    public override void OnEndDrag() {
        LR.positionCount = 0;
        targetRoot.SetActive(false);
        targetRoot.transform.position = transform.position;
    }

    public override void OnStartDrag() {
        cardTargetStart = transform.position;
        cardTargetStart.z = -16f;
        targetRoot.SetActive(true);

    }

    protected override bool DragSuccessful() {
        return true;
    }

    private void Awake() {
        targetRoot.SetActive(false);
        card = GetComponent<CardManager>();
        LR = GetComponent<LineRenderer>();
    }
    public Ray MouseInWorldCoords() {
        Vector3 screenMousePos = Input.mousePosition;
        return Camera.main.ScreenPointToRay(screenMousePos);

    }
}

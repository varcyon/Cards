using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnTarget : DraggingActions
{
    private GameObject targetRoot;
    private GameObject arrow;
    private LineRenderer LR;
    public override void OnDraggingInUpdate() {
        Vector3 notNormalized = transform.position - targetRoot.transform.GetChild(0).transform.position;
        Vector3 direction = notNormalized.normalized;
        float distanceToTarget = (direction * 2.3f).magnitude;
        if(notNormalized.magnitude > distanceToTarget) {
            LR.SetPositions(new Vector3[] { targetRoot.transform.position, transform.position - direction * 2.3f });
            arrow.transform.position = transform.position - 1.5f * direction;
            float rot_z = Mathf.Atan2(notNormalized.y, notNormalized.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        } else {
            targetRoot.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

        

    public override void OnEndDrag() {
        transform.localPosition = new Vector3(0f, 0f, -1f);
        targetRoot.transform.GetChild(0).gameObject.SetActive(false);
    }

    public override void OnStartDrag() {
        targetRoot.transform.GetChild(0).gameObject.SetActive(true);
    }

    protected override bool DragSuccessful() {
        return true;
    }

    private void Awake() {
        targetRoot = GameObject.FindGameObjectWithTag("TargetGraphics");
        targetRoot.transform.GetChild(0).gameObject.SetActive(false);
        LR = targetRoot.GetComponentInChildren<LineRenderer>();
        arrow = targetRoot.transform.GetChild(0).transform.GetChild(0).gameObject;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

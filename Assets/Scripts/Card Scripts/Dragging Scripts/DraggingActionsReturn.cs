using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DraggingActionsReturn : DraggingActions
{
    private Vector3 savedPos;
    public Transform oldParent;

    public override void OnStartDrag()
    {
        savedPos = transform.position;
        oldParent = this.transform.parent;
        this.transform.SetParent(oldParent.parent);
    }

    public override void OnEndDrag()
    {
        //transform.DOMove(savedPos, 1f); 
        //transform.DOMove(savedPos, 1f).SetEase(Ease.OutBounce, 0.5f, 0.1f);
        this.transform.SetParent(oldParent);
       // transform.DOMove(savedPos, 1f).SetEase(Ease.OutQuint);//, 0.5f, 0.1f);
    }

    public override void OnDraggingInUpdate(){}

    protected override bool DragSuccessful()
    {
        return true;
    }
}

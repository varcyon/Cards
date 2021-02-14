using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Draggable : MonoBehaviour {

    private bool dragging = false;

    private Vector3 pointerDisplacement;

    private float zDisplacement;

    private DraggingActions da;
    private DragOnTarget dt;
    private static Draggable _draggingThis;
    public static Draggable DraggingThis {
        get { return _draggingThis; }
    }
    private CardManager card;

    void Awake() {
        da = GetComponent<DraggingActions>();
        dt = GetComponent<DragOnTarget>();
        card = GetComponent<CardManager>();
    }

    void OnMouseDown() {
        if (da != null && da.CanDrag) {
            dragging = true;
            HoverPreview.PreviewsAllowed = false;
            _draggingThis = this;
            da.OnStartDrag();
            zDisplacement = -Camera.main.transform.position.z + transform.position.z;
            pointerDisplacement = -transform.position + MouseInWorldCoords();
        }
        if(dragging && card.inPlay && card.isMine) {
            dt.OnStartDrag();
        }
    }

    void Update() {
        if (dragging && card.isMine && !card.inPlay) {
            Vector3 mousePos = MouseInWorldCoords();
            transform.position = new Vector3(mousePos.x - pointerDisplacement.x, mousePos.y - pointerDisplacement.y, transform.position.z);
            //da.OnDraggingInUpdate();
        }
        if(dragging && card.inPlay && card.isMine) {
            dt.OnDraggingInUpdate();
        }
    }

    void OnMouseUp() {
        if (dragging && card.isMine) {
            dragging = false;
            HoverPreview.PreviewsAllowed = true;
            _draggingThis = null;
            da.OnEndDrag();
            dt.OnEndDrag();
        }
    }

    public Vector3 MouseInWorldCoords() {
        var screenMousePos = Input.mousePosition;
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }


}

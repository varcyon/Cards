using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CardRotation : MonoBehaviour
{
    public float cosAngle;
    public GameObject Front;
    public GameObject back;

    void Update()
    {
      cosAngle =   Vector3.Dot((Camera.main.transform.position - this.transform.position).normalized, this.transform.forward);
      if(cosAngle > 0) {
            back.SetActive(true);
            Front.SetActive(false);
      } else {
            back.SetActive(false);
            Front.SetActive(true);
        }
    }
}

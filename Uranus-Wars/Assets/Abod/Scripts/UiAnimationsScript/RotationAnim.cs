using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnim : MonoBehaviour
{
    private void Start()
    {
        LeanTween.rotateAround(this.gameObject , Vector3.up , 360 , 3f).setLoopClamp();
    }
}

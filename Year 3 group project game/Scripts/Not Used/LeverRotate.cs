using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRotate : AffectedObject
{
    public override void ExecuteAction()
    {
        StartCoroutine(RotateObject());
    }

    private IEnumerator RotateObject()
    {
        yield return new WaitForSeconds(1);
    }
}

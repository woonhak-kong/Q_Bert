using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SpinPad : Block
{
    public Transform Destination;

    private bool _isMoving = false;
    public override void SetComplete()
    {
        //base.SetComplete();
        isComplete = true;
    }

    public void StartSpinpadSequences(Callback callback)
    {
        StartCoroutine(MoveToPosition(Destination, callback));
    }

    protected virtual IEnumerator MoveToPosition(Transform target, Callback callback)
    {

        Vector3 currentPosition = transform.position;
        Vector2 position = target.position;


        _isMoving = true;
        float ElapsedTime = 0.0f;
        float FinishTime = 1.0f;

        yield return new WaitForSeconds(0.5f);

        while (_isMoving)
        {
            ElapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition, position, (ElapsedTime / FinishTime));

            yield return null;
            if (Vector3.Distance(transform.position, position) < 0.0001f)
            {
                //Debug.Log("arrived!");
                _isMoving = false;
                ElapsedTime = 0;
                callback();
            }

        }
    }
}

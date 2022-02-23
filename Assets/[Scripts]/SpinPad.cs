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

        Vector3 currentPosition1 = transform.position;
        Vector2 position1 = currentPosition1 + Vector3.up * 0.5f;
        Vector2 position2 = target.position;


        _isMoving = true;
        float ElapsedTime = 0.0f;
        float FinishTime1 = 0.2f;
        float FinishTime2 = 2.3f;

        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.PlaySound(Sounds.Lift);

        while (_isMoving)
        {
            ElapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition1, position1, (ElapsedTime / FinishTime1));

            yield return null;
            if (Vector3.Distance(transform.position, position1) < 0.0001f)
            {
                //Debug.Log("arrived!");
                _isMoving = false;
                ElapsedTime = 0;
            }
        }
        Vector3 currentPosition2 = transform.position;
        _isMoving = true;
        while (_isMoving)
        {
            ElapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition2, position2, (ElapsedTime / FinishTime2));

            yield return null;
            if (Vector3.Distance(transform.position, position2) < 0.0001f)
            {
                //Debug.Log("arrived!");
                _isMoving = false;
                ElapsedTime = 0;
                callback();
            }

        }
    }
}

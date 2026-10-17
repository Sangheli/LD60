using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private readonly Dictionary<Transform, IEnumerator> _started = new();

    public void PlayBounce(Transform view, Vector3 startPosition,AnimationSettings settings)
    {
        _started.TryAdd(view, null);
        
        if (_started[view] != null)
        {
            StopCoroutine(_started[view]);
        }
        
        _started[view] = PlayBounceIEnumerator(view, startPosition, settings);
        StartCoroutine(_started[view]);
    }

    IEnumerator PlayBounceIEnumerator(Transform view,  Vector3 startPosition,AnimationSettings settings)
    {
        var currentTime = 0f;

        while (currentTime<settings.Time)
        {
            var startTime = Time.time;
            var newPos = startPosition + 
                new Vector3
                (
                Random.Range(-settings.Shift, settings.Shift)*settings.ShiftV3.x,
                Random.Range(-settings.Shift, settings.Shift)*settings.ShiftV3.y,
                Random.Range(-settings.Shift, settings.Shift)*settings.ShiftV3.z
                );

            view.localPosition = newPos;
            
            yield return new WaitForSeconds(settings.Time/settings.StepCount);
            currentTime += Time.time - startTime;
        }

        view.localPosition = startPosition;
        _started[view] = null;
    }
    
    public void PlayFloatAnimation(Transform view, Vector3 startPosition,AnimationSettings settings)
    {
        _started.TryAdd(view, null);
        
        if (_started[view] != null)
        {
            StopCoroutine(_started[view]);
        }
        
        _started[view] = PlayFloatAnimationIEnumerator(view, startPosition,settings);
        StartCoroutine(_started[view]);
    }
    
    IEnumerator PlayFloatAnimationIEnumerator(Transform view,  Vector3 startPosition,AnimationSettings settings)
    {
        int currentIndex = 0;
        int currentDirection = 1;
        
        while (view!=null)
        {
            if (currentDirection > 0  && currentIndex >= settings.StepCount)
            {
                currentDirection = -1;
                currentIndex = (int)settings.StepCount;
            }
            else if (currentDirection < 0 && currentIndex <= 0)
            {
                currentDirection = 1;
                currentIndex = 0;
            }
                
            var newPos = startPosition + 
                         new Vector3
                         (
                             0,
                             currentIndex*settings.Shift/settings.StepCount,
                             0
                         );

            view.localPosition = newPos;
            currentIndex+=currentDirection;
            yield return new WaitForSeconds(settings.Time/settings.StepCount);
        }

        _started[view] = null;
    }

    public void PlayScale(Transform view, Vector3 startPosition, AnimationSettings settings)
    {
        _started.TryAdd(view, null);
        
        if (_started[view] != null)
        {
            StopCoroutine(_started[view]);
        }
        
        _started[view] = PlayScaleIEnumerator(view, startPosition,settings);
        StartCoroutine(_started[view]);
    }
    
    IEnumerator PlayScaleIEnumerator(Transform view,  Vector3 startPosition,AnimationSettings settings)
    {
        int currentIndex = settings.GetStartShift();
        int currentDirection = 1;
        var startScale = view.localScale;
        
        while (view!=null)
        {
            if (currentDirection > 0  && currentIndex >= settings.StepCount)
            {
                currentDirection = -1;
                currentIndex = (int)settings.StepCount;
            }
            else if (currentDirection < 0 && currentIndex <= 0)
            {
                currentDirection = 1;
                currentIndex = 0;
            }
                
            var scale = startScale + 
                         new Vector3
                         (
                             currentIndex*settings.Shift/settings.StepCount,
                             currentIndex*settings.Shift/settings.StepCount,
                             1
                         );

            view.localScale = scale;
            currentIndex+=currentDirection;
            yield return new WaitForSeconds(settings.Time/settings.StepCount);
        }

        _started[view] = null;
    }
    
    public void PlayScaleWithEnd(Transform view, Vector3 startScale, AnimationSettings settings)
    {
        _started.TryAdd(view, null);
        
        if (_started[view] != null)
        {
            StopCoroutine(_started[view]);
        }
        
        _started[view] = PlayScaleIEnumeratorWithEnd(view, startScale,settings);
        StartCoroutine(_started[view]);
    }
    
    IEnumerator PlayScaleIEnumeratorWithEnd(Transform view,  Vector3 startScale,AnimationSettings settings)
    {
        var currentTime = 0f;
        int currentIndex = settings.GetStartShift();
        int currentDirection = 1;
        
        while (currentTime<settings.Time)
        {
            var startTime = Time.time;
            if (currentDirection > 0  && currentIndex >= settings.StepCount)
            {
                currentDirection = -1;
                currentIndex = (int)settings.StepCount;
            }
            else if (currentDirection < 0 && currentIndex <= 0)
            {
                currentDirection = 1;
                currentIndex = 0;
            }
                
            var scale = startScale + 
                        new Vector3
                        (
                            currentIndex*settings.Shift/settings.StepCount,
                            currentIndex*settings.Shift/settings.StepCount,
                            currentIndex*settings.Shift/settings.StepCount
                        );

            view.localScale = scale;
            currentIndex+=currentDirection;
            yield return new WaitForSeconds(settings.Time/settings.StepCount);
            currentTime += Time.time - startTime;
        }

        view.localScale = startScale;
        _started[view] = null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BlinkAnimationSequence : MonoBehaviour
{
    [SerializeField] private List<BlinkAnimation> animations;
    private bool isNullList = false;
    private float animationDuration;

    private void OnEnable()
    {
        SetUp();
    }
    private void OnDisable()
    {
        StopCoroutine(DoAnimation());
    }
    private void Start()
    {
        if (isNullList)
            return;
        
        StartCoroutine(DoAnimation());
    }
    private IEnumerator DoAnimation()
    {
        int index = 0;
        int lenght = animations.Count;
        ActiveAnim(0);
        while (true) {
            if (animations[index].IsScaleDown)
            {
                int next = index + 1;

                if (next == lenght)
                {
                    next = 0;
                }

                ActiveAnim(next);
                index = next;
               
            }
            yield return null;
        }
    }
    private void ActiveAnim(int index)
    {
        animations[index].gameObject.SetActive(true);
        animations[index].DoAnimation();
    }

    private void SetUp()
    {
        if (!CanUseAnimation())
            return;

        DisableAnimationObject();
    }

    private void GetAnimationDurationValue()
    {
        Animator animator = animations[0].GetComponentInChildren<Animator>();
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;
        AnimationClip[] clips = controller.animationClips;

        animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        int hashname = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;

       /* Debug.Log($"animation Duration: {animationDuration}");
        if (Animator.StringToHash(clips[0].name) == hashname)
        {
            Debug.Log($"animation name: {clips[0].name}");
        }*/
    }

    private bool CanUseAnimation()
    {
        if (animations == null)
        {
            isNullList = true;
            return false;
        }

        return true;
    }
    private void DisableAnimationObject()
    {
        foreach (var obj in animations)
        {
            if (obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(false);
                obj.onAnimCompleted += () =>
                {
                    obj.gameObject.SetActive(false);
                };
            }
        }
    }
}

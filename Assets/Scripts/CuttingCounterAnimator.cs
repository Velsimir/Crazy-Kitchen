using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CuttingCounterAnimator : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter _cuttingCounter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _cuttingCounter.OnCut += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(CUT);
    }
}

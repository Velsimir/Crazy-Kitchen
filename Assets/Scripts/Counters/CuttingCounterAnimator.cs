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
        _cuttingCounter.Cut += ContainerCounter_OnPlayerCut;
    }

    private void OnDisable()
    {
        _cuttingCounter.Cut -= ContainerCounter_OnPlayerCut;
    }

    private void ContainerCounter_OnPlayerCut(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(CUT);
    }
}

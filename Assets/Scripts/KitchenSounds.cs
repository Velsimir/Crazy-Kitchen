using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class KitchenSounds : MonoBehaviour
{
    [SerializeField] private AudioClipRefSO _audioClipRefSO;

    public static KitchenSounds Instanse { get; private set; }

    private void Awake()
    {
        Instanse = this;
    }

    private void Start()
    {
        DeliveryOrders.Instance.OnRecipeSuccess += DeliveryOrdersOnRecipeSuccess;
        DeliveryOrders.Instance.OnRecipeFailed += DeliveryOrdersOnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounterOnAnyCut;
        Player.Instance.OnPickSomething += PlayerOnPickSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounterOnAnyObjectPlacedHere;
        TrashCounter.OnTrash += TrashCounterOnTrash;
    }

    public void PlayStepSound(Vector3 position, float volume)
    {
        PlaySound(_audioClipRefSO.FootStep, position, volume);
    }

    private void TrashCounterOnTrash(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(_audioClipRefSO.ObjectDrop, trashCounter.transform.position);
    }

    private void BaseCounterOnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(_audioClipRefSO.ObjectDrop, baseCounter.transform.position);
    }

    private void PlayerOnPickSomething(object sender, System.EventArgs e)
    {
        PlaySound(_audioClipRefSO.ObjectPickUp, Player.Instance.transform.position);
    }

    private void CuttingCounterOnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(_audioClipRefSO.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryOrdersOnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instanse;
        PlaySound(_audioClipRefSO.DeliveryFailed, deliveryCounter.transform.position);
    }

    private void DeliveryOrdersOnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instanse;
        PlaySound(_audioClipRefSO.DeliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipRange, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRange[Random.Range(0, audioClipRange.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}

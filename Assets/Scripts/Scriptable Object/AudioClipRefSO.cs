using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

public class AudioClipRefSO : ScriptableObject
{
    public AudioClip[] Chop;
    public AudioClip[] DeliveryFailed;
    public AudioClip[] DeliverySuccess;
    public AudioClip[] FootStep;
    public AudioClip[] ObjectPickUp;
    public AudioClip[] ObjectDrop;
    public AudioClip StoveSizzle;
    public AudioClip[] Trash;
    public AudioClip[] Warning;
}

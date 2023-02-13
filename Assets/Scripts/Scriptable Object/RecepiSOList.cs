using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

public class RecepiSOList : ScriptableObject
{
    public List<RecepiSO> _recepiSOsList;

    public List<RecepiSO> GetRecepiSoList()
    {
        return _recepiSOsList;
    }
}
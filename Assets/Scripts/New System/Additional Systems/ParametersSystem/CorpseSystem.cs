using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseSystem : MonoBehaviour
{
    internal bool isLooted = false;
    [SerializeField] private int _secondsBeforeDisappear;
    [SerializeField] private int _secondsBeforeDisappearUnlooted;

    private void Awake()
    {
        if(_secondsBeforeDisappear > _secondsBeforeDisappearUnlooted)
        {
            _secondsBeforeDisappearUnlooted = _secondsBeforeDisappear;
        }
    }

    public void InvokeDestroying()
    {
        if(isLooted)
        {
            Invoke("DestroyCorpse", _secondsBeforeDisappear);
        }
        else
        {
            Invoke("DestroyCorpse", _secondsBeforeDisappearUnlooted);
        }
    }

    private void DestroyCorpse()
    {
        Destroy(gameObject);
    }
}

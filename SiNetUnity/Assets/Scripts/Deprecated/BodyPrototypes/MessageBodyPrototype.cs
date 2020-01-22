using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MessageBodyPrototype
{
    public abstract string Encode();

    public abstract void DecodeFrom(string body);

    public abstract Object ToOriginal();
}

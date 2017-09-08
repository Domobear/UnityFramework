using System;
using UnityEngine;

public abstract class IScene : MonoBehaviour
{
    public abstract void FadeIn(Action iCallback);
    public abstract void FadeOut(Action iCallback);
}
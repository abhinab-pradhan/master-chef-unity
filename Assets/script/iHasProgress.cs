using System;
using UnityEngine;

public interface iHasProgress
{
    public event EventHandler<onProgressChangeEventArgs> onProgressChange;
    public class onProgressChangeEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    
}

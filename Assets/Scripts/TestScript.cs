using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public int maxCount = 10;

    public TestScript(int count = 0)
    {
        Count = count;
    }

    public void Increment()
    {
        Count = Math.Min(maxCount, Count + 1);
    }

    public int Count { get; private set; }
}

using System;

namespace Final.Logic;

public class SystemRandomSource : IRandomSource
{
    public int Next(int max)
    {
        return Random.Shared.Next(max);
    }
}

using System;

namespace Final.Logic;

public class SystemRandomSource : IRandomSource //Req 1.5.3
{
    public int Next(int max)
    {
        return Random.Shared.Next(max);
    }
}
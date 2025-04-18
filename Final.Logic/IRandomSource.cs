using System;

namespace Final.Logic;

public interface IRandomSource //Req 1.5.3  and //Req 2.2.1  SystemRandomSource in logic and Test randomsource in tests inheret
{
    int Next(int max);
}

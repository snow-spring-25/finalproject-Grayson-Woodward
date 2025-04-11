using Final.Logic;
namespace Final.Tests;

public class TestRandomSource(int notSoRandomValue) : IRandomSource
{
    public int Next(int max) => notSoRandomValue;
}

using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;

public class PerformanceTest : MonoBehaviour
{
    [Test, Performance]
    public void Test()
    {
        Measure.Method(Counter).Run();
    }

    private static void Counter()
    {
        var sum = 0;
        for (var i = 0; i < 10000000; i++)
        {
            sum += i * 3 / 3;
        }
    }
}

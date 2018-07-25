using System;
using System.Collections;

public class SimulationGameMode : GameMode
{
    private static Type[] states = { typeof(ResultState) };

    protected override IEnumerator Load()
    {
        yield return CreateStates(states);
    }
}

using UnityEngine;

public class EntryPoint : MonoBehaviour
{
	void Start ()
    {
        GameManager.Instance.SetGameMode(typeof(SimulationGameMode));
	}
}

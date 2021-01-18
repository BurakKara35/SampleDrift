using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriftDust : MonoBehaviour
{
    [SerializeField] private GameObject dustParticle;
    private float driftDustTimeInSecond;

    private void Awake()
    {
        CloseDust();
        driftDustTimeInSecond = dustParticle.GetComponent<ParticleSystem>().main.duration;
    }

    public void OpenDust()
    {
        dustParticle.SetActive(true);
    }

    private void CloseDust()
    {
        dustParticle.SetActive(false);
    }

    public IEnumerator DriftDust()
    {
        yield return new WaitForSeconds(driftDustTimeInSecond);
        CloseDust();
    }
}

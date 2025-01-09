using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatherProvider : MonoBehaviour
{
    [SerializeField] private List <ParticleSystem> particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator WeatherCycle()
    {
        int currentIndex = -1;
        while (true)
        {
            int timeDelay = Random.Range(5, 14);
            int nextWeatherIndex = Random.Range(-1, particles.Count);
            yield return new WaitForSeconds(timeDelay);

            if (currentIndex != -1)
            {
                float rateOverTime = particles[currentIndex].emission.rateOverTime.constant;
                float startRate = particles[currentIndex].emission.rateOverTime.constant;
                ParticleSystem.EmissionModule emission = particles[currentIndex].emission;
                while (emission.rateOverTime.constant > 0)
                {
                    yield return null;
                    rateOverTime -= startRate / 6 / 60;
                    emission.rateOverTime = new ParticleSystem.MinMaxCurve(rateOverTime);

                }

                particles[currentIndex].Stop();
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(startRate);
            }

            if (nextWeatherIndex != -1)
            {
                
                particles[nextWeatherIndex].Play();
                
                float endRate = particles[nextWeatherIndex].emission.rateOverTime.constant;
                float rateOverTime = 0;

                ParticleSystem.EmissionModule emission = particles[nextWeatherIndex].emission;
                while (emission.rateOverTime.constant < endRate)
                {
                    yield return null;
                    rateOverTime += endRate / 6 / 60;
                    emission.rateOverTime = new ParticleSystem.MinMaxCurve(rateOverTime);
                
                }
            }
            currentIndex = nextWeatherIndex;
        }
    }
    void Start()
    {
        StartCoroutine(WeatherCycle());
    }

}

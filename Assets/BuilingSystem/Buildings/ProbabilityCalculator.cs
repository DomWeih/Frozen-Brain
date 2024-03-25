using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityCalculator : MonoBehaviour
{
    // Variables for the set number and probabilities
    [Range(0, 1000)] public float setNumber = 500.0f;

    // Probability variables
    private float probability1;
    private float probability2;
    private float probability3;

    // Update is called whenever setNumber changes in the Inspector
    private void OnValidate()
    {
        // Calculate probabilities based on setNumber
        CalculateProbabilities();
    }

    private void Start()
    {
        // Calculate probabilities based on initial setNumber
        CalculateProbabilities();

        // Calculate random value
        float randomValue = Random.value;

        // Determine the outcome based on probabilities
        if (randomValue < probability1)
        {
            Debug.Log("Event 1 occurred!");
            // Add your event 1 logic here
        }
        else if (randomValue < probability1 + probability2)
        {
            Debug.Log("Event 2 occurred!");
            // Add your event 2 logic here
        }
        else
        {
            Debug.Log("Event 3 occurred!");
            // Add your event 3 logic here
        }        
        Debug.Log("Prob1: " + probability1);
        Debug.Log("Prob2: " + probability2);
        Debug.Log("Prob3: " + probability3);
    }    
    public void Calc()
    {
        // Calculate probabilities based on initial setNumber
        CalculateProbabilities();

        // Calculate random value
        float randomValue = Random.value;

        // Determine the outcome based on probabilities
        if (randomValue < probability1)
        {
            Debug.Log("Event 1 occurred!");
            // Add your event 1 logic here
        }
        else if (randomValue < probability1 + probability2)
        {
            Debug.Log("Event 2 occurred!");
            // Add your event 2 logic here
        }
        else
        {
            Debug.Log("Event 3 occurred!");
            // Add your event 3 logic here
        }        
        Debug.Log("Prob1: " + probability1);
        Debug.Log("Prob2: " + probability2);
        Debug.Log("Prob3: " + probability3);
    }

    private void CalculateProbabilities()
    {
        // Calculate probability1: Decreases as setNumber increases
        probability1 = 0.5f - (setNumber / 2000.0f);
        probability1 = Mathf.Clamp01(probability1);

        // Calculate probability2: Increases as setNumber increases
        probability2 = 0.5f + (setNumber / 2000.0f);
        probability2 = Mathf.Clamp01(probability2);

        // Calculate probability3: Increases up to a point, then decreases
        if (setNumber > 700)
        {
            probability3 = 0.3f + ((setNumber - 700) / 3000.0f);
            probability3 = Mathf.Clamp01(probability3);
        }
        else
        {
            probability3 = 0.3f;
        }
    }
}




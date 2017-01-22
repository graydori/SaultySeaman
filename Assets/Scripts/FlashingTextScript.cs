using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlashingTextScript : MonoBehaviour
{

    Text flashingText;
    public float delay = .5f;

    void Start()
    {
        //get the Text component
        flashingText = GetComponent<Text>();
        //Call coroutine BlinkText on Start
        StartCoroutine(BlinkText());
    }

    //function to blink the text
    public IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement
        while (true)
        {
            string original = flashingText.text;
            //set the Text's text to blank
            flashingText.text = string.Empty;
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(delay);
            //display “I AM FLASHING TEXT” for the next 0.5 seconds
            flashingText.text = original;
            yield return new WaitForSeconds(delay);
        }
    }

}
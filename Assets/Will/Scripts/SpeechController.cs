using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private Image CharacterAvatar;
    [SerializeField] private CanvasGroup SpeechHolder;
    [SerializeField] private GameObject SpeechBox;
    [SerializeField] private TextMeshProUGUI UIText;
    [SerializeField] private Sprite[] idleSprites;
    [SerializeField] private Sprite[] speakingSprites;

    public static SpeechController current;

    private void Awake()
    {
        if (!current)
        {
            current = this;
        }
        else
        {
            Destroy(current.gameObject);
            current = this;
        }
    }

    private void setCharacterToSpeak(int characterIndex)
    {
        CharacterAvatar.sprite = speakingSprites[characterIndex];
    }

    private void setCharacterToIdle(int characterIndex)
    {
        CharacterAvatar.sprite = idleSprites[characterIndex];
    }

    private IEnumerator writeText(string text, int charIndex)
    {
        UIText.text = "";
        setCharacterToSpeak(charIndex);
        for (int i = 0; i <text.Length; i++)
        {
            UIText.text += text[i];
            yield return new WaitForSeconds(0.025f);
        }
        setCharacterToIdle(charIndex);
    }

    public IEnumerator talk(string[] paragraphs, int charIndex)
    {
        SpeechBox.gameObject.SetActive(true);
        LeanTween.alphaCanvas(SpeechHolder, 1, 1);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < paragraphs.Length; i++)
        {
            StartCoroutine(writeText(paragraphs[i], charIndex));
            yield return new WaitForSeconds(1);
            //wait for player input
            while (Input.GetKeyDown(KeyCode.Space) == false)
            {
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.5f);
        LeanTween.alphaCanvas(SpeechHolder, 0, 1);
        yield return new WaitForSeconds(1);
        SpeechBox.gameObject.SetActive(false);
    }


    //private void Awake()
    //{
    //    talkTest();
    //}

    private void talkTest()
    {
        string[] newString = new string[3];
        newString[0] = "Hello my name is jeff, I am very cool";
        newString[1] = "fortnite";
        newString[2] = "gaming";
        StartCoroutine(talk(newString, 0));
    }
}

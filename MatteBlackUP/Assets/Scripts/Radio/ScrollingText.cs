using System.Collections;
using UnityEngine;
using TMPro;
using static System.String;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _delay;

    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_delay);
        StartCoroutine(Scroll());
    }

    private IEnumerator Scroll()
    {
        while (true)
        {
            yield return _wait;

            _text.text = MoveChars(_text.text);
        }
    }

    private static string MoveChars(string s)
    {
        if (IsNullOrEmpty(s)) return "";
        var result = s[1..] + s[0];
        
        if (s[0] == ' ' && s.Length != 1) result = MoveChars(result);

        return result;
    }
}

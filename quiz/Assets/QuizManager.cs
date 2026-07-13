using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class QuestionData
{
    [TextArea(2, 4)]
    public string questionText;
    public string[] choices;
    public int correctAnswerIndex;
}

public class QuizManager : MonoBehaviour
{
    [Header("問題のリスト")]
    public QuestionData[] questions;

    [Header("UI設定")]
    public TextMeshProUGUI questionUIText;
    public GameObject[] choiceButtons;

    private int currentQuestionIndex = 0;
    private int score = 0; // ★追加：正解数をカウント

    void Start()
    {
        ShowQuestion();
    }

    void ShowQuestion()
    {
        // 全ての問題が終わったらリザルト画面へ
        if (currentQuestionIndex >= questions.Length)
        {
            ShowResult();
            return;
        }

        QuestionData currentQ = questions[currentQuestionIndex];
        questionUIText.text = currentQ.questionText;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < currentQ.choices.Length)
            {
                choiceButtons[i].SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQ.choices[i];
            }
            else
            {
                choiceButtons[i].SetActive(false);
            }
        }
    }

    public void OnAnswerButtonClicked(int buttonIndex)
    {
        // 正解・不正解の判定
        if (buttonIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("正解！");
            score++; // 正解したらスコアを加算
        }
        else
        {
            Debug.Log("不正解...");
        }

        // 次の問題へ進む
        currentQuestionIndex++;
        ShowQuestion();
    }

    // ★追加：リザルト画面を表示する処理
    void ShowResult()
    {
        // ボタンを全部隠す
        foreach (GameObject btn in choiceButtons)
        {
            btn.SetActive(false);
        }

        // 問題文のテキストをリザルト表示に使い回す
        questionUIText.text = "結果発表！\n\n" + questions.Length + "問中 " + score + " 問正解！";

        // 満点なら特別なメッセージを追加
        if (score == questions.Length)
        {
            questionUIText.text += "\n\n全問正解！完璧ですね！";
        }
    }
}
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcText;
    public Transform choiceContainer;
    public Button choiceButtonPrefab; // �ҡ Prefab ����������͡�����
    public GameObject closeButtonDialogue;
    private DialogueSequen InteractNpcSequen;

    // �纻������١���ҧ��� ���͹�价����/��͹������ѧ
    private List<Button> activeButtons = new List<Button>();

    public void Setup(DialogueSequen sequen)
    {
        //1. Set Dialogue Sequen
        InteractNpcSequen = sequen;
        ShowDialogue(sequen.tree.root);
        dialoguePanel.SetActive(true);
        //Show UI
        gameObject.SetActive(true);
        closeButtonDialogue.SetActive(false);
    }

    public void ShowDialogue(DialogueNode node)
    {
        // 2. set ����� �˹��Ѩ�غѹ
        InteractNpcSequen.currentNode = node;
        npcText.text = node.text;
        ClearChoices();
        
        var choice = new List<string>(node.nexts.Keys);
        for (int i = 0; i < choice.Count; i++)
        {
            CreateChoiceButton(choice[i], 1);
        }
        // 3. �ʴ���ͤ����ͧ NPC

        // 4. ��ҧ����������͡���

        // 5. ���ҧ����������͡������ nexts
   
    }

    private void CreateChoiceButton(string text, int index)
    {
        Button newButton = Instantiate(choiceButtonPrefab, choiceContainer);

        // ��駤�Ң�ͤ���������
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = text;

        // ���� Listener ����͡�����
        // �� Lambda Expression ������ index ��Ѻ���� DialogueManager
        newButton.onClick.AddListener(() => OnChoiceSelected(index));

        activeButtons.Add(newButton);
    }

    private void ClearChoices()
    {
        foreach (Button button in activeButtons)
        {
            Destroy(button.gameObject);
        }
        activeButtons.Clear();
    }

    private void OnChoiceSelected(int index)
    {
        // �� index �ͧ������͡�����������͡��Ѻ���� DialogueManager �Ѵ���
        InteractNpcSequen.SelectChoice(index);
    }
    public void ShowCloseButtonDialog() {
        closeButtonDialogue.gameObject.SetActive(true);
    }
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        ClearChoices();
    }
}

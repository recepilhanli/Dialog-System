using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogSystem;
using XNode;
using TMPro;

public class DialogUI : MonoBehaviour
{
    public DialogGraph dialogGraph;
    [HideInInspector] public Node currentNode;

    [SerializeField] private GameObject _DialogCanvas;
    [SerializeField] private TextMeshProUGUI headlineTMP;
    [SerializeField] private TextMeshProUGUI descriptionTMP;

    [SerializeField] private TextMeshProUGUI option1TMP;
    [SerializeField] private TextMeshProUGUI option2TMP;
    [SerializeField] private TextMeshProUGUI option3TMP;
    [SerializeField] private TextMeshProUGUI option4TMP;


    private void Awake()
    {
        currentNode = dialogGraph.GetFirstNode();
        UpdateUI();
    }

    void UpdateButtons()
    {
        if (option1TMP.text == string.Empty)
        {
            option1TMP.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            option1TMP.transform.parent.gameObject.SetActive(true);
        }

        if (option2TMP.text == string.Empty)
        {
            option2TMP.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            option2TMP.transform.parent.gameObject.SetActive(true);
        }

        if (option3TMP.text == string.Empty)
        {
            option3TMP.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            option3TMP.transform.parent.gameObject.SetActive(true);
        }

        if (option4TMP.text == string.Empty)
        {
            option4TMP.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            option4TMP.transform.parent.gameObject.SetActive(true);
        }
    }


    private void UpdateUI()
    {
        DialogNode dialogNode = currentNode as DialogNode;
        if (dialogNode != null)
        {
            headlineTMP.text = dialogNode.Headline;
            descriptionTMP.text = dialogNode.Description;

            option1TMP.text = dialogNode.Option1;
            option2TMP.text = dialogNode.Option2;
            option3TMP.text = dialogNode.Option3;
            option4TMP.text = dialogNode.Option4;

            UpdateButtons();
        }
    }

    void CloseDialog()
    {
        _DialogCanvas.SetActive(false);
    }


    public void SelectOption(int option)
    {
        Node node = GetNextNodeOfDialogOption(option);

        DialogNode dialogNode = node as DialogNode;

        if (dialogNode != null)
        {
            InitNextDialogNode(dialogNode);
            return;
        }

        DialogSwitcher dialogSwitcher = node as DialogSwitcher;

        if (dialogSwitcher != null)
        {
            SwitchNewDialogGraph(dialogSwitcher.NextDialogGraph);
            return;
        }

        DialogEvent dialogEvent = node as DialogEvent;


        if (dialogEvent != null)
        {
            dialogEvent.InvokeEvent();
            dialogNode = dialogEvent.Output as DialogNode;
            if (dialogNode != null)
            {
                InitNextDialogNode(dialogNode);
                return;
            }

            dialogSwitcher = dialogEvent.Output as DialogSwitcher;
            if (dialogSwitcher != null)
            {
                SwitchNewDialogGraph(dialogSwitcher.NextDialogGraph);
                return;
            }

            return;
        }

        CloseDialog();

    }

    void SwitchNewDialogGraph(DialogGraph graph)
    {
        dialogGraph = graph;
        currentNode = dialogGraph.GetFirstNode();
        UpdateUI();
    }

    void InitNextDialogNode(DialogNode node)
    {
        currentNode = node;
        UpdateUI();
    }



    public Node GetNextNodeOfDialogOption(int option)
    {
        DialogNode dialogNode = currentNode as DialogNode;
        if (dialogNode != null)
        {
            switch (option)
            {
                case 0:
                    return dialogNode.Option1Output;
                case 1:
                    return dialogNode.Option2Output;
                case 2:
                    return dialogNode.Option3Output;
                case 3:
                    return dialogNode.Option4Output;
            }
        }
        return null;
    }

}

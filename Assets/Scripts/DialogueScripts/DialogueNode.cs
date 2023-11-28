using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : Node {
    public Dialogue dialogue;
    public Dialogue responses;

    [Input] public Node prevNode;

    [Output] public Node optionA;
    [Output] public Node optionB;
    [Output] public Node optionC;
    [Output] public Node optionD;

}
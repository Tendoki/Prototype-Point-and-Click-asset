using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("dialogue")]
public class DialogueController
{
    [XmlElement("node")]
    public Node[] nodes;

    public static DialogueController Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DialogueController));
        StringReader reader = new StringReader(_xml.text);
        DialogueController dial = serializer.Deserialize(reader) as DialogueController;
        return dial;
    }
}

[System.Serializable]
public class Node
{
    [XmlElement("npctext")]
    public string NpcText;

    [XmlArray("answers")]
    [XmlArrayItem("answer")]
    public Answer[] answers;
}

[System.Serializable]
public class Answer
{
    [XmlAttribute("tonode")]
    public int nextNode;
    [XmlElement("text")]
    public string text;
    [XmlElement("dialogend")]
    public string end;

    [XmlAttribute("questvalue")]
    public int QuestValue;
    [XmlAttribute("needquestvalue")]
    public int NeedQuestValue;
    [XmlElement("questname")]
    public string QuestName;
}
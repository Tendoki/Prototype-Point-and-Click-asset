using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("quests")]
public class DataQuestName
{
    [XmlElement("quest")]
    public List<Quest> Quests;

    public static DataQuestName Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DataQuestName));
        StringReader reader = new StringReader(_xml.text);
        DataQuestName qt = serializer.Deserialize(reader) as DataQuestName;
        return qt;
    }
}

[System.Serializable]
public class Quest
{
    [XmlElement("questname")]
    public string QuestName;
}
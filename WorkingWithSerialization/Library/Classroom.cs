namespace library.Shared;
using System.Xml.Serialization; 

public class Classroom
{
    [XmlAttribute("group")]
    public string? Group { get; set; }
    [XmlElement("Professor")]
    public Professor? ProfessorInGroup {get; set;}

    //constructor
    public Classroom(){}
    public Classroom(string? group, Professor professor){
        Group = group;
        ProfessorInGroup = professor;
    }
}
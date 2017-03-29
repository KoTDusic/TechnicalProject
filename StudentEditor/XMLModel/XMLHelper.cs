using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLModel
{
    public class XMLHelper
    {
        private int maxId=0;
        private static XMLHelper instance;
        const string fileName = "Students.xml";
        private XDocument document;
        private XMLHelper() 
        {
            document = XDocument.Load(fileName);         
        }
        public void deleteById(int[] ids)
        {

           XElement[] removes = document.Element("Students").Elements("Student").Where(e => ids.Contains(Convert.ToInt32(e.Attribute("Id").Value))).ToArray();
           for (int i = 0; i < removes.Length;i++ )
           {
               removes[i].Remove();
           }
           document.Save(fileName);
        }
        public ObservableCollection <Student> LoadAll()
        {
            ObservableCollection<Student>  result = new ObservableCollection<Student>();
            Student student;
            int id;
            IEnumerable<XElement> elements = document.Element("Students").Elements("Student");
            foreach (XElement element in elements)
            {
                
                id = Convert.ToInt32(element.Attribute("Id").Value);
                if (maxId < id) maxId = id;
                student = new Student
                {
                    ID = id,
                    FirstName = element.Element("FirstName").Value,
                    LastName = element.Element("Last").Value,
                    NumericAge = Convert.ToInt32(element.Element("Age").Value),
                    Gender = Convert.ToInt32(element.Element("Gender").Value) == 0 ? "М" : "Ж"
                };
                result.Add(student);
            }
            return result;
        }
        public void AddValue(Student newStudent)
        {
            XElement newNode = new XElement("Student");
            newNode.SetAttributeValue("Id", ++maxId);
            XElement childNode = new XElement("FirstName");
            childNode.Value = newStudent.FirstName;
            newNode.Add(childNode);
            childNode = new XElement("Last");
            childNode.Value = newStudent.LastName;
            newNode.Add(childNode);
            childNode = new XElement("Age");
            childNode.Value = newStudent.NumericAge.ToString();
            newNode.Add(childNode);
            childNode = new XElement("Gender");
            childNode.Value = newStudent.Gender=="М"?"0":"1";
            newNode.Add(childNode);
            document.Element("Students").AddFirst(newNode);
            document.Save(fileName);
        }
        public void UpdateValue(Student updatingStudent)
        {
            XElement element = document.Element("Students").Elements("Student").Single(
                e => Convert.ToInt32(e.Attribute("Id").Value) == updatingStudent.ID);
            element.Element("FirstName").Value = updatingStudent.FirstName;
            element.Element("Last").Value = updatingStudent.LastName;
            element.Element("Age").Value = updatingStudent.NumericAge.ToString();
            element.Element("Gender").Value = updatingStudent.Gender == "М" ? "0" : "1";
            document.Save(fileName);
        }
        public static XMLHelper GetInstance()
        {
            if (instance == null) instance = new XMLHelper();
            return instance;
        }
    }
}

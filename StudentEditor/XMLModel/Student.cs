using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace XMLModel 
{
    enum Genders {man, woman}
    public class Student : INotifyPropertyChanged
    {
        private int id;
        public int ID
        {
            get
            {
                return id;
            }
            set 
            {
                id = value;
            }
        }

        private bool selected;
        public bool Selected
        {
            get 
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }

        private string firstName = "";
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("FIO");
            }
        }
        public string FIO
        {
            get 
            {
                return firstName +" "+ lastName;
            }
        }
        private string lastName = "";
        public string LastName
        {
            get { return lastName; }
            set
            {
                
                lastName = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("FIO");
            }
        }
        private int age;
        public string Age
        {
            get 
            {
                if (age == 0) return "";
                else return age + GetAgeString(age);
            }
        }
        public string GetAgeString(int analyze_age)
        {
            int rest = analyze_age % 10;
            if (rest == 1) return " год";
            if ((rest <= 5) && (rest > 0)) return " года";
            else return " лет";
        }
        public int NumericAge
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }
        private Genders gender;
        public string Gender
        {
            get 
            { 
                switch (gender)
                {
                    case Genders.man:   return "М";
                    case Genders.woman: return "Ж";
                    default: return "М";
                }
            }
            set
            {
                switch(value)
                {
                    case "М": gender = Genders.man;
                        break;
                    case "Ж": gender = Genders.woman;
                        break;
                    default: gender = Genders.man;
                        break;
                }
                OnPropertyChanged("Gender");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.AppendFormat("ФИО:{0},", FIO);
            res.AppendFormat("Возраст:{0},", Age);
            res.AppendFormat("Пол:{0}", Gender);
            return res.ToString();
        }
    }
}

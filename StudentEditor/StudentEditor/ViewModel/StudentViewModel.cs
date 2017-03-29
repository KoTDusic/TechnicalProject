using StudentEditor.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XMLModel;

namespace StudentEditor
{
    enum ModelState { Empty,Edit,Add,Normal}
    class StudentViewModel : INotifyPropertyChanged
    {
        public StackPanel inputs;
        private Student newStudent;
        private Student selectedStudent;
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                EditButtonEnable = true;
                OnPropertyChanged("EditButtonEnable");
                OnPropertyChanged("SelectedStudent");
            }
        }
        public Visibility ListVisibility { get; set; }
        public Visibility FieldsVisibility { get; set; }
        public bool ListEnabled { get; set; }
        public bool CreateButtonEnable { get; set; }
        public bool EditButtonEnable { get; set; }
        public bool DeleteButtonEnable { get; set; }
        public Visibility SaveButtonVisible { get; set; }
        public Visibility UpdateButtonVisible { get; set; }
        public ObservableCollection<Student> students{ get; set; }
        private string validationText;
        public string ValidationText
        {
            get
            {
                return validationText;
            }
            set 
            {
                validationText = value;
                OnPropertyChanged("ValidationText");
            }
        }
        public StudentViewModel()
        {
            students = XMLHelper.GetInstance().LoadAll();
            if (students.Count == 0) ChangeState(ModelState.Empty);
            else ChangeState(ModelState.Normal);
        }

        private void ChangeState(ModelState newState)
        {
            switch(newState)
            {
                case ModelState.Normal:
                    ListVisibility = Visibility.Visible;
                    FieldsVisibility = Visibility.Collapsed;
                    ListEnabled = true;
                    CreateButtonEnable = true;
                    EditButtonEnable = false;
                    DeleteButtonEnable = true;
                    break;
                case ModelState.Empty:
                    ListVisibility = Visibility.Collapsed;
                    FieldsVisibility = Visibility.Collapsed;
                    ListEnabled = true;
                    CreateButtonEnable = true;
                    EditButtonEnable = false;
                    DeleteButtonEnable = false;
                    break;
                case ModelState.Add:
                    ListVisibility = Visibility.Visible;
                    FieldsVisibility = Visibility.Visible;
                    ListEnabled = false;
                    CreateButtonEnable = false;
                    EditButtonEnable = false;
                    SaveButtonVisible = Visibility.Visible;
                    UpdateButtonVisible = Visibility.Collapsed;
                    DeleteButtonEnable = false;
                    break;
                case ModelState.Edit:
                    ListVisibility = Visibility.Visible;
                    FieldsVisibility = Visibility.Visible;
                    ListEnabled = false;
                    CreateButtonEnable = false;
                    EditButtonEnable = false;
                    SaveButtonVisible = Visibility.Collapsed;
                    UpdateButtonVisible = Visibility.Visible;
                    DeleteButtonEnable = false;
                    break;
            }
            OnPropertyChanged("ListVisibility");
            OnPropertyChanged("FieldsVisibility");
            OnPropertyChanged("CreateButtonEnable");
            OnPropertyChanged("ListEnabled");
            OnPropertyChanged("EditButtonEnable");
            OnPropertyChanged("SaveButtonVisible");
            OnPropertyChanged("UpdateButtonVisible");
            OnPropertyChanged("DeleteButtonEnable");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private RelayCommand addCommand, editCommand, cancellCommand, updateCommand, saveCommand,deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null) deleteCommand = new RelayCommand(obj =>
                {
                    StringBuilder students_text = new StringBuilder();
                    List<int> ids = new List<int>();
                    List<Student> deleting_students = new List<Student>();
                    for(int i=0;i<students.Count;i++)
                    {
                        if(students[i].Selected)
                        {
                            ids.Add(students[i].ID);
                            deleting_students.Add(students[i]);
                            students_text.AppendFormat("{0}\n", students[i].ToString());
                        }
                    }
                    if (ids.Count != 0)
                    {
                        MyInputDialog dialog = new MyInputDialog("Вы действительно хотите удалить эти записи?", false, students_text.ToString(), 600, 300);
                        dialog.ShowDialog();
                        if (!dialog.cancelled)
                        {
                            XMLHelper.GetInstance().deleteById(ids.ToArray());
                            foreach (Student student in deleting_students) students.Remove(student);
                        }
                    }
                });
                return deleteCommand;
            }
        }
        public RelayCommand AddCommand
        {
            get
            {
                if(addCommand==null) addCommand = new RelayCommand(obj =>
                    {
                        Student student = new Student();
                        newStudent = student;
                        SelectedStudent = newStudent;
                        ChangeState(ModelState.Add);
                    });
                return addCommand;
            }
        }
        public RelayCommand EditCommand
        {
            get
            {
                if (editCommand == null) editCommand = new RelayCommand(obj =>
                {
                    ChangeState(ModelState.Edit);
                });
                return editCommand;
            }
        }
        public RelayCommand CancellCommand
        {
            get
            {
                if (cancellCommand == null) cancellCommand = new RelayCommand(obj =>
                {
                    ChangeState(ModelState.Normal);
                });
                return cancellCommand;
            }
        }
        public RelayCommand UpdateCommand
        {
            get
            {
                if (updateCommand == null) updateCommand = new RelayCommand(obj =>
                {
                    if (ValidateForm())
                    {
                        UpdateSource();
                        XMLHelper.GetInstance().UpdateValue(selectedStudent);
                        selectedStudent = null;
                        OnPropertyChanged("SelectedStudent");
                        ChangeState(ModelState.Normal);
                     }
                });
                return updateCommand;
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                if (saveCommand == null) saveCommand = new RelayCommand(obj =>
                {
                    if (ValidateForm())
                        {
                            UpdateSource();
                            XMLHelper.GetInstance().AddValue(selectedStudent);
                            students.Insert(0, newStudent);
                            XMLHelper.GetInstance();
                            selectedStudent = null;
                            newStudent = null;
                            OnPropertyChanged("SelectedStudent");
                            ChangeState(ModelState.Normal);
                        }
                });
                return saveCommand;
            }
        }
        public bool ValidateForm()
        {
            ValidationText = "";
            IEnumerable<TextBox> text_inputs = inputs.Children.OfType<TextBox>();
            string name_text = text_inputs.ElementAt(0).Text;
            string lastname_text = text_inputs.ElementAt(1).Text;
            string age_text = text_inputs.ElementAt(2).Text;
            try
            {
                int age = Convert.ToInt32(age_text);
                if (age < 16 || age > 100) throw new Exception();
            }
             catch(Exception)
            {
                ValidationText+="Возраст должен находиться в диапазоне [16, 100]\n";
            }
            if (lastname_text.Length == 0) ValidationText += "Поле фамилии обязательно для заполнения\n";
            if (name_text.Length == 0) ValidationText += "Поле имени обязательно для заполнения\n";
            if (ValidationText.Length!=0) return false;
            else return true;
        }
        public void UpdateSource()
        {
            IEnumerable<TextBox> text_inputs = inputs.Children.OfType<TextBox>();
            foreach (TextBox input in text_inputs)
            {
                input.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            inputs.Children.OfType<ComboBox>().ElementAt(0).GetBindingExpression(ComboBox.TextProperty).UpdateSource();
        }
    }
}

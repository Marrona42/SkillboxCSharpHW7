using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTry
{
    struct Worker
    {
        #region Конструктор
        public Worker(string Identity, string InsertionDate, string LastName, string FirstName, string Patronymic,
            string Birthday, string Age, string Height, string BirthPlace)
        {
            this.identity = Identity;
            this.insertionDate = InsertionDate;
            this.firstName = FirstName;
            this.lastName = LastName;
            this.patronymic = Patronymic;
            this.birthday = Birthday;
            this.age = Age;
            this.height = Height;
            this.birthPlace = BirthPlace;
        }
        #endregion

        public string Print()
        {
            return $"{this.identity,4} {this.insertionDate,20} {this.lastName,15} {this.firstName,15} {this.patronymic,15} " +
                $"{this.birthday,23} {this.age,11} {this.height,11} {this.birthPlace,21}";
        }

        #region Свойства
        public string Identity { get { return this.identity; } set { this.identity = value; } }
        public string InsertionDate { get { return this.insertionDate; } set { this.insertionDate = value; } }
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public string LastName { get { return this.lastName; } set { this.lastName = value;  } }
        public string Patronymic { get { return this.patronymic; } set { this.patronymic = value; } }
        public string Age { get { return this.age; } set { this.age = value; } }
        public string Height { get { return this.height; } set { this.height = value; } }
        public string Birthday { get { return this.birthday; } set { this.birthday = value; } }
        public string BirthPlace { get { return this.birthPlace; } set { this.birthPlace = value; } }
        #endregion

        #region Поля
        private string identity;
        private string insertionDate;
        private string firstName;
        private string lastName;
        private string patronymic;
        private string age;
        private string height;
        private string birthday;
        private string birthPlace;
        #endregion
    }
}

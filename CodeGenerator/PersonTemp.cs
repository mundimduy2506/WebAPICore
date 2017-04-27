using System;

namespace MyTypes
{
    public partial class Person
    {
        private string _lastName;
        private string _firstName;
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = (value);
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = (value);
            }
        }

        public Person(string LastName, string FirstName)
        {
            _lastName = (LastName);
            _firstName = (FirstName);
        }
    }
}
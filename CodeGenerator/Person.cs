using System;

namespace MyNameSpace
{
    public partial class Employee
    {
        private int? _recursionLevel;
        private int? _businessEntityID;
        private string _firstName;
        private string _lastName;
        private string _organizationNode;
        private string _managerFirstName;
        private string _managerLastName;
        public int? RecursionLevel
        {
            get
            {
                return _recursionLevel;
            }

            set
            {
                _recursionLevel = (value);
            }
        }

        public int? BusinessEntityID
        {
            get
            {
                return _businessEntityID;
            }

            set
            {
                _businessEntityID = (value);
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

        public string OrganizationNode
        {
            get
            {
                return _organizationNode;
            }

            set
            {
                _organizationNode = (value);
            }
        }

        public string ManagerFirstName
        {
            get
            {
                return _managerFirstName;
            }

            set
            {
                _managerFirstName = (value);
            }
        }

        public string ManagerLastName
        {
            get
            {
                return _managerLastName;
            }

            set
            {
                _managerLastName = (value);
            }
        }

        public Employee()
        {
        }

        public Employee(int? RecursionLevel, int? BusinessEntityID, string FirstName, string LastName, string OrganizationNode, string ManagerFirstName, string ManagerLastName)
        {
            _recursionLevel = (RecursionLevel);
            _businessEntityID = (BusinessEntityID);
            _firstName = (FirstName);
            _lastName = (LastName);
            _organizationNode = (OrganizationNode);
            _managerFirstName = (ManagerFirstName);
            _managerLastName = (ManagerLastName);
        }
    }
}
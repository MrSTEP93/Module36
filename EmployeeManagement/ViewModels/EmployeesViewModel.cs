using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EmployeesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private EmployeeRepository Repo { get; }

        public EmployeesViewModel()
        {
            Repo = new EmployeeRepository();
            FillListView();
        }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        private string _filter;
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                FillListView();
                //OnPropertyChanged();
            }
        }

        private void FillListView()
        {
            if (!string.IsNullOrEmpty(_filter))
            {
                _employees = new ObservableCollection<Employee>(
                  Repo.GetAll().Where(v => v.FirstName.Contains(_filter)));
            }
            else
                _employees = new ObservableCollection<Employee>(
                  Repo.GetAll());
        }
    }
}

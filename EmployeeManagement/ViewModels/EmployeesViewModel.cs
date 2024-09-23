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
    public class EmployeesViewModel : INotifyPropertyChanged, IEmployeesViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private readonly IEmployeeRepository _employeeRepo;
        private readonly ILogger _logger;

        public EmployeesViewModel(IEmployeeRepository employeeRepository, ILogger logger)
        {
            _employeeRepo = employeeRepository;
            _logger = logger;
            FillListView();
            logger.WriteEvent("Application started");
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
            }
        }

        private void FillListView()
        {
            if (!string.IsNullOrEmpty(_filter))
            {
                Employees = new ObservableCollection<Employee>(
                  _employeeRepo.GetAll().Where(v => v.FirstName.Contains(_filter)));
                FilterMessage = $"По вашему запросу найдено {_employees.Count} записей";
            }
            else
            {
                Employees = new ObservableCollection<Employee>(
                  _employeeRepo.GetAll());
                FilterMessage = "Введите данные для поиска";
            }
        }

        private string _filterMessage;
        public string FilterMessage
        {
            get
            {
                return _filterMessage;
            }

            set
            {
                _filterMessage = value;
                OnPropertyChanged();
            }
        }
    }
}

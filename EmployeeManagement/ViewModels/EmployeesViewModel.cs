using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EmployeesViewModel
    {
        private EmployeeRepository _repo { get; }

        public EmployeesViewModel()
        {
            _repo = new EmployeeRepository();
        }

        public ObservableCollection<Employee> Employees { get {
                return new ObservableCollection<Employee>
                    (this._repo.GetAll()); } }
    }
}

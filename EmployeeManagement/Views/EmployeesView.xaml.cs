using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeManagement.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployessView.xaml
    /// </summary>
    public partial class EmployessView : Window
    {
        IEmployeesViewModel _emplListViewModel;
        IEmployeeViewModel _oneEmpViewModel;
        public EmployessView(IEmployeesViewModel employeesViewModel,
            IEmployeeViewModel employeeViewModel)
        {
            _emplListViewModel = employeesViewModel;
            _oneEmpViewModel = employeeViewModel;

            InitializeComponent();
            DataContext = _emplListViewModel;
        }

        private void ListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;

            if (item is null ) {
                return;
            }

            var oneEmp = item as Employee;
            _oneEmpViewModel.Employee = oneEmp;
            var oneEmpView = new EmployeeView(_oneEmpViewModel);

            oneEmpView.Show();
            /*
            MessageBox.Show($"{emp.Position} in {emp.CompanyName}, {emp.CityName}",
                $"{emp.FirstName} {emp.LastName}, {emp.Age} y.o.");
            */
        }
    }
}

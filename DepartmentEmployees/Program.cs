
using DepartmentsEmployees.Data;
using DepartmentsEmployees.Models;
using System;
using System.Collections.Generic;

namespace DepartmentsEmployees
{
    class Program
    {
        static void Main(string[] args)
        {
           while(true)
            {

                /// users chooses from one of these prompts
                Console.WriteLine("Choose from one of the options below?");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("1. Show all deparments");
                Console.WriteLine("2. Show specific department by Id");
                Console.WriteLine("3. Add new department");
                Console.WriteLine("4. Update department Id");
                Console.WriteLine("5. Delete department by Id");
                Console.WriteLine("6. Show all employees");
                Console.WriteLine("7. Show specific employee by Id");
                Console.WriteLine("8. Add a new employee");
                Console.WriteLine("9. Update an employee");
                Console.WriteLine("10. Delete an employee");


                var choice = Console.ReadLine();
                DepartmentRepository departments = new DepartmentRepository();
                EmployeeRepository employees = new EmployeeRepository();

                switch (Int32.Parse(choice))
                 {
                    case 1:

                        Console.WriteLine("Getting all Departments:");
                        Console.WriteLine();

                        List<Department> allDepartments = departments.GetAllDepartments();

                        foreach (Department dept in allDepartments)
                        {
                            Console.WriteLine($"{dept.Id} {dept.DeptName}");
                        }
                        break;

                    case 2:

                        Console.WriteLine("Enter department Id.");
                        var deptChoice = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Getting department {deptChoice}");

                        Department singleDepartment = departments.GetDepartmentById(deptChoice);

                        Console.WriteLine($"{singleDepartment.Id}. {singleDepartment.DeptName}");
                        break;

                    case 3:

                        Console.WriteLine("Enter the name of new department.");
                        var deptName = Console.ReadLine();
                        Department newDepartment = new Department
                        {
                            DeptName = deptName
                        };

                        departments.AddDepartment(newDepartment);
                        Console.WriteLine($"Add new {deptName} to Departments." );


                        break;

                    case 4:

                        //Console.WriteLine("Enter the name of the department you wish to update.");
                        //var deptName = Console.ReadLine();

                        break;

                    case 5:

                        Console.WriteLine("Enter the Id of the deptarment you would like to delete.");
                        var deleteChoice = int.Parse(Console.ReadLine());

                        departments.DeleteDepartment(deleteChoice);
                        Console.WriteLine($"Department with Id of {deleteChoice} was deleted.");

                        break;

                    case 6:

                        Console.WriteLine("Getting all employees");
                        Console.WriteLine("");

                        List<Employee> allEmployees = employees.GetAllEmployees();

                        foreach (Employee e in allEmployees)
                        {
                            Console.WriteLine($"{e.Id}. {e.FirstName} {e.LastName}");
                        }

                        break;

                    case 7:

                        Console.WriteLine("Enter employee by Id.");
                        var employeeId = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Getting employee {employeeId}");

                        Employee singleEmployee = employees.GetEmployeeById(employeeId);

                        Console.WriteLine($"{singleEmployee.Id} {singleEmployee.FirstName} {singleEmployee.LastName}");

                        break;

                    case 8:

                        Console.WriteLine("Enter the first name of new employee.");
                        var eFirstName = Console.ReadLine();

                        Console.WriteLine("Enter the last name of new employee.");
                        var eLastName = Console.ReadLine();

                        Console.WriteLine("Enter departmentId of new employee.");
                        var eDeptName = int.Parse(Console.ReadLine());


                        Employee newEmployee = new Employee
                        {
                           FirstName = eFirstName,
                            LastName = eLastName,
                            DepartmentId = eDeptName
                        };

                        employees.AddEmployee(newEmployee);
                        Console.WriteLine($"Add new {eFirstName} {eLastName} to employees.");


                        break;

                    case 9:

                        //Console.WriteLine("Enter the name of the department you wish to update.");
                        //var deptName = Console.ReadLine();

                        break;

                    case 10:

                        Console.WriteLine("Enter the Id of the employee you would like to delete.");
                        var deleteEmployee = int.Parse(Console.ReadLine());

                        employees.DeleteEmployee(deleteEmployee);
                        Console.WriteLine($"Department with Id of {deleteEmployee} was deleted.");

                        break;

                    default:
                        break;



                }

            }


        }
    }
}
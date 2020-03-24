
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
                Console.WriteLine("1: Show all deparments");
                Console.WriteLine("2: Show specific department by Id");
                Console.WriteLine("3: Add new department");
                Console.WriteLine("4: Update department Id");
                Console.WriteLine("5: Delete department by Id");
                Console.WriteLine("6: Show all employees");
                Console.WriteLine("7: Show specific employee by Id");


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

                        Console.WriteLine($"{singleDepartment.Id} {singleDepartment.DeptName}");
                        break;



                }














            }

















        }
    }
}
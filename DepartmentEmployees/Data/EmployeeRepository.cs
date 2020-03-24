using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using DepartmentsEmployees.Models;

namespace DepartmentsEmployees.Data
{
    /// <summary>
    /// an object to contain all database interactions
    /// </summary>
    public class EmployeeRepository
    {
        /// <summary>
        /// represents a connection to database
        /// this is a tunnel to connect the app to the database
        /// all communication between the app and data passes through this connection
        /// </summary>
        
        public SqlConnection Connection
        {
            get 
            {
                //this is address of database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DepartmentsEmployees;Integrated Security=True";
                return new SqlConnection(_connectionString);

            }

        }
        /// <summary>
        /// Returns a list of all employees in the house
        /// </summary>

        public List<Employee> GetAllEmployees()
        {
            //we must use the database connection
            // because a database is a shared resource (other apps may use it) we must 
            // be careful about how e interact with it. Specifically we Open() connections we need
            // to interact with the database and we Close() them when we are finished.
            // in C# a using block ensures we correctinly diconnect from a resourse even if there is an error
            // for database connections this means the connection will be properly closed. 

            using (SqlConnection conn = Connection)
            {
                // Note, we must Opon() the connection, the using block doesnt do that for us
                conn.Open();

                // We must use commands too
                using SqlCommand cmd = conn.CreateCommand();
                //Here we set up the command with teh SQL we want to exectue before we execute it
                cmd.CommandText = "SELECT Id, FirstName, LastName, DepartmentId  FROM Employee";

                // Exectue the SQL in the database and get a reader that will give us acces to the date
                SqlDataReader reader = cmd.ExecuteReader();

                // A list to hold the employees we retrieve from the database
                List<Employee> employees = new List<Employee>();

                // Read() will return true if theres more data to read
                while (reader.Read())
                {
                    // the ordinal is the numeric position of the column in the query results
                    // for our query, Id has an ordinal value of 0 EmpName is 1
                    int idColumnPosition = reader.GetOrdinal("Id");

                    // We user the readers GetXXX methods to get the value for a praticular ordinal
                    int idValue = reader.GetInt32(idColumnPosition);

                    int empLastNameColumn = reader.GetOrdinal("LastName");
                    string empLastNameValue = reader.GetString(empLastNameColumn);

                    int empFirstNameColumn = reader.GetOrdinal("FirstName");
                    string empFirstNameValue = reader.GetString(empFirstNameColumn);

                    int departmentIdColumn = reader.GetOrdinal("DepartmentId");
                    int departmentIdValue = reader.GetInt32(departmentIdColumn);

                    // Now lets create a new employee object using the data from the database
                    Employee employee = new Employee
                    {
                        Id = idValue,
                        FirstName = empFirstNameValue,
                        LastName = empLastNameValue,
                        DepartmentId = departmentIdValue,
                        Department = null
                        

                    };

                    // add that employee object to our list
                    employees.Add(employee);

                }

                // we should Close() the reader. Unfortantely a using block wont work here
                reader.Close();

                // return the list of employees who whomever called this method
                return employees;

            }

        }
            
                /// <summary>
                /// return a single employee with the given id
                /// </summary>
                public Employee GetEmployeeById(int id)
                {
                    using(SqlConnection conn = Connection)
                    {
                         conn.Open();
                         using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Id, FirstName, LastName, DepartmentId FROM Employee WHERE Id =@id";
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        SqlDataReader reader = cmd.ExecuteReader();

                    Employee employee = null;

                // If we only expect a single row back from the database we dont need a hole loop
                if (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int IdValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int departmentIdColumnPosition = reader.GetOrdinal("DepartmentId");
                        int departmentIdValue = reader.GetInt32(departmentIdColumnPosition);

                        employee = new Employee
                        {
                            Id = IdValue,
                            FirstName = firstNameValue,
                            LastName = lastNameValue,
                            DepartmentId = departmentIdValue,
                           
                        };
                }

                    reader.Close();
                    
                    return employee;


                    }
                
                
                     }


                     }

                    /// <summary>
                    /// Add a new employee to the database
                    /// this method sends data to database
                    /// it does not get anything from the database so there is nothing to return
                    /// </summary>
                    
                    public void AddEmployee(Employee employee)
                    {
                        using (SqlConnection conn = Connection)
                        {

                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        // these SQL parameters are annoying. Why cant we use string interpolation
                        // sql injection
                        cmd.CommandText = @"INSERT INTO Employee (firstName, lastName, departmentId)
                            OUTPUT INSERTED.Id 
                            Values (@firstName, @lastName, @departmentId)";
                        cmd.Parameters.Add(new SqlParameter("@firstName", employee.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@lastName", employee.LastName));
                        cmd.Parameters.Add(new SqlParameter("@departmentId", employee.DepartmentId));
                          

                        int id = (int)cmd.ExecuteScalar();

                        employee.Id = id;

                    }
                
                
                        }
                        // when this method is finished we can look in the database and see the new employee
                     }

                    /// <sumary>
                    /// updates the department with the given id
                    /// </sumary>
                    
                    public void UpdateEmployee(int id, Employee employee)
                    {
                        using (SqlConnection conn = Connection)
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Employee
                                     SET FirstName = @firstName, LastName = @lastName, DepartmentId = @departmentId
                                     WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@firstName", employee.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@lastName", employee.LastName));
                        cmd.Parameters.Add(new SqlParameter("@departmentId", employee.DepartmentId));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            /// <summary>
            ///  Delete the department with the given id
            /// </summary>
            public void DeleteEmployee(int id)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Employee WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.ExecuteNonQuery();
                    }
                }
             }
       }

      }

    


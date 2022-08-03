using System;
using Xunit;
using Przychodnia.Class.DictionariesHanding;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;
using System.Threading;
using System.Collections.Generic;

namespace ClinicUnitTests.Administration
{
    public class DictionariesHandling : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(testCase => testCase.TestMethod.Method.Name);
        }
        /*[Fact]
        public void OfficeAllTests()
        {
            AddNewOfficeTest();
            DeleteOfficeTest();
        }
        private void AddNewOfficeTest()
        {
            var exptected = ClassSQLConnections.OfficeList().Count;

            int max = ClassSQLConnections.OfficeNumbersList().Max();
            ClassOffice office = new ClassOffice { OfficeDescription = "Specialist office", OfficeNumber = max + 1 };
            ClassSQLConnections.AddNewOffice(office);

            var actual = ClassSQLConnections.OfficeList().Count;
            Assert.Equal(exptected + 1, actual);
        }
        private void DeleteOfficeTest()
        {
            var exptected = ClassSQLConnections.OfficeList().Count;

            ClassSQLConnections.DeleteOffice(ClassSQLConnections.NotSpecifiedOffice().First().OfficeId);

            var actual = ClassSQLConnections.OfficeList().Count;
            Assert.Equal(exptected - 1, actual);
        }

        [Fact]
        public void UserAndEmployeeAllTests()
        {
            AddNewUserTest();
            AddNewEmployeeTest();
            DeleteEmployeeTest();
            DeleteUserTest();
        }

        private void AddNewUserTest()
        {
            var exptected = ClassSQLConnections.UserList().Count;

            ClassPermission permission = new ClassPermission();
            permission.PermissionId = 3;
            ClassUser user = new ClassUser { Login = "knowaks42", Password = "Knowaks422!", Email = "Knowaks321@gmail.com", Permission = permission };
            ClassSQLConnections.AddUser(user);

            var actual = ClassSQLConnections.UserList().Count;
            Assert.Equal(exptected + 1, actual);
        }

        private void AddNewEmployeeTest()
        {
            var exptected = ClassSQLConnections.EmployeeList().Count;

            ClassEmployee employee = new ClassEmployee { Name = "Kamil", Surname = "Nowak", PhoneNumber = "564839222", DateOfBirth = new DateTime(1976, 12, 12), PersonalIdentityNumber = "76121287333", User_id = ClassSQLConnections.NotSpecifiedUsers().Find(x => x.Permission.PermissionId == 3).User_id };
            ClassSQLConnections.AddNewEmployee(employee);

            var actual = ClassSQLConnections.EmployeeList().Count;
            Assert.Equal(exptected + 1, actual);
        }

        private void DeleteEmployeeTest()
        {
            var exptected = ClassSQLConnections.EmployeeList().Count;

            ClassSQLConnections.DeleteEmployee(ClassSQLConnections.NotSpecifiedEmployeeIndex().First());

            var actual = ClassSQLConnections.EmployeeList().Count;
            Assert.Equal(exptected - 1, actual);
        }

        private void DeleteUserTest()
        {
            var exptected = ClassSQLConnections.UserList().Count;

            ClassSQLConnections.DeleteUser(ClassSQLConnections.NotSpecifiedUserIndex().First());

            var actual = ClassSQLConnections.UserList().Count;
            Assert.Equal(exptected - 1, actual);
        }*/
    }
}

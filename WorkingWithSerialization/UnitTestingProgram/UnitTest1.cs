namespace UnitTestingProgram;
using library.Shared;
using System;
using static System.Environment;
using static System.IO.Path;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
public class UnitTest1
{
    [Fact]
    public void ValidateIfGroup()
    {
        //Checks if the group is in the correct format (semester/Goup/Number between 1 and 2)
        // Arrange
            string validGroup = "9F";
            // Act
            bool result = Program.ValidateGroup(validGroup);
            // Assert
            Assert.False(result);
    }
    [Fact]
    public void CheckIfPasswordIsExist()
    {
            // Arrange
            string? Name= "bRunO";
            string? MiddleName = "sanchez";
            string? LastName = "padIllA";
            string? Password = "123";
            List<Storer> storers = new(){
            new("Bruno","Sanchez","Padilla","123"){
            },
            };
            // Act
            bool result = Program.CheckIfPassword(Name, MiddleName, LastName, Password, storers);
            // Assert
            Assert.True(result);
    }

    [Fact]
    public void Validate_String_ArgumentNullException()
    {
            // Arrange
            string? validString = null;
            // Act
            Program program = new();
            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => Program.ValidateString(validString));
    }


        
    [Fact]
    public void GenerateReport_Field_ArgumentNullExpeption()
    {
            // Arrange
            Program program = new();

            List<Professor> professors = new(){
            new("Bruno","Sanchez","Padilla","123","69","Desarrollo de Software", null){
            },
            };
            string? Field = null;

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => Program.GenerateReport(Field,professors));
    }
    
    [Fact]
    public void Validate_String_ArgumentException()
    {
            // Arrange
            string? validString = "";
            // Act
            Program program = new();
            // Assert
            var exception = Assert.Throws<ArgumentException>(() => Program.ValidateString(validString));
    }

        [Fact]
    public void ValidateIfSalaryAccount()
    {
            // Arrange
            string validSalaryAccount = "94345987234";
            string NotvalidSalaryAccount = "9434598asd234";
            // Act
            bool resultTrue = Program.ValidateSalaryAccount(validSalaryAccount);
            bool resultFalse = Program.ValidateSalaryAccount(NotvalidSalaryAccount);

            // Assert
            Assert.True(resultTrue);
            Assert.False(resultFalse);

    }

    [Fact]
    public void CheckIfProfessorExist()
    {
            // Arrange
            Program program = new();
            List<Professor> professors = new(){
            new("Bruno","Sanchez","Padilla","123","69","Desarrollo de Software", null){
            },
            };
            string password = professors[0].GetDecryptedSalaryAccount();
            // Act
            bool result = Program.ProfessorExist(password,professors);
            // Assert
            Assert.True(result);
    }

    [Fact]
    public void CheckIfStorerExists()
    {
            // Arrange
            string? Name= "bRunO";
            string? MiddleName = "sanchez";
            string? LastName = "padIllA";
            List<Storer> storers = new(){
            new("Bruno","Sanchez","Padilla","123"){
            },
            };
            // Act
            bool result = Program.CheckIfStorerExist(Name, MiddleName, LastName, storers);
            // Assert
            Assert.True(result);
    }


    [Fact]
    public void GenerateReport_ArgumentNullExpeption()
    {
            // Arrange
            Program program = new();

            List<Professor> professors = new(){
            };

            string? Field = null;
            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => Program.GenerateReport(Field,professors));
    }



    [Fact]
    public void CheckIfProfessorIsDeleted()
    {
            // Arrange
            Program program = new();
                
            List<Professor> professors = new(){
            new("Bruno","Sanchez","Padilla","123","69","Desarrollo de Software", null){},
            };

            HashSet<Classroom> classrooms = new(){
                new("7F1" , professors[0]){},
            };

            string SalaryAccToDelete = "69";
            // Act
            Program.DeleteProfessor(SalaryAccToDelete,professors,classrooms);
            // Assert
                Assert.DoesNotContain(professors, p => p.GetDecryptedSalaryAccount() == SalaryAccToDelete);
    }

}
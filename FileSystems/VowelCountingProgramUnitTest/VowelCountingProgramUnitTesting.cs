namespace VowelCountingProgramUnitTest;
using VowelCountingProgram;
using Xunit;
using static System.IO.Directory; // Create or kill folders
using static System.IO.Path; // Creates URLS // C://Documentos...

public class VowelCountingProgramUnitTests
{
    [Fact]
    public void ArgumentNullException()
    {
    //Arrange
        string? textFile = null;
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<ArgumentNullException>(() => vowelCountingProgram.TextProcessor(textFile));
    }

        [Fact]
    public void NotSupportedException()
    {
    //Arrange
        //get path of current directory
        string dir = GetCurrentDirectory();
        //combine directory with the source txt file
        string textFile = Combine(dir, @"..\..\..\..","text_files","text.bak");
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<NotSupportedException>(() => vowelCountingProgram.TextProcessor(textFile));
    }

    [Fact]
        public void PathTooLongException()
    {
    //Arrange
        //set 'path' to 260 characters plus .txt
        string textFile = new('*', 260);
        textFile = Combine(textFile, ".txt");
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<PathTooLongException>(() => vowelCountingProgram.TextProcessor(textFile));
    }
    
        [Fact]
        public void FileNotFoundException()
    {
    //Arrange
        //get path of current directory
        string dir = GetCurrentDirectory();
        //combine directory with the source txt file
        string textFile = Combine(dir, @"..\..\..\..","text_files","iDontExisthaha.txt");
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<FileNotFoundException>(() => vowelCountingProgram.TextProcessor(textFile));
    }
    
        [Fact]
    public void ArgumentException()
    {
    //Arrange
        string? textFile = "";
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<ArgumentException>(() => vowelCountingProgram.TextProcessor(textFile));
    }

    [Fact]
    public void EmptyString()
    {
    //Arrange
        //get path of current directory
        string dir = GetCurrentDirectory();
        //combine directory with the source txt file
        string textFile = Combine(dir, @"..\..\..\..","text_files","emptyString.txt");
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<ArgumentException>(() => vowelCountingProgram.TextProcessor(textFile));
    }

    [Fact]
    public void ReadOnly()
    {
    //Arrange
        //get path of current directory
        string dir = GetCurrentDirectory();
        //combine directory with the source txt file
        string textFile = Combine(dir, @"..\..\..\..","text_files","ReadOnly.txt");
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<UnauthorizedAccessException>(() => vowelCountingProgram.TextProcessor(textFile));
    }

    [Fact]
    public void HiddenFile()
    {
    //Arrange
        //get path of current directory
        string dir = GetCurrentDirectory();
        //combine directory with the source txt file
        string textFile = Combine(dir, @"..\..\..\..","text_files","ImAGhost.txt");
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<UnauthorizedAccessException>(() => vowelCountingProgram.TextProcessor(textFile));
    }

    [Fact]
    public void NullString()
    {
    //Arrange
        //get path of current directory
        string dir = GetCurrentDirectory();
        //combine directory with the source txt file
        string textFile = Combine(dir, @"..\..\..\..","text_files","ntxt.txt");
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<ArgumentException>(() => vowelCountingProgram.TextProcessor(textFile));
    }

    [Fact]
    public void NotAPath()
    {
    //Arrange
        string textFile = "txt.txt";
    //Act
        VowelCountingProgram vowelCountingProgram = new();
    //Assert
        var exception = Assert.Throws<FileNotFoundException>(() => vowelCountingProgram.TextProcessor(textFile));
    }
}
using static System.Environment;
using static System.IO.Path;
using System.Xml.Serialization;
using library.Shared;
using System.Text.RegularExpressions;


//create professors list
HashSet<Professor> professors = new();
var professorComparer = new ProfessorEqualityComparer();


//creating storers
List<Storer> storers = new(){
    new("Bruno","Sanchez","Padilla","123"){
    },
    new("Refugio","Calabazo","Perez","SecurePassword123"){
    }
};

//create Classroom list
HashSet<Classroom> classrooms = new();

//Storer variables
string? StorerName, StorerMiddleName, StorerLastName, StorerPassword;
//Professor variables
string? ProfessorName, ProfessorMiddleName, ProfessorLastName, ProfessorPassword, SalaryAccount, Group; 

//Begin Program
//terminator variable
bool terminateProgram = false;
//paths for XML & JSON Files
string ProfessorsPathXML = Combine(CurrentDirectory, "Files", "professors.xml");
string ProfessorsPathJSON = Combine(CurrentDirectory, "Files", "professors.json");
//paths for XML & JSON Files
string ClassroomsPathXML = Combine(CurrentDirectory, "Files", "classrooms.xml");
string ClassroomsPathJSON = Combine(CurrentDirectory, "Files", "classrooms.json");
//paths for XML & JSON Files
string storersPathXML = Combine(CurrentDirectory, "Files", "storer.xml");
string storersPathJSON = Combine(CurrentDirectory, "Files", "storer.json");
//serialize current storers, since these are hard coded and won't change
SerializeStorerXML(storersPathXML);
SerializeStorerJSON(storersPathJSON);


Login();
while(terminateProgram == false){
WriteLine("Choose an option. \n 1-Add a Professor \n 2-Edit a Professor \n 3-Delete a Professor\n 4-Change Password\n 5-Generate report\n 6-Login with another Storer\n 7-Exit");
int? answer = int.Parse(ReadLine());
Clear();
while(!(answer == 1 || answer == 2 || answer == 3 || answer == 4 || answer == 5 || answer == 6 || answer == 7)){
WriteLine("Choose a valid number betwen 1 and 6 \n 1-Add a Professor \n 2-Edit a Professor \n 3-Delete a Professor\n 4-Change Password\n 5-Generate report\n 6-Login with another Storer\n 7-Exit");
answer  = int.Parse(ReadLine());
Clear();
}
switch(answer){
//Add Professor
case 1:{
//insert and validate Name
WriteLine("Professor Name: ");
ProfessorName = ReadLine().ToUpper();
Clear();
while(false == ValidateString(ProfessorName)){
WriteLine("Enter a valid Name: ");
ProfessorName = ReadLine().ToUpper();
Clear();
}

//insert and validate MiddleName
WriteLine("Professor Middle Name: ");
ProfessorMiddleName = ReadLine().ToUpper();
Clear();
while(false == ValidateString(ProfessorMiddleName)){
WriteLine("Enter a valid Middle Name: ");
ProfessorMiddleName = ReadLine().ToUpper();
Clear();
}

//insert and validate LastName
WriteLine("Professor Last Name: ");
ProfessorLastName = ReadLine().ToUpper();
Clear();
while(false == ValidateString(ProfessorLastName)){
WriteLine("Enter a valid Last Name: ");
ProfessorLastName = ReadLine().ToUpper();
Clear();
}

//insert Password
WriteLine("Professor Password: ");
ProfessorPassword = ReadLine();
Clear();


//insert salary account
WriteLine("Professor Salary Account: ");
SalaryAccount = ReadLine();
Clear();
while(false == ValidateSalaryAccount(SalaryAccount)){
WriteLine("Enter a valid Salary Account(Only numbers): ");
SalaryAccount = ReadLine();
Clear();
}

//Add the subjects that the professor has
List<string>? Subject = new();
WriteLine("Insert the amount of Subjects that the professor has.");
int amount = int.Parse(ReadLine());
Clear();
while(amount > 5){
    WriteLine("A Professor cannot have more than 5 subjects: ");
    amount = int.Parse(ReadLine());
    Clear();
}
for(int i = 0; i < amount ; i++){
    WriteLine($"Insert the subject number {i + 1}:");
    Subject.Add(ReadLine().ToUpper());
    Clear();
}
Subject.Sort();

//get data from XML file
if(File.Exists(ProfessorsPathXML))if(DeserializeProfessorsXML(ProfessorsPathXML) is not null)professors = DeserializeProfessorsXML(ProfessorsPathXML);
if(File.Exists(ClassroomsPathXML))if(DeserializeClassroomsXML(ClassroomsPathXML) is not null)classrooms = DeserializeClassroomsXML(ClassroomsPathXML);

Professor professor = new(ProfessorName,ProfessorMiddleName,ProfessorLastName,ProfessorPassword,SalaryAccount,"Desarrollo de Software",Subject);
    
    if (!professors.Contains(professor, professorComparer))
    {
        //add professor to the list of professors
        professors.Add(professor);
    }
    else
    {
        WriteLine("There is already a professor with that salary account.");
        break;
    }


//ask which classroom is associated with the professor
WriteLine("In wich Group does the professor teaches: ");
Group = ReadLine().ToUpper();
Clear();
while(false == ValidateGroup(Group)){
    WriteLine("The group must be in the next format (Semester/Group/Number between 1 and 2)");
    Group = ReadLine().ToUpper();
    Clear();
}
Classroom classroom = new(Group, professor);
classrooms.Add(classroom);


//send classrooms to an XML file
SerializeClassroomsXML(ClassroomsPathXML);
//send classrooms to a JSON file
SerializeClassroomsJSON(ClassroomsPathJSON);

//send professors to an XML file
SerializeProfessorsXML(ProfessorsPathXML);
//send professors to a JSON file
SerializeProfessorsJSON(ProfessorsPathJSON);
break;
}
//Edit Professor
case 2:{
//get data from XML files
if(File.Exists(ProfessorsPathXML)){
if(DeserializeProfessorsXML(ProfessorsPathXML) is not null)professors = DeserializeProfessorsXML(ProfessorsPathXML);
}
else {
    WriteLine("There is no one to edit.");
break;
}
if(File.Exists(ClassroomsPathXML)){
if(DeserializeClassroomsXML(ClassroomsPathXML) is not null)classrooms = DeserializeClassroomsXML(ClassroomsPathXML);
}
else {
    WriteLine("There is no one to edit.");
break;
}
//search professor to edit
string? SalaryAccToEdit;
do{
WriteLine("Insert the Salary account of the professor that you want to edit: ");
SalaryAccToEdit = ReadLine();
Clear();
while(false == ValidateSalaryAccount(SalaryAccToEdit)){
WriteLine("Enter a valid Salary Account(Only numbers): ");
SalaryAccToEdit = ReadLine();
Clear();
//Check if that professor exist
}
}while(!ProfessorExist(SalaryAccToEdit));

//Get data to edit and edit the file
EditProfessor(SalaryAccToEdit);

break;
}
//Delete Professor
case 3:{
//get data from XML files
if(File.Exists(ProfessorsPathXML)){
if(DeserializeProfessorsXML(ProfessorsPathXML) is not null)professors = DeserializeProfessorsXML(ProfessorsPathXML);
}
else {
    WriteLine("There is no one to delete.");
break;
}
if(File.Exists(ClassroomsPathXML)){
if(DeserializeClassroomsXML(ClassroomsPathXML) is not null)classrooms = DeserializeClassroomsXML(ClassroomsPathXML);
}
else {
    WriteLine("There is no one to delete.");
break;
}

//get salary account, wich is unique
string? SalaryAccToDelete;
do{
WriteLine("Insert the Salary account of the professor that you want to delete: ");
SalaryAccToDelete = ReadLine();
Clear();
while(false == ValidateSalaryAccount(SalaryAccToDelete)){
WriteLine("Enter a valid Salary Account(Only numbers): ");
SalaryAccToDelete = ReadLine();
Clear();
//Check if that professor exist
}
}while(!ProfessorExist(SalaryAccToDelete));
DeleteProfessor(SalaryAccToDelete);


//send data to files
//send classrooms to an XML file
SerializeClassroomsXML(ClassroomsPathXML);
//send classrooms to a JSON file
SerializeClassroomsJSON(ClassroomsPathJSON);

//send professors to an XML file
SerializeProfessorsXML(ProfessorsPathXML);
//send professors to a JSON file
SerializeProfessorsJSON(ProfessorsPathJSON);
break;
}
//Change password
case 4:{
WriteLine(" 1-Change storer Password \n 2-Change Professor Password \n 3-Go back");
int? res = int.Parse(ReadLine());
Clear();
while(!(res == 1 || res == 2 || res == 3)){
WriteLine("Choose a valid number\n 1-Change storer Password \n 2-Change Professor Password \n 3-Go back");
res = int.Parse(ReadLine());
Clear();
}
switch (res){
case 1:{
    //get data from XML files
    if(File.Exists(storersPathXML)){
    if(DeserializeStorerXML(storersPathXML) is not null)storers = DeserializeStorerXML(storersPathXML);
    }
    else {
        WriteLine("There is no one to change password.");
    break;
    }

    int index = 0;
    foreach(Storer s in storers){
        WriteLine($"{index + 1}- Storer: {s.Name} {s.MiddleName} {s.LastName}");
        index++;
    }
    int response;
    do{
    WriteLine("Select the number of the storer that you want to change the password of: ");
    response = int.Parse(ReadLine());
    }while(!(response <= storers.Count && response > 0));
    Clear();
    response -= 1;
    Storer st = storers[response];
    //ask for the new password
    WriteLine($"Storer {st.Name} {st.MiddleName} {st.LastName} new Password: ");
    string StorernewPassword = ReadLine();
    Clear();
    //remove old storer and add new storer with updated password
    Storer storer = new(st.Name,st.MiddleName,st.LastName,StorernewPassword);
    storers.RemoveAt(response);
    storers.Add(storer);

    //serialize data
    SerializeStorerXML(storersPathXML);
    SerializeStorerJSON(storersPathJSON);

    break;
}
case 2:{
    //get data from XML files
    if(File.Exists(ProfessorsPathXML)){
    if(DeserializeProfessorsXML(ProfessorsPathXML) is not null)professors = DeserializeProfessorsXML(ProfessorsPathXML);
    }
    else {
        WriteLine("There is no one to change password.");
    break;
    }
    if(File.Exists(ClassroomsPathXML)){
    if(DeserializeClassroomsXML(ClassroomsPathXML) is not null)classrooms = DeserializeClassroomsXML(ClassroomsPathXML);
    }
    else {
        WriteLine("There is no one to change password.");
    break;
    }


    //get salary account, wich is unique
    string? SalaryAccToUpdate;
    do{
    WriteLine("Insert the Salary account of the professor that you want to change the password: ");
    SalaryAccToUpdate = ReadLine();
    Clear();
    while(false == ValidateSalaryAccount(SalaryAccToUpdate)){
    WriteLine("Enter a valid Salary Account(Only numbers): ");
    SalaryAccToUpdate = ReadLine();
    Clear();
    //Check if that professor exist
    }
    }while(!ProfessorExist(SalaryAccToUpdate));
    UpdateProfessorPassword(SalaryAccToUpdate);
    break;
}
case 3:{
    break;
}
}
break;
}
//Generate reports
case 5:{
HashSet<Professor> professorsXMl = new();
HashSet<Classroom> classroomsXML = new();
HashSet<Professor> professorsJSON = new();
HashSet<Classroom> classroomsJSON = new();

//get data from Json files
if(File.Exists(ProfessorsPathJSON)){
if(DeserializeProfessorsJSON(ProfessorsPathJSON) is not null)professorsJSON = DeserializeProfessorsJSON(ProfessorsPathJSON);
}
else {
    WriteLine("There no Professors yet.");
break;
}
if(File.Exists(ClassroomsPathJSON)){
if(DeserializeClassroomsJSON(ClassroomsPathJSON) is not null)classroomsJSON = DeserializeClassroomsJSON(ClassroomsPathJSON);
}
else {
    WriteLine("There no Professors yet.");
break;
}

//get data from XML files
if(File.Exists(ProfessorsPathXML)){
if(DeserializeProfessorsXML(ProfessorsPathXML) is not null)professorsXMl = DeserializeProfessorsXML(ProfessorsPathXML);
}
else {
    WriteLine("There no Professors yet.");
break;
}
if(File.Exists(ClassroomsPathXML)){
if(DeserializeClassroomsXML(ClassroomsPathXML) is not null)classroomsXML = DeserializeClassroomsXML(ClassroomsPathXML);
}
else {
    WriteLine("There no Professors yet.");
break;
}


WriteLine(" 1-Show all Professors by the Name  \n 2-Show Passwords \n 3-Show Salary Accounts \n 4-Show professors by group\n 5-Show all subjects in Alphabetic order\n 6-Go back");
int res = int.Parse(ReadLine());
Clear();
while(!(res == 1 || res == 2 || res == 3|| res == 4 || res == 5 || res == 6)){
WriteLine("Choose a valid number\n  1-Show all Professors by the Name  \n 2-Show Passwords \n 3-Show Salary Accounts \n 4-Show professors by group\n 5-Show all subjects in Alphabetic order\n 6-Go back");
res = int.Parse(ReadLine());
Clear();
}
switch(res){
    case 1:{
        WriteLine("Professors in the XML File:");
        WriteLine($"{"Name",-10} {"Middle Name",-12} {"Last Name",-12}");
        foreach(Professor p in professorsXMl){
            WriteLine($"{p.Name,-10} {p.MiddleName,-12} {p.LastName,-12}");
        }
        WriteLine("\n\nProfessors in the JSON File:");
        WriteLine($"{"Name",-10} {"Middle Name",-12} {"Last Name",-12}");
        foreach(Professor p in professorsJSON){
            WriteLine($"{p.Name,-10} {p.MiddleName,-12} {p.LastName,-12}");
        }

        WriteLine("Press enter to continue");
        ReadLine();
        Clear();
        break;
    }
    case 2:{
        WriteLine("Professors passwords in the XML File:");
        WriteLine($"{"Name",-10} {"Middle Name",-12} {"Last Name",-12} {"Password",-10}");
        foreach(Professor p in professorsXMl){
        WriteLine($"{p.Name,-10} {p.MiddleName,-12} {p.LastName,-12} {p.GetDecryptedPassword(),-10}");
        }
        WriteLine("\n\nProfessors passwords in the JSON File:");
        WriteLine($"{"Name",-10} {"Middle Name",-12} {"Last Name",-12} {"Password",-10}");
        foreach(Professor p in professorsJSON){
        WriteLine($"{p.Name,-10} {p.MiddleName,-12} {p.LastName,-12} {p.GetDecryptedPassword(),-10}");

        }

        WriteLine("Press enter to continue");
        ReadLine();
        Clear();
        break;
    }
    case 3:{
        WriteLine("Professors Salary Accounts in the XML File:");
        WriteLine($"{"Name",-10} {"Middle Name",-12} {"Last Name",-12} {"Salary Acc",-10}");
        foreach(Professor p in professorsXMl){
        WriteLine($"{p.Name,-10} {p.MiddleName,-12} {p.LastName,-12} {p.GetDecryptedSalaryAccount(),-10}");
        }
        WriteLine("\n\nProfessors Salary Accounts in the JSON File:");
        WriteLine($"{"Name",-10} {"Middle Name",-12} {"Last Name",-12} {"Salary Acc",-10}");
        foreach(Professor p in professorsJSON){
        WriteLine($"{p.Name,-10} {p.MiddleName,-12} {p.LastName,-12} {p.GetDecryptedSalaryAccount(),-10}");

        }

        WriteLine("Press enter to continue");
        ReadLine();
        Clear();
        break;
    }
    case 4:{
        WriteLine("Professors by group in the XML File:");
        WriteLine($"{"Group",-6} {"Name",-10} {"Middle Name",-12} {"Last Name",-12}");
        foreach(Classroom classroom in classroomsXML){
            WriteLine($"{classroom.Group,-6} {classroom.ProfessorInGroup.Name,-10} {classroom.ProfessorInGroup.MiddleName,-12} {classroom.ProfessorInGroup.LastName,-12}");   
        }

        WriteLine("\n\nProfessors by group in the JSON File:");
        WriteLine($"{"Group",-6} {"Name",-10} {"Middle Name",-12} {"Last Name",-12}");
        foreach(Classroom classroom in classroomsJSON){
            WriteLine($"{classroom.Group,-6} {classroom.ProfessorInGroup.Name,-10} {classroom.ProfessorInGroup.MiddleName,-12} {classroom.ProfessorInGroup.LastName,-12}");   
        }
        WriteLine("Press enter to continue");
        ReadLine();
        Clear();
        break;
    }
    case 5:{
        //Subjects on XML file
        List<string> sXML = new();
        foreach(Professor p in professorsXMl){
            if(p.Subject is not null)sXML.AddRange(p.Subject);
        }
        sXML.Sort();
        //create another list that doesnt take duplicates
        List<string> DistinctXML = sXML.Distinct().ToList();
        WriteLine("Subjects in the XML file: ");
        DistinctXML.ForEach(i => WriteLine("{0}\t", i));

        //Subjects on JSON file
        List<string> sJSON = new();
        foreach(Professor p in professorsJSON){
            if(p.Subject is not null)sJSON.AddRange(p.Subject);
        }
        sJSON.Sort();
        //create another list that doesnt take duplicates
        List<string> DistinctJSON = sJSON.Distinct().ToList();
        WriteLine("\nSubjects in the JSON file: ");
        DistinctJSON.ForEach(i => WriteLine("{0}\t", i));

        WriteLine("Press enter to continue");
        ReadLine();
        Clear();
        break;
    }
    case 6:{
        break;
    }
}
break;
}
//login
case 6:{
Login();
break;  
}
case 7:{
    terminateProgram = true;
    break;
}
}

}

bool ValidateGroup(string s){
    if(Regex.IsMatch(s, @"^[1-8][A-Z][1-2]$"))return true;
    else return false;
}

bool ValidateString(string s){
 if(Regex.IsMatch(s, @"^[A-Z]+$"))return true;
 return false;
}

bool ValidateSalaryAccount(string s){
 if(Regex.IsMatch(s, @"^[0-9]+$"))return true;
 return false;
}

//check if professor exist
bool ProfessorExist(string? s){
foreach(Professor p in professors){
    if(p.GetDecryptedSalaryAccount() == s){
        return true;
    }
}
return false;
}

//edit Professor password
void UpdateProfessorPassword(string SalaryAccToUpdate){
    foreach(Professor p in professors){
    if(p.GetDecryptedSalaryAccount() == SalaryAccToUpdate){
        //insert Password
        WriteLine("New Password: ");
        string? newProfessorPassword = ReadLine();
        Clear();
        string? ProfessorsOldSalaryAccount = p.GetDecryptedSalaryAccount();
        List<string>? ProfessorsOldSubject = p.Subject;
        string? ProfessorsOldMiddleName = p.MiddleName;
        string? ProfessorsOldName = p.Name;
        string? ProfessorsOldLastName = p.LastName;

        //delete old professor and classroom
        professors.Remove(p);
        //Add updated professor  
        Professor professor = new(ProfessorsOldName,ProfessorsOldMiddleName,ProfessorsOldLastName,newProfessorPassword ,ProfessorsOldSalaryAccount,"Desarrollo de Software",ProfessorsOldSubject);
        professors.Add(professor);
        foreach(Classroom c in classrooms){
            if(c.ProfessorInGroup.GetDecryptedSalaryAccount() == SalaryAccToUpdate){
            string? oldGroup = c.Group;
            //Add updated classroom
            Classroom classroom = new(oldGroup, professor);
            classrooms.Add(classroom);
            classrooms.Remove(c);
            break;
        }
        }

        //send classrooms to an XML file
        SerializeClassroomsXML(ClassroomsPathXML);
        //send classrooms to a JSON file
        SerializeClassroomsJSON(ClassroomsPathJSON);

        //send professors to an XML file
        SerializeProfessorsXML(ProfessorsPathXML);
        //send professors to a JSON file
        SerializeProfessorsJSON(ProfessorsPathJSON);
        break;
        }
    }
}

//edit professor
void EditProfessor(string SalaryAccToEdit){
//find if the account is in professors and if it is, delete it
foreach(Professor p in professors){
    if(p.GetDecryptedSalaryAccount() == SalaryAccToEdit){
        WriteLine($"Current Name: {p.Name}");
        //insert and validate Name
        WriteLine("New Name: ");
        ProfessorName = ReadLine().ToUpper();
        Clear();
        while(false == ValidateString(ProfessorName)){
        WriteLine($"Current Name: {p.Name}");
        WriteLine("Enter a valid Name: ");
        ProfessorName = ReadLine().ToUpper();
        Clear();
        }

        WriteLine($"Current Middle Name: {p.MiddleName}");
        //insert and validate Middle Name
        WriteLine("New Middle Name: ");
        ProfessorMiddleName = ReadLine().ToUpper();
        Clear();
        while(false == ValidateString(ProfessorMiddleName)){
        WriteLine($"Current Middle Name: {p.MiddleName}");
        WriteLine("Enter a valid Middle Name: ");
        ProfessorMiddleName = ReadLine().ToUpper();
        Clear();
        }

        WriteLine($"Current Last Name: {p.LastName}");
        //insert and validate Last Name
        WriteLine("New Last Name: ");
        ProfessorLastName = ReadLine().ToUpper();
        Clear();
        while(false == ValidateString(ProfessorLastName)){
        WriteLine($"Current Last Name: {p.LastName}");
        WriteLine("Enter a valid Last Name: ");
        ProfessorLastName = ReadLine().ToUpper();
        Clear();
        }

        WriteLine($"Current amount of subjects: {p.Subject.Count}");
        //Add the subjects that the professor has
        List<string>? Subject = new();
        WriteLine("Insert the new amount of Subjects that the professor has.");
        int amount = int.Parse(ReadLine());
        Clear();
        while(amount > 5){
        WriteLine("A Professor cannot have more than 5 subjects: ");
        amount = int.Parse(ReadLine());
        Clear();
        }

        for(int i = 0; i < amount ; i++){
        WriteLine($"Insert the subject number {i + 1}:");
        Subject.Add(ReadLine().ToUpper());
        Clear();
        }

        //ask which classroom is associated with the professor
        WriteLine($"In wich Group does the professor teaches: ");
        Group = ReadLine().ToUpper();
        Clear();
        while(false == ValidateGroup(Group)){
        WriteLine("The group must be in the next format (Semester/Group/Number between 1 and 2)");
        Group = ReadLine();
        Clear();
        }       

        string? ProfessorsOldPassword  = p.GetDecryptedPassword();
        string? ProfessorsOldSalaryAccount = p.GetDecryptedSalaryAccount();
        //delete old professor and classroom
        professors.Remove(p);
        foreach(Classroom c in classrooms){
            if(c.ProfessorInGroup.GetDecryptedSalaryAccount() == SalaryAccToEdit){
            classrooms.Remove(c);
            break;
        }
        }

        //Add updated professor  
        Professor professor = new(ProfessorName,ProfessorMiddleName,ProfessorLastName,ProfessorsOldPassword ,ProfessorsOldSalaryAccount,"Desarrollo de Software",Subject);
        professors.Add(professor);
        //Add updated classroom
        Classroom classroom = new(Group, professor);
        classrooms.Add(classroom);

        //send classrooms to an XML file
        SerializeClassroomsXML(ClassroomsPathXML);
        //send classrooms to a JSON file
        SerializeClassroomsJSON(ClassroomsPathJSON);

        //send professors to an XML file
        SerializeProfessorsXML(ProfessorsPathXML);
        //send professors to a JSON file
        SerializeProfessorsJSON(ProfessorsPathJSON);
        break;
        }
    }
}

//delete professor
void DeleteProfessor(string SalaryAccToDelete){
//find if the account is in professors and if it is, delete it
foreach(Professor p in professors){
    if(p.GetDecryptedSalaryAccount() == SalaryAccToDelete){
        WriteLine($"Professor {p.Name} {p.MiddleName} {p.LastName} with the following salary account {p.GetDecryptedSalaryAccount()} deleted succesfully.");
        professors.Remove(p);
        foreach(Classroom c in classrooms){
            if(c.ProfessorInGroup.GetDecryptedSalaryAccount() == SalaryAccToDelete){
            classrooms.Remove(c);
            break;
        }
        }
        }
    }
}

#region Classroom Files
//Serialize XML
void SerializeClassroomsXML(string path){
    XmlSerializer xs = new(type: classrooms.GetType());
    using (FileStream stream = File.Create(path))
    {
        xs.Serialize(stream, classrooms);
    }
}

//Deserialize XML
HashSet<Classroom> DeserializeClassroomsXML(string path){
    XmlSerializer xs = new(type: classrooms.GetType());
    using (FileStream xmlLoad = File.Open(path, FileMode.Open))
    {
        HashSet<Classroom> classrooms1 = xs.Deserialize(xmlLoad) as HashSet<Classroom>;
        return classrooms1;
    }
}

//Serialize JSON
void SerializeClassroomsJSON(string path){
    Newtonsoft.Json.JsonSerializer jss = new();
    using (StreamWriter jsonStream = File.CreateText(path))
    {
        jss.Serialize(jsonStream, classrooms);
    }
}

//Deserialize JSON
HashSet<Classroom> DeserializeClassroomsJSON(string path)
{
    using (FileStream jsonLoad = File.Open(path, FileMode.Open))
    {
        var classrooms = System.Text.Json.JsonSerializer.Deserialize<HashSet<Classroom>>(utf8Json: jsonLoad);
        return classrooms ?? new HashSet<Classroom>();
    }
}

#endregion

#region Professor Files
//professor files
//Serialize XML
void SerializeProfessorsXML(string path){
    XmlSerializer xs = new(type: professors.GetType());
    using (FileStream stream = File.Create(path))
    {
        xs.Serialize(stream, professors);
    }
}


//Deserialize XML
HashSet<Professor> DeserializeProfessorsXML(string path){
    XmlSerializer xs = new(type: professors.GetType());
    using (FileStream xmlLoad = File.Open(path, FileMode.Open))
    {
        HashSet<Professor> professors1 = xs.Deserialize(xmlLoad) as HashSet<Professor>;
        return professors1;
    }
}

//Serialize JSON
void SerializeProfessorsJSON(string path){
    Newtonsoft.Json.JsonSerializer jss = new();
    using (StreamWriter jsonStream = File.CreateText(path))
    {
        jss.Serialize(jsonStream, professors);
    }
}

//Deserialize JSON
HashSet<Professor> DeserializeProfessorsJSON(string path)
{
    using (FileStream jsonLoad = File.Open(path, FileMode.Open))
    {
        var professors = System.Text.Json.JsonSerializer.Deserialize<HashSet<Professor>>(utf8Json: jsonLoad);
        return professors ?? new HashSet<Professor>();
    }
}

#endregion

#region Storer Files
    //Serialize JSON
    void SerializeStorerJSON(string path){
    Newtonsoft.Json.JsonSerializer jss = new();
    using (StreamWriter jsonStream = File.CreateText(path))
    {
        jss.Serialize(jsonStream, storers);
    }
    }

//Serialize XML
void SerializeStorerXML(string path){
    XmlSerializer xs = new(type: storers.GetType());
    using (FileStream stream = File.Create(path))
    {
        xs.Serialize(stream, storers);
    }
}

    //Deserialize XML
    List<Storer> DeserializeStorerXML(string path){
    XmlSerializer xs = new(type: storers.GetType());
    using (FileStream xmlLoad = File.Open(path, FileMode.Open))
    {
        List<Storer>? storer1 = xs.Deserialize(xmlLoad) as List<Storer>;
        return storer1;
    }


}
#endregion

//login
void Login(){
bool StorerExist;
//register storer name
do{
WriteLine("Login with your Storer credentials.\nIntroduce your name: ");
StorerName = ReadLine();
WriteLine("Introduce your middle name: ");
StorerMiddleName = ReadLine();
WriteLine("Introduce your last name: " );
StorerLastName = ReadLine();
StorerExist = CheckIfStorerExist(StorerName,StorerMiddleName,StorerLastName);
Clear();
//check if the given name is a storer, if not repeat the process
WriteLine("That Name doesn't exist.");
}while(!StorerExist);
Clear();
//ask for password and check if it is correct
WriteLine("Introduce your password:");
StorerPassword = ReadLine();
Clear();
CheckIfPassword(StorerName,StorerMiddleName,StorerLastName,StorerPassword);
}

bool CheckIfStorerExist(string? Name, string? MiddleName, string? LastName){
foreach(Storer s in storers){
    if(Name.ToLower() == s.Name.ToLower() && MiddleName.ToLower() == s.MiddleName.ToLower() && LastName.ToLower() == s.LastName.ToLower()){  
        return true;
    }
}
return false;
}

bool CheckIfPassword(string? Name, string? MiddleName, string? LastName, string? Password){
foreach(Storer s in storers){
    if(Name.ToLower() == s.Name.ToLower() && MiddleName.ToLower() == s.MiddleName.ToLower() && LastName.ToLower() == s.LastName.ToLower()){  
        while(Password != s.GetDecryptedPassword()){
            WriteLine("Incorrect password. Try again: ");
            Password = ReadLine();
            Clear();
        }
        return true;
    }
}
return false;
}
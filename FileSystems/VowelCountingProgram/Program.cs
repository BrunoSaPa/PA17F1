using static System.IO.Directory; // Create or kill folders
using static System.IO.Path; // Creates URLS // C://Documentos...
using System.Text.RegularExpressions;

//get path of current directory
string dir = GetCurrentDirectory();
//combine directory with the source txt file
string textFile = Combine(dir, @"..\..","FileSystems","text_files","text.txt");

//create reader
StreamReader textReader = File.OpenText(textFile);

//text contains all the text from the txt file
string? text = textReader.ReadToEnd();
textReader.Close();

//send text to be processed
if(text !=null) textProcessor(text, textFile);
else WriteLine($"The file {textFile} is null.");


static void textProcessor(string text, string textFile){
//create palindrome variable
string? Palindrome = null;
//create another text variable that will later be used to write the last vowels with the last vowel count
string replacementText = text;
//clean the text from special characters
text = cleanText(text);
//create an array wichs stores each word in the text string
string[] words = Regex.Split(text, @" ");
//remove blank spaces that might have been stored
words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();

//check for each word if is the largest palindrome
foreach(string s in words){
    s.ToLower();
    Palindrome = LongestPalindrome(s, Palindrome);
}

//count vowels
//Vocal counts
int? aCount, eCount, iCount, oCount, uCount;
aCount =+ CountVowels(text, 'a');
eCount =+ CountVowels(text, 'e');
iCount =+ CountVowels(text, 'i');
oCount =+ CountVowels(text, 'o');
uCount =+ CountVowels(text, 'u');

    //replace last vowels for its count
    RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.RightToLeft;
    if(aCount != null){
        Regex regex = new Regex("[aá]" , options);
        replacementText = regex.Replace(replacementText, aCount.ToString(), 1) ;
    }
    if(eCount != null){
        Regex regex = new Regex("[eé]" , options);
        replacementText = regex.Replace(replacementText, eCount.ToString(), 1) ;
    }   
    if(iCount != null){
        Regex regex = new Regex("[ií]" , options);
        replacementText = regex.Replace(replacementText, iCount.ToString(), 1) ;
    } 
    if(oCount != null){
        Regex regex = new Regex("[oó]" , options);
        replacementText = regex.Replace(replacementText, oCount.ToString(), 1) ;
    }    
    if(uCount != null){
        Regex regex = new Regex("[uú]" , options);
        replacementText = regex.Replace(replacementText, uCount.ToString(), 1) ;
    }

    writeOnFile(replacementText, textFile, aCount, eCount, iCount, oCount, uCount, Palindrome);

}

static void writeOnFile(string replacementText, string textFile, int? aCount,int? eCount,int? iCount,int? oCount,int? uCount, string? Palindrome){
    //create writer
    StreamWriter textWriter = File.CreateText(textFile);
    textWriter.WriteLine(replacementText);

     //print the amount of times each vowel appeared
    textWriter.WriteLine();
    if(aCount != null){
        textWriter.WriteLine($"The vowel a appears {aCount} times in this file.");
        WriteLine($"The vowel a appears {aCount} times in the file.");
    }
    else{
        textWriter.WriteLine($"The vowel a appears don't appears in this file.");
        WriteLine($"The vowel a appears don't appears in this file."); 
    }
    if(eCount != null){
        textWriter.WriteLine($"The vowel e appears {eCount} times in this file.");
        WriteLine($"The vowel e appears {eCount} times in the file.");
    }
    else{
        textWriter.WriteLine($"The vowel e appears don't appears in this file.");
        WriteLine($"The vowel e appears don't appears in this file."); 
    }
    if(iCount != null){
        textWriter.WriteLine($"The vowel i appears {iCount} times in this file.");
        WriteLine($"The vowel i appears {iCount} times in the file.");
    }
    else{
        textWriter.WriteLine($"The vowel i appears don't appears in this file.");
        WriteLine($"The vowel i appears don't appears in this file."); 
    }
    if(oCount != null){
        textWriter.WriteLine($"The vowel o appears {oCount} times in this file.");
        WriteLine($"The vowel o appears {oCount} times in the file.");
    }
    else{
        textWriter.WriteLine($"The vowel o appears don't appears in this file.");
        WriteLine($"The vowel o appears don't appears in this file."); 
    }
    if(uCount != null){
        textWriter.WriteLine($"The vowel u appears {uCount} times in this file.");
        WriteLine($"The vowel u appears {uCount} times in the file.");
    }
    else{
        textWriter.WriteLine($"The vowel u appears don't appears in this file.");
        WriteLine($"The vowel u appears don't appears in this file."); 
    }


    //print the largest palindrome on the file if found
    textWriter.WriteLine();
    if(Palindrome != null){
    textWriter.WriteLine($"The longest palindrome in this file is: {Palindrome}");
    WriteLine($"The longest palindrome was written in the file and it was: {Palindrome}");
    }
    else{
    textWriter.WriteLine("There is no palindrome in this file.");
    WriteLine($"There is no palindrome in the file {textFile}");
    }
    textWriter.Close();
}

static string cleanText(string text){
//replace all final newlines with a space to prevent the last word of a paragraph and the first word of a paragraph from clipping 
text = Regex.Replace(text,"\n", " " );

//Replace vowels with accent
RegexOptions options = RegexOptions.IgnoreCase;
text = Regex.Replace(text,"[á]", "a",options);
text = Regex.Replace(text,"[é]", "e", options);
text = Regex.Replace(text,"[í]", "i", options );
text = Regex.Replace(text,"[ó]", "o", options );
text = Regex.Replace(text,"[ú]", "u", options );

//remove all non character nor number from the string
string pattern = @"[^0-9a-zA-Z ñ]";
text = Regex.Replace(text,pattern,"");

return text;
}

static int CountVowels(string s, char c){
    int vowelCount = 0;
    //count 'c' vowel
    string v = s.ToLower();
    foreach(char i in v){
        if(i == c){
            vowelCount++;
        }
    }
    return vowelCount;
}

static string? LongestPalindrome(string s, string? p){
    /*
    if we dont have a palindrome yet, check if the given string
    is a palindrome, if it is return s
    */
    string v = s.ToLower();
    if(p == null){
        if(ItsPalindrome(v)) return v;
        else return null;
    }
    /*
    If s is shorter than p, there is no need to even check if
    s is palindrome
    */
    else if(p.Length > v.Length){
        return p;
    }
    else{
        /*
        Iterate through half the string to find out if s is palindrome
        with a pointer on each side of the string.
        Consider if a number is odd, the for will stop at s.Length / 2,
        since i is an int it will stop just before the middle.
        This will leave an unchecked char, but it does not matter
        because if the substrings on the left and on the right side
        are equal, then there is no need to check that char in 
        specific.
        */
        for(int i = 0; i < v.Length / 2; i++){
            /*
            Compare first char of the string to the last char of the string.
            If they are not equal, return the actual largest palindrome.
            If they are equal continue comparing with the next pair of chars.
            */
            if(v[i] != v[v.Length - (i + 1)]) return p;
        }
        //return the greater palindrome
        return v;
    }
}

static bool ItsPalindrome(string s){
    //The for is the same as in LongestPalindrome
        for(int i = 0; i < s.Length / 2; i++){
            if(s[i] != s[s.Length - (i + 1)]) return false;
        }
        return true;
}

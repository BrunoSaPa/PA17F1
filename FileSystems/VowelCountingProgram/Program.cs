using static System.IO.Directory; // Create or kill folders
using static System.IO.Path; // Creates URLS // C://Documentos...
using System.Text.RegularExpressions; 

//get path of current directory
string dir = GetCurrentDirectory();
//combine directory with the source txt file
string textFile = Combine(dir, @"..\..","FileSystems","text_files","text.txt");
//create longest palindrome variable
string? Palindrome = null;
//Vocal counts
int? aCount, eCount, iCount, oCount, uCount;
string? replacementText;


//read file and send each word to LongestPalindrome() and CountingVocals()
using (StreamReader? textReader = new(textFile, true)){
//text contains all the text from the txt file
string? text = textReader.ReadToEnd();
replacementText = text;
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

    //count vowels and sum them all
    aCount =+ CountVowels(text, 'a');
    eCount =+ CountVowels(text, 'e');
    iCount =+ CountVowels(text, 'i');
    oCount =+ CountVowels(text, 'o');
    uCount =+ CountVowels(text, 'u');

    //replace last vowels for its count
    //replacementText = replaceVowelsWithCounts(replacementText, aCount, eCount, iCount, oCount, uCount);
}


//Replace last vocal with its count and write the largest palindrome
using (StreamWriter? textWriter = new(textFile, true)){
    //print the amount of times each vowel appeared
    textWriter.WriteLine();
    textWriter.WriteLine($"The vowel a appears {aCount} times on this file.");
    WriteLine($"The vowel a appears {aCount} times the file.");
    textWriter.WriteLine($"The vowel e appears {eCount} times on this file.");
    WriteLine($"The vowel e appears {eCount} times the file.");
    textWriter.WriteLine($"The vowel i appears {iCount} times on this file.");
    WriteLine($"The vowel i appears {iCount} times the file.");
    textWriter.WriteLine($"The vowel o appears {oCount} times on this file.");
    WriteLine($"The vowel o appears {oCount} times the file.");
    textWriter.WriteLine($"The vowel u appears {uCount} times on this file.");
    WriteLine($"The vowel u appears {uCount} times the file.");


    //print the largest palindrome on the file if found
    textWriter.WriteLine();
    if(Palindrome != null){
    textWriter.WriteLine($"The longest palindrome on this file is: {Palindrome}");
    WriteLine($"The longest palindrome was written on the file and it was: {Palindrome}");
    }
    else{
    textWriter.WriteLine("There is no palindrome on this file.");
    WriteLine($"There is no palindrome on the file {textFile}");
    }

}

/*static string? replaceVowelsWithCounts(string s, int aCount,int eCount,int iCount,int oCount,int uCount){
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == 'a' || s[i] == 'A' || s[i] == 'Á' || s[i] == 'á')
            {
                s[i] = 
            }
        }
        return "";
    }
}*/

static string? cleanText(string? text){
//replace all final newlines with a space to prevent the last word of a paragraph and the first word of a paragraph from clipping 
text = Regex.Replace(text,"\n", " " );

//Replace vowels with accent
text = Regex.Replace(text,"[á]", "a" );
text = Regex.Replace(text,"[é]", "e" );
text = Regex.Replace(text,"[í]", "i" );
text = Regex.Replace(text,"[ó]", "o" );
text = Regex.Replace(text,"[u]", "u" );
text = Regex.Replace(text,"[Á]", "A" );
text = Regex.Replace(text,"[É]", "E" );
text = Regex.Replace(text,"[Í]", "I" );
text = Regex.Replace(text,"[Ó]", "O" );
text = Regex.Replace(text,"[Ú]", "U" );

//remove all non character nor number from the string
string pattern = @"[^0-9a-zA-Z ñ]";
text = Regex.Replace(text,pattern,"");

return text;
}

static int CountVowels(string s, char c){
    int vowelCount = 0;
    foreach(char i in s){
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
    if(p == null){
        if(ItsPalindrome(s)) return s;
        else return null;
    }
    /*
    If s is shorter than p, there is no need to even check if
    s is palindrome
    */
    else if(p.Length > s.Length){
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
        for(int i = 0; i < s.Length / 2; i++){
            /*
            Compare first char of the string to the last char of the string.
            If they are not equal, return the actual largest palindrome.
            If they are equal continue comparing with the next pair of chars.
            */
            if(s[i] != s[s.Length - (i + 1)]) return p;
        }
        //return the greater palindrome
        return s;
    }
}

static bool ItsPalindrome(string s){
    //The for is the same as in LongestPalindrome
        for(int i = 0; i < s.Length / 2; i++){
            if(s[i] != s[s.Length - (i + 1)]) return false;
        }
        return true;
}

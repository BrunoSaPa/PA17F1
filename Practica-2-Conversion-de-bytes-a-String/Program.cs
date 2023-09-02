//declaration of array and random
byte[] byteArray = new byte[128];
var rand = new Random();
//fill array with random numbers
for (int i = 0; i < byteArray.Length; i++) {
     byteArray[i] = (byte)rand.Next(256);
 }

//print array in X2 wich is hex
for (int i = 0; i < byteArray.Length; i++){
    Write(format: "{0}{1}", arg0:byteArray[i].ToString("X2"), arg1:" ");
}


WriteLine("\n");

//print array as base 64 string
string base64String = Convert.ToBase64String(byteArray);
WriteLine(base64String);

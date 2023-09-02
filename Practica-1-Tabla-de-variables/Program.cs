//Headers
WriteLine("--------------------------------------------------------------------------");
WriteLine(format: "{0,-8}{1,-32}{2,-31}{3,-3}", "Type", "Byte(s) of memory", "Min", "Max");
WriteLine("--------------------------------------------------------------------------");
//sbyte row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "sbyte", sizeof(sbyte), sbyte.MinValue, sbyte.MaxValue );
//byte row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "byte", sizeof(byte), byte.MinValue, byte.MaxValue );
//short row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "short", sizeof(short), short.MinValue, short.MaxValue );
//ushort row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "ushort", sizeof(ushort), ushort.MinValue, ushort.MaxValue );
//int row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "int", sizeof(int), int.MinValue, int.MaxValue );
//uint row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "uint", sizeof(uint), uint.MinValue, uint.MaxValue );
//long row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "long", sizeof(long), long.MinValue, long.MaxValue );
//ulong row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "ulong", sizeof(ulong), ulong.MinValue, ulong.MaxValue );
//float row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "float", sizeof(float), float.MinValue, float.MaxValue );
//double row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "double", sizeof(double), double.MinValue, double.MaxValue );
//decimal row
WriteLine(format: "{0,-8}{1,-5}{2,30}{3,31}", "decimal", sizeof(decimal), decimal.MinValue, decimal.MaxValue );
WriteLine("--------------------------------------------------------------------------");

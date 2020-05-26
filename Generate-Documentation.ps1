dotnet tool install -g loxsmoke.mddox
dotnet build
mddox .\AbstractSyntaxTree\bin\Debug\netcoreapp3.1\AbstractSyntaxTree.dll --msdn -o .\doc\AbstractSyntaxTree.md
mddox .\CodeGeneration\bin\Debug\netcoreapp3.1\CodeGeneration.dll --msdn -o .\doc\CodeGeneration.md
mddox .\Contextual_analysis\bin\Debug\netcoreapp3.1\Contextual_analysis.dll --msdn -o .\doc\Contextual_analysis.md
mddox .\Core\bin\Debug\netcoreapp3.1\Core.dll --msdn -o .\doc\Core.md
mddox .\Lexer\bin\Debug\netcoreapp3.1\Lexer.dll --msdn -o .\doc\Lexer.md
mddox .\Parser\bin\Debug\netcoreapp3.1\Parser.dll --msdn -o .\doc\Parser.md
mddox .\SymbolTable\bin\Debug\netcoreapp3.1\SymbolTable.dll --msdn -o .\doc\SymbolTable.md

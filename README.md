# bubbler-vscode

A bubbler language server for VSCode.

Powered by [uni-vscode](https://github.com/kaby76/uni-vscode).

Quick install for VSCode: [bubbler-vscode](https://marketplace.visualstudio.com/items?itemName=xaxys.Bubbler).

This is a "bubbler language" vscode extension based on Antlr
and Language Server Protocol. It is useful for quick parsing checks
using VSCode. The only real requirement is that the grammar should be
[target agnostic](https://github.com/antlr/antlr4/blob/master/doc/python-target.md#target-agnostic-grammars).
Then, using [trgen](https://github.com/kaby76/Domemtech.Trash/tree/main/trgen),
a parse can be generated for the grammar and plugged into
this extension. Semantic highlighting is the only major component implemented
because static semantics computations (aka attributes) are not implemented
for Antlr4 grammars.

The code is divided into two parts:
Server and Client:

* The server (with Logger, LspHelpers, and Workspaces) is C# code that
implements an LSP server.
* The client is Typescript code that implemements the client VSCode
extension.

## How to use this extension

1) You will need prerequisites:

* [.NET SDK](https://dotnet.microsoft.com/) (7.0 or higher)
* [Trash](https://github.com/kaby76/Domemtech.Trash#install) (0.14.3)

2) Clone the repo. Run dotnet to build the language server, and run the "install.sh" script
to create the extesion for VSCode.

The server is a
C# program that reads stdin and writes to stdout. VSCode will redirect the input and output
in order to communicate with the server.

The client is a thin layer of code in Typescript. The "install.sh" script builds the
.vsix file which you can install.

    git clone https://github.com/xaxys/bubbler-vscode.git
    cd bubbler-vscode
    dotnet build
    cd VsCode
    bash clean.sh && bash install.sh

3) Create (or copy) an Antlr4 Bubbler grammar
The grammar must be processed by the
[trgen](https://github.com/kaby76/Domemtech.Trash/tree/main/trgen) (0.14.3) application of Trash.
`trgen` creates a standardized parser application from templates.

    cd Trgen
    trgen -s proto; cd Generated; dotnet build

4) Run VSCode, and install the .vsix 

    code .
    
In VSCode, open a file (e.g., a Java source file). In the lower right corner, there is a type. Change
the type of the file to "Bubbler". It takes a little while, but it should colorize the source file.

## Implementation

* LSP server in C#.
* VSCode client code in Typescript.
* Grammars are implemented in Antlr4. The parser driver is implemented
using [trgen](https://github.com/kaby76/Domemtech.Trash/tree/main/trgen).

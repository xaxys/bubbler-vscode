# bubbler-vscode

[![License](https://img.shields.io/badge/license-MIT-green.svg)](https://github.com/xaxys/bubbler-vscode/LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](https://github.com/xaxys/bubbler-vscode/pulls)
[![Release](https://img.shields.io/github/v/release/xaxys/bubbler-vscode.svg)](https://github.com/xaxys/bubbler-vscode/releases)
[![Visual Studio Marketplace Installs](https://img.shields.io/visual-studio-marketplace/i/xaxys.Bubbler)](https://marketplace.visualstudio.com/items?itemName=xaxys.Bubbler)
[![Visual Studio Marketplace Downloads](https://img.shields.io/visual-studio-marketplace/d/xaxys.Bubbler)](https://marketplace.visualstudio.com/items?itemName=xaxys.Bubbler&ssr=false#version-history)

A bubbler language server for VSCode.

Powered by [uni-vscode](https://github.com/kaby76/uni-vscode).

Quick install for VSCode: [bubbler-vscode](https://marketplace.visualstudio.com/items?itemName=xaxys.Bubbler)

This is a "bubbler language" vscode extension based on Antlr
and Language Server Protocol. It is useful for quick parsing checks
using VSCode. Semantic highlighting is the only major component implemented
because static semantics computations (aka attributes) are not implemented
for grammars.

The code is divided into two parts:
Server and Client:

* The server (with Logger, LspHelpers, and Workspaces) is C# code that
implements an LSP server.
* The client is Typescript code that implemements the client VSCode
extension.

## How to build this extension

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

```shell
git clone https://github.com/xaxys/bubbler-vscode.git
cd bubbler-vscode
dotnet build
cd VsCode
bash clean.sh && bash install.sh
```

3) Create (or copy) an Antlr4 Bubbler grammar
The grammar must be processed by the
[trgen](https://github.com/kaby76/Domemtech.Trash/tree/main/trgen) (0.14.3) application of Trash.
`trgen` creates a standardized parser application from templates.

```shell
cd Trgen
trgen -s proto
cd Generated
dotnet build
```

4) Run VSCode, and install the .vsix 

```
code .
```
    
In VSCode, open a file (e.g., a Bubbler source file). In the lower right corner, there is a type. Change
the type of the file to "Bubbler". It takes a little while, but it should colorize the source file.

## Implementation

* LSP server in C#.
* VSCode client code in Typescript.
* Grammars are implemented in Antlr4. The parser driver is implemented
using [trgen](https://github.com/kaby76/Domemtech.Trash/tree/main/trgen).

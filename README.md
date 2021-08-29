# Light Photo Manager
A basic photo manager that organizes automatically your photos

## Publish
Use the command and options below to create a self-contained package <br/>

<span style="color:blue">&ensp;dotnet publish [<PROJECT>|<SOLUTION>] [-a|--arch <ARCHITECTURE>] </span><br/>
<span style="color:blue">&ensp;&ensp;[-c|--configuration <CONFIGURATION>] </span><br/>
<span style="color:blue">&ensp;&ensp;[-f|--framework <FRAMEWORK>] [--force] [--interactive] </span><br/>
<span style="color:blue">&ensp;&ensp;[--manifest <PATH_TO_MANIFEST_FILE>] [--no-build] [--no-dependencies] </span><br/>
<span style="color:blue">&ensp;&ensp;[--no-restore] [--nologo] [-o|--output <OUTPUT_DIRECTORY>] </span><br/>
<span style="color:blue">&ensp;&ensp;[--os <OS>] [-r|--runtime <RUNTIME_IDENTIFIER>] </span><br/>
<span style="color:blue">&ensp;&ensp;[--self-contained [true|false]] </span><br/>
<span style="color:blue">&ensp;&ensp;[--no-self-contained] [-v|--verbosity <LEVEL>] </span><br/>
<span style="color:blue">&ensp;&ensp;[--version-suffix <VERSION_SUFFIX>] </span><br/>

Go to the folder where .csproj project file is and run the following command: <br/>

&ensp;**dotnet publish --self-contained true --runtime win-x86** <br/>

After the package files are generated, execute the following tasks:

1) Zip to source the package to publish.package.rar<br/>
2) Unzip the package to destination 




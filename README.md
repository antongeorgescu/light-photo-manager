# Light Photo Manager
A basic photo manager that organizes automatically your photos. The photos are distributed in year/season based on the capture date. One or multiple source folders will be processed and their combined photos will be copied in as many destination folders as specified. Both source and destinations paths are set in **configuration.json** file, which is deployed in the same installation folder with the executable. 

The media extensions that are getting processed are listed in **media.extensions.json** file and can be extended anytime.

## Functionality ##
**LightPhotoManager.exe** executable can run in command line mode, with a bunch of options:
 
* **LightPhotoManager.exe X** - extracts the set of media extensions from the source folders and compares with the application extensions; if need be the application extensions can be updated in **media.extensions.json** file<br/>
* **LightPhotoManager.exe D** - de-duplicates the files in a tree folder structure, by file name only<br/>
* **LightPhotoManager.exe D2** - de-duplicates the files in a tree folder structure, by file name, modified date, size and extension<br/>
* **LightPhotoManager.exe P** - processes (organizes) the media from a source tree folder structure to a bunch of destination folders, all configured in **configuration.json** file<br/>

## Dependencies

&ensp;&ensp;Microsoft .NET Core 2.1 <br/>
&ensp;&ensp;Newtonsoft.Json 13.0


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




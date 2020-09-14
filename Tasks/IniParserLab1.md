## INI Parser
### Laboratory work №1

### [Solution](https://github.com/annchous/OopLabs/tree/master/OopLabs/IniParser)

The task is to create a tool for processing the configuration INI file.
> *An **INI file** is a configuration file for computer software that consists of a text-based content with a structure and syntax comprising key-value pairs for **properties**, and **sections** that organize the properties.*

*More information about INI file on [Wikipedia](https://en.wikipedia.org/wiki/INI_file).*

### File format
_**All parameter and section names are strings without spaces, consisting of Latin characters, numbers and underscores.**_
#### Keys (properties)
The basic element contained in an INI file is the key or property. Every ```key``` has a ```name``` and a ```value```, delimited by an equals sign (```=```). The name appears to the left of the equals sign.

Parameter values can be of one of the following types:
* ```integer```
* ```real```
* ```string```: no spaces, but unlike the parameter name may contain dots (.)

#### Sections
Keys may, but need not, be grouped into arbitrarily named sections. The section name appears on a line by itself, in square brackets (```[``` and ```]```). All keys after the section declaration are associated with that section. There is no explicit "end of section" delimiter; sections end at the next section declaration, or at the end of the file. Sections cannot be nested.

In this implementation, the absence of a section is considered an _**erroneous**_ INI file representation.

#### Case sensitivity
In this implementation, the names of sections and parameters are _**case sensitive**_.

#### Comments
Semicolons (```;```) at the beginning of the line indicate a comment. Comment lines are _**ignored**_.

#### An example of INI file
```
[COMMON]
StatisterTimeMs = 5000      
LogNCDM = 1 ; Logging ncdm proto  
LogXML = 0 ; Logging XML proto   
DiskCachePath = /sata/panorama ; path for the file cache 
OpenMPTThreadsCount =     2     

[ADC_DEV]
BufferLenSecons = 0.65   
SampleRate =    120000000.0   
Driver =   libusb   

[NCMD]                  
EnableChannelControl = 1
SampleRate = 900000.0
TidPacketVersionForTidControlCommand = 2

; TidPacket versions

[LEGACY_XML]
ListenTcpPort = 1976

[DEBUG]
PlentySockMaxSize = 126
```


### Code description
#### [Program.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/IniParser/Program.cs)
Designed to interact with the parser.
Parser initialization:
```
IniParser parser = new IniParser(*file_path_here*);
```
Data structure with parsed values initialization:
```
var data = parser.Parse();
```

#### [IniParser.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/IniParser/IniParser.cs)
Implements the parsing process by interacting with the ```Parser``` class and ```IniData``` class.

The ```Parse``` method returns an object of the ```IniData``` class with the processed values and has the signature:
```
public IniData Parse()
```

#### [IniData.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/IniParser/IniData.cs)
Contains the processed data of the INI file and implements the getters required by the job conditions.

The data is stored in a ```Dictionary``` collection with a key as a ```Section``` entity and a value as a ```List``` of ```Property``` entities:
```
public Dictionary<Section, List<Property>> Data;
```
So the search for the required value is performed first by the section name (```Section``` contains the ```Name``` field), and then the property with the required parameter is searched for in the Property list (```Property``` contains the ```Name``` field - the parameter name and the ```Value``` field - the parameter value).

There are 4 getters implemented in the ```IniData``` class:
```
public int TryGetInt(string sectionName, string propertyName)
public double TryGetDouble(string sectionName, string propertyName)
public string TryGetString(string sectionName, string propertyName)
public T TryGet<T>(string sectionName, string propertyName)
```

#### [Parser.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/IniParser/Parser.cs)
Contains methods for processing strings and parsing values.

Methods used in the ```IniParser``` class to get the processed ```Section``` and ```Property``` values:
```
public Section ParseSectionLine(string sectionLine)
public Property ParsePropertyLine(string propertyLine)
```

#### [Section.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/IniParser/Section.cs) & [Property.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/IniParser/Property.cs)
Contain a view of entities ```Section``` and ```Property```.

#### [IniFile.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/IniParser/IniFile.cs)
Represents the source file.
Contains a list of lines (```List<string>```) read from a file named ```Name``` at ```Path```.
Checks the correctness of the file format (```.ini```).

#### [IniParserException.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/IniParser/IniParserException.cs)
Contains custom exception classes.

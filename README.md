GTFS.NET
========

A .NET library for reading [General Transit Feet Specification] files.

## Components ##

### Gtfs.Contract ###

This assembly contains the data contracts used by the other assemblies. 
These classes are designed to be easily serialized and deserialized by [JSON.NET].

### Gtfs.IO ###

This assembly is used to read a [GTFS] Zip stream and convert it into objects.

This project references [CsvHelper] via [NuGet].


### Gtfs.Test ###

This assembly contains Visual Studio unit tests.

## Development Environment ##

Visual Studio 2012

## License ##
[The MIT License]


[CsvHelper]:http://joshclose.github.io/CsvHelper/
[General Transit Feet Specification]:https://developers.google.com/transit/gtfs/
[GTFS]:https://developers.google.com/transit/gtfs/
[JSON.NET]:http://james.newtonking.com/json
[The MIT License]:http://choosealicense.com/licenses/mit/
[NuGet]:https://www.nuget.org/

GTFS.NET
========

A .NET library for reading [General Transit Feed Specification] files.

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

[CsvHelper]:http://joshclose.github.io/CsvHelper/
[General Transit Feed Specification]:https://developers.google.com/transit/gtfs/
[GTFS]:https://developers.google.com/transit/gtfs/
[JSON.NET]:http://james.newtonking.com/json
[NuGet]:https://www.nuget.org/

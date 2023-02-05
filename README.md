# netCache
netCache is a simple, dependency injection friendly and persistant cache storage library.

## Design goals
netCache is designed to be a simple and fast library that provides an almost silver-bullet solution with caching and persistance for .NET applications. I've designed the library to be functional for all modern .NET applications.

## Features
- Garbage collection (soon);
- Object lifetimes;
- Persistant caching to the disk;
- JSON serialization with Snappy compression - you can use your own serialization implementation;
- Layered memory caching;
- Uses modern C# features (generic types & nullable reference types);
- Simple and easy to use interface, no further abstraction layers required;
- Fast and can be modified to be even faster without forking.

## Installation
Run the following in your package manager console or install via the NuGet package manager on Visual Studio or Rider;

```shell
PM> Install-Package NetCache
```

### Dependency injection
If you wish to use NetCache with dependency injection, you should also install the following package

```shell
PM> Install-Package NetCache.DependencyInjection
```

## Usage
You can start using the package by either injecting it into your service collection or creating a new instance of the NetCacher class.

```cs
var cacher = new NetCacher(new NetCacherOptions()); // Create a new cacher with default options
```

Or, with dependency injection

```cs
services.AddNetCacher(opts =>
{
    ...
});
```

### Getting and setting records
Firstly, here is our mock class that we'll be using in the following examples:

```cs
public class Mock
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Mock(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
```

Now we can get started, to set a new record in the cache, you can call the `SetObject<TObj>` method as shown

```cs
var myObj = new Mock("Foo", "Bar");

cacher.SetObject<Mock>("myKey", myObj, TimeSpan.FromMinutes(1));

// Object has been cached under the key 'myKey' for 1 minute before it expires
```

And for getting that same record, we can use the `GetRecord<TObj>` method as shown below

```cs
var myObj = cacher.GetObject<Mock>("myKey");
if (myObj is null)
{
    // No object was found in the cache, it could've expired or never existed
    Console.WriteLine("No object found!");
    return;
}

Console.WriteLine(myObj.FirstName); // Foo

```

### Custom serializers
Maybe, the default serializer just isn't cutting it for you and you need your own custom serializer that gives you specifically tailored solutions. No problem, find below a simple implementation of a custom serializer.

```cs
public class CustomSerializer : ISerializer
{
    public ReadOnlySpan<char> Serialize<TObj>(TObj obj)
    {
        // ...
    }

    public TObj? Deserialize<TObj>(ReadOnlySpan<char> rawData)
    {
        // ...
    }
}
```

Granted that this one does absolutely nothing, but it can easily provide you enough room to build your own requirements into the serializing system. You could even speed up the performance of the library (in that case, open a pull request!).

## License
Licensed under the MIT license and currently leveraging these open source packages:
- [IronSnappy](https://github.com/aloneguid/IronSnappy)
- [xxHash.NET](https://github.com/wilhelmliao/xxHash.NET)

using System.Text;
using Extensions.Data;
using Microsoft.VisualBasic.CompilerServices;
using NetCache.Interfaces;
using NetCache.Models;

namespace NetCache.Tests;

public class Tests
{
    private readonly INetCacher _cacher = new NetCacher(new NetCacheOptions()
    {
        PersistantCacheLocation = Path.Combine(Path.GetTempPath(), "netcache"),
        Lifetime = TimeSpan.FromMinutes(3)
    });
    
    private MockClass _mock = new("Linus", "Torvalds");

    public class MockClass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public MockClass(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    [SetUp]
    public void SetUp()
    {
        _cacher.SetObject("testKey", _mock); // Set test entry
    }

    [Test]
    public void Get_Cache_ReturnsInvalid()
    {
        var obj = _cacher.GetObject<MockClass>("badKey"); // Get a bad value
        Assert.That(obj, Is.Null); // Must be null
        Assert.Pass();
    }

    [Test]
    public void Get_PersistantCache_ReturnsValid()
    {
        var retVal = _cacher.GetObject<MockClass>("testKey"); // Assumes memory test has been ran
        
        Assert.Multiple(() =>
        {
            Assert.That(retVal, Is.Not.Null); // Ensure not null
            Assert.That(retVal!.FirstName, Is.EqualTo(_mock.FirstName)); // Ensure vals are the same
            Assert.That(retVal!.LastName, Is.EqualTo(_mock.LastName)); // No corruption
        });
        
        Assert.Pass();
    }

    [Test]
    public void GetSet_MemoryCache_ReturnsValid()
    {
        var retVal = _cacher.GetObject<MockClass>("testKey");
        
        Assert.Multiple(() =>
        {
            Assert.That(retVal, Is.Not.Null); // Ensure not null
            Assert.That(retVal!.FirstName, Is.EqualTo(_mock.FirstName)); // Ensure vals are the same
            Assert.That(retVal!.LastName, Is.EqualTo(_mock.LastName)); // No corruption
        });
        
        Assert.Pass();
    }
}
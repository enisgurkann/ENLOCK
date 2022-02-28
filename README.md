
# ENLOCK - EFCORE With(No Lock) Tool
<img src="https://github.com/enisgurkann/ENLOCK/blob/master/ENLOCK.png?raw=true" data-canonical-src="https://github.com/enisgurkann/ENLOCK/blob/master/ENLOCK.png?raw=true" width="150" height="150" />


[![GitHub](https://img.shields.io/github/license/enisgurkann/ENLOCK?color=594ae2&logo=github&style=flat-square)](https://github.com/enisgurkann/ENLOCK/blob/master/LICENSE)
[![GitHub Repo stars](https://img.shields.io/github/stars/enisgurkann/ENLOCK?color=594ae2&style=flat-square&logo=github)](https://github.com/enisgurkann/ENLOCK/stargazers)
[![GitHub last commit](https://img.shields.io/github/last-commit/enisgurkann/ENLOCK?color=594ae2&style=flat-square&logo=github)](https://github.com/mudblazor/mudblazor)
[![Contributors](https://img.shields.io/github/contributors/enisgurkann/ENLOCK?color=594ae2&style=flat-square&logo=github)](https://github.com/enisgurkann/ENLOCK/graphs/contributors)
[![Discussions](https://img.shields.io/github/discussions/enisgurkann/ENLOCK?color=594ae2&logo=github&style=flat-square)](https://github.com/enisgurkann/ENLOCK/discussions)
[![Nuget version](https://img.shields.io/nuget/v/ENLOCK?color=ff4081&label=nuget%20version&logo=nuget&style=flat-square)](https://www.nuget.org/packages/ENLOCK/)
[![Nuget downloads](https://img.shields.io/nuget/dt/ENLOCK?color=ff4081&label=nuget%20downloads&logo=nuget&style=flat-square)](https://www.nuget.org/packages/ENLOCK/)



Entity freamwork kullanırken database kilitlenme yani lock olayını engellemek için kullandığımız transcaction scope olayını basite indirgemek için yaptığım extention dur
 
 ## Methods
 FirstOrDefault,Single,ToList,Any and Async

## Efcore Provider Usage

```
PM> Install-Package ENLOCK
```


```
PM> Standart FirstOrDefault
```

```csharp

        var customer = await _context
        .Customers
        .Where(x => x.Name == 'Enis' && x.Surname == 'Gürkan')
        .FirstOrDefaultkAsync();
 
```
 
```
PM> Using ToFirstOrDefaultWithNoLockAsync
```

```csharp

        var customer = await _context
        .Customers
        .Where(x => x.Name == 'Enis' && x.Surname == 'Gürkan')
        .ToFirstOrDefaultWithNoLockAsync();
 
```

```
PM> Using other expressions
```

```csharp

        var customers = await _context.NoLock(s => s.Customers.ToListAsync());
 
```



 

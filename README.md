# **Metarwiz** | [![](https://img.shields.io/nuget/v/ZippyNeuron.Metarwiz.svg?style=flat-square&logo=appveyor&color=238636)](https://www.nuget.org/packages/ZippyNeuron.Metarwiz)
Is a simple class library that can be used to parse and visualise METAR reports (ICAO and North American).

The **metarwiz** project contains the library that delivers all the parsing functionality.

The **metarwiz-tests** project contains some basic tests that support the development of Metarwiz.
 
The **metarwiz-console** project contains a wider range of code examples and can be used as a playground for Metarwiz.

The following is a very simple example of how you might start using Metarwiz.

```c#
/* this is a small example METAR report */
string metar = @"METAR EGLC 221850Z AUTO 29005KT 9999 NCD 19/16 Q1022="

/* the following uses the static method to parse and create an instance
 * of Metarwiz. You can also use the instance constructor if desired.
 */
Metarwiz metarwiz = Metarwiz.Parse(metar);

/* let's get the temperature and dew point information from this METAR */
MwTemperature t = metarwiz.Get<MwTemperature>();

    _ = t.Celsius;
    _ = t.DewPoint;
```

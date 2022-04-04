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

Inclusion Key:
 - [**M**] = Mandatory
 - [**C**] = Conditional
 - [**O**] = Optional

Metar Example
 - METAR EGLC 221630Z AUTO 24008KT 350V070 9999 1200NE R32L/M0400V1200D +VCFG -RA BKN025 SCT030CB M01/M10 Q1022 REFZRA WS R18C W15/S2 R24L/123456

| Supported | Example                                   | Type                | Description                                                          |
| :-:       | :-                                        | :-                  | :-                                                                   |
| ✔         | METAR [**M**]                             | MwMetar             | Type of report                                                       |
| ✔         | EGLC [**M**]                              | MwLocation          | ICAO location indicator                                              |
| ✔         | 221630Z [**M**]                           | MwTimeOfObservation | Day and actual time of the observation in UTC                        |
| ✔         | AUTO [**C**]                              | MwAutoOrNil         | Automated or missing report identifier                               |
| ✔         | 24008KT [**M**] 350V070 [**C**]           | MwSurfaceWindGroup  | Surface wind and Significant directional wind variations             |
| ✔         | 9999 [**M**] 1200NE [**C**] 3/4SM [**M**] | MwVisibilityGroup   | Prevailing or minimum visibility (**ICAO** & **NA**)                 |
| ✔         | R32L/M0400V1200D [**C**]                  | MwRunwayVisualRange | Runway visual range                                                  |
| ✔         | +VCFG -RA [**C**]                         | MwWeather           | Characteristics, type, intensity or proximity of present weather     |
| ✔         | BKN025 [**M**] SCT030CB [**C**]           | MwCloud             | Cloud amount, type and height of cloud base or vertical visibility   |
| ✔         | M01/M10 [**M**]                           | MwTemperature       | Air and dew-point temperature                                        |
| ✔         | Q1022 [**M**] or A2992 [**M**]            | MwPressure          | Pressure value  (**ICAO** & **NA**)                                  |
| ✔         | REFZRA [**C**]                            | MwRecentWeather     | Recent weather                                                       |
| ✔         | WS R18C [**C**]                           | MwWindShearGroup    | Wind shear                                                           |
| ✔         | W15/S2 [**C**]                            | MwStateOfSeaSurface | Sea-surface temperature and state of the sea                         |
| ✔         | R27L/421594 [**C**]                       | MwStateOfRunway     | State of the runway                                                  |
| ✔         | NOSIG [**O**]                             | MwNoSig             | No significant weather                                               |
| ✖         | BECMG, TEMPO [**O**]                      | -                   | Trend Forecast                                                       |

Remarks Example
 - RMK AO2 P1234 T00640036 $ PK WND 20032/25 58033 SLP125 10066 21012 60009 TWR VIS 1 TORNADO 70090 CIG 013V017 WSHFT 1715

| Supported | Example                                   | Type                              | Description                                                     |
| :-:       | :-                                        | :-                                | :-                                                              |
| ✔        | RMK [**O**]                               | RwRemarks                         | Remarks                                                         |
| ✔        | AO2 [**O**]                               | RwAutomatedStation                | Automated station precipitation discriminator indicator         |
| ✔        | P1234 [**O**]                             | RwHourlyPrecipitation             | Hourly precipitation amount                                     |
| ✔        | T00640036 [**O**]                         | RwHourlyTemperature               | Hourly temperature and dew point                                |
| ✔        | $ [**O**]                                 | RwNeedsMaintenance                | Automated system needs maintenance indicator                    |
| ✔        | PK WND 20032/25 [**O**]                   | RwPeakWindGroup                   | Peak wind and time of observation                               |
| ✔        | 58033 [**O**]                             | RwPressureTendency                | Pressure change over the last 3 hours                           |
| ✔        | SLP125 [**O**]                            | RwSeaLevelPressure                | Sea level pressure                                              |
| ✔        | 10066 [**O**]                             | RwSixHourMaxTemperature           | Maximum temperature for the last 6 hours                        |
| ✔        | 21012 [**O**]                             | RwSixHourMinTemperature           | Minimum temperature for the last 6 hours                        |
| ✔        | 60009 [**O**]                             | RwSixHourPrecipitation            | Precipitation amount for the last 6 hours                       |
| ✔        | TWR VIS 1 or SFC VIS 2 [**O**]            | RwSurfaceTowerVisibilityGroup     | Tower visibility and/or surface visibility                      |
| ✔        | TORNADO [**O**]                           | RwTornadicGroup                   | Tornadic activity                                               |
| ✔        | 70090 [**O**]                             | RwTwentyFourHourPrecipitation     | Precipitation amount for the last 24 hours                      |
| ✔        | CIG 013V017 [**O**]                       | RwVariableCeilingGroup            | Ceiling in the body of the report is < 3000 feet and variable   |
| ✔        | WSHFT 1715 [**O**]                        | RwWindShiftGroup                  | Wind shift                                                      |

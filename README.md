# **Metarwiz**
This simple class library can be used to parse and visualise METAR reports.  Below is some example code that shows you how you can use the Metarwiz API to retrieve information from a real METAR report.
<br/>

The following code is only for demonstration purposes and does not show the complete features of Metarwiz.
<br/>

```c#
/* this is a small example metar report */
string metar = @"METAR EGLC 221850Z AUTO 29005KT 9999 NCD 19/16 Q1022="

/* the following uses the static method to parse and create an instance of Metarwiz */
Metarwiz metarwiz = Metarwiz.Parse(metar);

/* let's get the temperature information using Metarwiz */
MwTemperature t = metarwiz.Get<MwTemperature>();

    _ = t.Celsius;
    _ = t.DewPoint;

/* let's get the cloud layers */
IEnumerable<MwCloud> clouds = metarwiz.GetMany<MwCloud>();

foreach(MwCloud cloud in clouds)
{
    /* cloud types can be broken, scattered, few, overcast or not detected. */
    CloudType cloudType = cloud.Cloud;

    /* let's see how high the cloud layer is above ground level */
    _ = cloud.AboveGroundLevel
}
```
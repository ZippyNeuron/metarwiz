# metarwiz
This simple class library can be used to parse and visualise METAR reports.

Example Usage
```
string metar = @"METAR EGLC 220350Z AUTO 30007KT 4100 RA BKN009/// OVC042/// //////CB 17/16 Q1015 RERA"

Metarviz m = Metarwiz.Parse(metar);
```


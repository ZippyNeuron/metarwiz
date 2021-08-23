# metarwiz
This simple class library can be used to parse and visualise METAR reports.

Example Usage
```
  {
      Metarwiz metarwiz = Metarwiz.Parse(metar);

      MwLocation location = m.Get<MwLocation>();
          Console.WriteLine(location.ICAO);

      IEnumerable<MwCloud> clouds = m.GetMany<MwCloud>();
      foreach(MwCloud cloud in clouds)
      {
          Console.WriteLine($"{cloud.Cloud} at {cloud.AboveGroundLevel}");
      }
  }
```

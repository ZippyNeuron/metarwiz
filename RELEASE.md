# Release Notes

## 1.6.0
* Updated package information
* Added NuGet.org icon

## 1.5.1
* Implemented WSHFT (Wind Shift)
* Fixed a bug in PK WND (Peak Wind)

## 1.5.0
* A major overhaul of the parser due to the expressive and verbose nature of the North American METAR
* The overhaul of the parser has seen a 75-100x increase in performance
* Added a console application to the GitHub repository to better demonstrate the Metarwiz class library
* Any class property that returns an Enum type now has a corresponding description property
* Implemented RAE (Rain Ending) and RAB (Rain Beginning)
* Implemented CIG (Variable Ceiling)
* Implemented SFC VIS and TWR VIS (Surface and Tower Visibility)
* Implemented PK WND (Peak Wind)
* Extended the TendencyIndicator to include FT in RVR observations
* Added a new Metarwiz constructor so that a tag can be specified
* Some minor refactoring and code reorganisation

## 1.4.0
* RwPressureTendancy metar item class has been renamed to RwPressureTendency
* Added an icon for the nuget.org package
* Obsolete properties identified in version 1.3.0 have been removed
* MetarItemFactory and MetarRemarksFactory were combined to form MetarParserFactory
* Refactored and encapsulated the parsing functionality to reduce code duplication
* Some general internal code tidy

## 1.3.0

* Supports instance creation using the public constructor (no longer limited to the static method)
* Supports Dependency Injection - An interface has been added so that a Metarwiz instance can be injected
* Support for remarks has now been added and the following remarks are now recognised
  * Automated Station
  * Hourly Precipitation
  * Hourly Temperature
  * Station Needs Maintenance
  * Pressure Tendency
  * Sea Level Pressure
  * 3-6 Hour Maximum Temperature
  * 3-6 Hour Minimum Temperature
  * 3-6 Hour Precipitation
  * 24 Hour Precipitation

**Please Note - Important**
* MwCavok.IsCavok - This property will be removed with next release
* MwPressure.hPa - This property will be removed with next release, please use HPa
* MwPressure.inHg - This property will be removed with next release, please use InHg
* MwStatuteMiles.IsSM - This property will be removed with next release
* MwStatuteMiles.Amount - This property will be removed with next release, please use Distance
* MwTimeOfObservation.Date - This property will be removed with next release
* MwVisibility.Visibility - This property will be removed with next release, please use Distance
* MwWeather.InVacinity - This property will be removed with next release, please use IsInVacinity
* Metarwiz.Remarks - This property will be removed with next release, please use Metar.Remarks or Metar.HasRemarks instead

## 1.2.0
This is the first release. This version will parse the base METAR information but will not parse the remarks section

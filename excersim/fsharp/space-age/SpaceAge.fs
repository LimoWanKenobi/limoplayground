module SpaceAge

type SpaceAge(seconds: decimal) =

    let earthDaySeconds = 24m * 60m * 60m
    let daysInEarthOrbitalYear = 365.25m

    let mercuryOrbitalPeriod =  0.2408467m
    let venusyOrbitalPeriod =  0.61519726m
    let earthOrbitalPeriod = 1m
    let marsOrbitalPeriod =  1.8808158m
    let jupiterOrbitalPeriod =  11.862615m
    let saturnOrbitalPeriod =  29.447498m
    let uranusOrbitalPeriod =  84.016846m
    let neptuneOrbitalPeriod =  164.79132m

    let calculateYears orbitalPeriod =
        let obitalPeriodInSeconds = orbitalPeriod * earthDaySeconds * daysInEarthOrbitalYear
        let years = seconds / obitalPeriodInSeconds
        System.Math.Round(years, 2)

    member this.Seconds = seconds

    member this.onMercury = calculateYears mercuryOrbitalPeriod
    member this.onVenus = calculateYears venusyOrbitalPeriod
    member this.onEarth = calculateYears earthOrbitalPeriod
    member this.onMars = calculateYears marsOrbitalPeriod
    member this.onJupiter = calculateYears jupiterOrbitalPeriod
    member this.onSaturn = calculateYears saturnOrbitalPeriod
    member this.onUranus = calculateYears uranusOrbitalPeriod
    member this.onNeptune = calculateYears neptuneOrbitalPeriod
module SpaceAge

type SpaceAge(seconds: decimal) =

    let earthDaySeconds = 24m * 60m * 60m

    let calculateYears orbitalPeriod =
        let obitalPeriodInSeconds = orbitalPeriod * earthDaySeconds
        let years = seconds / obitalPeriodInSeconds
        System.Math.Round(years, 2)

    let mercuryOrbitalPeriod =  0.2408467m
    let venusyOrbitalPeriod =  0.2408467m
    let earthOrbitalPeriod = 365.25m
    let marsOrbitalPeriod =  0.2408467m
    let jupiterOrbitalPeriod =  0.2408467m
    let saturnOrbitalPeriod =  0.2408467m
    let uranusOrbitalPeriod =  0.2408467m
    let neptuneOrbitalPeriod =  0.2408467m
 
    member this.Seconds = seconds

    member this.onMercury = calculateYears mercuryOrbitalPeriod
    member this.onVenus = calculateYears venusyOrbitalPeriod
    member this.onEarth = calculateYears earthOrbitalPeriod
    member this.onMars = calculateYears marsOrbitalPeriod
    member this.onJupiter = calculateYears jupiterOrbitalPeriod
    member this.onSaturn = calculateYears saturnOrbitalPeriod
    member this.onUranus = calculateYears uranusOrbitalPeriod
    member this.onNeptune = calculateYears neptuneOrbitalPeriod
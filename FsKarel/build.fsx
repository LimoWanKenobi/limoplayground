// include Fake lib
#r "packages/build/FAKE/tools/FakeLib.dll"
open Fake

// Properties
let buildDir = "./build/"
let testDir  = "./test/"

// Targets
Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "Build" (fun _ ->
    !! "src/app/**/*.fsproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "Build-Output: "
)

Target "BuildTest" (fun _ ->
    !! "src/tests/**/*.fsproj"
      |> MSBuildDebug testDir "Build"
      |> Log "TestBuild-Output: "
)

Target "Test" (fun _ ->
    !! (testDir + "/FsKarel.Test.*.dll")
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true;
             OutputFile = testDir + "TestResults.xml" })
)

Target "Default" (fun _ ->
    trace "Hello World from FAKE"
)

// Dependencies
"Clean"
  ==> "Build"
  ==> "BuildTest"
  ==> "Test"
  ==> "Default"

// start build
RunTargetOrDefault "Default"
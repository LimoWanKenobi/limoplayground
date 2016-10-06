open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open System.Threading.Tasks

let awaitTask = Async.AwaitIAsyncResult >> Async.Ignore

type Startup(env: IHostingEnvironment ) =
    member x.Configure(app: IApplicationBuilder) =
        do app.Run(fun context ->
            async {
                do! awaitTask (context.Response.WriteAsync("Hi!"))
            } |> Async.StartAsTask :> Task
        )

[<EntryPoint>]
let main argv =
    let host = WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build()

    do host.Run()
    0 // return an integer exit code

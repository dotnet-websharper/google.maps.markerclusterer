#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.Google.Maps.MarkerClusterer", "3.0")
        .References(fun r ->
            [
                r.Assembly "System.Web"
                r.NuGet("WebSharper.Google.Maps").Reference()
            ])
    |> fun bt -> bt.WithFramework(bt.Framework.Net40)

let main =
    bt.WebSharper.Extension("WebSharper.Google.Maps.MarkerClusterer")
        .Embed(["markerclusterer_compiled.js"])
        .SourcesFromProject()

let test =
    bt.WebSharper.HtmlWebsite("WebSharper.Google.Maps.MarkerClusterer.Tests")
        .SourcesFromProject()
        .References(fun r -> [r.Project main])

bt.Solution [
    main
    test

    bt.NuGet.CreatePackage()
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper.Google.Maps.MarkerClusterer-3.0"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://github.com/intellifactory/websharper.google.maps.markerclusterer"
                Description = "WebSharper Extensions for Google Maps MarkerClusterer 3.0"
                RequiresLicenseAcceptance = true })
        .Add(main)

]
|> bt.Dispatch

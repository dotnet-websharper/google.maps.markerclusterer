#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.Google.Maps.MarkerClusterer")
        .VersionFrom("WebSharper")
        .WithFSharpVersion(FSharpVersion.FSharp30)
        .WithFramework(fun fw -> fw.Net40)
        .References(fun r ->
            [
                r.Assembly "System.Web"
                r.NuGet("WebSharper.Google.Maps").Reference()
            ])

let main =
    bt.WebSharper.Extension("WebSharper.Google.Maps.MarkerClusterer")
        .Embed(["markerclusterer_compiled.js"])
        .SourcesFromProject()

let test =
    bt.WebSharper.HtmlWebsite("WebSharper.Google.Maps.MarkerClusterer.Tests")
        .SourcesFromProject()
        .References(fun r ->
            [
                r.Project main
                r.NuGet("WebSharper.Html").Reference()
            ])

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

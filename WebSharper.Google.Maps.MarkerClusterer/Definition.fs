namespace WebSharper.Google.Maps.MarkerClusterer
open WebSharper.InterfaceGenerator

module Definition =
    open WebSharper.InterfaceGenerator.Type
    open WebSharper.Google.Maps

    let resource = Resource "Js" "markerclusterer_compiled.js"

    let CalculatorResult =
        Pattern.Config "CalculatorResult" {
            Required =
                [
                    "text",     T<string>
                    "index",    T<int>
                ]
            Optional = []
        }

    let MarkerClustererOptions =
        Pattern.Config "MarkerClustererOptions" {
            Required = []
            Optional =
                [
                    "gridSize",             T<int>
                    "maxZoom",              T<float>
                    "zoomOnClick",          T<bool>
                    "averageCenter",        T<bool>
                    "minimumClusterSize",   T<int>
                ]
        }

    let Style =
        Pattern.Config "Style" {
            Required =
                [
                    "url",                  T<string>
                    "height",               T<int>
                    "width",                T<int>
                    "anchor",               (Type.ArrayOf T<int>)
                    "textColor",            T<string>
                    "textSize",             T<float>
                    "backgroundPosition",   T<string>
                ]
            Optional = []
        }

    let MarkerClusterer =
        Class "MarkerClusterer"
        |+> Static [
                Constructor (T<Map>?map * (!? (Type.ArrayOf T<Marker>)?opt_markers) * (!? MarkerClustererOptions?opt_options))
            ]
        |+> Instance [
                "addMarker"         => T<Marker>?marker * (!? T<bool>?opt_nodraw) ^-> T<unit>
                "addMarkers"        => (Type.ArrayOf T<Marker>)?markers * (!? T<bool>?opt_nodraw) ^-> T<unit>
                "clearMarkers"      => T<unit> ^-> T<unit>
                "getCalculator"     => T<unit> ^-> (T<Marker> * T<int> ^-> CalculatorResult)
                "getExtendedBounds" => T<LatLngBounds>?bounds ^-> T<LatLngBounds>
                "getGridSize"       => T<unit> ^-> T<float>
                "getMap"            => T<unit> ^-> T<Map>
                "getMarkers"        => T<unit> ^-> Type.ArrayOf T<Marker>
                "getMaxZoom"        => T<unit> ^-> T<float>
                "getStyles"         => T<unit> ^-> Type.ArrayOf Style
                "getTotalClusters"  => T<unit> ^-> T<int>
                "getTotalMarkers"   => T<unit> ^-> T<int>
                "isZoomOnClick"     => T<unit> ^-> T<bool>
                "redraw"            => T<unit> ^-> T<unit>
                "removeMarker"      => T<Marker>?marker ^-> T<bool>
                "resetViewport"     => T<unit> ^-> T<unit>
                "setCalculator"     => (T<Marker> * T<int> ^-> CalculatorResult)?calculator ^-> T<unit>
                "setGridSize"       => T<float>?size ^-> T<unit>
                "setMap"            => T<Map>?map ^-> T<unit>
                "setMaxZoom"        => T<float>?maxZoom ^-> T<unit>
                "setStyles"         => Type.ArrayOf Style ^-> T<unit>
            ]
        |> Requires [resource]

    let Assembly =
        Assembly [
            Namespace "WebSharper.Google.Maps.MarkerClusterer.Resources" [
                resource
            ]
            Namespace "WebSharper.Google.Maps.MarkerClusterer" [
                 Style
                 MarkerClustererOptions
                 CalculatorResult
                 MarkerClusterer
            ]
        ]


[<Sealed>]
type MarkerClustererExtension() =
    interface IExtension with
        member x.Assembly = Definition.Assembly

[<assembly: Extension(typeof<MarkerClustererExtension>)>]
do ()

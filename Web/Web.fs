module MyWebApi.Program

open Suave
open Suave.Successful
open Suave.Filters
open Suave.Operators
open Suave.RequestErrors
open Newtonsoft.Json

let JSON a =
    JsonConvert.SerializeObject a |> OK
    >=> Writers.setMimeType "application/json; charset=utf-8"


let search = 
    request ( fun r -> 
        match r.queryParam "q" with 
        | Choice1Of2 q -> ( SpotifySearch.searchTrackByQuery q ) |> JSON
        | _ -> BAD_REQUEST  "query parameter is missing"
    )

let app = 
    choose
        [ GET >=> choose 
            [ path "/search" >=> request (fun r -> search)
              path "/playlists/planetebleue" >=> warbler (fun x -> PlaneteBleue.GetEpisodeList |> JSON ) 
               ]
        ]

[<EntryPoint>]
let main argv =
    startWebServer defaultConfig app
    0

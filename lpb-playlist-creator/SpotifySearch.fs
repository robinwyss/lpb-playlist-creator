module SpotifySearch

open FSharp.Data

let search artist track = 
    let query = sprintf "track:%s artist:%s" track artist
    let result = Http.RequestString(
                    "https://api.spotify.com/v1/search", httpMethod = "GET", 
                    query   = [ "q", query; "type", "track" ])
                    |> JsonValue.Parse
    //result
        //|> JsonValue.Parse
    match result with 
        | JsonValue.Record r -> 
            let (name, value) = r.[0]
            name
        | _ -> "Error"



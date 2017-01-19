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
            let tracks = r |> Seq.find (fun e -> (fst e) = "tracks") |> snd 
            let items = tracks.TryGetProperty "items"
            match items with
                | Some  i -> 
                    let first = i.AsArray() |> Seq.take 1 // (fun e -> e.As)
                    i.ToString()
                    //first |> Seq.find (fun e -> match e.TryGetProperty "name" with
                    //                                | Some name -> name.AsString()
                    //                                | None -> false )
                | None -> "Error 2"
            //match tracks with
            //    | (a,b) -> 
            //        let items = r |> Seq.find (fun e -> (fst e) = "items")
            //        match items with 
            //        | (_, JsonValue.Array a) ->  
            //            let firstTrack = a |> Seq.take 1
            //            let e1 = firstTrack |> Seq.take 1 // Seq.find (fun e -> )
            //            e1.ToString()
            //        | _ -> "Error 1 " + tracks.ToString()
            //    | _ -> "Error 1 " + tracks.ToString()
        | _ -> "Error 1"



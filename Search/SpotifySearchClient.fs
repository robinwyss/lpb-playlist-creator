module SpotifySearchClient 

open FSharp.Data

let searchTrack query =
    Http.RequestString(
        "https://api.spotify.com/v1/search", httpMethod = "GET", 
        query   = [ "q", query; "type", "track" ])

let searchTrackByNameAndArtist artistName trackName = 
    let query = sprintf "track:%s artist:%s" trackName artistName
    searchTrack query  

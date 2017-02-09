module YoutubeSearchClient

open FSharp.Data

let searchVideo apiKey query =
    Http.RequestString(
        "https://www.googleapis.com/youtube/v3/search", httpMethod = "GET", 
        query   = [ "type", "video"; "part","snippet"; "q", query; "key", apiKey ])
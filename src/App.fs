module PlaylistCreator

open FSharp.Data

open PlaylistExtractor
open SpotifySearch
open YoutubeSearch

type private ConfigParser = JsonProvider<"""{"youtube": {"apiKey": "a"}}""">
let config = ConfigParser.Load("config.json")

let searchOnYoutube = searchVideo config.Youtube.ApiKey

let printTracks (tracks:seq<SpotifyTrack>) =
    tracks |> Seq.iter (fun t -> printfn "SpotifyTrack: %s %s %s %s " t.Title t.Artists.Head.Name t.Id t.Link )

let printVideos (tracks:seq<YoutubeVideo>) =
    tracks |> Seq.iter (fun t -> printfn "YoutubeVideo: %s %s %s " t.Title t.ChannelTitle t.Id )
let rec interactiveSearch () =
    let input = System.Console.ReadLine()
    match input with
        | "" -> ()
        | _ -> 
            searchTrackByQuery input |> printTracks
            searchOnYoutube input |> printVideos
            interactiveSearch ()


        
[<EntryPoint>]
let main argv =
    printfn "search"
    if Array.isEmpty argv then
        interactiveSearch ()
    else 
        let query = argv |> Array.reduce (fun a b -> a + " " + b)
        searchTrackByQuery query |> printTracks
    //let episodes = GetEpisodeList 
    //let playlist = episodes |> Seq.map GetPlaylist 
    //printfn "%A" playlist
    0 // return an integer exit code

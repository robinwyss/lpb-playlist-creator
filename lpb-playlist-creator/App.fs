module PlaylistCreator

open PlaylistExtractor
open SpotifySearch

let printTracks tracks =
    tracks |> Seq.iter (fun t -> printfn "%s %s %s %s " t.Title t.Artists.Head.Name t.Id t.Link )
let testSearch =
    let result = searchTrackByNameAndArtist "Bonobo" "Migration"
    printTracks result

let interactiveSearch =
    let input = System.Console.ReadLine()
    match input with
        | null -> ()
        | _ -> 
            searchTrackByQuery input |> printTracks
        
[<EntryPoint>]
let main argv =
    printf "search"
    if Array.isEmpty argv then
        interactiveSearch
    else 
        let query = argv |> Array.reduce (fun a b -> a + " " + b)
        searchTrackByQuery query |> printTracks
    //let episodes = GetEpisodeList 
    //let playlist = episodes |> Seq.map GetPlaylist 
    //printfn "%A" playlist  
    0 // return an integer exit code

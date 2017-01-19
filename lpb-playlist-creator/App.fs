module PlaylistCreator

open PlaylistExtractor
open SpotifySearch

[<EntryPoint>]
let main argv =
    //let episodes = GetEpisodeList 
    //let playlist = episodes |> Seq.map GetPlaylist 
    //printfn "%A" playlist
    let result = search "Bonobo" "Migration"
    printfn "%s" result
    0 // return an integer exit code

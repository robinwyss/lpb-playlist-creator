module PlaylistCreator

open PlaylistExtractor
open SpotifySearch

[<EntryPoint>]
let main argv =
    //let episodes = GetEpisodeList 
    //let playlist = episodes |> Seq.map GetPlaylist 
    //printfn "%A" playlist
    let result = searchTrack "Bonobo" "Migration"
    printfn "%s %s %s %s " result.Title result.Artists.Head.Name result.Id result.Link 
    0 // return an integer exit code

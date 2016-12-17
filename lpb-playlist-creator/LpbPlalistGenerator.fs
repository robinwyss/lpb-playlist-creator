module PlaylistCreator

open PlaylistExtractor

[<EntryPoint>]
let main argv =
    let episodes = GetEpisodeList 
    printfn "%A" episodes
    0 // return an integer exit code

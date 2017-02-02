module SpotifySearch

open FSharp.Data

type TrackSearch = JsonProvider<"""{"tracks":{"href":"","items":[{"album":{"album_type":"","artists":[{"external_urls":{"spotify":""},"href":"","id":"","name":"","type":"","uri":""}],"available_markets":[""],"external_urls":{"spotify":""},"href":"","id":"","images":[{"height":575,"url":"","width":640}],"name":"","type":"","uri":""},"artists":[{"external_urls":{"spotify":""},"href":"","id":"","name":"=","type":"","uri":""}],"available_markets":[""],"disc_number":1,"duration_ms":230693,"explicit":false,"external_ids":{"isrc":""},"external_urls":{"spotify":""},"href":"","id":"","name":"","popularity":65,"preview_url":"","track_number":2,"type":"","uri":""}],"limit":20,"next":null,"offset":0,"previous":null,"total":12}}""">

type SpotifyArtist = { Name: string; Id: string; Link: string }

type SpotifyTrack = { Title: string; Artists: SpotifyArtist list; Id: string; Link: string }


let searchTrackByQuery query =
    let result = Http.RequestString(
                    "https://api.spotify.com/v1/search", httpMethod = "GET", 
                    query   = [ "q", query; "type", "track" ])
    let tracks = TrackSearch.Parse result

    tracks.Tracks.Items |> Seq.map (fun track -> 
        let artists = track.Artists |> 
                        Seq.map (fun a ->  
                            {   Name=a.Name.ToString(); 
                                Id= a.Id.ToString(); 
                                Link=a.Href.ToString() 
                            }) |> Seq.toList
        {   Title=track.Name.ToString();
        Artists=artists; 
        Id=track.Id.ToString();
        Link=track.Href.ToString() } ) |> Seq.toList

let searchTrackByNameAndArtist artistName trackName = 
    let query = sprintf "track:%s artist:%s" trackName artistName
    searchTrackByQuery query
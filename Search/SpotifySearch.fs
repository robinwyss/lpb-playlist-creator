module SpotifySearch 

open FSharp.Data

type private TrackSearch = JsonProvider<"""{"tracks":{"href":"a","items":[{"album":{"album_type":"a","artists":[{"external_urls":{"spotify":"a"},"href":"a","id":"a","name":"a","type":"a","uri":"a"}],"available_markets":["a"],"external_urls":{"spotify":"a"},"href":"a","id":"a","images":[{"height":575,"url":"a","width":640}],"name":"a","type":"a","uri":"a"},"artists":[{"external_urls":{"spotify":"a"},"href":"b","id":"a","name":"=","type":"a","uri":"a"}],"available_markets":["a"],"disc_number":1,"duration_ms":230693,"explicit":false,"external_ids":{"isrc":"a"},"external_urls":{"spotify":"a"},"href":"a","id":"a","name":"a","popularity":65,"preview_url":"a","track_number":2,"type":"a","uri":"a"}],"limit":20,"next":null,"offset":0,"previous":null,"total":12}}""">

type SpotifyArtist = { Name: string; Id: string; Link: string }

type SpotifyTrack = { Title: string; Artists: SpotifyArtist list; Id: string; Link: string }

let parseTrackSearchResult result = 
    let tracks = TrackSearch.Parse result
    tracks.Tracks.Items |> Seq.map (fun track -> 
        let artists = track.Artists |> 
                        Seq.map (fun a ->  
                            {   Name=a.Name; 
                                Id= a.Id; 
                                Link=a.Href 
                            }) |> Seq.toList
        {   Title=track.Name.ToString();
        Artists=artists; 
        Id=track.Id.ToString();
        Link=track.Href.ToString() } ) |> Seq.toList


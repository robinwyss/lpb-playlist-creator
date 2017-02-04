module PlaneteBleue

open FSharp.Data
open System.Text.RegularExpressions

open PlaylistScraper

type ParseResult =
    | Success of Release
    | Error of string

    
let GetEpisodeList =

    let extractEpisodeNum txt = 
        let result = Regex.Match(txt,"LPB\s(\d+)(\s.*)")
        if result.Success then 
            Success ({Id= result.Groups.[1].Value; Name = result.Groups.[2].Value})
        else 
            Error txt

    let results = HtmlDocument.Load("https://laplanetebleue.com/emissions")

    let select = 
        results.Descendants ["select"]
        |> Seq.find (fun x -> x.AttributeValue("id") = "emission")
 
    select.Descendants ["option"]
        |> Seq.map (fun x -> extractEpisodeNum (x.InnerText()))
        |> Seq.choose (fun x-> 
            match x with
                | Success e -> Some e
                |_ -> None)

let GetPlaylist (episode: Release) =

    let episodeNbr = episode.Id

    let doc = HtmlDocument.Load("https://laplanetebleue.com/emission-"+episodeNbr)
    let songs = 
        doc.CssSelect("table")
        |> List.map (fun n ->
            let a = n.Descendants("span") |> Seq.head
            let artist = a.InnerText()
            let t = n.Descendants("i") |> Seq.head
            let title = t.InnerText()
            { Title = title; Artist = artist})
    {Tracks= songs;Name= episodeNbr}
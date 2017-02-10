#load @"paket-files/include-scripts/net46/include.fsharp.data.fsx"

open FSharp.Data
open System.Text.RegularExpressions

module Lpb = 
    type Episode = Episode of string * string

    type ParseResult =
        | Success of Episode
        | Error of string 

    type Song = {Title: string; Artist: string}

    type Playlist = {Songs: Song list; Name: string} 
    type Year = {Year: string; Url: string}
    type Month = {Month: string; Year: Year; Url: string}
   


    let LoadRA path = 
        HtmlDocument.Load("https://www.residentadvisor.net" + path)

    let GetYearsFromRa (result:HtmlDocument) =
        let elements = 
            result.CssSelect("ul.monthYear") |> Seq.head
        let years = elements.Descendants ["ul"] |> Seq.head
        
        years.Descendants ["a"]
            |> Seq.map (fun x -> {Year.Year = x.InnerText(); Url = x.AttributeValue("href")})


    let GetMonthsFromRa (year:Year) (result:HtmlDocument)=
        let elements = 
            result.CssSelect("ul.monthYear") |> Seq.head
        let years = elements.Descendants ["ul"] |> Seq.skip 1 |> Seq.head
        
        years.Descendants ["a"]
            |> Seq.filter (fun x -> x.InnerText() <> "Latest")
            |> Seq.map (fun x -> {Month = x.InnerText(); Year = year; Url= x.AttributeValue("href")})

    let GetPlaylist (result:HtmlDocument) =
        let songs = 
            result.CssSelect("table")
            |> List.map (fun n ->
                let a = n.Descendants("span") |> Seq.head
                let artist = a.InnerText()
                let t = n.Descendants("i") |> Seq.head
                let title = t.InnerText()
                { Title = title; Artist = artist})
        {Songs= songs;Name= episodeNbr}

let years = Lpb.GetYearsFromRa (Lpb.LoadRA "/tracks")
let yearone = years |>  Seq.head
let months = years |> Seq.collect (fun x -> Lpb.LoadRA x.Url |> (Lpb.GetMonthsFromRa x)) 

let playlist = months |> Seq.map (fun m -> Lpb.LoadRA m.Url |> GetPlaylist 
printf "bla %A" months
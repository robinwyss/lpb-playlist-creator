#load @"paket-files/include-scripts/net45/include.fsharp.data.fsx"

open FSharp.Data
open System.Text.RegularExpressions

type Episode = Episode of string * string

type ParseResult =
    | Success of Episode
    | Error of string 
    
let GetEpisodeList =

    let extractEpisodeNum txt = 
        let result = Regex.Match(txt,"LPB\s(\d+)(\s.*)")
        if result.Success then 
            Success (Episode (result.Groups.[1].Value, result.Groups.[2].Value))
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

let GetPlaylist (episode: Episode) =
    let episodeNbr = match episode with Episode (nbr, name) -> nbr 
    HtmlDocument.Load("https://laplanetebleue.com/emission-" + episodeNbr)

let episodes = GetEpisodeList 

printf "bla %A" episodes
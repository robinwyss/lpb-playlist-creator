module YoutubeSearch

open FSharp.Data

type private VideoSearch = JsonProvider<"""{"kind":"a","etag":"a","nextPageToken":"a","regionCode":"a","pageInfo":{"totalResults":10,"resultsPerPage":5},"items":[{"kind":"a","etag":"a","id":{"kind":"a","videoId":"a"},"snippet":{"publishedAt":"2016-12-22T08:00:01.000Z","channelId":"a","title":"a","description":"a","thumbnails":{"default":{"url":"a","width":120,"height":90},"medium":{"url":"a","width":320,"height":180},"high":{"url":"a","width":480,"height":360}},"channelTitle":"a","liveBroadcastContent":"a"}},{"kind":"a","etag":"a","id":{"kind":"a","videoId":"a"},"snippet":{"publishedAt":"2016-12-22T08:00:01.000Z","channelId":"a","title":"a","description":"a","thumbnails":{"default":{"url":"a","width":120,"height":90},"medium":{"url":"a","width":320,"height":180},"high":{"url":"a","width":480,"height":360}},"channelTitle":"a","liveBroadcastContent":"a"}}]}""">

type YoutubeVideo = {Title:string; Description:string; ChannelTitle: string; Id: string}

let searchVideo apiKey query =
    let result = Http.RequestString(
                    "https://www.googleapis.com/youtube/v3/search", httpMethod = "GET", 
                    query   = [ "type", "video"; "part","snippet"; "q", query; "key", apiKey ])
    let searchResult = VideoSearch.Parse result
    searchResult.Items |> Seq.map (fun i -> {
                                            Title = i.Snippet.Title;
                                            Description = i.Snippet.Description;
                                            ChannelTitle = i.Snippet.ChannelTitle;
                                            Id = i.Id.VideoId
                                            }) |> Seq.toList
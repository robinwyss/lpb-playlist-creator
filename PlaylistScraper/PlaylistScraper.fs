namespace PlaylistScraper

type Release = {Id: string; Name: string}

type Track = {Title: string; Artist: string}

type Playlist = {Tracks: Track list; Name: string} 

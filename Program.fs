open System


//hour to take the best timezone approximation of
//formatted in 24-hours, so hour=17 would be considered 5pm
let hour = 12

//define TimeZoneInfo "Class" to better store timezone information
type TZInfo = {tzName: string; minDiff: float; localTime: string; utcOffset: float}

//grab timezones
let tzs = TimeZoneInfo.GetSystemTimeZones()

let tzList = [

    for tz in tzs do

    //convert the current time to the local time zone
    let localTz = TimeZoneInfo.ConvertTime(DateTime.Now, tz) 

    //get the datetime object for hour input
    let time = DateTime(localTz.Year, localTz.Month, localTz.Day, hour, 0, 0)

    //get the difference between now local time and hour local time
    let minDifference = abs (localTz - time).TotalMinutes

    yield {

        tzName=tz.StandardName;

        minDiff=minDifference;

        localTime=localTz.ToString("hh:mm tt");

        utcOffset=tz.BaseUtcOffset.TotalHours;

    }

]

let closest = tzList 
            // sort by minDiff
            |> List.sortBy (fun (i:TZInfo) -> i.minDiff) 

            // Get the first item
            |> List.head

printfn "%A" closest
